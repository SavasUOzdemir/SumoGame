using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    [SerializeField] float rotateSpeed = 25f;
    [SerializeField] Vector3 target;
    [SerializeField] float growAmount = 0.033f;
    [SerializeField] float pushForce = 10f;

    private GameObject lastTouchPlayer;
    private GameObject[] players;
    private GameObject[] foods;
    private Rigidbody rb;
    [SerializeField] bool eatMode = true;

    //When the scene starts, repeated update functions are invoked. Also, for each AI agent the modecheck function is also invoked. This function determines AI
    //behavior in terms of whether AI is trying to kill other players or if it is searching for food. 
    private void Start()
    {
        InvokeRepeating(nameof(UpdateFoodsPlayers), 0f, 0.1f);
        InvokeRepeating(nameof(ModeCheck), 0.01f, 1f);
        rb = GetComponent<Rigidbody>();
    }
    //i guess it would not give NullException errors if i kept this in a singleton object, but it works in its current form. at least in play mode, who knows about builds...
    private void UpdateFoodsPlayers()
    {
        foods = GameObject.FindGameObjectsWithTag("Food");
        players = GameObject.FindGameObjectsWithTag("Player");
    }

    //if the AI agent is largest player on board, kill mode is activated and the target is closest object with the tag "Player", otherwise it is the closest object with the tag "food"
    void ModeCheck()
    {
        int largestXAmount = 0;
        float largestX = 0;
        foreach (GameObject player in players)
        {
            if (player.transform.localScale.x > largestX)
            {
                largestX = player.transform.localScale.x;
            }
        }
        foreach (GameObject player in players)
        {
            if (player.transform.localScale.x == largestX)
                largestXAmount++;
        }
        if (transform.localScale.x == largestX && largestXAmount == 1)
            eatMode = false;
        else
            eatMode = true;
    }
    //character by default moves forward and it can only turn to adjust direction. 
    private void FixedUpdate()
    {
        Vector3 movement = transform.forward * speed * Time.deltaTime;
        rb.MovePosition(transform.position + movement);
        Vector3 target = UpdateTarget();


        // Change rotation to head towards the target
        RotateTowardsTarget(target);

    }

    //by default, the target is the centre of the board, which is vec3.zero. if the agent is in eat mode but there is no food available, in order to
    //prevent it from just moving off the field and dying, I made it target centre. otherwise, target is either closest food or closest player, based on how large agent is.
    private Vector3 UpdateTarget()
    {
        target = Vector3.zero;
        float distance = Mathf.Infinity;
        if (eatMode && foods == null)
        {
            return target;
        }
        else if (eatMode && foods != null)
        {
            foreach (GameObject food in foods)
            {
                if (food != null)
                    if (Vector3.Distance(food.transform.position, transform.position) < distance && foods != null)
                    {
                        distance = Vector3.Distance(food.transform.position, transform.position);
                        target = food.transform.position;
                    }
            }
        }
        else if (!eatMode)
        {
            foreach (GameObject player in players)
            {
                if (player != null)
                    if (Vector3.Distance(player.transform.position, transform.position) < distance && Vector3.Distance(player.transform.position, transform.position) > 0)
                    {
                        distance = Vector3.Distance(player.transform.position, transform.position);
                        target = player.transform.position;
                    }
            }
        }
        if (target == transform.position)
            target = Vector3.zero;
        return target;
    }
    //change transform.rotation using quaternion lookrotatþon.
    private void RotateTowardsTarget(Vector3 target)
    {
        target.y = transform.position.y;
        Vector3 direction = (target - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotateSpeed);
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
        }
        else if (other.CompareTag("Fall"))
        {
            Destroy(this.gameObject);
            lastTouchPlayer.SendMessage(nameof(GetKillScore));
        }
        else if (other.CompareTag("PlayerBack"))
        {
            other.transform.parent.GetComponent<Rigidbody>().AddForce(transform.forward * pushForce * 30);
        }
    }

    //normal push, pretty straightforward i gýess
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            lastTouchPlayer = collision.gameObject;
            collision.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * pushForce * 15);
        }
    }

    private void GetKillScore() 
    {
        return;
    }
}