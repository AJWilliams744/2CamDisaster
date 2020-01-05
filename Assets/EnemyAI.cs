using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private EnemyBody character;
    private Environment map;

    private bool atDestination = true;

    private void StartMove()
    {
        EnvironmentTile destination = map.GetRandomTile();
        if (destination != null)
        {
            List<EnvironmentTile> route = map.Solve(character.CurrentPosition, destination);

            character.SetSpeed(Speeds.medium);
            character.GoTo(route);
            
        }
    }

    private void Update()
    {
        if (atDestination)
        {
            StartMove();
            atDestination = false;
        }
    }

    //arrived at destination, need a new route
    public void TriggerArrived()
    {        //wait 5 seconds then move on
        character.SetSpeed(Speeds.stop);
        StartCoroutine(FunctionTimmer(StartMove, 5));
    }

    
    //Do function after timmer is up
    private IEnumerator FunctionTimmer(MyDelegate function, int delay)
    {
        yield return new WaitForSeconds(delay);
        function();
        Debug.Log("New Route Started");
    }

    //Used for timmer
    delegate void MyDelegate();
    

    public void SetMap(Environment inMap)
    {
        map = inMap;
    }

}
