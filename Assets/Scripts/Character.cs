using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Character : MonoBehaviour
{

    private Game game;

    [SerializeField] private float SingleNodeMoveTime = 0.5f;

    [SerializeField] private AnimationController characterAnimation;

    [SerializeField] private GameObject spotLightObj;

    [SerializeField] private Light spotLight;

    private Coroutine turnCoroutine;

    public EnvironmentTile CurrentPosition { get; set; }

    private bool IAmDead = false;

    private bool HasKey = false;

    private void Start()
    {
        game = GameObject.FindGameObjectWithTag("Game").GetComponent<Game>();
    }

    public void SetHasKey(bool inValue)
    {
        game.FoundKey();
        HasKey = inValue;
    }

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
       // characterAnimation.SetWalking(false);

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
            
            //Vector3 position = CurrentPosition.Position;
            Vector3 position = this.transform.position;

            if(route.Count == 1) position = CurrentPosition.Position;
            for (int count = 0; count < route.Count; ++count)
            {
                if (this.transform.position != CurrentPosition.Position && route.Count != 1) count++;
                Vector3 next = route[count].Position;
                yield return DoMove(position, next);
                CurrentPosition = route[count];
                position = next;
            }            
        }
        characterAnimation.SetWalking(false);
    }

    public void GoTo(List<EnvironmentTile> route)
    {
        // Clear all coroutines before starting the new route so 
        // that clicks can interupt any current route animation
        if(route != null) StopAllCoroutines();
        if (!IAmDead)
        {
            characterAnimation.SetWalking(true);
            StartCoroutine(DoGoTo(route));
        }
        
    }

    public void StopMovement()
    {
        StopAllCoroutines();
        characterAnimation.SetWalking(false);
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
        spotLightObj.SetActive(value);
    }

    private IEnumerator ReduceSpotLight()
    {
        float t = 0;
        while (t < 5)
        {
            t += Time.deltaTime * 1f;
            spotLight.intensity = 1 - (t / 5);
            yield return new WaitForEndOfFrame();
        }        
    }    

    public void TriggerDeath()
    {
        StopAllCoroutines();
        characterAnimation.TriggerHit();
        game.IAmDead(5);
        IAmDead = true;

        StartCoroutine(ReduceSpotLight());
    }

    public void HandleDoorPress()
    {
        if (!CurrentPosition.IsDoor) return;
        if (!HasKey) return;
        if (turnCoroutine != null) StopCoroutine(turnCoroutine);
        Vector3 newRotation = this.transform.localEulerAngles;

        newRotation.y = CurrentPosition.transform.localEulerAngles.y;

        this.transform.localEulerAngles = newRotation;

        CurrentPosition.TriggerDoorRotation();
        StopAllCoroutines();
        game.IAmDead(3);
        //GetLookDirection();
    }
    public void SwitchTorch()
    {
        spotLightObj.SetActive(!spotLightObj.activeSelf); 
    }

    //private void GetLookDirection()
    //{
    //    float currentRot = this.transform.eulerAngles.y;
    //    float tileRot = this.transform.eulerAngles.y;
    //}

    ////Get in under 360
    //private void FixEulerAngle(float inAngle)
    //{

    //}
    
}
