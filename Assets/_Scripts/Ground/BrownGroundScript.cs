using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrownGroundScript : MonoBehaviour
{
    public AudioClip groundSound;
    void Start()
    {
        
    }

    // Update is called once per frame
    private void OnCollisionExit2D(Collision2D collision)
    {
        
    }
    void Update()
    {
        
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            SoundManager.instance.PlaySoundFX(groundSound, .5f);
        }
    }
 
}
