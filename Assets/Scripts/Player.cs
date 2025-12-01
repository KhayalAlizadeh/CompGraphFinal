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

    private Animator animator;
    private int runParameterID = 0;
    private int danceParameterID = 1;
    private Vector2 moveInput;
    private Vector3 moveDirection;

    void Start() {
        animator = GetComponent<Animator>();
        runParameterID = animator.parameters[runParameterID].nameHash;
        danceParameterID = animator.parameters[danceParameterID].nameHash;
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

    void Update() {
        moveInput = move.action.ReadValue<Vector2>();
        isRunning = (moveInput != Vector2.zero) && !isDancing;
        moveDirection = new Vector3((moveInput.normalized * moveSpeed).x, 0, (moveInput.normalized * moveSpeed).y);
    }

    void FixedUpdate() {
        if (!isDancing && isRunning) {
            animator.SetBool(danceParameterID, false);
            animator.SetBool(runParameterID, true);
            controller.Move(moveDirection * Time.deltaTime);
        }
        else if (!isRunning && !isDancing) {
            animator.SetBool(runParameterID, false);
            animator.SetBool(danceParameterID, false);
        }
    }

    public void GameOver() {
        if (hasLost) {
            return;
        }
        
        gameOverText.gameObject.SetActive(true);
        hasLost = true;
    }
}
