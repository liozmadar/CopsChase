using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CarsClickable : MonoBehaviour, IPointerClickHandler
{
    public bool isOpen;

    public GameObject buyCarsUIImage;
    public GameObject canvas;
    private Vector3 offSet = new Vector3(0, 50, 0);

    private Transform firstChildPosition;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void HideToolTip()
    {
        if (canvas != null && isOpen)
        {
            Destroy(canvas);
            isOpen = false;
        }
    }
    private void ShowToolTip(int i)
    {
        firstChildPosition = transform.GetChild(0).gameObject.GetComponent<Transform>();
        canvas = Instantiate(buyCarsUIImage, firstChildPosition.transform.position, Quaternion.identity);
        canvas.transform.SetParent(CarsUI.instance.carsImages[i], false);
        canvas.transform.position = firstChildPosition.transform.position + offSet;
        ShowCarCostText.instance.carsCostText.text = CarsUI.instance.allCars[i].carCostNumber.ToString();
        CarsUI.instance.allCars[i].carBuyBoolImage = 1;
        isOpen = true;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        for (int i = 0; i < CarsUI.instance.allCars.Count; i++)
        {
            if (gameObject.name == CarsUI.instance.allCars[i].name)
            {
                if (PlayerPrefs.GetInt(CarsUI.instance.allCars[i].id.ToString()) == 0)
                {
                    if (isOpen) 
                    {
                        HideToolTip();
                    } 
                    else ShowToolTip(i);
                }
            }
            else
            {
                CarsClickable carsClickable = CarsUI.instance.carsImages[i].gameObject.GetComponent<CarsClickable>();
                CarsUI.instance.allCars[i].carBuyBoolImage = 0;
                carsClickable.HideToolTip();
            }
        }
    }
}
