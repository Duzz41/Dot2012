using UnityEngine;
using UnityEngine.InputSystem;

public class Move : MonoBehaviour
{
    public float speed = 5f;  // Hız
    private Rigidbody rb;
    private Vector2 moveInput;
    public InputActionAsset inputActions;
    private Camera camera;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Start()
    {
        camera = Camera.main;
    }
    private void FixedUpdate()
    {
        // Hareket yönünü belirle
        Vector3 moveDirection = new Vector3(moveInput.x, 0, moveInput.y);

        // Topu hareket ettir ve döndür
        rb.AddTorque(new Vector3(moveInput.y, 0, -moveInput.x) * speed);
        camera.transform.position = new Vector3(transform.position.x, camera.transform.position.y, transform.position.z);
    }

    // Input System'den gelen veriyi al
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        Debug.Log($"Move Input: {moveInput}");
    }

    private void OnEnable()
    {
        var playerActions = inputActions.FindActionMap("Player"); // Replace "Player" with your action map name
        playerActions["Move"].performed += OnMove; // Subscribe to the OnMove method
        playerActions["Move"].canceled += OnMove; // Optional: handle when the input is released
        playerActions.Enable(); // Enable the action map
    }

    private void OnDisable()
    {
        var playerActions = inputActions.FindActionMap("Player");
        playerActions["Move"].performed -= OnMove; // Unsubscribe
        playerActions["Move"].canceled -= OnMove; // Unsubscribe
        playerActions.Disable(); // Disable the action map
    }
}