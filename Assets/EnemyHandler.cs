using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject EnemyPrefab;

    [SerializeField]
    private Environment enviroment;

    private List<GameObject> AllEnemies;

    private int enemyCount = 10; // 1-10

    private void Start()
    {
        AllEnemies = new List<GameObject>();
    }
    public void StartHunt()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {   
        EnvironmentTile randomPosition = enviroment.GetRandomTile();
        GameObject enemy = Instantiate(EnemyPrefab, randomPosition.Position, Quaternion.identity);
        enemy.GetComponent<EnemyAI>().SetMap(enviroment);
        enemy.GetComponent<EnemyBody>().CurrentPosition = randomPosition;

        AllEnemies.Add(enemy);
    }

    public void SetEnemyCount(int inEnemyCount)
    {
       enemyCount = inEnemyCount;
    }

    public void MapChanged()
    {
        foreach(GameObject enemy in AllEnemies)
        {
            enemy.GetComponent<EnemyAI>().TriggerArrived();
        }
    }
}
