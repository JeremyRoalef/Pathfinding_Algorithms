using TMPro;
using UnityEngine;

[ExecuteAlways]
public class Tile_Dijkstra : MonoBehaviour
{
    [SerializeField]
    TextMeshPro coordinatesText;

    [SerializeField]
    bool isWalkable;

    [SerializeField]
    MeshRenderer renderer;

    [SerializeField]
    Material CheckeredMat1;

    [SerializeField]
    Material CheckeredMat2;

    [SerializeField]
    Material notWalkableMat;

    GridManager_Dijkstra gridManager;

    private void Start()
    {
        renderer = GetComponent<MeshRenderer>();

        if (!Application.isPlaying)
        {
            return;
        }

        SetObjectCoordinates();
        InitializeGridColors();

        gridManager = FindObjectOfType<GridManager_Dijkstra>();
        if (gridManager == null)
        {
            Debug.Log("No grid manager found");
        }


        if (!gridManager.Grid.ContainsKey(GridManager_BFS.GetCoordinatesFromPosition(transform.position)))
        {
            Debug.Log($"Tile at coordinates {GridManager_BFS.GetCoordinatesFromPosition(transform.position)} does not" +
                $"exist. Disabling game object");
            gameObject.SetActive(false);
        }
        else
        {
            gridManager.Grid[GridManager_BFS.GetCoordinatesFromPosition(transform.position)].tileObj = gameObject;
        }



        //If tile is not walkable, update the tile node at this position to not walkable
        if (!isWalkable)
        {
            gridManager.Grid[GridManager_BFS.GetCoordinatesFromPosition(transform.position)].isWalkable = false;
            //Debug.Log(gridManager.Grid[GridManager_BFS.GetCoordinatesFromPosition(transform.position)].position);
            //Debug.Log(gridManager.Grid[GridManager_BFS.GetCoordinatesFromPosition(transform.position)].isWalkable);
        }
    }

    private void InitializeGridColors()
    {
        Vector2Int tileCoordinates = new Vector2Int(
            (int)transform.position.x,
            (int)transform.position.z
            );

        //Creating checkered pattern for the tiles to better visualize nodes
        if ((tileCoordinates.x + tileCoordinates.y) % 2 > 0)
        {
            renderer.sharedMaterial = CheckeredMat1;
        }
        else
        {
            renderer.sharedMaterial = CheckeredMat2;
        }

        if (!isWalkable)
        {
            renderer.sharedMaterial = notWalkableMat;
            return;
        }
    }

    private void Update()
    {
        if (Application.isPlaying) { return; }
        SetObjectCoordinates();
        InitializeGridColors();
    }

    private void SetObjectCoordinates()
    {
        //Display the coordinates from real-world position
        //x-coordinate corresponds to parent x-coordinate
        //y-coordinate corresponds to parent z-coordinate

        //Storing the x,y coordinates
        Vector2Int tileCoordinates = new Vector2Int(
            (int)transform.position.x,
            (int)transform.position.z
            );

        //Creating the coordinate string
        string coordinates = "(" + tileCoordinates.x + "," +
            tileCoordinates.y + ")";

        gameObject.name = coordinates;
        coordinatesText.text = coordinates;
    }
}
