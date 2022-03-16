using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public float rotateSpeed = 100, ballSpeed = 15;
    public int ammo = 4;
    public float ballLifeTime = 1.5f;
    public float shootTime;
    private float distanceX;

    private Transform ballPos;
    private GameObject ballSprite;
    private Transform shootPos1, shootPos2;
    private LineRenderer lineRenderer;

    public GameObject Ball;
    private GameObject crossHair;

    public AudioClip ballShootAudio;

    private Animator animator;

    private bool canShoot = true;


    void Awake()
    {
        crossHair = GameObject.Find("CrossHair");
        crossHair.SetActive(false);

        ballPos = GameObject.Find("BallPos").transform;
        ballSprite = GameObject.Find("BallSprite");
        shootPos1 = GameObject.Find("ShootPos1").transform;
        shootPos2 = GameObject.Find("ShootPos2").transform;

        lineRenderer = GameObject.Find("Gun").GetComponent<LineRenderer>();
        lineRenderer.enabled = false;

        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        distanceX = ballPos.position.x - transform.position.x;
    }

    void Update()
    {
        if (!IsMouseOnUI())
        {
            if (Input.GetMouseButton(0))
            {
                Aim();
                ballSprite.SetActive(true);
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (ammo > 0 && canShoot && GameObject.FindGameObjectsWithTag("Kale").Length > 0)
                {
                    Shoot();
                    ballSprite.SetActive(false);
                }

                else
                {
                    lineRenderer.enabled = false;
                    crossHair.SetActive(false);
                }
            }
        }
    }

    void Aim()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - ballPos.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        ballPos.rotation = Quaternion.Slerp(transform.rotation, rotation, rotateSpeed);

        RotatePlayer(direction);

        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, shootPos1.position);
        lineRenderer.SetPosition(1, shootPos2.position);
        
        crossHair.SetActive(true);
        crossHair.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + (Vector3.forward * 10);
    }

    void RotatePlayer(Vector2 dir)
    {
        if (dir.x >= 0 && transform.localScale.x == -1 && distanceX > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            ballPos.localScale = new Vector3(1, 1, 1);
            transform.position = new Vector3(ballPos.position.x - distanceX, transform.position.y, transform.position.z);
        }

        else if (dir.x < 0 && transform.localScale.x == 1 && distanceX > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            ballPos.localScale = new Vector3(-1, 1, 1);
            transform.position = new Vector3(ballPos.position.x + distanceX, transform.position.y, transform.position.z);
        }

        else if (dir.x >= 0 && transform.localScale.x == -1 && distanceX < 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            ballPos.localScale = new Vector3(1, 1, 1);
            transform.position = new Vector3(ballPos.position.x + distanceX, transform.position.y, transform.position.z);
        }

        else if (dir.x < 0 && transform.localScale.x == 1 && distanceX < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            ballPos.localScale = new Vector3(-1, 1, 1);
            transform.position = new Vector3(ballPos.position.x - distanceX, transform.position.y, transform.position.z);
        }
    }

    void Shoot()
    {
        
        SoundManager.instance.PlaySoundFX(ballShootAudio, .1f);

        lineRenderer.enabled = false;
        crossHair.SetActive(false);

        GameObject Ball = Instantiate(this.Ball, shootPos1.position, Quaternion.identity);

        if (transform.localScale.x > 0)
            Ball.GetComponent<Rigidbody2D>().AddForce(shootPos1.right * ballSpeed, ForceMode2D.Impulse);
        else
            Ball.GetComponent<Rigidbody2D>().AddForce(-shootPos1.right * ballSpeed, ForceMode2D.Impulse);

        ammo--;

        canShoot = false;

        Invoke("ChangeShootState", shootTime);

        FindObjectOfType<GameManager>().CheckBalls();

        animator.SetTrigger("Shoot");

        Destroy(Ball, ballLifeTime);
    }


    void ChangeShootState()
    {
        canShoot = true;
    }

    bool IsMouseOnUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
}
