using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRifleAnimator : MonoBehaviour, IPlayerModule
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


    // Public Fields


    // Player Movement Blend Tree
    public string horizontalParamName = "Velocity X";
    public string verticalParamName = "Velocity Z";

    // Attack Settings
    [Header("Attack Settings")]
    public float resetTime = 1.0f;
    public float speedIncrement = 0.2f;
    public float maxSpeed = 2.0f;

    // Private Fields
    [Header("Animator Settings")]
    public Animator RifleAnimator;
    private PlayerMove playerMove;




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

        if (RifleAnimator == null)
        {
            Debug.LogError("Animator is not assigned!");
        }

        if (playerMove == null)
        {
            Debug.LogError("PlayerMove is missing!");
        }
    }




    // Handle Attack
    private void HandleAttack()
    {
        // Handle Mouse Input;
        // HandleMouseInput();
    }


    // Handle Mouse Input;
    private void HandleMouseInput()
    {
        // Mouse Left Press: Down
        if (Input.GetMouseButtonDown(0))
        {
            // Start Attack
            if (!RifleAnimator.GetBool("isAttacking"))
            {
                // Animator; Attack, isAttacking
                RifleAnimator.SetTrigger("Attack");
                RifleAnimator.SetBool("isAttacking", true);

                // isAttacking
                isAttacking = true;
            }
        }
    }





    // Animation Event
    // Start Slide
    public void PlayerStartSlide()
    {
        playerMove.PlayerForwardSlide();
    }


    // Attack Animation End
    public void OnAttackAnimationEnd()
    {
        // Finish Attack
        RifleAnimator.SetBool("isAttacking", false);
        isAttacking = false;
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
        if (isRunning)
        {
            RifleAnimator.SetBool("isRunning", true);
        }
        else if (!isRunning)
        {
            RifleAnimator.SetBool("isRunning", false);
        }
    }


    // Handle Player Move Blend Tree
    private void HandlePlayerMoveBlendTree()
    {
        if (RifleAnimator == null) return;
        RifleAnimator.SetFloat(horizontalParamName, playerMove.currentXSpeed);
        RifleAnimator.SetFloat(verticalParamName, playerMove.currentZSpeed);
    }

}