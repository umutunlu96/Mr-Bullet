using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public float gravityScale = 2;

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "Bullet")
        {
            if (transform.childCount > 0)
            {
                transform.GetChild(0).transform.GetComponent<Rigidbody2D>().gravityScale = gravityScale;
                transform.GetChild(0).SetParent(null);
            }
        }
    }
}
