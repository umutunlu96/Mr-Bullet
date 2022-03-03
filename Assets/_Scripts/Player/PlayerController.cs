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
    public GameObject shirken;
    //private GameObject crossHair;

    [SerializeField]
    private float shirkenLifeTime = 2;

    private Animator animator;
    public AudioClip throwSound;

    void Awake()
    {
        //crossHair = GameObject.Find("CrossHair");
        //crossHair.SetActive(false);
        handPos = GameObject.Find("RightArm").transform;
        firePos1 = GameObject.Find("FirePos1").transform;
        firePos2 = GameObject.Find("FirePos2").transform;
        lineRenderer = GameObject.Find("Shiriken").GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
    }
    private void Start()
    {
        //start
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
                    Shoot();
                else
                {
                    lineRenderer.enabled = false;
                    //crossHair.SetActive(false);
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
        
        //crossHair.SetActive(true);
        //crossHair.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + (Vector3.forward * 10);
    }


    void Shoot()
    {
        lineRenderer.enabled = false;
        //crossHair.SetActive(false);

        GameObject shirken = Instantiate(this.shirken, firePos1.position, Quaternion.identity);

        if (transform.localScale.x > 0)
            shirken.GetComponent<Rigidbody2D>().AddForce(firePos1.right * bullSpeed, ForceMode2D.Impulse);
        else
            shirken.GetComponent<Rigidbody2D>().AddForce(-firePos1.right * bullSpeed, ForceMode2D.Impulse);

        ammo--;

        FindObjectOfType<GameManager>().CheckBullets();

        SoundManager.instance.PlaySoundFX(throwSound, .3f);

        Destroy(shirken, shirkenLifeTime);
    }

    bool IsMouseOnUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
}
