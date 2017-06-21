using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 10.0f;

    public float gravity = -9.8f;
    public float jumpForce = 10.0f;

    private Vector3 moveVector;

    private CharacterController controller;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        Vector2 inputVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if(inputVector.magnitude > 1)
            inputVector.Normalize();

        if (controller)
        {
            Vector3 forward = Camera.main.transform.forward;
            forward.y = 0;
            forward.Normalize();

            Vector3 right = Camera.main.transform.right;
            right.y = 0;
            right.Normalize();

            moveVector.x = 0;
            moveVector.z = 0;

            moveVector += right * inputVector.x * moveSpeed;
            moveVector += forward * inputVector.y * moveSpeed;

            if (Input.GetButtonDown("Jump") && controller.isGrounded)
                moveVector.y = jumpForce;

            if (!controller.isGrounded)
                moveVector.y += gravity * Time.deltaTime;
            else if (moveVector.y <= 0)
                moveVector.y = 0;

            controller.Move(moveVector * Time.deltaTime);
        }
    }
}
