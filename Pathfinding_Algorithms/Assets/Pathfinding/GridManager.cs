using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField]
    [Min(1)]
    int length;

    [SerializeField]
    [Min(1)]
    int width;

    Dictionary<Vector2Int, TileNode> grid = new Dictionary<Vector2Int, TileNode>();
    public Dictionary<Vector2Int, TileNode> Grid
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
                TileNode newNode = new TileNode(new Vector2Int(i, j), true);
                grid[newNode.position] = newNode;
            }
        }

        //Debug.Log("Grid created: ");
        //foreach (TileNode tileNode in grid.Values)
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
