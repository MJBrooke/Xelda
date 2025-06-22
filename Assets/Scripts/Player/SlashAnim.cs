using UnityEngine;

public class SlashAnim : MonoBehaviour
{
    // This is invoked in the animation event at the end of the swing
    public void DestroySelf() => Destroy(gameObject);
}
