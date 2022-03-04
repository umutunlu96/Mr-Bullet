using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kale : MonoBehaviour
{
    public AudioClip goalSound;

    public void Death()
    {
        gameObject.tag = "Untagged";

        FindObjectOfType<GameManager>().CheckKaleCount();

        SoundManager.instance.PlaySoundFX(goalSound, .5f);
    }


    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "Ball")
        {
            Death();
        }
    }
}
