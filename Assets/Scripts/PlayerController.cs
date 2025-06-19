using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private static readonly int MoveX = Animator.StringToHash("moveX");
    private static readonly int MoveY = Animator.StringToHash("moveY");
    
    [SerializeField] private float moveSpeed = 4f;

    // Reference to the generated code from the Input System
    private PlayerControls _playerControls;
    
    // Reference to current movement input direction from the Input System
    private Vector2 _movement;
    private Rigidbody2D _rb;
    
    private Animator _animator;

    private void Awake()
    {
        _playerControls = new PlayerControls();
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void OnEnable() => _playerControls.Enable();

    private void Update() => PlayerInput();

    private void FixedUpdate()
    {
        AdjustPlayerFacingDirection();
        Move();
    }

    private void PlayerInput()
    {
        _movement = _playerControls.Movement.Move.ReadValue<Vector2>();
        _animator.SetFloat(MoveX, _movement.x);
        _animator.SetFloat(MoveY, _movement.y);
    }

    private void Move() => _rb.MovePosition(_rb.position + _movement * (moveSpeed * Time.fixedDeltaTime));

    private void AdjustPlayerFacingDirection()
    {
        
    }
}
