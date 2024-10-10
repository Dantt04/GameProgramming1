using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 1f;
    public float rotationSpeed = 1f;
    private Vector2 movementValue;
    private float lookValue;

    private Rigidbody rb;

    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        rb.AddRelativeForce(movementValue.x,0,movementValue.y);
        rb.AddRelativeTorque(0, lookValue*Time.deltaTime, 0);
    }

    public void OnMove(InputValue value )
    {
        movementValue = value.Get<Vector2>()*speed;
    }

    public void OnLook(InputValue value)
    {
        lookValue = value.Get<Vector2>().x*rotationSpeed;
    }

}
