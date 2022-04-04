using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuAnim : MonoBehaviour
{

    private Animator animator;
    private Animation anims;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        anims = GetComponent<Animation>();
    }

    public void IdleAnimEndEvents()
    {
        PickOtherClip();
    }


    private void PickOtherClip()
    {
        int random = Random.Range(0, 3);

        switch (random)
        {
            case 0:
                break;
            case 1:
                animator.SetTrigger("ThrowUp");
                break;
            case 2:
                animator.SetTrigger("Throw");
                break;
        }
    }

}