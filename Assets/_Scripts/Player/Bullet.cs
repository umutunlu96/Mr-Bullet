using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public AudioClip boxHit;
    public AudioClip plankHit;
    public AudioClip groundHit;
    public AudioClip explodeHit;

    private void OnCollisionEnter2D(Collision2D target)
    {
        if (target.gameObject.tag == "Box")
        {
            SoundManager.instance.PlaySoundFX(boxHit,.5f);
            Destroy(target.gameObject);
        }

        if (target.gameObject.tag == "Plank")
        {
            SoundManager.instance.PlaySoundFX(plankHit, .5f);
        }

        if (target.gameObject.tag == "Ground")
        {
            SoundManager.instance.PlaySoundFX(groundHit, .5f);
        }

        if (target.gameObject.tag == "Tnt")
        {
            SoundManager.instance.PlaySoundFX(explodeHit, .5f);
        }
    }
}
