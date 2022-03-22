using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class CameraScaler : MonoBehaviour
{
    public SpriteRenderer background;

    private void Update()
    {
        if (background == null)
        {
            background = GameObject.Find("CameraScale").GetComponent<SpriteRenderer>();
        }
        float ortoSize = background.bounds.size.x * Screen.height / Screen.width * .5f;
        Camera.main.orthographicSize = ortoSize;
    }
}
