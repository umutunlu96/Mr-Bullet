using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public float gravityScale = 2;
    public float maxRotateAngle = 10;
    public float rotateSpeed = 2;
    public bool rotate;
    public bool rotateRight = true;

    private Transform rotateAround;

    private void Awake()
    {
        rotateAround = transform.GetChild(0).GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        if (rotate)
        {
            print(transform.rotation.eulerAngles.z);

            if (rotateRight)
            {
                transform.RotateAround(rotateAround.position, Vector3.forward, rotateSpeed * Time.fixedDeltaTime);
                print("RR");

                if (transform.rotation.eulerAngles.z >= maxRotateAngle)
                    rotateRight = false;
            }
            else
            {
                transform.RotateAround(rotateAround.position, Vector3.back, rotateSpeed * Time.fixedDeltaTime);
                print("RL");

                if (transform.rotation.eulerAngles.z <= 360 - maxRotateAngle && transform.rotation.eulerAngles.z >= maxRotateAngle)
                {
                    rotateRight = true;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "Bullet")
        {
            transform.GetChild(1).transform.GetComponent<Rigidbody2D>().gravityScale = gravityScale;
        }
    }
}
