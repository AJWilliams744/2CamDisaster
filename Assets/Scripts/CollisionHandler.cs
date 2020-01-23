using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private Character character;

    private bool AmIDead = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!AmIDead && other.tag == "Enemy")
        {
            character.TriggerDeath();
           // Debug.LogError("IVE BEEN HIT");
            AmIDead = true;
            character.gameObject.transform.parent = other.gameObject.transform;
            other.gameObject.GetComponent<EnemyAI>().TriggerPlayerCaught();
        }
        
    }
}
