using System.Collections.Generic;
using UnityEngine;

public class GroundFollow : MonoBehaviour
{
    public GameObject player;
    public GameObject groundFloor;
    float spawnFloorTimer = 5;
    public GameObject newGroundFloor;
    public List<GameObject> newGroundFloors;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // SpawnGroundFloorTimer();
        GroundFloorFollowPlayer();
    }
    void SpawnGroundFloorTimer()
    {
        if (GameObject.FindGameObjectWithTag("Player") == isActiveAndEnabled)
        {
            player = GameObject.FindGameObjectWithTag("Player");

            spawnFloorTimer -= Time.deltaTime;
            if (spawnFloorTimer <= 0)
            {
                SpawnGroundFloor();
                spawnFloorTimer = 5;
                if (newGroundFloors.Count >= 2)
                {
                    Destroy(newGroundFloors[0]);
                    newGroundFloors.RemoveAt(0);
                }
            }
        }
    }
    void GroundFloorFollowPlayer()
    {
        if (GameObject.FindGameObjectWithTag("Player") == isActiveAndEnabled)
        {
            player = GameObject.FindGameObjectWithTag("Player");

            var posX = player.transform.position.x;
            var posZ = player.transform.position.z;
            var newPos = new Vector3(posX, 0, posZ);
            gameObject.transform.position = newPos;
        }
    }
    void SpawnGroundFloor()
    {
        Vector3 newPos = new Vector3(player.transform.position.x, 0, player.transform.position.z);
        newGroundFloor = Instantiate(groundFloor, newPos, Quaternion.identity);
        newGroundFloors.Add(newGroundFloor);
    }
}
