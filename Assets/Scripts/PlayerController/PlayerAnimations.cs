using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] PlayerController _controller;
    [SerializeField] PlayerAttack _attackController;
    [SerializeField] WeaponStats weaponStats;

    [Header("Camera")]
    [SerializeField] float walkFieldView;
    [SerializeField] float sprintFieldView;

    [Header("Weapon")]
    [SerializeField] GameObject weapon;
    [SerializeField] float swayMultiplier;
    [SerializeField] float weaponSmooth;

    [Header("Reloading")]
    [SerializeField] AudioSource reloadingSource;

    // Useful fields
    bool walking = false;
    bool sprinting = false;

    private void Update()
    {
        CameraFieldView();

        if (Input.GetKeyDown(KeyCode.R) && _attackController.playerAttackState == PlayerAttackState.Idle)
        {
            ReloadingAnimation();
        }

        if (_attackController.playerAttackState != PlayerAttackState.Reloading)
        {
            Conditional_WeaponShake();
            WeaponSmooth(swayMultiplier, weaponSmooth);
        }
    }
    // weapon shake
    private void Conditional_WeaponShake()
    {
        // check if walking
        if (_controller.playerActionState == PlayerActionState.Walking && !walking)
        {
            walking = true;
            WeaponWalkingShake();
        }
        if (_controller.playerActionState != PlayerActionState.Walking)
        {
            walking = false;
        }
        // check if sprinting
        if (_controller.playerActionState == PlayerActionState.Sprinting && !sprinting)
        {
            sprinting = true;
            WeaponSprintShake();
        }
        if (_controller.playerActionState != PlayerActionState.Sprinting)
        {
            sprinting = false;
        }
    }
    private void WeaponWalkingShake()
    {
        if (walking)
        {
            weapon.transform.DOShakeRotation(0.3f, 1f, 0).onComplete += WeaponWalkingShake;
        }
    }
    private void WeaponSprintShake()
    {
        if (sprinting)
        {
            weapon.transform.DOShakeRotation(0.3f, 2f, 0).onComplete = WeaponSprintShake;
        }
    }

    // weapon smooth
    private void WeaponSmooth(float swayMultiplier,float smooth)
    {
        float mouseX = Input.GetAxis("Mouse X") * swayMultiplier;
        float mouseY = Input.GetAxis("Mouse Y") * swayMultiplier;

        // calculate target rotation
        Quaternion rotationX = Quaternion.AngleAxis(-mouseY, Vector3.right);
        Quaternion rotationY = Quaternion.AngleAxis(mouseX, Vector3.up);

        Quaternion targetRotation = rotationX * rotationY;

        //rotate
        weapon.transform.localRotation = Quaternion.Slerp(weapon.transform.localRotation, targetRotation, smooth * Time.deltaTime);
    }
    // on Reloading
    private void ReloadingSound()
    {
        reloadingSource.Play();
    }
    private void ReloadingAnimation()
    {
        ReloadingSound();
        weapon.transform.DOKill();
        weapon.transform.DOShakeRotation(weaponStats.reload_time, 20).OnComplete(() =>
        {
            sprinting = false;
            walking = false;
        });
    }

    private void CameraFieldView()
    {
        if (_controller.playerActionState == PlayerActionState.Walking)
        {
            Camera.main.DOFieldOfView(walkFieldView, 0.5f);
        }
        if (_controller.playerActionState == PlayerActionState.Sprinting)
        {
            Camera.main.DOFieldOfView(sprintFieldView, 0.5f);
        }
        if (_controller.playerActionState == PlayerActionState.Idle)
        {
            Camera.main.DOFieldOfView(60, 0.5f);
        }
    }
}
