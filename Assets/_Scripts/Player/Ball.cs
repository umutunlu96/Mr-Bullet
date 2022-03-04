using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public AudioClip boxHit;

    private GameManager gameManager;
    private bool stopBall;
    
    private void Awake()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }


    private void Update()
    {
        if(stopBall)
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }


    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "Kale")
        {
            GetComponent<CircleCollider2D>().sharedMaterial = null;
        }
    }



    private void OnCollisionEnter2D(Collision2D target)
    {

        //if (target.gameObject.tag == "Box")
        //{
        //    SoundManager.instance.PlaySoundFX(boxHit, .5f);
        //    Destroy(target.gameObject);
        //}

        if (target.gameObject.tag == "BackKale")
        {
            GetComponent<Rigidbody2D>().gravityScale = 5;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }


        if (target.gameObject.name == "Bottom" && GetComponent<Rigidbody2D>().gravityScale == 5)
        {
            stopBall = true;
        }

    }
}
