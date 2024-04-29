using OpenCover.Framework.Model;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class MazeGenerator : MonoBehaviour
{
    // reference to the prefab used to instantiate maze cells
    [SerializeField]
    private MazeCell _mazeCellPrefab;

    [SerializeField]
    private int _mazeWidth;

    [SerializeField]
    private int _mazeDepth;

    //2D array to store the maze grid
    private MazeCell[,] _mazeGrid;
    public NavMeshSurface _surface;


    void Start()
    {
        //initialize maze grid
        _mazeGrid = new MazeCell[_mazeWidth, _mazeDepth];

        //instantiate maze cells in a grid pattern
        for (int x = 0; x < _mazeWidth; x++)
        {
            for (int z = 0; z < _mazeDepth; z++)
            {
                _mazeGrid[x, z] = Instantiate(_mazeCellPrefab, new Vector3(x, 0, z), Quaternion.identity);

            }
        }

        //generate maze
        GenerateMaze(null, _mazeGrid[0, 0]);

        _surface.BuildNavMesh();

    }
    // method to generate the maze
    private void GenerateMaze(MazeCell previousCell, MazeCell currentCell)
    {
        //mark current cell as visited
        currentCell.Visit();

        //remove walls between the current and previous cells
        ClearWalls(previousCell, currentCell);

        //get next unvisited cell
        MazeCell nextCell;

        do
        {
            nextCell = GetNextUnvisitedCell(currentCell);

            if (nextCell != null)
            {
                //recursively generate maze starting from the next cell
                GenerateMaze(currentCell, nextCell);
            }
        } while (nextCell != null);
    }
    //method to get next unvisited cell adjacent to the current cell
    private MazeCell GetNextUnvisitedCell(MazeCell currentCell)
    {
        var unvisitedCells = GetUnvisitedCells(currentCell);

        return unvisitedCells.OrderBy(_ => Random.Range(1, 10)).FirstOrDefault();
    }

    //method to get unvisited neighboring cells of the current cell
    private IEnumerable<MazeCell> GetUnvisitedCells(MazeCell currentCell)
    {
        int x = (int)currentCell.transform.position.x;
        int z = (int)currentCell.transform.position.z;

        if (x + 1 < _mazeWidth)
        {
            var cellToRight = _mazeGrid[x + 1, z];

            if (cellToRight.isVisited == false)
            {
                yield return cellToRight;
            }
        }

        if (x - 1 >= 0)
        {
            var cellToLeft = _mazeGrid[x - 1, z];

            if (cellToLeft.isVisited == false)
            {
                yield return cellToLeft;
            }
        }

        if (z + 1 < _mazeDepth)
        {
            var cellToFront = _mazeGrid[x, z + 1];

            if (cellToFront.isVisited == false)
            {
                yield return cellToFront;
            }
        }

        if (z - 1 >= 0)
        {
            var cellToBack = _mazeGrid[x, z - 1];

            if (cellToBack.isVisited == false)
            {
                yield return cellToBack;
            }
        }
    }

    //method to clear walls between two cells that are next to each other
    private void ClearWalls(MazeCell previousCell, MazeCell currentCell)
    {
        if (previousCell == null)
        {
            return;
        }

        if (previousCell.transform.position.x < currentCell.transform.position.x)
        {
            previousCell.ClearRightWall();
            currentCell.ClearLeftWall();
            return;
        }

        if (previousCell.transform.position.x > currentCell.transform.position.x)
        {
            previousCell.ClearLeftWall();
            currentCell.ClearRightWall();
            return;
        }

        if (previousCell.transform.position.z < currentCell.transform.position.z)
        {
            previousCell.ClearFrontWall();
            currentCell.ClearBackWall();
            return;
        }

        if (previousCell.transform.position.z > currentCell.transform.position.z)
        {
            previousCell.ClearBackWall();
            currentCell.ClearFrontWall();
            return;
        }
    }

}