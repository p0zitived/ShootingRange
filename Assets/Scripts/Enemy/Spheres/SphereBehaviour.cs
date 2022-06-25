using DG.Tweening;
using UnityEngine;

public class SphereBehaviour : MonoBehaviour
{
    [SerializeField] float value;

    private void Start()
    {
        move(value);
    }

    private void move(float value)
    {
        transform.DOMoveY(transform.position.y + value, 2).SetEase(Ease.InBounce).OnComplete(() => {
            move(-value);
        });
    }
}
