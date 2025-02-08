using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandAnimator : MonoBehaviour, IPlayerModule
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
    private PlayerAnimatorManager playerAnimatorManager;
    private Animator HandAnimator => playerAnimatorManager.animator;
    private PlayerMove playerMove;




    // Start Frame
    void Start()
    {
        // Get Components
        GetComponents();

        // Key Manager
        if (KeyManager.Instance != null)
        {
            KeyManager.Instance.OnDodgePressed += HandlePlayerDodge;
        }
        else
        {
            Debug.LogError("KeyManager.Instance Not Found");
        }
    }


    private void OnDestroy()
    {
        if (KeyManager.Instance != null)
        {
            KeyManager.Instance.OnDodgePressed -= HandlePlayerDodge;
        }
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
        PlayerAnimatorScriptComponentHelper.GetPlayerAnimatorScriptComponents(gameObject,
                                            out playerAnimatorManager,
                                            out playerProperties,
                                            out playerMove);
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
            if (!HandAnimator.GetBool("isAttacking"))
            {
                // Animator; Attack, isAttacking
                HandAnimator.SetTrigger("Attack");
                HandAnimator.SetBool("isAttacking", true);

                // isAttacking
                isAttacking = true;
            }
        }
    }





    // Animation Event
    // Start Slide
    public void PlayerHandStartSlide(string direction)
    {
        playerMove.PlayerSlide(direction);
    }


    // Attack Animation End
    public void OnRifleAttackAnimationEnd()
    {
        // Finish Attack
        HandAnimator.SetBool("isAttacking", false);
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
            HandAnimator.SetBool("isRunning", true);
        }
        else if (!isRunning)
        {
            HandAnimator.SetBool("isRunning", false);
        }
    }


    // Handle Player Dodge
    private void HandlePlayerDodge(string direction)
    {
        if (isRunning) return;

        switch (direction)
        {
            case "Forward":
                HandAnimator.SetTrigger("ForwardDodge");
                break;

            case "Backward":
                HandAnimator.SetTrigger("BackwardDodge");
                break;

            case "Left":
                HandAnimator.SetTrigger("LeftDodge");
                break;

            case "Right":
                HandAnimator.SetTrigger("RightDodge");
                break;

            default:
                break;
        }
    }




    // Handle Player Move Blend Tree
    private void HandlePlayerMoveBlendTree()
    {
        if (HandAnimator == null) return;
        HandAnimator.SetFloat(horizontalParamName, playerMove.currentXSpeed);
        HandAnimator.SetFloat(verticalParamName, playerMove.currentZSpeed);
    }

}