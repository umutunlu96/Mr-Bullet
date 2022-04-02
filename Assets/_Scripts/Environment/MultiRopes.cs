using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiRopes : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "Shirken")
        {
            target.gameObject.GetComponent<CircleCollider2D>().isTrigger = enabled;
        }
    }
}
