using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelOnDestroy : MonoBehaviour
{
    public float moveSpeed;
    public float downSpeed;
    int randomDirection;
    // Start is called before the first frame update
    void Start()
    {
        randomDirection = Random.Range(0, 4);
    }

    // Update is called once per frame
    void Update()
    {
        RandomDirection();
        transform.position += -transform.up * downSpeed * Time.deltaTime;

        Destroy(gameObject, 5);
    }
    void RandomDirection()
    {
        if (randomDirection == 0)
        {
            transform.position += transform.right * moveSpeed * Time.deltaTime;
        }
        else if (randomDirection == 1)
        {
            transform.position += -transform.right * moveSpeed * Time.deltaTime;
        }
        else if (randomDirection == 2)
        {
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }
        else if (randomDirection == 3)
        {
            transform.position += -transform.forward * moveSpeed * Time.deltaTime;
        }
    }
}
