using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Physics_Float : MonoBehaviour
{
    public Vector3 force;
    Rigidbody rb;
    public Vector3 mousePos;
    public float speed;




    void Awake()
    {
        speed = 16;
        rb = GetComponent<Rigidbody>();

    }

    private void Update()
    {
        mousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        //mousePos -= new Vector3(0.5f, 0.5f, 0.0f); // from center
        force = new Vector3(0, speed * mousePos.y, 0);
    }

    void FixedUpdate()
    {
        //rb.AddForce(force);
    }
}
