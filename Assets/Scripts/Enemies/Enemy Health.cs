using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int startingHealth = 3;
    [SerializeField] private GameObject deathParticlesPrefab;
    
    private Knockback _knockback;
    private Flash _flash;
    
    private int _currentHealth;

    private void Awake()
    {
        _knockback = GetComponent<Knockback>();
        _flash = GetComponent<Flash>();
    }

    private void Start() => _currentHealth = startingHealth;

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        // TODO - this should really be handled with some kind of Event. Fine for now though, for simplicity.
        _knockback.InvokeKnockback(PlayerController.Instance.transform, 15f);
        StartCoroutine(DetectDeath(.2f));
    }
    
    private IEnumerator DetectDeath(float waitTime)
    {
        yield return StartCoroutine(_flash.FlashCoroutine(waitTime/2));
        if (_currentHealth > 0) yield break;
        Instantiate(deathParticlesPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
