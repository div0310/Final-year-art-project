using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeCellController : MonoBehaviour
{
    public Vector3 position;
    public bool hasNorthWall;
    public bool hasSouthWall;
    public bool hasEastWall;
    public bool hasWestWall;

    //initialize cells position
    public MazeCellController(Vector3 position)
    {
        this.position = position;
        hasNorthWall = true;
        hasSouthWall = true;
        hasEastWall = true;
        hasWestWall = true;
    }

    public bool HasWalkableArea()
    {
        // check for at least one open direction for walkable area
        return !hasNorthWall || !hasSouthWall || !hasEastWall || !hasWestWall;
    }

    // methods to clear specific walls 
    public void ClearNorthWall() { hasNorthWall = false; }
    public void ClearSouthWall() { hasSouthWall = false; }
    public void ClearEastWall() { hasEastWall = false; }
    public void ClearWestWall() { hasWestWall = false; }
}
