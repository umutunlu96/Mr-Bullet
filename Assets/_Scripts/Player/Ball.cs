using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public AudioClip boxHit;

    private GameManager gameManager;

    public bool isGoal;

    private void Awake()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "Goal")
        {
            GetComponent<CircleCollider2D>().sharedMaterial = null;
            gameManager.gameOver = true;
            isGoal = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D target)
    {

        if (target.gameObject.tag == "Box")
        {
            SoundManager.instance.PlaySoundFX(boxHit, .5f);
            Destroy(target.gameObject);
        }

        if (target.gameObject.tag == "GoalPost" && gameManager.gameOver)
        {
            GetComponent<Rigidbody2D>().gravityScale = 5;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }
}
