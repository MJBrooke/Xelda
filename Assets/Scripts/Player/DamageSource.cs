using UnityEngine;

public class DamageSource : MonoBehaviour
{
    [SerializeField] private int damageAmount = 1;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.GetComponent<EnemyHealth>()) return;
        
        other.gameObject.GetComponent<EnemyHealth>().TakeDamage(damageAmount);
    }
}