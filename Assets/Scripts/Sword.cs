using UnityEngine;
using UnityEngine.InputSystem;

public class Sword : MonoBehaviour
{
    // We have a separate GameObject with its own renderer and animator.
    // It is a blueprint to instantiate, and will take care of destroying itself.
    [SerializeField] private GameObject slashAnimPrefab;
    
    // This is a point attached to our Player character, providing a relative start point for the animation.
    [SerializeField] private Transform slashAnimSpawnPoint;

    // Reference to the permanent collider attached to the player character
    [SerializeField] private Transform weaponCollider;
    
    private static readonly int Attack1 = Animator.StringToHash("Attack");
    
    private PlayerControls _playerControls; // Auto-generated access to inputs
    private PlayerController _playerController; // Access to player-based info, like direction
    private ActiveWeapon _activeWeapon; // Direct parent, providing an anchor point for any weapon
    private Animator _animator; // Allow access to animation variable setting
    private Camera _cam; // Allows access to relative screen space co-ordinates
    
    private GameObject _slashAnim; // Holder for the current slash animation on screen

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
        // Add a lambda to be kicked off on each attack input
        _playerControls.Combat.Attack.started += _ => Attack();
    }

    private void Update()
    {
        AdjustFacingDirection();
    }

    // Rotate the up-swing when facing left
    // This is applied in the animation window itself as an animation event
    public void SwingUpFlipAnim()
    {
        _slashAnim.gameObject.transform.rotation = Quaternion.Euler(-180, 0, 0);
        if (_playerController.FacingLeft) _slashAnim.GetComponent<SpriteRenderer>().flipX = true;
    }

    // Rotate the down-swing when facing left
    // This is applied in the animation window itself as an animation event
    public void SwingDownFlipAnim()
    {
        _slashAnim.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        if (_playerController.FacingLeft) _slashAnim.GetComponent<SpriteRenderer>().flipX = true;
    }

    // Kick off a set of actions each time we attack with the sword-weapon
    private void Attack()
    {
        _animator.SetTrigger(Attack1); // Ensure the animator knows to trigger
        _slashAnim = Instantiate(slashAnimPrefab, slashAnimSpawnPoint.position, Quaternion.identity); // Create a slash animation
        _slashAnim.transform.SetParent(transform.parent); // Move the animation to the sword's location
    }

    // Ensure the sword rotates to the left or right side of the player.
    // TODO - Instead of rotating this individually, can we not just rotate the top-level player GameObject once?
    private void AdjustFacingDirection()
    {
        var mousePos = _cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        var playerPos = _playerController.transform.position;

        // Allow the sword to rotate with the mouse as it is 'aimed'
        var angle = Mathf.Atan2(mousePos.y - playerPos.y, Mathf.Abs(mousePos.x - playerPos.x)) * Mathf.Rad2Deg;
        
        // By rotating the sword, we can use the same up/down animations on the LHS of the player
        var rotation = mousePos.x < playerPos.x ? -180 : 0;
        _activeWeapon.transform.rotation = Quaternion.Euler(0, rotation, angle);
        weaponCollider.transform.rotation = Quaternion.Euler(0, rotation, 0);
    }
}
