using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemySpawners : MonoBehaviour
{
    public GameObject[] EnemySpawnPoints;
    public GameObject EnemyPolice;
    public GameObject EnemyPoliceLevel2;
    public GameObject EnemyPoliceLevel3;
    public float timer = 1;
    //   
    public int EnemyPoliceTimerLevels;

    public TextMeshProUGUI copsCountText;
    private int copsCountNumber = 1;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        EnemyPoliceTimerLevels = GameObject.Find("Timer").GetComponent<Timer>().timerText;
   
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            if (EnemyPoliceTimerLevels < 10)
            {
                EnemySpawn();
                timer = 1;
            }
            else if (EnemyPoliceTimerLevels > 10)
            {
                EnemySpawnLevel2();
                timer = 1;
            }
            else if (EnemyPoliceTimerLevels > 20)
            {
                timer = 1;
                EnemySpawnLevel3();
            }
            copsCountNumber++;
            copsCountText.text = copsCountNumber.ToString();
        }      
    }
    void EnemySpawn()
    {
        int RandomPoint = Random.Range(0, EnemySpawnPoints.Length);
        Instantiate(EnemyPolice, EnemySpawnPoints[RandomPoint].transform.position, Quaternion.identity);
    }
    void EnemySpawnLevel2()
    {
        int RandomPoint = Random.Range(0, EnemySpawnPoints.Length);
        Instantiate(EnemyPoliceLevel2, EnemySpawnPoints[RandomPoint].transform.position, Quaternion.identity);
    }
    void EnemySpawnLevel3()
    {
        int RandomPoint = Random.Range(0, EnemySpawnPoints.Length);
        Instantiate(EnemyPoliceLevel3, EnemySpawnPoints[RandomPoint].transform.position, Quaternion.identity);
    }
}
