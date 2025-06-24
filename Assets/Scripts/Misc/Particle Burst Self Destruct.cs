using UnityEngine;

public class ParticleBurstSelfDestruct : MonoBehaviour
{
    private ParticleSystem _particleSystem;

    private void Awake() => _particleSystem = GetComponent<ParticleSystem>();

    private void Update()
    {
        if (_particleSystem && !_particleSystem.IsAlive())  Destroy(gameObject);
    }
}
