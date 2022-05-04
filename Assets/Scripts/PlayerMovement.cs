using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float vx = 0f;
    Rigidbody rb;
    float moveSpeed = 5f;
    float jumpForce = 5f;
    public bool grounded { get; set; }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            vx = -1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            vx = 1;
        }
        else
        {
            vx = 0;
        }

        if (Input.GetKeyDown(KeyCode.W) && grounded)
        {
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
        }

        rb.velocity = new Vector3(vx * moveSpeed, rb.velocity.y, 0);
    }
}
