using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IndicatorMeters : MonoBehaviour
{
    private GameObject player;
    private GameObject cone;
    private GameObject meters;
    public GameObject metersPrefab;
    private TextMeshProUGUI metersText;
    private GameObject canvas;

    private Vector3 offSet = new Vector3(0,50,0);

    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("Canvas");
        CreateMeterObject();
        player = GameObject.FindGameObjectWithTag("Player");
        cone = GameObject.Find("IndicatorOffScreen:Cone(Clone)");
    }

    // Update is called once per frame
    void Update()
    {
        if (meters !=null)
        {
            PlayerMetersFromCone();
            ShowMetersOnCanvas();
            TurnOffTheMetersWhenTooClose();
        }
    }
    void TurnOffTheMetersWhenTooClose()
    {
        if (!cone.activeInHierarchy)
        {
            meters.gameObject.SetActive(false);
        }
        else
        {
            meters.gameObject.SetActive(true);
        }
    }
    void CreateMeterObject()
    {
        meters = Instantiate(metersPrefab, transform.position, Quaternion.identity);
        meters.transform.SetParent(canvas.transform, false);
        metersText = meters.GetComponent<TextMeshProUGUI>();
    }
    void ShowMetersOnCanvas()
    {
        meters.transform.position = cone.transform.position + offSet;
    }
    void PlayerMetersFromCone()
    {
        float dist = Vector3.Distance(player.transform.position, transform.position);
        dist = ((int)dist) / 10;
        metersText.text = dist.ToString();
    }
}
