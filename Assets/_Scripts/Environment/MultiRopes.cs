using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiRopes : MonoBehaviour
{
    private Transform[] ropes;
    private int ropeCount;

    private void Awake()
    {
        foreach (Transform rope in ropes)
        {
            ropeCount++;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }



}
