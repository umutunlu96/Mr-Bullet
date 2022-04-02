using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    private GameObject parent;
    private void Start()
    {
        parent = transform.parent.gameObject;
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.gameObject.tag == "Shirken")
        {
            transform.parent.GetComponent<DistanceJoint2D>().enabled = false;
            //transform.GetComponent<HingeJoint2D>().enabled = false;

            foreach (var component in GetComponents<HingeJoint2D>())
            {
                component.enabled = false;
            }

            for (int i = 0; i < parent.transform.childCount; i++)
            {
                if (parent.transform.GetChild(i).tag != "Tnt")
                {
                    parent.transform.GetChild(i).GetComponent<Rigidbody2D>().mass = 50;
                    parent.transform.GetChild(i).GetComponent<CapsuleCollider2D>().isTrigger = false;
                }
            }

        }
    }
}
