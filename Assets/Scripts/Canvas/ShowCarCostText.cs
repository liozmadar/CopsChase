using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowCarCostText : MonoBehaviour
{
    public static ShowCarCostText instance; 
    public TextMeshProUGUI carsCostText;

    private void Awake()
    {
        instance = this;
        carsCostText = GetComponent<TextMeshProUGUI>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
