using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private float SingleNodeMoveTime = 0.5f;

    [SerializeField] private AnimationController characterAnimation;

    [SerializeField] private GameObject spotLight;

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
        characterAnimation.SetWalking(false);

    }

    public void Turn(Vector3 turnValue)
    {
       
        Vector3 LookPos = CurrentPosition.Position + turnValue;

        if (turnCoroutine != null) StopCoroutine(turnCoroutine);
       
        turnCoroutine =  StartCoroutine(TurnOverTime(LookPos - CurrentPosition.Position));        
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
        }
        //characterAnimation.SetWalking(false);
    }

    public void GoTo(List<EnvironmentTile> route)
    {
        // Clear all coroutines before starting the new route so 
        // that clicks can interupt any current route animation
        StopAllCoroutines();
        characterAnimation.SetWalking(true);
        StartCoroutine(DoGoTo(route));
        
    }

    public void SetSitting(bool value)
    {
        SetSpotLight(!value);
        characterAnimation.SetSitting(value);
    }

    public void TriggerHit()
    {
        characterAnimation.TriggerHit();
    }

    public void SetRootMotion(bool value)
    {
        characterAnimation.SetRootMotion(value);
    }

    private void SetSpotLight(bool value)
    {
        spotLight.SetActive(value);
    }
}
