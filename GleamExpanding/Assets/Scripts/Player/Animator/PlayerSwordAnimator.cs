using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwordAnimator : MonoBehaviour, IPlayerModule
{
    [Header("Player Properties")]
    private PlayerProperties playerProperties;
    private bool isAttacking
    {
        get => playerProperties.isAttacking;
        set => playerProperties.isAttacking = value;
    }
    private bool isRunning
    {
        get => playerProperties.isRunning;
        set => playerProperties.isRunning = value;
    }


    [Header("Camera")]
    public CameraController cameraController;

    // Public Fields


    // Player Movement Blend Tree
    public string horizontalParamName = "Velocity X";
    public string verticalParamName = "Velocity Z";


    // Private Fields
    [Header("Animator Settings")]
    public Animator SwordAnimator;
    private PlayerMove playerMove;

    [Header("Attack Particle System Settings")]
    public AttackParticleEffectController attackParticleEffectController;

    // Combo Settings
    [Header("Combo Settings")]
    [SerializeField]
    private int comboCount = 0;
    [SerializeField]
    private bool canCombo = false;

  


    // Start Frame
    void Start()
    {
        // Get Components
        GetComponents();
    }




    // Handle Module: Void Update
    public void HandleModule()
    {
        // Handle Move
        HandlePlayerMove();

        // Handle Attack
        HandleAttack();
    }




    // Get Components
    private void GetComponents()
    {
        // Get Components
        playerProperties = GetComponent<PlayerProperties>();
        playerMove = GetComponent<PlayerMove>();


        // Debug: Log Error
        if (playerProperties == null)
        {
            Debug.LogError("Player Properties is not assigned!");
        }

        if (SwordAnimator == null)
        {
            Debug.LogError("Animator is not assigned!");
        }

        if (playerMove == null)
        {
            Debug.LogError("PlayerMove is missing!");
        }

        if (attackParticleEffectController == null)
        {
            Debug.LogError("SwordAttackParticleEffectController is missing!");
        }

        if(cameraController == null)
        {
            Debug.LogError("No Camera Controller assigned!");
        }
    }




    // Handle Attack
    private void HandleAttack()
    {
        // Handle Mouse Input
        HandleMouseInput();

        // Handle Key Input
        HandleKeyInput();
    }


    // Handle Mouse Input;
    private void HandleMouseInput()
    {
        // Mouse Left Press: Down
        if (Input.GetMouseButtonDown(0))
        {
            // Start Attack
            if (!SwordAnimator.GetBool("isAttacking"))
            {
                comboCount = 1;

                // Animator; Attack, isAttacking
                SwordAnimator.SetTrigger("Attack");
                SwordAnimator.SetBool("isAttacking", true);
                SwordAnimator.SetBool("isBlendTreeMove", false);
                SwordAnimator.SetInteger("comboValue", comboCount);

                // isAttacking
                isAttacking = true;
            }
            else if (canCombo) // Continue Attack
            {
                comboCount++; // Next
                SwordAnimator.SetInteger("comboValue", comboCount);
                canCombo = false;
            }
        }
    }

    // Handle Key Input
    private void HandleKeyInput()
    {
        // Magic Attack
        if (Input.GetKey(KeyCode.U) && !isAttacking && !canCombo && SwordAnimator.GetBool("isBlendTreeMove"))
        {
            // isAttacking
            isAttacking = true;
            SwordAnimator.SetBool("isAttacking", true);
            SwordAnimator.SetBool("isBlendTreeMove", false);
            SwordAnimator.SetTrigger("MagicAttack");
        }
    }




    // Animation Event
    public void AttackCameraShake()
    {

    }


    public void EnableCombo()
    {
        canCombo = true;
    }

    public void MagicParticleEffectStart()
    {
        // Start Effect

    }

    public void MagicParticleEffectEnd()
    {
        // End Effect

    }

    public void ComboParticleEffectStart()
    {
        // Start Effect
        attackParticleEffectController.PlayEffect();

        // Shake Camera
        cameraController.ShakeCamera();
    }

    public void ComboParticleEffectEnd()
    {
        // End Effect
        attackParticleEffectController.StopEffect();
    }


    // Start Slide
    public void PlayerStartSlide()
    {
        playerMove.PlayerForwardSlide();
    }


    // Attack Animation End
    public void OnAttackAnimationEnd()
    {
        // Finish Attack
        isAttacking = false;
        SwordAnimator.SetBool("isAttacking", false);
        SwordAnimator.SetBool("isBlendTreeMove", true);
        SwordAnimator.ResetTrigger("Attack");
        SwordAnimator.ResetTrigger("MagicAttack");

        canCombo = false;
        comboCount = 0;
        SwordAnimator.SetInteger("comboValue", comboCount);
    }





    // Handle Player Move
    private void HandlePlayerMove()
    {
        // Handle Player Run
        HandlePlayerRun();

        // Handle Player Move Blend Tree
        HandlePlayerMoveBlendTree();
    }


    // Handle Player Run
    private void HandlePlayerRun()
    {
        if(isRunning)
        {
            SwordAnimator.SetBool("isRunning", true);
        }
        else if (!isRunning)
        {
            SwordAnimator.SetBool("isRunning", false);
        }
    }


    // Handle Player Move Blend Tree
    private void HandlePlayerMoveBlendTree()
    {
        if (SwordAnimator == null) return;
        SwordAnimator.SetFloat(horizontalParamName, playerMove.currentXSpeed);
        SwordAnimator.SetFloat(verticalParamName, playerMove.currentZSpeed);
    }

}