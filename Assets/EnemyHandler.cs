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

    public void ResetAllEnemy()
    {
       foreach(GameObject enemy in AllEnemies)
       {
            ResetEnemy(enemy);
       }
    }

    public void DestroyAllEnemies()
    {
        if (AllEnemies != null)
        {


            foreach (GameObject enemy in AllEnemies)
            {
                GameObject.Destroy(enemy);
            }
        }
    }

    private void ResetEnemy(GameObject enemy)
    {
        EnvironmentTile randomPosition = enviroment.GetRandomTile();
        enemy.transform.position = randomPosition.Position;
        enemy.GetComponent<EnemyAI>().SetMap(enviroment);
        enemy.GetComponent<EnemyBody>().CurrentPosition = randomPosition;
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
