using UnityEngine;
using System.Collections;
public class PlayerMove : MonoBehaviour, IPlayerModule
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
    [Header("Player Movement Properties")]
    public float moveSpeed = 2f; // Movement Speed
    public const float runSpeedMultiplier = 2f; // Run Speed Multiplier
    public float rotationSpeed = 5f; // Rotation Speed

    [Header("Player Slide Properties")]
    public float slideDistance = 5f;  // Sliding Distance
    public float slideDuration = 0.5f; // Sliding Time
    private bool isSliding = false;

    public float currentXSpeed { get; private set; }
    public float currentZSpeed { get; private set; }



    [Header("Mouse Look Properties")]
    public float mouseSensitivity = 50f; // Mouse sensitivity


    [Header("Speed Smoothing")]
    public float speedSmoothTime = 0.1f;


    // Protected Fields
    protected CharacterController characterController;


    // Private Fieldd
    private float xSpeedVelocity; // SmoothDamp
    private float zSpeedVelocity; // SmoothDamp
    private float horizontal, vertical; // Keyboard Input




    // Start Frame
    void Start()
    {
        // Get Components
        GetComponents();

        // Lock Cursor
        Cursor.lockState = CursorLockMode.Locked;

        // Key Manager
        if (KeyManager.Instance != null)
        {
            KeyManager.Instance.OnMoveInput += HandleMoveInput;
            KeyManager.Instance.OnRunPressed += HandleRunInput;
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
            KeyManager.Instance.OnMoveInput -= HandleMoveInput;
            KeyManager.Instance.OnRunPressed -= HandleRunInput;
        }
    }


    // Get Components
    private void GetComponents()
    {
        playerProperties = GetComponent<PlayerProperties>();
        characterController = GetComponent<CharacterController>();

        if(playerProperties == null)
        {
            Debug.LogError("Player Properties is not assigned!");
        }

        if (characterController == null)
        {
            Debug.LogError("CharacterController is not assigned!");
        }
    }




    // Handle Module: Void Update
    public void HandleModule()
    {
        // Handle Player Move
        if (!isAttacking) // Cannot move while Attacking
        {
            HandlePlayerMove();
        }
        else if(isAttacking) // Attacking Set Speed To Zero
        {
            // Set Speed To Zero
            SetSpeedSmoothToZero();
        }


        // Handle Mouse Look
        HandleMouseHorizontalTurn();

        // Apply Gravity
        ApplyGravity();
    }


    // Handle Move Input
    private void HandleMoveInput(float h, float v)
    {
        horizontal = h;
        vertical = v;
    }

    // Handle Run Input
    private void HandleRunInput(bool run)
    {
        isRunning = run;
    }



    // Handle Player Move
    private void HandlePlayerMove()
    {
        // Get Direction
        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        // Check Input
        if (direction.magnitude >= 0.1f)
        {
            // Handle Move
            PlayerMovement(direction);
        }
        else
        {
            // Speed Smooth Set To Zero
            SetSpeedSmoothToZero();
        }
    }




    // Speed Smooth Set To Zero
    private void SetSpeedSmoothToZero()
    {
        // Speed Smooth Set To Zero
        currentXSpeed = Mathf.SmoothDamp(currentXSpeed, 0f, ref xSpeedVelocity, speedSmoothTime);
        currentZSpeed = Mathf.SmoothDamp(currentZSpeed, 0f, ref zSpeedVelocity, speedSmoothTime);

        // Set Speed To Zero
        SetSpeedToZero();
    }


    // Set Speed To Zero
    private void SetSpeedToZero()
    {
        // Speed Set To Zero
        if (Mathf.Abs(currentXSpeed) < 0.05f)
            currentXSpeed = 0f;
        if (Mathf.Abs(currentZSpeed) < 0.05f)
            currentZSpeed = 0f;

        // Set Running: False
        isRunning = false;
    }




    // Player Move
    private void PlayerMovement(Vector3 direction)
    {
        // Spped Multiplier
        float speedMultiplier = isRunning ? runSpeedMultiplier : 1.0f;

        // Direction
        if (direction.x != 0 && direction.z != 0)
        {
            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.z))
            {
                direction.z = 0;
            }
            else
            {
                direction.x = 0;
            }
        }

        // Handle Player Movement Speed
        HandlePlayerMovementSpeed(direction, speedMultiplier);

        // Handle Player Movement Direction
        HandlePlayerMovementDirection();
    }


    // Handle Player Movement Speed
    private void HandlePlayerMovementSpeed(Vector3 direction, float speedMultiplier)
    {
        // Set Target Speed
        float targetXSpeed = direction.x * moveSpeed * speedMultiplier;
        float targetZSpeed = direction.z * moveSpeed * speedMultiplier;

        // Update Current Velocity
        currentXSpeed = Mathf.SmoothDamp(currentXSpeed, targetXSpeed, ref xSpeedVelocity, speedSmoothTime);
        currentZSpeed = Mathf.SmoothDamp(currentZSpeed, targetZSpeed, ref zSpeedVelocity, speedSmoothTime);

        // Set To Target Velocity
        if (Mathf.Abs(currentXSpeed - targetXSpeed) < 0.01f)
            currentXSpeed = targetXSpeed;

        if (Mathf.Abs(currentZSpeed - targetZSpeed) < 0.01f)
            currentZSpeed = targetZSpeed;
    }


    // Handle Player Movement Direction
    private void HandlePlayerMovementDirection()
    {
        // Calculate Movement Direction
        Vector3 moveDirection = transform.TransformDirection(new Vector3(currentXSpeed, 0, currentZSpeed));

        // Apply Movement
        characterController.Move(moveDirection * Time.deltaTime);
    }




    // Handle Rotation
    private void HandleMouseHorizontalTurn()
    {
        // Get Mouse Input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;

        // Horizontal Rotation: Yaw
        if (currentXSpeed != 0 || currentZSpeed != 0)
        {
            transform.Rotate(Vector3.up * mouseX);
        }
    }




    // Player Forward Slide
    public void PlayerSlide(string direction)
    {
        if (!isSliding)
        {
            StartCoroutine(HandlePlayerSlide(direction));
        }
    }

    // Coroutine: Handle Player Forward Slide
    private IEnumerator HandlePlayerSlide(string direction)
    {
        isSliding = true;
        Vector3 startPosition = transform.position;
        Vector3 targetPosition = startPosition;

        switch (direction)
        {
            case "Forward":
                targetPosition += transform.forward * slideDistance;
                break;
            case "Backward":
                targetPosition -= transform.forward * slideDistance;
                break;
            case "Left":
                targetPosition -= transform.right * slideDistance;
                break;
            case "Right":
                targetPosition += transform.right * slideDistance;
                break;
            default:
                isSliding = false;
                yield break;
        }

        float elapsedTime = 0f;
        while (elapsedTime < slideDuration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / slideDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
        isSliding = false;
    }




    // Apply Gravity
    private void ApplyGravity()
    {
        if (!characterController.isGrounded)
        {
            characterController.Move(Vector3.down * 9.8f * Time.deltaTime);
        }
    }
}
