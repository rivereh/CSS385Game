using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public static EnemySpawner instance;
    public Floor[] floors;
    public Skeleton enemy;

    Floor currentFloor;
    int currentFloorNumber;

    int enemiesRemainingToSpawn;
    int enemiesRemainingAlive;
    float nextSpawnTime;
    Transform floorSpawn;

    public float maxXSpawn = 0;
    public float minXSpawn = 0;

    void Start()
    {
        instance = this;
    }

    

    void Update()
    {
        if (enemiesRemainingToSpawn > 0 && Time.time > nextSpawnTime)
        {
            enemiesRemainingToSpawn--;
            nextSpawnTime = Time.time + currentFloor.timeBetweenSpawns;

            float spawnPosX = Random.Range(minXSpawn, maxXSpawn);
            Vector3 spawnPos = new Vector3(spawnPosX, floorSpawn.position.y, 0);

            Skeleton spawnedEnemy = Instantiate(enemy, spawnPos, Quaternion.identity) as Skeleton;
            spawnedEnemy.OnDeath += OnEnemyDeath;
        }
    }

    void OnEnemyDeath()
    {
        enemiesRemainingAlive--;
        if (enemiesRemainingAlive == 0)
        {
            GameObject floorBlock = GameObject.Find("FloorBlock" + currentFloorNumber);
            floorBlock.GetComponent<BoxCollider2D>().enabled = true;
            floorBlock.GetComponent<SpriteRenderer>().enabled = true;
            
        }
    }

    public void NextFloor()
    {
        currentFloorNumber++;
        if (currentFloorNumber - 1 < floors.Length)
        {
            currentFloor = floors[currentFloorNumber - 1];
            floorSpawn = GameObject.Find("FloorSpawner" + currentFloorNumber).transform;

            enemiesRemainingToSpawn = currentFloor.totalEnemies;
            enemiesRemainingAlive = enemiesRemainingToSpawn;
        }
        
    }

    [System.Serializable]
    public class Floor {
        public int totalEnemies;
        public int enemiesToSpawnEachTime;
        public float timeBetweenSpawns;
    }

}
