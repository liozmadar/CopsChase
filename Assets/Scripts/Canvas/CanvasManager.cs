using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.EventSystems;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager instance;

    public TextMeshProUGUI headerText;

    public TextMeshProUGUI timerScore;
    public TextMeshProUGUI copsDestroyed;
    public TextMeshProUGUI coneCollected;

    //The gameobjects on the gamePlayScreenUI
    public GameObject playScreenTimer;
    public GameObject playScreenCops;
    public GameObject playScreenCones;
    public GameObject playScreenBoost;

    public TextMeshProUGUI bestScoreText;
    public int bestScoreCount;
    public TextMeshProUGUI mostCopsDestroyedText;
    public int mostCopsDestroyedCount;
    public TextMeshProUGUI mostConeCollectedText;
    public int mostConeCollectedCount;

    public TextMeshProUGUI coinsEarndFromConeCollected;

    public GameObject newScore;
    public bool newScoreBool;
    public GameObject newCops;
    public bool newCopsBool;
    public GameObject newCones;
    public bool newConesBool;

    //Home screen UI
    public GameObject buyCarsScreenUI;
    public GameObject homeScreenUI;
    public GameObject playerCarsChange;
    public GameObject endGameCard;
    public GameObject resetButton;
    public GameObject adButton;
    public GameObject touchScreenButtons;
    public bool adButtonBool;
    public bool resetButtonBool;

    public GameObject onlyOnMobileAdImage;
    private bool onlyOnMobileAdImageBool;

    public GameObject clickOnAdButton;

    private int endGameCardClickToPlayAgain;

    public GameObject GameTutorialText;
    public TextMeshProUGUI ConesTutorialText;
    public bool ConesTutorialTextBool;


    public int coinsFromCops;
    public TextMeshProUGUI TotalCoinsInGame;
    public TextMeshProUGUI coinsFromCopsEndGameCardText;
    public int conesAndCopsEarnSum;
    public TextMeshProUGUI conesAndCopsEarnSumText;

    AsyncOperation loadingOperation;
    //

    // Start is called before the first frame update
    void Start()
    {
        SceneManager.UnloadSceneAsync(0);
        instance = this;

        GameTutorialText = GameObject.Find("ShowHowToTheCarsTurn");

        playerCarsChange.SetActive(false);
        GameTutorialText.SetActive(false);
        touchScreenButtons.SetActive(false);
        playScreenTimer.gameObject.SetActive(false);
        playScreenCops.gameObject.SetActive(false);
        playScreenCones.gameObject.SetActive(false);
        playScreenBoost.gameObject.SetActive(false);
        TotalCoinsInGame.gameObject.SetActive(false);

        buyCarsScreenUI.SetActive(false);
        endGameCard.SetActive(false);
        homeScreenUI.SetActive(true);


        //Get the prefs of 1 = close the homeUI , or 2 = keep the homeUI
        endGameCardClickToPlayAgain = PlayerPrefs.GetInt("PlayAgain");

        //Stop the car from moveing
        //  Time.timeScale = 0;

        //If 1 then you want to play again , after the game reset close the homeUI and and let the cars move
        if (endGameCardClickToPlayAgain == 1)
        {
            homeScreenUI.SetActive(false);

            playerCarsChange.SetActive(true);
            GameTutorialText.SetActive(true);
            touchScreenButtons.SetActive(true);
            playScreenTimer.gameObject.SetActive(true);
            playScreenCops.gameObject.SetActive(true);
            playScreenCones.gameObject.SetActive(true);
            playScreenBoost.gameObject.SetActive(true);
            TotalCoinsInGame.gameObject.SetActive(true);

            PlayerPrefs.SetInt("PlayAgain", 0);
            //   Time.timeScale = 1;
            for (int i = 0; i < CarsUI.instance.allCars.Count; i++)
            {
                if (PlayerPrefs.GetInt(CarsUI.instance.allCars[i].id.ToString()) == 1)
                {
                    CarSelection.instance.availableCars.Add(CarSelection.instance.playerCarSelection[i]);
                }
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        CheckTheBestScoreNumber();
        CheckIfNewScore();

        conesAndCopsEarnSum = coinsFromCops + Cones.instance.totalCoinsFromCones;
        TotalCoinsInGame.text = "<color=orange>+" + conesAndCopsEarnSum.ToString() + "</color> ";
        conesAndCopsEarnSumText.text = "+" + conesAndCopsEarnSum.ToString();
    }
    //All UI screens
    public void ClickToGoToBuyCarsUI()
    {
        homeScreenUI.SetActive(false);
        buyCarsScreenUI.SetActive(true);
        endGameCard.SetActive(false);
    }
    public void ClickToGoBackToHomeUI()
    {
        homeScreenUI.SetActive(true);
        buyCarsScreenUI.SetActive(false);
        endGameCard.SetActive(false);
        //  Time.timeScale = 0;

        for (int i = 0; i < CarsUI.instance.allCars.Count; i++)
        {
            CarsClickable carsClickable = CarsUI.instance.carsImages[i].gameObject.GetComponent<CarsClickable>();
            if (carsClickable.canvas != null && carsClickable.isOpen)
            {
                Destroy(carsClickable.canvas);
                carsClickable.isOpen = false;
            }
        }
    }
    //Click to play the game from the first homeUI
    public void ClickToPlayGameFromHomeUI()
    {
        homeScreenUI.SetActive(false);
        playerCarsChange.SetActive(true);
        touchScreenButtons.SetActive(true);

        //Show the tutorial and the car change buttons
        GameTutorialText.SetActive(true);
        //

        playScreenTimer.gameObject.SetActive(true);
        playScreenCops.gameObject.SetActive(true);
        playScreenCones.gameObject.SetActive(true);
        playScreenBoost.gameObject.SetActive(true);
        TotalCoinsInGame.gameObject.SetActive(true);

        //  Time.timeScale = 1;

        for (int i = 0; i < CarsUI.instance.allCars.Count; i++)
        {
            if (PlayerPrefs.GetInt(CarsUI.instance.allCars[i].id.ToString()) == 1)
            {
                CarSelection.instance.availableCars.Add(CarSelection.instance.playerCarSelection[i]);
            }
        }
    }
    //Reset the game but close the first homeUI
    public void TryAgainButton()
    {
        PlayerPrefs.SetInt("PlayAgain", 1);
        SceneManager.LoadScene(0);
        // Here im playing the preloading scene
        // loadingOperation.allowSceneActivation = true;
    }
    //Reset the game and keep the first homeUI
    public void GoToHomeUIAfterTheEndCard()
    {
        PlayerPrefs.SetInt("PlayAgain", 0);
        SceneManager.LoadScene(0);
        // I set my preload scene to true
        // loadingOperation.allowSceneActivation = true;

    }
    public void AdButtonOpenClose()
    {
        if (!adButtonBool)
        {
            adButton.SetActive(true);
            adButtonBool = true;
        }
        else
        {
            adButton.SetActive(false);
            adButtonBool = false;
        }

    }
    public void ClickOnAdButton()
    {
        Debug.Log("Watch ad now !");
        if (!onlyOnMobileAdImageBool)
        {
            onlyOnMobileAdImage.SetActive(true);
            onlyOnMobileAdImageBool = true;
            Invoke("OnlyOnMobileAdDestroy", 2);
        }
    }
    void OnlyOnMobileAdDestroy()
    {
        onlyOnMobileAdImage.SetActive(false);
        onlyOnMobileAdImageBool = false;
    }
    //

    void CheckIfNewScore()
    {
        if (newScoreBool)
        {
            newScore.SetActive(true);
        }
        if (newConesBool)
        {
            newCones.SetActive(true);
        }
        if (newCopsBool)
        {
            newCops.SetActive(true);
        }
    }
    public void EndGameCardWin()
    {
        Invoke("DelayTheEndCardWin", 1);
    }
    void DelayTheEndCardWin()
    {
        endGameCard.SetActive(true);
        headerText.text = "You win!";
        //Change the text of time,copsDestroy and ConeCollected
        timerScore.text = Timer.instance.timerText.ToString();
        copsDestroyed.text = GameManager.instance.copsDestroyedNumber.ToString();
        coneCollected.text = Cones.instance.coneCollectedCount.ToString();
        //
        // Here im loading the same scene and set it false (like preload the next scene)
        /* loadingOperation = SceneManager.LoadSceneAsync(0, LoadSceneMode.Additive);
         loadingOperation.allowSceneActivation = false;*/
        //
        //Add the coinsFromCops and fromCones and sum them
        coinsEarndFromConeCollected.text = "+" + Cones.instance.totalCoinsFromCones.ToString();
        coinsFromCopsEndGameCardText.text = "+" + coinsFromCops.ToString();

        //Add the coins i got from the game to the totalScorePoints
        ScoreSystem.instance.totalScorePoints += conesAndCopsEarnSum;
        PlayerPrefs.SetInt("totalScorePoints", ScoreSystem.instance.totalScorePoints);
    }
    public void EndGameCardLose()
    {
        Invoke("DelayTheEndCardLose", 1);
    }
    void DelayTheEndCardLose()
    {
        endGameCard.SetActive(true);
        headerText.text = "Busted!";
        //Change the text of time,copsDestroy and ConeCollected
        timerScore.text = Timer.instance.timerText.ToString();
        copsDestroyed.text = GameManager.instance.copsDestroyedNumber.ToString();
        coneCollected.text = Cones.instance.coneCollectedCount.ToString();
        //
        // Here im loading the same scene and set it false (like preload the next scene)
        /* loadingOperation = SceneManager.LoadSceneAsync(0, LoadSceneMode.Additive);
         loadingOperation.allowSceneActivation = false;*/
        //
        //Show the coinsFromCops and coinsFromCones
        coinsEarndFromConeCollected.text = "+" + Cones.instance.totalCoinsFromCones.ToString();
        coinsFromCopsEndGameCardText.text = "+" + coinsFromCops.ToString();

        //Add the coins i got from the game to the totalScorePoints
        ScoreSystem.instance.totalScorePoints += conesAndCopsEarnSum;
        PlayerPrefs.SetInt("totalScorePoints", ScoreSystem.instance.totalScorePoints);
    }
    void MostConesCollectedCheck()
    {
        mostConeCollectedCount = Cones.instance.coneCollectedCount;
        PlayerPrefs.SetInt("Cones", mostConeCollectedCount);
    }
    void MostCopsDestroyed()
    {
        mostCopsDestroyedCount = GameManager.instance.copsDestroyedNumber;
        PlayerPrefs.SetInt("Cops", mostCopsDestroyedCount);
    }
    void BestScore()
    {
        bestScoreCount = Timer.instance.timerText;
        PlayerPrefs.SetInt("Score", bestScoreCount);
    }
    void CheckTheBestScoreNumber()
    {
        if (Cones.instance.coneCollectedCount > PlayerPrefs.GetInt("Cones"))
        {
            MostConesCollectedCheck();
            newConesBool = true;
        }
        else
        {
            mostConeCollectedText.text = PlayerPrefs.GetInt("Cones").ToString();
        }
        if (GameManager.instance.copsDestroyedNumber > PlayerPrefs.GetInt("Cops"))
        {
            MostCopsDestroyed();
            newCopsBool = true;
        }
        else
        {
            mostCopsDestroyedText.text = PlayerPrefs.GetInt("Cops").ToString();
        }
        if (Timer.instance.timerText > PlayerPrefs.GetInt("Score"))
        {
            BestScore();
            newScoreBool = true;
        }
        else
        {
            bestScoreText.text = PlayerPrefs.GetInt("Score").ToString();
        }
    }
    public void OpenResetButton()
    {
        if (!resetButtonBool)
        {
            resetButton.SetActive(true);
            resetButtonBool = true;
        }
        else
        {
            resetButton.SetActive(false);
            resetButtonBool = false;
        }

    }
    public void DeleteAllEndGameCardKeys()
    {
        PlayerPrefs.DeleteKey("Cones");
        PlayerPrefs.DeleteKey("Cops");
        PlayerPrefs.DeleteKey("Score");
    }
}
