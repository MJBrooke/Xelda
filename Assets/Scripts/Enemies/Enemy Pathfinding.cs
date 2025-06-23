using UnityEngine;

public class EnemyPathfinding : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    
    private Vector2 _direction;
    private Rigidbody2D _rb;
    private Knockback _knockback;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _knockback = GetComponent<Knockback>();
        _direction = Vector2.zero;
    }

    private void FixedUpdate()
    {
        if (_knockback.IsKnockedBack) return;
        MoveSprite();
    }
    
    public void SetDirection(Vector2 direction) => _direction = direction;
    
    private void MoveSprite() => _rb.MovePosition(_rb.position + _direction * (moveSpeed * Time.fixedDeltaTime));
}
