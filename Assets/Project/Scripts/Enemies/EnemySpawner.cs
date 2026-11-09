using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform enemiesParent;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float zoneRadius = 8f;

    [SerializeField] private float spawnInterval = 3.5f;
    [SerializeField] private float minDistanceFromPlayer = 6f;
    [SerializeField] private float maxDistanceFromPlayer = 16f;
    [SerializeField] private int maxEnemies = 3;

    private Transform player;
    private float nextSpawnTime;

    private void Start()
    { 

        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        nextSpawnTime = Time.time + spawnInterval;
    }

    private void Update()
    {
        if (player == null || enemyPrefab == null || enemiesParent == null)
            return;

        if (Time.time < nextSpawnTime)
            return;

        nextSpawnTime = Time.time + spawnInterval;

            if (enemiesParent.childCount < maxEnemies)
                SpawnEnemy();

    }
    private void SpawnEnemy()
    {
        List<Transform> validPoints = new List<Transform>();

        foreach(Transform point in spawnPoints)
        {
            if (EnemyNearPoint(point.position))
                continue;

            float distance = Vector2.Distance(point.position, player.position);
            if (distance >= minDistanceFromPlayer && distance <= maxDistanceFromPlayer)
                validPoints.Add(point);
        }
        if (validPoints.Count == 0)
            return;

        Transform chosen = validPoints[Random.Range(0, validPoints.Count)];
        Instantiate(enemyPrefab, chosen.position, Quaternion.identity, enemiesParent);
    }
    private bool EnemyNearPoint(Vector2 point)
    {
        foreach (Transform enemy in enemiesParent)
        {
            if (Vector2.Distance(enemy.position, point) < zoneRadius)
                return true;
        }
        return false;
    }
}
