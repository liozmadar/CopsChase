using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovment : MonoBehaviour
{
    public GameObject player;
    //public GameObject CameraPlayer;
    public GameObject planet;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        CameraMove();
    }
    void CameraMove()
    {
        float PosX = player.transform.position.x;
        float PosZ = player.transform.position.z - 180;

        transform.position = new Vector3(PosX, transform.position.y, PosZ);
    }
    void NewNewCameraMove()
    {
        transform.position = Vector3.Lerp(transform.position, player.transform.position, 1f);
        Vector3 gravDirection = (transform.position - planet.transform.position).normalized;

        Quaternion toRotation = Quaternion.FromToRotation(transform.up, gravDirection) * transform.rotation;
        transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, 1f);
    }
}
