using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shirken : MonoBehaviour
{
    private float speed = 10;

    public AudioClip boxHit;
    public AudioClip plankHit;
    public AudioClip groundHit;
    public AudioClip explodeHit;

    private GameObject[] trails = new GameObject[4];

    private void Awake()
    {
        for (int i = 0; i < 4; i++)
        {
            trails[i] = transform.GetChild(i).gameObject;
        }
    }

    void LateUpdate()
    {
        transform.Rotate(0, 0, speed, Space.Self);

        foreach (var trail in trails)
        {
            trail.transform.Rotate(0, 0, -speed, Space.Self);
        }

    }



    private void OnCollisionEnter2D(Collision2D target)
    {
        //if (target.gameObject.tag == "Box")
        //{
        //    SoundManager.instance.PlaySoundFX(boxHit,.5f);
        //    Destroy(target.gameObject);
        //}

        //if (target.gameObject.tag == "Plank")
        //{
        //    SoundManager.instance.PlaySoundFX(plankHit, .5f);
        //}

        //if (target.gameObject.tag == "Ground")
        //{
        //    SoundManager.instance.PlaySoundFX(groundHit, .5f);
        //}

        //if (target.gameObject.tag == "Tnt")
        //{
        //    SoundManager.instance.PlaySoundFX(explodeHit, .5f);
        //}
    }
}
