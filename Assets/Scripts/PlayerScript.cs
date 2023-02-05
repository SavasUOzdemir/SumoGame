using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] float growAmount = 0.033f;
    [SerializeField] float pushForce = 10f;
    [SerializeField] float speed = 1f;
    [SerializeField] float rotateSpeed = 75f;
    int score = 0;
    public int Score { get; private set; }
    private Rigidbody rb;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] Camera spareCamera;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Score = score;
    }
    //movement in fixedupdate for performance. 
    private void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal") * rotateSpeed;
        Vector3 rotation = new Vector3(0, horizontal, 0);
        transform.Rotate(rotation * speed * Time.deltaTime);

        Vector3 movement = transform.forward * speed * Time.deltaTime;
        rb.MovePosition(transform.position + movement);
    }
    //trigger "others", either food, death or players' backs. from sumo.io ive seen that if an agent contacts the back of a player it hits harder. 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Food"))
        {
            transform.localScale += new Vector3(growAmount, growAmount, growAmount);
            Destroy(other.gameObject);
            pushForce *= 1.1f;
            rotateSpeed *= 1.02f;
            speed *= 0.98f;
            Score += 1;
            scoreText.text = "Score: " + Score;
        }
        //default cam follows player and is a child of player gameobject. when it dies, I activate sparecam that views from above. 
        else if (other.CompareTag("Fall"))
        {
            Destroy(this.gameObject);
            spareCamera.gameObject.SetActive(true);
        }

        else if (other.CompareTag("PlayerBack"))
        {
            other.transform.parent.GetComponent<Rigidbody>().AddForce(transform.forward * pushForce * 30);
            StartCoroutine(KillListener(other.transform.parent.gameObject));
        }
    
    }
    //normal push, pretty straightforward i gýess
    //check killlistener coroutine
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * pushForce * 15);
            StartCoroutine(KillListener(collision.gameObject));
        }
    }
    //this coroutine checks whether an agent dies after being hit by player. if they die in 5sec window, player gets score. 

    IEnumerator KillListener(GameObject other)
    {
        float killTimer = 5f;
        while (killTimer >= 0.01f)
        {
            if (other == null)
            {
                Score += 10;
                scoreText.text = "Score: " + Score;
                break;
            }
            killTimer -= 0.1f ;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
