using System.Collections.Generic;
using UnityEngine;

public class GridManager_Dijkstra : MonoBehaviour
{
    [SerializeField]
    [Min(1)]
    int length;

    public int Length
    {
        get { return length; }
    }

    [SerializeField]
    [Min(1)]
    int width;

    public int Width
    {
        get { return width; }
    }

    Dictionary<Vector2Int, TileNode_BFS> grid = new Dictionary<Vector2Int, TileNode_BFS>();
    public Dictionary<Vector2Int, TileNode_BFS> Grid
    {
        get { return grid; }
    }

    private void Awake()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        //Create the grid of nodes
        for (int i = 0; i < length; i++)
        {
            for (int j = 0; j < width; j++)
            {
                //Create node at given position
                //Update second argument when feature is added.
                TileNode_BFS newNode = new TileNode_BFS(new Vector2Int(i, j), true);
                grid[newNode.position] = newNode;
            }
        }

        //Debug.Log("Grid created: ");
        //foreach (TileNode_BFS tileNode in grid.Values)
        //{
        //    Debug.Log(tileNode.position);
        //}

        //Debug.Log("Getting node at (1,3)");
        //Debug.Log(grid[new Vector2Int(1, 3)].position);
    }

    public static Vector2Int GetCoordinatesFromPosition(Vector3 pos)
    {
        Vector2Int coordinates = new Vector2Int(
            (int)pos.x,
            (int)pos.z
            );
        return coordinates;
    }
    public static Vector3 GetPositionFromCoordinates(Vector2Int coordinates)
    {
        Vector3 pos = new Vector3(coordinates.x, 0, coordinates.y);
        return pos;
    }
}
