using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boxes : MonoBehaviour
{
    private string childTag;
    private int childCount;

    [System.Obsolete]
    void Awake()
    {
        childCount = gameObject.transform.GetChildCount();

        for (int i = 0; i < childCount; i++)
        {
            if(gameObject.transform.GetChild(i).gameObject.activeInHierarchy)
                childTag = gameObject.transform.GetChild(i).gameObject.tag;
        }

        gameObject.tag = childTag;

    }
}
