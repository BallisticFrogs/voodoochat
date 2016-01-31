using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{

    public float spawnDelay = 15f;

    public Transform spawnPoint;

    private GameObject enemyPrefab;

    private float lastSpawnTime = -10000f;

    void Awake()
    {
        enemyPrefab = Resources.Load<GameObject>("enemy");
        gameObject.layer = Layers.spawners;
    }

    void OnTriggerStay(Collider other)
    {
        if (Time.time >= lastSpawnTime + spawnDelay && other.CompareTag(Tags.Player))
        {
            lastSpawnTime = Time.time;
            Spawn();
        }
    }

    private void Spawn()
    {
        GameObject enemyObj = (GameObject)Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
    }

}
