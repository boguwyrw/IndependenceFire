using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRotation : MonoBehaviour
{
    [SerializeField] private Transform playerBody;

    private PlayerInput playerInput = null;
    private float verticalRotation = 0.0f;
    private float verticalRot = 0.0f;
    private float horizontalRotation = 0.0f;
    private float headSensitivity = 25.0f;
    private float bodySensitivity = 0.40f;
    private float headUpBoundaries = -85.0f;
    private float headDownBoundaries = 75.0f;

    private void Awake()
    {
        playerInput = new PlayerInput();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        Rotation();
    }

    private void OnEnable()
    {
        playerInput.Enable();
        playerInput.Player.BodyRotation.performed += OnBodyRotationPerformed;
        playerInput.Player.HeadRotation.performed += OnHeadRotationPerformed;

        playerInput.Player.BodyRotation.canceled += OnBodyRotationCancelled;
        playerInput.Player.HeadRotation.canceled += OnHeadRotationCancelled;
    }

    private void OnDisable()
    {
        playerInput.Disable();
        playerInput.Player.BodyRotation.performed -= OnBodyRotationPerformed;
        playerInput.Player.HeadRotation.performed -= OnHeadRotationPerformed;

        playerInput.Player.BodyRotation.canceled -= OnBodyRotationCancelled;
        playerInput.Player.HeadRotation.canceled -= OnHeadRotationCancelled;
    }

    private void OnBodyRotationPerformed(InputAction.CallbackContext context)
    {
        horizontalRotation = context.ReadValue<float>() * bodySensitivity;
    }

    private void OnHeadRotationPerformed(InputAction.CallbackContext context)
    {
        verticalRot = context.ReadValue<float>() * Time.deltaTime * headSensitivity;
    }

    private void OnBodyRotationCancelled(InputAction.CallbackContext context)
    {
        horizontalRotation = 0.0f;
    }

    private void OnHeadRotationCancelled(InputAction.CallbackContext context)
    {
        verticalRot = 0.0f;
    }

    private void Rotation()
    {
        playerBody.Rotate(Vector3.up * horizontalRotation);
        verticalRotation -= verticalRot;
        verticalRotation = Mathf.Clamp(verticalRotation, headUpBoundaries, headDownBoundaries);
        transform.localRotation = Quaternion.Euler(verticalRotation, 0.0f, 0.0f);
    }
}
