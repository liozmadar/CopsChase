using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarSelection : MonoBehaviour
{
    public static CarSelection instance;
    public GameObject[] playerCarSelection;
    private int currentCarIndex;
    public GameObject firstStartCar;

    public Button nextCar;
    public Button previousCar;
    private bool cantMakeMoreThenTwoCars;

    public float speed = 40;
    public List<GameObject> availableCars;

    private void Awake()
    {
        instance = this;

        FirstCarStart();
    }
    // Start is called before the first frame update
    void Start()
    {
        previousCar.interactable = false;
    }
    // Update is called once per frame
    void Update()
    {
        CloseTheChangeCarsButtons();
    }
    public void CloseTheChangeCarsButtons()
    {
        if (GameManager.instance.startTheGame)
        {
            nextCar.gameObject.SetActive(false);
            previousCar.gameObject.SetActive(false);

            CanvasManager.instance.GameTutorialText.SetActive(false);

            if (!CanvasManager.instance.ConesTutorialTextBool)
            {
                CanvasManager.instance.ConesTutorialText.gameObject.SetActive(true);
                Invoke("CloseConeTutorialText", 5);
                CanvasManager.instance.ConesTutorialTextBool = true;
            }
        }
    }
    void CloseConeTutorialText()
    {
        CanvasManager.instance.ConesTutorialText.gameObject.SetActive(false);
    }
    public void FirstCarStart()
    {
        if (!cantMakeMoreThenTwoCars)
        {
            cantMakeMoreThenTwoCars = true;
            firstStartCar = Instantiate(playerCarSelection[0], transform.position, Quaternion.identity);
        }
    }
    public void NextCarIndex()
    {
        if (availableCars.Count > currentCarIndex + 1)
        {
            Destroy(GameObject.FindGameObjectWithTag("Player"));
            currentCarIndex++;
            Instantiate(availableCars[currentCarIndex], transform.position, Quaternion.identity);
            if (availableCars.Count <= currentCarIndex + 1)
            {
                nextCar.interactable = false;
            }
            previousCar.interactable = true;
        }
    }
    public void PreviousCarIndex()
    {
        if (currentCarIndex > 0)
        {
            Destroy(GameObject.FindGameObjectWithTag("Player"));
            currentCarIndex--;
            Instantiate(availableCars[currentCarIndex], transform.position, Quaternion.identity);
            if (currentCarIndex <= 0)
            {
                previousCar.interactable = false;
            }
            nextCar.interactable = true;
        }
    }
}
