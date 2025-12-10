using System;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed;

    public float jumpForce;
    public float jumpCooldown;
    public float airMult;
    private bool readyToJump = true;
     
    public KeyCode jumpKey = KeyCode.Space;
    
    public float groundDrag;
    public float playerHeight;
    public LayerMask groundMask;
    private bool grounded;
    
    public Transform orientation;
    
    float horizontalInput;
    float verticalInput;

    private Vector3 moveDir;
    
    Rigidbody rb;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();    
        rb.freezeRotation = true;

    }

    void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            Debug.Log("Jump");
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        } 
    }
    
    // Update is called once per frame
    void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight *0.5f + 0.2f, groundMask);
        
        MyInput();
        SpeedControl();
        
        if (grounded) rb.linearDamping = groundDrag;
        else rb.linearDamping = 0;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        moveDir = orientation.forward * verticalInput + orientation.right * horizontalInput;
        
        if (grounded) rb.AddForce(moveDir.normalized * (moveSpeed * 10f), ForceMode.Force);
        else if (!grounded) rb.AddForce(moveDir.normalized * (moveSpeed * 10f * airMult), ForceMode.Force);
    }

    void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);

        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
        }
    }

    void Jump()
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
        
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    void ResetJump()
    {
        readyToJump = true;
    }
}
