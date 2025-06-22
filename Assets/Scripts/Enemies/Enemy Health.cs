using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int startingHealth = 3;
    
    private int _currentHealth;

    private void Start()
    {
        _currentHealth = startingHealth;
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        DetectDeath();
    }

    private void DetectDeath()
    {
        if (_currentHealth <= 0) Destroy(gameObject);
    }
}
