using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] CharacterEnemy character;
    private Environment map;
    private void StartMove()
    {
        EnvironmentTile destination = map.GetRandomTile();
        if (destination != null)
        {
            List<EnvironmentTile> route = map.Solve(character.CurrentPosition, destination);
            character.GoTo(route);
        }
    }

}
