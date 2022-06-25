using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerGravityState
{
    NotFalling,
    Falling,
}
public enum PlayerActionState
{
    Idle,
    Sprinting,
    Walking,
    Damaged,
}

public class PlayerController : MonoBehaviour
{
    [Header("View")]
    [SerializeField] private Camera _playerCamera;
    private float yRotation = 0;
    private float xRotation = 0;

    [Header("Stats")]
    [SerializeField] public PlayerStats _playerStats;

    [Header("Movement")]
    [SerializeField] float _sprintMultiply = 1.5f;
    [SerializeField] private CharacterController _characterController;
    private float _velocity = 0;
    private Vector3 move;

    [Header("Physics")]
    [SerializeField] float _gravity = 9.81f;

    // public to get from PlayerAnimations
    [Header("Player States")]
    public PlayerGravityState playerGravityState = PlayerGravityState.NotFalling;
    public PlayerActionState playerActionState = PlayerActionState.Idle;

    [Header("Roof Detecting")]
    [SerializeField] LayerMask layer;

    private void Start()
    {
        GlobalFields.Player = transform.gameObject;
        Cursor.lockState = CursorLockMode.Locked;

        _playerStats.Sensitivity_x = GlobalFields.sensitivity;
        _playerStats.Sensitivity_y = GlobalFields.sensitivity;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && playerGravityState == PlayerGravityState.NotFalling)
        {
            Jump();
        }
        if (Input.GetKey(KeyCode.LeftShift) && (Input.GetAxis("Vertical")!=0 || Input.GetAxis("Horizontal")!=0)) // in order to don't sprint on place
        {
            //                          (Player State) SPRINTING
            playerActionState = PlayerActionState.Sprinting;
            Sprint(_sprintMultiply);
        } else
        {
            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                //                          (Player State) WALKING
                playerActionState = PlayerActionState.Walking;
            }
            else
            {
                //                          (Player State) IDLE
                playerActionState = PlayerActionState.Idle;
            }
            Move();
        }

        GravityCalc();
        Rotation();
        RoofDetecting();
    }

    private void Move()
    {
        move = transform.right * Input.GetAxis("Horizontal") + transform.forward * Input.GetAxis("Vertical");
        move *= _playerStats.Movement_speed;
        move.y = _velocity;
        move *= Time.deltaTime;

        _characterController.Move(move);
    }
    private void Rotation()
    {
        yRotation += Input.GetAxis("Mouse X") * _playerStats.Sensitivity_x;
        xRotation -= Input.GetAxis("Mouse Y") * _playerStats.Sensitivity_y;

        xRotation = Mathf.Clamp(xRotation, -90, 90);
        _playerCamera.transform.localRotation = Quaternion.Euler(xRotation,0,0);
        transform.localRotation = Quaternion.Euler(0, yRotation, 0);
    }
    private void GravityCalc()
    {
        if (!_characterController.isGrounded)
        {
            if (playerGravityState != PlayerGravityState.Falling)
            {
                if (_velocity < 0)
                {
                    _velocity = 0;
                }
                playerGravityState = PlayerGravityState.Falling;
            }

            _velocity -= _gravity * Time.deltaTime;
        } else
        {
            playerGravityState = PlayerGravityState.NotFalling;
        }
    }
    private void Jump()
    {
        _velocity = _playerStats.Jump_force;
    }
    private void Sprint(float multiply)
    {
        float aux = _playerStats.Movement_speed;
        _playerStats.Movement_speed *= multiply;
        Move();
        _playerStats.Movement_speed = aux;
    }

    private void RoofDetecting()
    {
        Ray ray = new Ray(Camera.main.transform.position, Vector3.up);
        if (Physics.Raycast(ray,0.3f,layer))
        {
            if (_velocity > 0)
            {
                _velocity = 0;
            }
        }
    }
}
