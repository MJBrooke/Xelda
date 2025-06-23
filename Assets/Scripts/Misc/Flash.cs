using System.Collections;
using UnityEngine;

public class Flash : MonoBehaviour
{
    [SerializeField] private Material whiteMaterial;
    
    private Material _defaultMaterial;
    
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _defaultMaterial = _spriteRenderer.material;
    }

    public void FlashWhite(float flashTime = .2f) => StartCoroutine(FlashCoroutine(flashTime));

    private IEnumerator FlashCoroutine(float flashTime)
    {
        _spriteRenderer.material = whiteMaterial;
        yield return new WaitForSeconds(flashTime);
        _spriteRenderer.material = _defaultMaterial;
    }
}
