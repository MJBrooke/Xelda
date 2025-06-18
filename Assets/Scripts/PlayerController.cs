using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 4f;

    // Reference to the generated code from the Input System
    private PlayerControls _playerControls;
    
    // Reference to current movement input direction from the Input System
    private Vector2 _movement;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _playerControls = new PlayerControls();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable() => _playerControls.Enable();

    private void Update() => PlayerInput();

    private void FixedUpdate() => Move();

    private void PlayerInput() => _movement = _playerControls.Movement.Move.ReadValue<Vector2>();

    private void Move() => _rb.MovePosition(_rb.position + _movement * (moveSpeed * Time.fixedDeltaTime));
}
