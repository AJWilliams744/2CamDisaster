using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Speeds { stop, medium, fast }
public class EnemyBody : MonoBehaviour
{   
    private float SingleNodeMoveTime = 3f;
    private float damage = 5f;

    [SerializeField] private EnemyAI AI;

    [SerializeField] private AnimationControllerEnemy characterAnimation;

    private Coroutine turnCoroutine;

    public EnvironmentTile CurrentPosition { get; set; }

    private IEnumerator DoMove(Vector3 position, Vector3 destination)
    {
        // Move between the two specified positions over the specified amount of time
        if (position != destination)
        {
            //transform.rotation = Quaternion.LookRotation(destination - position, Vector3.up);
            //StartCoroutine(TurnOverTime(destination, position));
            Vector3 p = transform.position;
            float t = 0.0f;

            while (t < SingleNodeMoveTime)
            {

                t += Time.deltaTime;
                p = Vector3.Lerp(position, destination, t / SingleNodeMoveTime);
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(destination - position, Vector3.up), t / SingleNodeMoveTime);
                transform.position = p;
                yield return null;
            }
        }
       

    }

    public void Turn(Vector3 turnValue)
    {

        Vector3 LookPos = CurrentPosition.Position + turnValue;

        if (turnCoroutine != null) StopCoroutine(turnCoroutine);

        turnCoroutine = StartCoroutine(TurnOverTime(LookPos - CurrentPosition.Position));
    }

    private IEnumerator TurnOverTime(Vector3 Rotation)
    {
        float t = 0;

        while (t < 1)
        {
            t += Time.deltaTime / 10;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Rotation, Vector3.up), t);
            yield return new WaitForEndOfFrame();
        }


    }

    private IEnumerator DoGoTo(List<EnvironmentTile> route)
    {
        // Move through each tile in the given route
        if (route != null)
        {
            Vector3 position = CurrentPosition.Position;
            for (int count = 0; count < route.Count; ++count)
            {
                Vector3 next = route[count].Position;
                yield return DoMove(position, next);
                CurrentPosition = route[count];
                position = next;
            }
            //End of route, Trigger New route
            AI.TriggerArrived();
        }
        
    }

    public void GoTo(List<EnvironmentTile> route)
    {
        // Clear all coroutines before starting the new route so 
        // that clicks can interupt any current route animation
        StopAllCoroutines();       
        StartCoroutine(DoGoTo(route));
    }    

    private float GetSpeed(Speeds speed)
    {
        switch (speed)
        {
            case Speeds.stop:
                return 0;
            case Speeds.medium:
                return 0.1f;
            case Speeds.fast:
                return 1;
        }
        return 0.1f;
    }

    //Temp function may use later
    public void Create(Speeds speed = Speeds.medium, float inDamage = 5f )
    {
        SetSpeed(speed);
        damage = inDamage;        
    }

    public void SetSpeed(Speeds speed)
    {
        float movementSpeed = GetSpeed(speed);
        SingleNodeMoveTime = 1 / movementSpeed;
        characterAnimation.SetSpeed(movementSpeed);
    }

    public void SetDamage(float inDamage)
    {
        damage = inDamage;
    }

    private float GetDamage()
    {
        return damage;
    }

}
