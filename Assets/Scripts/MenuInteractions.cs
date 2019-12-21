using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuInteractions : MonoBehaviour
{
    private Character character;
    // Start is called before the first frame update
    public void SetCharacter(Character inCharacter)
    {
        character = inCharacter;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetButtonDown("Fire1"))
        {            
            if (Vector3.Distance(Input.mousePosition, new Vector3(404, 525, 0)) < 100)
            {
                if (character != null)
                {
                    character.TriggerHit();
                }
            }
            

        }
    
    }
}
