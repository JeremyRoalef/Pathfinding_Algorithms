using TMPro;
using UnityEngine;

[ExecuteAlways]
public class CoordinateLabeler : MonoBehaviour
{
    [SerializeField]
    TextMeshPro coordinatesText;

    [SerializeField]
    MeshRenderer renderer;

    [SerializeField]
    Color color1;

    [SerializeField]
    Color color2;

    void Update()
    {
        if (!Application.isPlaying)
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

            //Additional modifications

            //Creating checkered pattern for the tiles to better visualize nodes
            if ((tileCoordinates.x + tileCoordinates.y) % 2 > 0)
            {
                renderer.sharedMaterial.color = color1;
            }
            else
            {
                renderer.sharedMaterial.color = color2;
            }
        }
    }
}
