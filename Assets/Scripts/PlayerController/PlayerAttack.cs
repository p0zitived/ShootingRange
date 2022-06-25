using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public enum PlayerAttackState
{
    Idle,
    Shooting,
    Reloading,
}

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] public WeaponStats weaponStats;
    [SerializeField] GameObject prefab;
    [SerializeField] LayerMask layer;

    [Header("Weapon shooting")]
    [SerializeField] ParticleSystem fireParticles;
    [SerializeField] GameObject fireSoundPrefab;
    [SerializeField] GameObject damageNumbersPrefab;
    [Header("Weapon kickback")]
    [SerializeField] Transform kickBackPoint;
    [SerializeField] GameObject weapon;

    // Useful fields
    Vector3 weapong_initial_localPosition;
    Vector3Int screenCenter = new Vector3Int(Screen.width / 2,Screen.height/2,0);
    float lastShootTime = 0;

    [Header("Usefuls")]
    // States ====================================================================
    public PlayerAttackState playerAttackState;

    // acctual Ammount
    public int acc_amount;

    private void Start()
    {
        weapong_initial_localPosition = weapon.transform.localPosition;
        acc_amount = weaponStats.amount;
    }

    private void Update()
    {
        if (playerAttackState != PlayerAttackState.Reloading)
        {
            if (Input.GetMouseButton(0))
            {
                playerAttackState = PlayerAttackState.Shooting;
                Shoot();
            }
            else
            {
                playerAttackState = PlayerAttackState.Idle;
            }
        }
        if (Input.GetKeyDown(KeyCode.R) && playerAttackState == PlayerAttackState.Idle)
        {
            StartCoroutine(Reload());
        }
    }

    private void ExecuteKickBack()
    {
        weapon.transform.DOKill();
        weapon.transform.DOLocalMove(kickBackPoint.localPosition, 0.1f).OnComplete(() => {
            weapon.transform.DOLocalMove(weapong_initial_localPosition, 0.4f);
        });

    }
    private void ExecuteParticlesAndSound()
    {
        GameObject aux = Instantiate(fireSoundPrefab, transform);
        Destroy(aux, 5f);
        fireParticles.Play();
    }
    private void Shoot()
    {
        if (Time.time > lastShootTime + weaponStats.fire_interval)
        {
            if (acc_amount > weaponStats.bullets_per_fire-1)
            {
                // Particles and animations
                ExecuteKickBack();
                ExecuteParticlesAndSound();
                // interval mechanic
                lastShootTime = Time.time;

                for (int i = 0; i < weaponStats.bullets_per_fire; i++)
                {
                    // amount mechanic
                    acc_amount--;

                    Ray ray = Camera.main.ScreenPointToRay(new Vector3(screenCenter.x + Random.Range(-weaponStats.accuracy, weaponStats.accuracy), screenCenter.y + Random.Range(-weaponStats.accuracy, weaponStats.accuracy)));
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, 100, layer))
                    {
                        GameObject obj = Instantiate(prefab);
                        obj.transform.parent = hit.transform;
                        obj.transform.position = hit.point;
                        Destroy(obj, 10);

                        try
                        {
                            hit.transform.GetComponentInParent<EnemyLimb>().getDamage(weaponStats.damage);
                            GameObject aux = Instantiate(damageNumbersPrefab);
                            aux.transform.position = hit.point;
                            aux.GetComponent<DamageNumbers>().multiplier = hit.transform.GetComponentInParent<EnemyLimb>().getMultiplier();
                        } catch
                        {

                        }
                    }
                }
            }
        }
    }
    private IEnumerator Reload()
    {
        if (playerAttackState != PlayerAttackState.Reloading)
        {
            playerAttackState = PlayerAttackState.Reloading;
            yield return new WaitForSeconds(weaponStats.reload_time);
            acc_amount = weaponStats.amount;
            playerAttackState = PlayerAttackState.Idle;
        }
    }
}
