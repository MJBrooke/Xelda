using UnityEngine;
using UnityEngine.InputSystem;

public class Sword : MonoBehaviour
{
    [SerializeField] private GameObject _slashAnimPrefab;
    [SerializeField] private Transform _slashAnimSpawnPoint;
    
    private static readonly int Attack1 = Animator.StringToHash("Attack");
    
    private PlayerControls _playerControls;
    private PlayerController _playerController;
    private ActiveWeapon _activeWeapon;
    private Animator _animator;
    private Camera _cam;
    
    private GameObject _slashAnim;

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

    public void SwingUpFlipAnim()
    {
        _slashAnim.gameObject.transform.rotation = Quaternion.Euler(-180, 0, 0);
        if (_playerController.FacingLeft) _slashAnim.GetComponent<SpriteRenderer>().flipX = true;
    }

    public void SwingDownFlipAnim()
    {
        _slashAnim.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        if (_playerController.FacingLeft) _slashAnim.GetComponent<SpriteRenderer>().flipX = true;
    }

    private void Attack()
    {
        _animator.SetTrigger(Attack1);
        _slashAnim = Instantiate(_slashAnimPrefab, _slashAnimSpawnPoint.position, Quaternion.identity);
        _slashAnim.transform.SetParent(transform.parent);
    }

    private void AdjustFacingDirection()
    {
        var mousePos = _cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        var playerPos = _playerController.transform.position;

        var angle = Mathf.Atan2(mousePos.y - playerPos.y, Mathf.Abs(mousePos.x - playerPos.x)) * Mathf.Rad2Deg;
        var rotation = mousePos.x < playerPos.x ? -180 : 0;
        _activeWeapon.transform.rotation = Quaternion.Euler(0, rotation, angle);
    }
}
