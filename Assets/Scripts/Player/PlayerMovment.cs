using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovment : MonoBehaviour
{
    public static PlayerMovment instance;
    public float speed;
    public float currentSpeed;
    public float angleSpeed;
    private Rigidbody rb;
    public Animator anim;
    //
    public GameObject smokeEffect, fireEffect, explosionEffect, boostFlame, boostFlame2;
    public float invincibleTime = 1;
    public int life = 10;

    public int currentLife;
    public float currentinvincibleTime;
    private bool stopCheckIfCollide = true;


    private bool winGameCantMove;

    public TrailRenderer trailRenderer;


    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        rb = GetComponent<Rigidbody>();
        trailRenderer = GetComponent<TrailRenderer>();
        anim = GetComponentInChildren<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.startTheGame)
        {
            if (!Boost.instance.playerSpeedBoost)
            {
                currentSpeed = speed;
            }
        }
        else
        {
            currentSpeed = 0;
        }
    }
    private void FixedUpdate()
    {
        if (!winGameCantMove)
        {
            AllMovment();
        }
        WinDrift();
    }
    void AllMovment()
    {
        //here is movement so the car can go up through gravity;
        Vector3 newPositon = transform.position + (transform.forward * currentSpeed * Time.deltaTime);
        rb.MovePosition(newPositon);

        //Player Auto move forward , and here is the movement for collision with other objects
        var v3 = transform.forward * currentSpeed;
        v3.y = rb.velocity.y;
        rb.velocity = v3;

        //Invincible timer
        // currentinvincibleTime -= Time.deltaTime;

        //The old movement
        //PlayerMovement();

        //The new movement
        PlayerMovementButtons();
        rb.AddForce(Vector3.down * rb.mass * 50);
    }
    void PlayerMovement()
    {
        if (Input.GetMouseButton(0) || Input.touchCount > 0)
        {
            float x = Input.mousePosition.x;
            if (x < Screen.width / 2 && x > 0)
            {
                MoveLeft();
            }
            if (x > Screen.width / 2 && x < Screen.width)
            {
                MoveRight();
            }
        }
    }
    void PlayerMovementButtons()
    {
        if (ScreenButtonMovement.instance.pressingLeft)
        {
            MoveLeft();
        }
        else if (ScreenButtonMovement.instance.pressingRight)
        {
            MoveRight();
        }
        else return;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "EnemyPolice" && stopCheckIfCollide)
        {
            if (currentinvincibleTime <= 0)
            {
                currentinvincibleTime = 1;
                life--;
                if (life < 3)
                {
                    smokeEffect.SetActive(true);
                }
                if (life < 1)
                {
                    fireEffect.SetActive(true);
                }
                if (life <= 0)
                {
                    GameObject ExplosionPrefab = Instantiate(explosionEffect, transform.position, Quaternion.identity);
                    Destroy(ExplosionPrefab, 2);
                    for (int a = 0; a < 1; a++)
                    {
                        transform.GetChild(a).gameObject.SetActive(false);
                    }
                    rb.constraints = RigidbodyConstraints.FreezeAll;
                    smokeEffect.SetActive(false);
                    fireEffect.SetActive(false);
                    stopCheckIfCollide = false;
                    GameManager.instance.allCopsGoAway = true;
                    GameManager.instance.StopTheGameHelper = true;
                    GameManager.instance.startTheGame = false;
                    CanvasManager.instance.EndGameCardLose();
                }
            }
        }
    }
    public void MoveLeft()
    {
        if (!GameManager.instance.StopTheGameHelper)
        {
            transform.Rotate(-Vector3.up * angleSpeed * Time.deltaTime);
            GameManager.instance.startTheGame = true;
        }
    }
    public void MoveRight()
    {
        if (!GameManager.instance.StopTheGameHelper)
        {
            transform.Rotate(Vector3.up * angleSpeed * Time.deltaTime);
            GameManager.instance.startTheGame = true;
        }
    }
    void WinDrift()
    {
        if (Cones.instance.allConesCollected)
        {
            anim.SetBool("WinDrift", true);
            anim.SetBool("Boost", false);
            winGameCantMove = true;
            Invoke("StopCarPlayerSpeed", 1);
            GameManager.instance.allCopsGoAway = true;
        }
    }
    void StopCarPlayerSpeed()
    {
        rb.constraints = RigidbodyConstraints.FreezeAll;
        CanvasManager.instance.EndGameCardWin();
    }
}
