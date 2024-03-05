using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Runtime.InteropServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float groundDrag;
    public float playerHeight;
    public LayerMask ground;
    bool grounded;
    public Transform orientation;
    private float horizontalIn;
    private float verticalIn;
    private Vector3 moveDirection;
    private Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, ground);
        input();

        if (grounded)
        {
            rb.drag = groundDrag;
        } else {
            rb.drag = 0;
        }
    }

    private void FixedUpdate(){
        movePlayer();
    }

    private void input(){
        horizontalIn = Input.GetAxisRaw("Horizontal");
        verticalIn = Input.GetAxisRaw("Vertical");
    }

    private void movePlayer(){
        // calculate move direction
        moveDirection = orientation.forward * verticalIn + orientation.right * horizontalIn;

        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);


    }
}