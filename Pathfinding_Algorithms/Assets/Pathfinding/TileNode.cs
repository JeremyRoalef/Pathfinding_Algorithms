using UnityEngine;

public class TileNode
{
    //TileNode has a position, parent node (for path construction), explored status,
    //and isWalkable status

    public TileNode parent;
    public Vector2Int position;
    public bool isExplored;
    public bool isWalkable;
    public GameObject tileObj;

    public TileNode(Vector2Int position, bool isWalkable)
    {
        this.position = position;
        isExplored = false;
        this.isWalkable = isWalkable;
        parent = null;
        tileObj = null;
    }

}
