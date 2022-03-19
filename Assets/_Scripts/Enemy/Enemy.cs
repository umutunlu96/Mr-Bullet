using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public AudioClip deathSound;
    private bool isLiving = true;

    public void Death()
    {
        gameObject.tag = "Untagged";

        FindObjectOfType<GameManager>().CheckEnemyCount();

        SoundManager.instance.PlaySoundFX(deathSound, .2f);
        foreach (Transform obj in transform)
        {
            obj.GetComponent<Rigidbody2D>().gravityScale = 5;
        }

        isLiving = false;
    }


    private void OnTriggerEnter2D(Collider2D target)
    {
        Vector2 direction = transform.position - target.transform.position;

        if (target.tag == "Shirken")
        {

            if(transform.GetChild(0).GetComponent<Rigidbody2D>().gravityScale <1 && isLiving)
                Death();
            GetComponent<Rigidbody2D>().AddForce(new Vector2((direction.x > 0 ? 1 : -1) * .5f, direction.y > 0 ? .3f : -.3f),
                ForceMode2D.Impulse);
        }

        if ((target.tag == "Plank" || target.tag == "BoxPlank") && isLiving)
        {
            //if (target.GetComponent<Rigidbody2D>().mass > 4f)
            Death();
        }

        if (target.tag == "Tnt")
        {
            Death();
        }

        if (target.tag == "Ground" && isLiving)
        {
            if (GetComponent<Rigidbody2D>().velocity.magnitude > .2f)
                Death();
        }
    }

}
