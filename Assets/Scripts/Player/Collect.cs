using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Collect : MonoBehaviour
{
    public static Collect instance;

    // Cops spawn
    public GameObject[] EnemySpawnPoints;
    public float timer = 1;

    public GameObject cop1;
    public GameObject cop2;
    public GameObject cop3;

    public TextMeshProUGUI copsCountText;
    public int copsCountNumber = 1;
    //;

    private void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectWithTag("CopsCountText") == isActiveAndEnabled)
        {
            copsCountText = GameObject.FindGameObjectWithTag("CopsCountText").GetComponent<TextMeshProUGUI>();
            copsCountText.text = GameManager.instance.copsDestroyedNumber.ToString();
        }
        if (GameManager.instance.startTheGame)
        {
            SpawnCop();
        }

    }
    void SpawnCop()
    {
        if (!Cones.instance.allConesCollected)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                int RandomPoint = Random.Range(0, EnemySpawnPoints.Length);
                if (Timer.instance.timerText < 10)
                {
                    Instantiate(cop1, EnemySpawnPoints[RandomPoint].transform.position, Quaternion.identity);
                }
                else if (Timer.instance.timerText > 10 && Timer.instance.timerText < 20)
                {
                    Instantiate(cop2, EnemySpawnPoints[RandomPoint].transform.position, Quaternion.identity);
                }
                else if (Timer.instance.timerText > 20)
                {
                    Instantiate(cop3, EnemySpawnPoints[RandomPoint].transform.position, Quaternion.identity);
                }
                timer = 1;

                copsCountNumber++;
            }
        }

    }
}
