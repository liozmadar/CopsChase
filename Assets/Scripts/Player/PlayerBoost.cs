using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBoost : MonoBehaviour
{
    public static PlayerBoost instance;
    public bool activeTheBoost;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ClickToBoost()
    {
        activeTheBoost = true;
    }
}
