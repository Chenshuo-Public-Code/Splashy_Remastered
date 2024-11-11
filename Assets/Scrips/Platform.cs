using UnityEngine;
using DG.Tweening;

public class Platform : MonoBehaviour
{
    public float sinkDuration = 0.5f;
    public void DeactivatePlatform()
    {
        transform.DOMoveY(transform.position.y - 1f, sinkDuration).OnComplete(() =>
        {
            PlatformManager.Instance.RecyclePlatform(this.gameObject);
        });
    }
}
