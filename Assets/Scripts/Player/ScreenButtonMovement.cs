using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenButtonMovement : MonoBehaviour
{
    public static ScreenButtonMovement instance;

    public bool pressingLeft;
    public bool pressingRight;

    public Button screenLeft;
    public Button screenRight;

    public bool onlyDisableOnTestMode;

    // Start is called before the first frame update
    private void Awake()
    {
        screenLeft = GameObject.FindGameObjectWithTag("ScreenLeft").GetComponent<Button>();
        screenRight = GameObject.FindGameObjectWithTag("ScreenRight").GetComponent<Button>();
    }
    void Start()
    {
        instance = this;
    }
    // Update is called once per frame
    void Update()
    {
        if (onlyDisableOnTestMode)
        {
            FingerCount();
        }
    }
    void FingerCount()
    {
        var fingerCount = 0;
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
            {
                fingerCount++;
            }
        }
        if (fingerCount <= 0)
        {
            pressingLeft = false;
            pressingRight = false;
        }
    }
    public void ClickLeftDown()
    {
        pressingLeft = true;
    }
    public void ClickRightDown()
    {
        pressingRight = true;
    }
    public void ClickLeftUp()
    {
        pressingLeft = false;
    }
    public void ClickRightUp()
    {
        pressingRight = false;
    }
}
