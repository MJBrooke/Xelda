using System.Collections;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    [SerializeField] private float knockbackTime = 0.2f;
    
    public bool IsKnockedBack { get; private set; }
    
    private Rigidbody2D _rb;
    
    private void Awake() => _rb = GetComponent<Rigidbody2D>();
    
    // Anything that wants to invoke a Knockback can call this function.
    // It creates a single time-bounded impulse of force to the respective RigidBody.
    public void InvokeKnockback(Transform knockbackSource, float knockbackStrength)
    {
        IsKnockedBack = true;
        var force = (transform.position - knockbackSource.position).normalized * knockbackStrength *_rb.mass;
        _rb.AddForce(force, ForceMode2D.Impulse);
        StartCoroutine(KnockbackCoroutine());
    }

    private IEnumerator KnockbackCoroutine()
    {
        yield return new WaitForSeconds(knockbackTime);
        _rb.linearVelocity = Vector2.zero;
        IsKnockedBack = false;
    }
}
