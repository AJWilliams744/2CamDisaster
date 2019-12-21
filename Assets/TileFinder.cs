using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileFinder : MonoBehaviour
{
    [SerializeField] private Camera MainCamera;
    private RaycastHit[] mRaycastHits;

    private readonly int NumberOfRaycastHits = 1;

    // Start is called before the first frame update
    void Start()
    {
        mRaycastHits = new RaycastHit[NumberOfRaycastHits];
    }

    public EnvironmentTile GetMouseRayTargetTile()
    {
        Ray screenHit = MainCamera.ScreenPointToRay(Input.mousePosition);
        int hits = Physics.RaycastNonAlloc(screenHit, mRaycastHits);
        if (hits > 0)
        {            
            EnvironmentTile tile = mRaycastHits[0].transform.GetComponent<EnvironmentTile>();
            if (tile != null)
            {
                return tile;
            }           
        }
        return null;
    }
}
