using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : CharBase, IObserver
{
    [Header("Movement Settings")]
    public float walkSpeed = 2.5f;
    public float jumpHeight = 5f;
    public float maxSpeed = 500f;


    [Header("Ground Check")]
    public Transform groundCheckTransform;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundMask;

    [Header("Targeting & Shooting")]
    public Transform targetTransform;
    public LayerMask mouseAimMask;
    public GameObject bulletPrefab;
    public Transform muzzleTransform;

    [Header("Recoil & Effects")]
    public AnimationCurve recoilCurve;
    public float recoilDuration = 0.25f;
    public float recoilMaxRotation = 45f;
    public AudioClip gunShot, reloadSFX;
    public Transform rightLowerArm;
    public Text bulletText, healthText;
    public GameObject ragdollPrefab;

    [Header("Bullet Ammunition")]
    public int fullMagazine = 5;
    private int currentMagazine;

    
    
    private float walkSpeedRelativeToLevel;
    private Animator animator;
    private Rigidbody rbody;
    private bool isGrounded;
    private Camera mainCamera;
    private float recoilTimer;
    private InputManager inputManager; 
    private bool isReloading = false;



    void Start()
    {
        base.Start();
        inputManager = InputManager.Instance;
        animator = GetComponent<Animator>();
        rbody = GetComponent<Rigidbody>();
        mainCamera = Camera.main;

        currentMagazine = fullMagazine;
        ScrollSpeedUp(0);
        GameplayController.instance.RegisterObserver(this);
        UpdateHealthUI();
        UpdateBulletUI();
    }
    void Update()
    {

        Ray ray = mainCamera.ScreenPointToRay(inputManager.GetMousePos());

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, mouseAimMask))
        {
            targetTransform.position = hit.point;
        }

        if (inputManager.GetReloadingAction())
        {
            StartCoroutine(Reload());
        }

        if (inputManager.GetShottingAction() && currentMagazine > 0 && !isReloading)
        {
            currentMagazine--;
            UpdateBulletUI();
            Fire();
        }
    }
    private void Fire()
    {
        recoilTimer = Time.time;
        if (GameRef.GetMusicState() == 0)
        {
            AudioSource.PlayClipAtPoint(gunShot, mainCamera.transform.position);
        }
        var bulletObject = Instantiate(bulletPrefab, muzzleTransform.position, muzzleTransform.rotation);
        var bullet = bulletObject.GetComponent<Bullet>();
        bullet.Fire(bulletObject.transform.position, targetTransform.position, damage);
    }
    IEnumerator Reload()
    {
        if (isReloading) yield break;
        if (GameRef.GetMusicState() == 0)
        {
            AudioSource.PlayClipAtPoint(reloadSFX, mainCamera.transform.position);
        }
        yield return new WaitForSeconds(.5f);
        currentMagazine = fullMagazine;
        UpdateBulletUI();
        isReloading = false;
    }
    private void LateUpdate()
    {
        // Recoil Animation
        if (recoilTimer < 0)
        {
            return;
        }

        float curveTime = (Time.time - recoilTimer) / recoilDuration;
        if (curveTime > 1f)
        {
            recoilTimer = -1;
        }
        else
        {
            rightLowerArm.Rotate(Vector3.forward, recoilCurve.Evaluate(curveTime) * recoilMaxRotation, Space.Self);
        }
    }
    private void FixedUpdate()
    {
        // Movement
        rbody.velocity = new Vector3(walkSpeedRelativeToLevel * Time.deltaTime, rbody.velocity.y, 0);

        // Facing Rotation
        rbody.MoveRotation(Quaternion.Euler(new Vector3(0, 90 * Mathf.Sign(targetTransform.position.x - transform.position.x), 0)));

        // Ground Check
        isGrounded = Physics.CheckSphere(groundCheckTransform.position, groundCheckRadius, groundMask, QueryTriggerInteraction.Ignore);
        animator.SetBool("isGrounded", isGrounded);

        if (inputManager.GetPlayerMovement().y > 0.1 && isGrounded)
        {
            rbody.velocity = new Vector3(rbody.velocity.x, 0, 0);

            rbody.AddForce(Vector3.up * Mathf.Sqrt(jumpHeight * -1 * Physics.gravity.y), ForceMode.Impulse);
        }
        else if (inputManager.GetPlayerMovement().y < -0.1)
        {
            animator.SetBool("isCrouching", true);
            rbody.velocity = new Vector3(rbody.velocity.x, 0, 0);
            if(!isGrounded)
            {
                rbody.AddForce(Vector3.down * Mathf.Sqrt(jumpHeight * -1 * Physics.gravity.y), ForceMode.Impulse);
            }
        }
        else
        {
            animator.SetBool("isCrouching", false);
        }
    }
    public override void UpdateHealthUI()
    {
        heath_UI.fillAmount = health / fullHealth;
        healthText.text = health +"/"+ fullHealth;
    }
    private void ScrollSpeedUp(int difficultyLevel)
    {
        walkSpeedRelativeToLevel = walkSpeed + difficultyLevel * ConstantsValue.WALKSPEEDGROWPERWEIGHT;
    }
    private void UpdateBulletUI()
    {
        bulletText.text = currentMagazine + "/" + fullMagazine;
    }
    private void OnAnimatorIK()
    {
        if (Time.timeScale == 0)
        {
            // Disable IK by setting weights to zero
            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0);
            animator.SetLookAtWeight(0);
            return;
        }
        // Weapon Aim at Target IK
        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
        animator.SetIKPosition(AvatarIKGoal.RightHand, targetTransform.position);
        animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0.5f);
        animator.SetIKPosition(AvatarIKGoal.LeftHand, targetTransform.position);

        // Look at target IK
        animator.SetLookAtWeight(1);
        animator.SetLookAtPosition(targetTransform.position);
    }

    public void Heal(int value)
    {
        if (health + value > fullHealth)
        {
            health = fullHealth;
        }
        else
        {
            health += value;
        }
        UpdateHealthUI();
    }
    public void HeartUp(int value)
    {
        fullHealth += value;
        health += value;
        UpdateHealthUI();
    }
    public void PowerUp(int value)
    {
        damage += value;
    }
    public void BulletUp()
    {
        fullMagazine += 1;
        UpdateBulletUI();
    }

    public override void OnDie()
    {
        gameObject.SetActive(false);
        GameObject ragdoll = Instantiate(ragdollPrefab, transform.position, transform.rotation);
        Rigidbody[] ragdollRigidbodies = ragdoll.GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in ragdollRigidbodies)
        {
            rb.velocity = rbody.velocity;
        }
        GameplayController.instance.GameOver();
    }

    public void OnNotify(string eventType, object parameter)
    {
        if (eventType == "DifficultyLevelUpNotify")
        {
            ScrollSpeedUp((int)parameter);
        }
    }
    private void OnDisable()
    {
        GameplayController.instance.UnregisterObserver(this);
    }
}
