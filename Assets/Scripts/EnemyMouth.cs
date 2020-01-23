using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMouth : MonoBehaviour
{
    [SerializeField] private AudioSource Groan;

    private void Start()
    {
        Groan.pitch = Random.Range(0.8f, 1.5f);

        Groan.PlayDelayed(Random.Range(0.0f, 3.0f));

    }

    

   
}
