using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreSystem : MonoBehaviour
{
    public static ScoreSystem instance;

    //"totalScorePoints" is the playerPrefs
    public int totalScorePoints;

    public TextMeshProUGUI totalScorePointsTextHomeScreen;
    public TextMeshProUGUI totalScorePointsTextCarsScreen;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

    }

    // Update is called once per frame
    void Update()
    {

        totalScorePoints = PlayerPrefs.GetInt("totalScorePoints");
       
        if (totalScorePoints >= 10000)
        {
            string newInt =  totalScorePoints / 1000 + "k";

            totalScorePointsTextHomeScreen.text = newInt;
            totalScorePointsTextCarsScreen.text = newInt;

           // Debug.Log(newInt);
        }
        else
        {
            totalScorePointsTextHomeScreen.text = totalScorePoints.ToString();
            totalScorePointsTextCarsScreen.text = totalScorePoints.ToString();
        }
    }
}
