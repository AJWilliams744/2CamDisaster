using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControllerEnemy : MonoBehaviour
{

    [SerializeField]
    private Animator animController;
    public void SetSpeed(float speed)
    {
        animController.SetFloat("Speed", speed);
    }
}
