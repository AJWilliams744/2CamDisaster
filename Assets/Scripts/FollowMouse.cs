using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    [SerializeField]
    [Range(5,15)]
    private float Height;

    [SerializeField] private TileFinder tileFinder;

    [SerializeField] private Camera MainCamera;
    private RaycastHit[] mRaycastHits;

    private readonly int NumberOfRaycastHits = 1;

    // Start is called before the first frame update
    void Start()
    {
        mRaycastHits = new RaycastHit[NumberOfRaycastHits];
    }

    // Update is called once per frame
    void Update()
    {
        //Same Code from mouse Click

        //EnvironmentTile tile = tileFinder.GetMouseRayTargetTile();
        //if (tile != null)
        //{
        //    Vector3 tempVec = tile.Position;
        //    tempVec.y = Height;
        //    transform.position = Vector3.Lerp(transform.position,tempVec, Time.deltaTime * 3);           
        //}
        RaycastHit hit;
        Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 tempVec = hit.point;
            tempVec.y = Height;
            transform.position = Vector3.Lerp(transform.position,tempVec, Time.deltaTime * 3); 

            
        }
    }
}
