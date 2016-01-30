using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{

    public float spawnDelay = 15f;

    private GameObject enemyPrefab;

    private float lastSpawnTime = -10000f;

    void Awake()
    {
        enemyPrefab = Resources.Load<GameObject>("enemy");
        gameObject.layer = Layers.spawners;
    }

    void OnTriggerStay(Collider other)
    {
        if (Time.time >= lastSpawnTime + spawnDelay)
        {
            lastSpawnTime = Time.time;
            Spawn();
        }
    }

    private void Spawn()
    {
        GameObject enemyObj = Instantiate(enemyPrefab);
        enemyObj.transform.position = gameObject.transform.position;
    }

}
