using UnityEngine;
using UnityEngine.InputSystem;

public class Sword : MonoBehaviour
{
    // What do we want to do? We want to:
    //    - Flip the X-axis depending on the mouse location (or rather, rotate using a Quarternion?)
    // So what do we need?
    //    - We need those same PlayerControls for the Mouse input
    //    - The location of the Player? 
    
    
    private static readonly int Attack1 = Animator.StringToHash("Attack");
    
    private PlayerControls _playerControls;
    private PlayerController _playerController;
    private ActiveWeapon _activeWeapon;
    private Animator _animator;
    private Camera _cam;

    private void Awake()
    {
        _playerControls = new PlayerControls();
        _activeWeapon = GetComponentInParent<ActiveWeapon>();
        _playerController = GetComponentInParent<PlayerController>();
        _cam = Camera.main;
    }

    private void OnEnable()
    {
        _playerControls.Enable();
        _animator = GetComponent<Animator>();
    }
    
    private void Start()
    {
        _playerControls.Combat.Attack.started += _ => Attack();
    }

    private void Update()
    {
        AdjustFacingDirection();
    }

    private void Attack() => _animator.SetTrigger(Attack1);
    
    private void AdjustFacingDirection()
    {
        var mousePos = _cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        var playerPos = _playerController.transform.position;

        var angle = Mathf.Atan2(mousePos.y - playerPos.y, Mathf.Abs(mousePos.x - playerPos.x)) * Mathf.Rad2Deg;
        var rotation = mousePos.x < playerPos.x ? -180 : 0;
        _activeWeapon.transform.rotation = Quaternion.Euler(0, rotation, angle);
    }
}
