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
        // Check for at least one open direction for walkable area
        return !hasNorthWall || !hasSouthWall || !hasEastWall || !hasWestWall;
    }

    // Methods to clear specific walls (implement based on your cell visuals)
    public void ClearNorthWall() { hasNorthWall = false; }
    public void ClearSouthWall() { hasSouthWall = false; }
    public void ClearEastWall() { hasEastWall = false; }
    public void ClearWestWall() { hasWestWall = false; }
}
