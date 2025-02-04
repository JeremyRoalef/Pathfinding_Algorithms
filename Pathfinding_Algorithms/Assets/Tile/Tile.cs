using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField]
    bool isWalkable;

    [SerializeField]
    Color notWalkable = Color.black;

    [SerializeField]
    MeshRenderer renderer;

    GridManager gridManager;

    private void Start()
    {
        gridManager = FindObjectOfType<GridManager>();
        if (gridManager == null)
        {
            Debug.Log("No grid manager found");
        }

        //If tile is not walkable, update the tile node at this position to not walkable
        if (!isWalkable)
        {
            gridManager.Grid[GridManager.GetCoordinatesFromPosition(transform.position)].isWalkable = false;
            renderer.material.color = notWalkable;
            //Debug.Log(gridManager.Grid[GridManager.GetCoordinatesFromPosition(transform.position)].position);
            //Debug.Log(gridManager.Grid[GridManager.GetCoordinatesFromPosition(transform.position)].isWalkable);
        }
    }
}
