using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chain : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D target)
    {
        if (target.gameObject.tag == "Shirken")
        {
            GameObject parent = transform.parent.gameObject;
            
            transform.parent.GetComponent<DistanceJoint2D>().enabled = false;
            transform.GetComponent<HingeJoint2D>().enabled = false;

            for (int i = 0; i < parent.transform.childCount; i++)
            {
                parent.transform.GetChild(i).GetComponent<Rigidbody2D>().mass = 50;
            }

        }
    }
}
