using DG.Tweening;
using UnityEngine;

public class RotatingObstacle : MonoBehaviour
{
    [SerializeField] float angle;

    private void FixedUpdate()
    {
        transform.Rotate(Vector2.up, angle);
    }
}
