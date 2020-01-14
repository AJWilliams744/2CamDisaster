using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private EnemyBody character;
    private Environment map;

    private bool atDestination = true;

    private bool PlayerCaught = false;

    private void StartMove(bool forcedSpeed)
    {
        EnvironmentTile destination = map.GetRandomTile();
        if (destination != null)
        {
            List<EnvironmentTile> route = map.Solve(character.CurrentPosition, destination);

            if (!forcedSpeed)
            {
                character.SetSpeed(GetRandomSpeed());
                character.GoTo(route);
            }
            else
            {
                character.SetSpeed(Speeds.fast);
                character.GoTo(route);
            }
                  
        }
    }

    private void Update()
    {
        if (atDestination)
        {
            if (PlayerCaught)
            {
                StartMove(true);
            }
            else
            {
                StartMove(false);
            }
            
            atDestination = false;
        }
    }

    //arrived at destination, need a new route
    public void TriggerArrived()
    {        //wait 5 seconds then move on
        character.SetSpeed(Speeds.stop);
        if (PlayerCaught)
        {
            StartCoroutine(FunctionTimmer(StartMove,0, PlayerCaught));
        }
        else
        {
            StartCoroutine(FunctionTimmer(StartMove, 5, PlayerCaught));
        }
        
    }

    public void TriggerPlayerCaught()
    {        
        PlayerCaught = true;
        TriggerArrived();
    }

    private Speeds GetRandomSpeed()
    {
        if(Random.Range(0,10) < 7)
        {
            return Speeds.medium;
        }
        else
        {
            return Speeds.fast;
        }
    }

    
    //Do function after timmer is up
    private IEnumerator FunctionTimmer(MyDelegate function, int delay, bool forced)
    {
        yield return new WaitForSeconds(delay);
        function(forced);
        Debug.Log("New Route Started");
    }

    //Used for timmer
    delegate void MyDelegate(bool forced);
    

    public void SetMap(Environment inMap)
    {
        map = inMap;
    }

}
