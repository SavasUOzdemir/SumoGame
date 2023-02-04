using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] float growAmount = 0.1f;
    [SerializeField] float pushForce = 10f;
    [SerializeField] float pushDistance = 1f;
    [SerializeField] float speed = 10f;
    [SerializeField] float rotateSpeed = 5f;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal")*rotateSpeed;
        Vector3 rotation = new Vector3(0, horizontal, 0);
        transform.Rotate(rotation * speed * Time.deltaTime);

        Vector3 movement = transform.forward * speed * Time.deltaTime;
        rb.MovePosition(transform.position + movement);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Food"))
        {
            transform.localScale += new Vector3(growAmount, growAmount, growAmount);
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector3 pushDirection = (collision.transform.position - transform.position).normalized;
            RaycastHit hit;
            if (Physics.Raycast(transform.position, pushDirection, out hit, pushDistance))
            {
                if (hit.collider.gameObject.CompareTag("Player") && hit.normal == Vector3.back)
                {
                    rb.AddForce(pushDirection * pushForce, ForceMode.Impulse);
                }
            }
        }
    }
}
