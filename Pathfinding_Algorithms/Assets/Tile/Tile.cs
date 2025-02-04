using System;
using TMPro;
using UnityEngine;

[ExecuteAlways]
public class Tile : MonoBehaviour
{
    [SerializeField]
    TextMeshPro coordinatesText;

    [SerializeField]
    bool isWalkable;

    [SerializeField]
    MeshRenderer renderer;

    [SerializeField]
    Color color1;

    [SerializeField]
    Color color2;

    [SerializeField]
    Color notWalkableColor = Color.black;

    GridManager gridManager;

    private void Start()
    {
        if (!Application.isPlaying) { return; }

        SetObjectCoordinates();
        InitializeGridColors();

        gridManager = FindObjectOfType<GridManager>();
        if (gridManager == null)
        {
            Debug.Log("No grid manager found");
        }

        gridManager.Grid[GridManager.GetCoordinatesFromPosition(transform.position)].tileObj = gameObject;

        //If tile is not walkable, update the tile node at this position to not walkable
        if (!isWalkable)
        {
            gridManager.Grid[GridManager.GetCoordinatesFromPosition(transform.position)].isWalkable = false;
            //Debug.Log(gridManager.Grid[GridManager.GetCoordinatesFromPosition(transform.position)].position);
            //Debug.Log(gridManager.Grid[GridManager.GetCoordinatesFromPosition(transform.position)].isWalkable);
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
            renderer.sharedMaterial.color = color1;
        }
        else
        {
            renderer.sharedMaterial.color = color2;
        }

        if (!isWalkable)
        {
            renderer.sharedMaterial.color = notWalkableColor;
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
