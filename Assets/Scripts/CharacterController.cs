using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private Character character;
    
    public void TurnLeft()
    {
        character.Turn(new Vector3(-10,0,0));
    }
    public void TurnRight()
    {
        character.Turn(new Vector3(10, 0, 0));
    }
    public void TurnForward()
    {
        character.Turn(new Vector3(0, 0, 10));
    }
    public void TurnBack()
    {
        character.Turn(new Vector3(0, 0, -10));
    }

    public void SetCharacterController(Character inCharacter)
    {
        character = inCharacter;
    }

    public void HandleDoorPress()
    {
        character.HandleDoorPress();
    }

    public void SwitchTorch()
    {
        character.SwitchTorch();
    }

   
}
