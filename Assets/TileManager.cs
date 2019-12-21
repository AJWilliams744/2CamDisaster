using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    [SerializeField] private Vector3 baseTileLocation = new Vector3(-100, 0, -70);
    [SerializeField] private int range = 5;
    [SerializeField] private Environment enviroment;
    
    //public Vector2 GetMapCoordinates(EnvironmentTile tile)
    //{
    //    Vector3 CurrentLocation = tile.Position - baseTileLocation;
    //    Vector2 CurrentCoorLocation = new Vector2(CurrentLocation.x, CurrentLocation.z);

    //    return CurrentCoorLocation;
    //}

    public void HideSurroundingTiles(Vector2Int currentCoor)
    {
        //Min Max
        Vector2Int X = new Vector2Int(0,0);
        Vector2Int Y = new Vector2Int(0,0);

        // removes the need for constant -1 each value fetch
        Vector2Int maxTiles = enviroment.GetMaxTiles() - new Vector2Int(1,1);

        X[0] = currentCoor[0] - range < 0 ? 0 : currentCoor[0] - range;
        Y[0] = currentCoor[1] - range < 0 ? 0 : currentCoor[1] - range;

        X[1] = currentCoor[0] + range > maxTiles[0] ? maxTiles[0] : currentCoor[0] + range;
        Y[1] = currentCoor[1] + range > maxTiles[1] ? maxTiles[1] : currentCoor[1] + range;

        //Debug.LogError(X[1]);

        UpdateTiles(X, Y);
        UpdateTiles(Y, X);
        
    }
    private void UpdateTiles(Vector2Int LoopValue, Vector2Int ChangeValue) 
    {
        for (int i = LoopValue[0]; i < LoopValue[1]; i++)
        {
            enviroment.SetTileVisibility(new Vector2Int(i, ChangeValue[0]), false);
            enviroment.SetTileVisibility(new Vector2Int(i, ChangeValue[1]), false);
            Debug.LogError(i + " : " + ChangeValue[0]);
        }
    }
    
}
