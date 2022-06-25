using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageNumbers : MonoBehaviour
{
    [SerializeField] Text text;
    [SerializeField] float move_distance;

    public float multiplier;

    private void Start()
    {
        switch (multiplier)
        {
            case 1:text.color = Color.red; break;
            case 2:text.color = Color.black; break;
            case 4:text.color = Color.yellow;break;
        }

        text.text = GlobalFields.Player.GetComponent<WeaponStats>().damage * multiplier + "";

        transform.LookAt(GlobalFields.Player.transform.position);
        transform.DOMove(transform.position + Random.onUnitSphere * move_distance,2f).OnComplete(() => {
            Destroy(transform.gameObject);
        });
    }

    private void Update()
    {
        transform.LookAt(GlobalFields.Player.transform.position);
    }
}
