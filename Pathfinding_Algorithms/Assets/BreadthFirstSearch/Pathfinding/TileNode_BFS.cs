using UnityEngine;

public class TileNode_BFS
{
    //TileNode_BFS has a position, parent node (for path construction), explored status,
    //and isWalkable status

    public TileNode_BFS parent;
    public Vector2Int position;
    public bool isExplored;
    public bool isWalkable;
    public GameObject tileObj;

    public TileNode_BFS(Vector2Int position, bool isWalkable)
    {
        this.position = position;
        isExplored = false;
        this.isWalkable = isWalkable;
        parent = null;
        tileObj = null;
    }

}
