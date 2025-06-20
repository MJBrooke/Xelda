using UnityEngine;

public class Sword : MonoBehaviour
{
    private static readonly int Attack1 = Animator.StringToHash("Attack");
    
    private PlayerControls _playerControls;
    private Animator _animator;

    private void Awake()
    {
        _playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        _playerControls.Enable();
        _animator = GetComponent<Animator>();
    }
    
    void Start()
    {
        _playerControls.Combat.Attack.started += _ => Attack();
    }

    private void Attack() => _animator.SetTrigger(Attack1);
}
