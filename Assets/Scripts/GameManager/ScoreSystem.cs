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
        totalScorePointsTextHomeScreen.text = totalScorePoints.ToString();
        totalScorePointsTextCarsScreen.text = totalScorePoints.ToString();
    }
}
