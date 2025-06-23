using UnityEngine;

public class DamageSource : MonoBehaviour
{
    [SerializeField] private int damageAmount = 1;
    
    // We attach this to whatever GameObject our hit collider is attached to.
    // If the thing it collides with has an 'EnemyHealth' class, it will take damage.
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.GetComponent<EnemyHealth>()) return;
        other.gameObject.GetComponent<EnemyHealth>().TakeDamage(damageAmount);
    }
}