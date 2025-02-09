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

    Dictionary<Vector2Int, TileNode_Dijkstra> grid = new Dictionary<Vector2Int, TileNode_Dijkstra>();
    public Dictionary<Vector2Int, TileNode_Dijkstra> Grid
    {
        get { return grid; }
    }

    private void Awake()
    {
        GenerateGrid();
    }

    private void Start()
    {
        //DebugNodes();
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
                TileNode_Dijkstra newNode = new TileNode_Dijkstra(new Vector2Int(i, j), true);
                grid[newNode.position] = newNode;
            }
        }
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

    void DebugNodes()
    {
        //Test node position, cost to travel, and weight

        foreach (KeyValuePair<Vector2Int, TileNode_Dijkstra> keyValuePair in grid)
        {
            Debug.Log($"Node at position {keyValuePair.Key}:\n" +
                $"Node Weight: {keyValuePair.Value.weight}\n" +
                $"Cost to Travel: {keyValuePair.Value.costToTravel}\n");
        }
    }
}
