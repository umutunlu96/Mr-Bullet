using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public AudioClip boxHit;
    public AudioClip groundHit;

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

        if (target.gameObject.tag == "Box")
        {
            SoundManager.instance.PlaySoundFX(boxHit, .3f);
        }

        if (target.gameObject.tag == "Ground")
        {
            SoundManager.instance.PlaySoundFX(groundHit, .2f);
        }        
        
        if (target.gameObject.tag == "BackKale")
        {
            SoundManager.instance.PlaySoundFX(groundHit, .2f);
            GetComponent<Rigidbody2D>().gravityScale = 5;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }


        if (target.gameObject.name == "Bottom" && GetComponent<Rigidbody2D>().gravityScale == 5)
        {
            stopBall = true;
        }

    }
}
