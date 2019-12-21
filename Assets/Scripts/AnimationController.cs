using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class AnimationController : MonoBehaviour
{
    [SerializeField]
    private Animator animController;

    // Start is called before the first frame update
    void Start()
    {
        SetWalking(false);
    }

    public void SetWalking(bool value)
    {
        animController.SetBool("Movement", value);
    }

    public void SetSitting(bool value)
    {
        animController.SetBool("Sitting", value);
    }

    public void TriggerHit()
    {
        animController.SetTrigger("Hit");
    }

    public void SetRootMotion(bool value)
    {
        animController.applyRootMotion = value;
    }
}
