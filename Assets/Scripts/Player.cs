using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] private bool isRunning;
    [SerializeField] private bool isDancing;
    [SerializeField] private bool hasLost;
    public bool IsRunning {
        get {
            return isRunning;
        }
        private set {
            isRunning = value;
        }
    }
    
    public bool IsDancing {
        get {
            return isDancing;
        }
        private set {
            isDancing = value;
        }
    }
    
    [Header("References")]
    [SerializeField] private InputActionReference move;
    [SerializeField] private InputActionReference dance;
    [SerializeField] private TextMeshProUGUI gameOverText;
    private CharacterController controller;
    [Header("Values")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float gravityMultiplier = 1f;

    private bool isDeathDelayed = false;
    private const float gravity = -9.81f;
    private Animator animator;
    private int runParameterID = 0;
    private int danceParameterID = 1;
    private int deathParameterID = 2;
    private Vector2 moveInput;
    private Vector3 moveDirection;

    void Start() {
        animator = GetComponent<Animator>();
        runParameterID = animator.parameters[runParameterID].nameHash;
        danceParameterID = animator.parameters[danceParameterID].nameHash;
        deathParameterID = animator.parameters[deathParameterID].nameHash;
        controller = GetComponent<CharacterController>();
    }
    
    void OnEnable() {
        dance.action.performed += Dance;
        dance.action.Enable();

    }

    void OnDisable() {
        dance.action.started -= Dance;
        dance.action.Disable();
    }

    void Dance(InputAction.CallbackContext ctx) { //Runs once on Start for some reason ahhh
        IsDancing = !IsDancing; // Enter -> Dance -> Enter -> Stop -> ..
        animator.SetBool(runParameterID, false);
        animator.SetBool(danceParameterID, true);
    }

    public void Dance() {
        animator.SetBool(runParameterID, false);
        animator.SetBool(danceParameterID, true);
    }

    void Update() {
        moveInput = move.action.ReadValue<Vector2>();
        IsRunning = (moveInput != Vector2.zero) && !IsDancing;
        moveDirection = new Vector3((moveInput.normalized * moveSpeed).x, gravity * gravityMultiplier, (moveInput.normalized * moveSpeed).y);
    }

    void FixedUpdate() {
        if (!IsDancing && IsRunning && !hasLost) { //run
            animator.SetBool(danceParameterID, false);
            animator.SetBool(runParameterID, true);
            controller.Move(moveDirection * Time.fixedDeltaTime);
        }
        else if (!IsRunning && !IsDancing) { //stop
            animator.SetBool(runParameterID, false);
            animator.SetBool(danceParameterID, false);
            controller.Move(moveDirection * Time.fixedDeltaTime);
        }
        else if (IsDancing) { //dance
            controller.Move(new Vector3(0, gravity * gravityMultiplier, 0) * Time.fixedDeltaTime);
        }
    }

    public void GameOver() {
        if (hasLost) {
            return;
        }
        
        animator.SetTrigger(deathParameterID);
        gameOverText.gameObject.SetActive(true);
        hasLost = true;
    }
}
