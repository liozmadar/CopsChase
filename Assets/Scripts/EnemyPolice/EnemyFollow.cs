using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFollow : MonoBehaviour
{
    public GameObject player;
    public Rigidbody rb;
    public float speed;
    public float slowSpeed;
    public float currentSpeed;
    public float angleSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        CopsSpeedChange();
        AnotherMovment();
        Rotation();
        AllCopsGoAway();
    }
    void CopsSpeedChange()
    {
        if (GameManager.instance.startTheGame)
        {
            if (transform.position.y > 8)
            {
                currentSpeed = slowSpeed;
            }
            else
            {
                currentSpeed = speed;
            }
        }
        else if (GameManager.instance.allCopsGoAway)
        {
            currentSpeed = speed;
        }
        else
        {
            currentSpeed = PlayerMovment.instance.currentSpeed;
        }
    }
    void AnotherMovment()
    {
        if (player != null)
        {
            //here is movement so the car can go up through gravity;
            Vector3 newPositon = transform.position + (transform.forward * currentSpeed * Time.deltaTime);
            rb.MovePosition(newPositon);

            //Player Auto move forward , and here is the movement for collision with other objects
            var v3 = transform.forward * currentSpeed;
            v3.y = rb.velocity.y;
            rb.velocity = v3;
            //
            rb.AddForce(Vector3.down * rb.mass * 50);
        }
    }
    void AllCopsGoAway()
    {
        if (GameManager.instance.allCopsGoAway)
        {
            var targetRotation = Quaternion.LookRotation(transform.position - player.transform.position);

            // Smoothly rotate towards the target point.
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, angleSpeed * Time.deltaTime);
        }
    }
    void Rotation()
    {
        if (!GameManager.instance.allCopsGoAway && GameManager.instance.startTheGame)
        {
            if (player != null)
            {
                var targetRotation = Quaternion.LookRotation(player.transform.position - transform.position);

                // Smoothly rotate towards the target point.
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, angleSpeed * Time.deltaTime);
            }
        }
    }
}
