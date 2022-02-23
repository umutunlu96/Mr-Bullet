using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public float rotateSpeed = 100, bullSpeed = 100;
    public int ammo = 4;
    private Transform handPos;
    private Transform firePos1, firePos2;
    private LineRenderer lineRenderer;
    public GameObject bullet;
    private GameObject crossHair;
    private GameObject ball;
    private Transform shootPos;

    public AudioClip gunShot;

    void Awake()
    {
        crossHair = GameObject.Find("CrossHair");
        crossHair.SetActive(false);
        handPos = GameObject.Find("RightLeg").transform;
        firePos1 = GameObject.Find("FirePos1").transform;
        firePos2 = GameObject.Find("FirePos2").transform;
        shootPos = GameObject.Find("ShootPos").transform;
        ball = GameObject.Find("ball");
        lineRenderer = GameObject.Find("Gun").GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
    }
    private void Start()
    {
        
    }

    void Update()
    {
        if (!IsMouseOnUI())
        {
            if (Input.GetMouseButton(0))
            {
                Aim();
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (ammo > 0)
                {
                    handPos.gameObject.GetComponent<Animator>().SetTrigger("Shoot");
                    Shoot();
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
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - handPos.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        handPos.rotation = Quaternion.Slerp(transform.rotation, rotation, rotateSpeed);

        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, firePos1.position);
        lineRenderer.SetPosition(1, firePos2.position);
        
        crossHair.SetActive(true);
        crossHair.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + (Vector3.forward * 10);
    }


    void Shoot()
    {
        lineRenderer.enabled = false;
        crossHair.SetActive(false);

        //GameObject bullet = Instantiate(this.bullet, firePos1.position, Quaternion.identity);

        if (transform.localScale.x > 0)
            ball.GetComponent<Rigidbody2D>().AddForce(shootPos.right * bullSpeed, ForceMode2D.Impulse);
        else
            ball.GetComponent<Rigidbody2D>().AddForce(-shootPos.right * bullSpeed, ForceMode2D.Impulse);

        ammo--;

        FindObjectOfType<GameManager>().CheckBullets();

        //SoundManager.instance.PlaySoundFX(gunShot,.3f);

        //Destroy(bullet,2);
    }

    bool IsMouseOnUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

















}
