using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentTile : MonoBehaviour
{
    public List<EnvironmentTile> Connections { get; set; }
    public EnvironmentTile Parent { get; set; }
    public Vector3 Position { get; set; }
    public float Global { get; set; }
    public float Local { get; set; }
    public bool Visited { get; set; }
    public bool IsAccessible { get; set; }
    public bool IsDoor { get; set; }

    [SerializeField] private Animator animControl;


    [SerializeField] private GameObject RotationBlock;

    [SerializeField] private GameObject Wall;
    [SerializeField] private GameObject Door;

    public void SetDoorOn()
    {
        IsDoor = true;
        Wall.SetActive(false);
        Door.SetActive(true);
    }
    public void TriggerDoorRotation()
    {
        animControl.SetTrigger("Open");
    }
    public Vector3 GetRotationBlockPosition(){ return RotationBlock.transform.position; }
}
