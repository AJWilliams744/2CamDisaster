using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField]
    private CharacterController characterController;
    [SerializeField]
    private CameraFollow MainCamFollow;
    
    private bool gameStart = false;
    
    public void StartGameControls()
    {
        gameStart = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (gameStart) HandleGameRun();
         

    }
    private void HandleGameRun()
    {
        if (Input.GetKeyDown(KeyCode.A)) HandleLeftFace();
        if (Input.GetKeyDown(KeyCode.D)) HandleRightFace();
        if (Input.GetKeyDown(KeyCode.W)) HandleForwardFace();
        if (Input.GetKeyDown(KeyCode.S)) HandleBackFace();

        if (Input.GetMouseButton(1))
        {
          
            HandleCameraFollow();
        }
        else
        {
         
            HandleCameraStop();
        }
    }
    //Face Character Left
    private void HandleLeftFace()
    {
        characterController.TurnLeft();
    }
    //Face Character Right
    private void HandleRightFace()
    {
        characterController.TurnRight();
    }
    //Face Character Left
    private void HandleForwardFace()
    {
        characterController.TurnForward();
    }
    //Face Character Right
    private void HandleBackFace()
    {
        characterController.TurnBack();
    }

    private void HandleCameraFollow()
    {
        MainCamFollow.StartFollow();
    }
    private void HandleCameraStop()
    {
        MainCamFollow.StopFollow();
    }
}
