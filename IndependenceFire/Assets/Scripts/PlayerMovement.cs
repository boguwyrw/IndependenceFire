using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody playerRigidbody;
    [SerializeField] private float movementSpeed = 4.0f;

    private PlayerInput playerInput = null;
    private Vector2 moveVector = Vector2.zero;

    private void Awake()
    {
        playerInput = new PlayerInput();
    }

    private void Update()
    {
        Vector3 movement = new Vector3(moveVector.x, 0.0f, moveVector.y);
        transform.Translate(movement * movementSpeed * Time.deltaTime);
    }

    private void OnEnable()
    {
        playerInput.Enable();
        playerInput.Player.Movement.performed += OnMovementPerformed;
        playerInput.Player.Movement.canceled += OnMovementCancelled;
    }

    private void OnDisable()
    {
        playerInput.Disable();
        playerInput.Player.Movement.performed -= OnMovementPerformed;
        playerInput.Player.Movement.canceled -= OnMovementCancelled;
    }

    private void OnMovementPerformed(InputAction.CallbackContext context)
    {
        moveVector = context.ReadValue<Vector2>();
    }

    private void OnMovementCancelled(InputAction.CallbackContext context)
    {
        moveVector = Vector2.zero;
    }
}
