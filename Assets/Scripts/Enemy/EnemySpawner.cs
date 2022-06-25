using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] float distanceFromCenter;
    [SerializeField] float maxdistance;
    [SerializeField] List<GameObject> enemies;

    private int oldPoints;

    private void Start()
    {
        oldPoints = GlobalFields.points;
    }

    private void Update()
    {
        if (oldPoints != GlobalFields.points)
        {
            oldPoints = GlobalFields.points;
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        Vector3 rand_spawnPos = new Vector3(Random.insideUnitSphere.x * maxdistance, 0, Random.insideUnitSphere.z * maxdistance);
        if (Vector3.Distance(rand_spawnPos, Vector3.zero) >= distanceFromCenter)
        {
            if (!Physics.Raycast(rand_spawnPos, Vector3.up, 10))
            {
                GameObject enemy = Instantiate(enemies[Random.Range(0, enemies.Count)]);
                enemy.transform.position = rand_spawnPos;
            }
            else
            {
                SpawnEnemy();
            }
        }
        else
            SpawnEnemy();
    }
}
