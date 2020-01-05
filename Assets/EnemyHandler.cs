using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject EnemyPrefab;

    [SerializeField]
    private Environment enviroment;

    private int difficulty = 1; // 1-10

    public void StartHunt()
    {
        for (int i = 0; i < difficulty; i++)
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
    }
}
