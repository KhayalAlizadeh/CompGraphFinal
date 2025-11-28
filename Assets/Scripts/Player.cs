using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] private bool isRunning;
    [SerializeField] private bool isDancing;

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
    private CharacterController controller;
    [Header("Values")]
    [SerializeField] private float moveSpeed;
    private Vector2 moveInput;
    private Vector3 moveDirection;

    void Start() {
        controller = GetComponent<CharacterController>();
    }
    
    void OnEnable() {
        dance.action.started += Dance;
        dance.action.Enable();
    }

    void OnDisable() {
        dance.action.started -= Dance;
        dance.action.Disable();
    }

    void Dance(InputAction.CallbackContext ctx) {
        IsDancing = !IsDancing; // Enter -> Dance -> Enter -> Stop
    }

    void Update() {
        moveInput = move.action.ReadValue<Vector2>();
        isRunning = moveInput != Vector2.zero;
        moveDirection = new Vector3((moveInput.normalized * moveSpeed).x, 0, (moveInput.normalized * moveSpeed).y);
    }

    void FixedUpdate() {
        controller.Move(moveDirection * Time.deltaTime);
    }
}
