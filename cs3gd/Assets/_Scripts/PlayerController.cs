using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Runtime.InteropServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float groundDrag;
    public float rollDrag;
    public float playerHeight;
    public LayerMask ground;
    bool grounded;
    public Transform orientation;
    private float horizontalIn;
    private float verticalIn;
    private bool spacebar;
    private bool leftMouse;
    private Vector3 moveDirection;
    private Rigidbody rb;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // check if player is on the ground. If so, add drag.
    
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, ground);
        input();

        if (grounded)
        {
            rb.drag = groundDrag;
        } else {
            rb.drag = 0;
        }

        if (spacebar && !animator.GetCurrentAnimatorStateInfo(2).IsName("Roll") && animator.GetFloat("speed") > 0){
            gameObject.BroadcastMessage("roll");
        }

        if (leftMouse && !animator.GetCurrentAnimatorStateInfo(1).IsName("Attack")){
            gameObject.BroadcastMessage("attacking");
        }

        // Animate player
        animator.SetFloat("speed", Vector3.Magnitude(rb.velocity));
    }

    private void FixedUpdate(){
        if (grounded){
        movePlayer();
        }
    }

    private void input(){
        horizontalIn = Input.GetAxisRaw("Horizontal");
        verticalIn = Input.GetAxisRaw("Vertical");
        spacebar = Input.GetKeyDown("space");
        leftMouse = Input.GetKeyDown("mouse 0");
    }

    private void movePlayer(){
        // calculate move direction
        moveDirection = orientation.forward * verticalIn + orientation.right * horizontalIn;

        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
    }

    private void roll()
    {
        animator.SetTrigger("roll");
        rb.drag = rollDrag;
        moveDirection = orientation.forward * verticalIn + orientation.right * horizontalIn;
        rb.AddForce(moveDirection.normalized * moveSpeed * 100f);
    }

    private void attacking()
    {
        rb.velocity = Vector3.zero;
        animator.SetTrigger("attack");
    }
}
