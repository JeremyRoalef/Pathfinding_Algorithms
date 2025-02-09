using System;
using System.Collections.Generic;
using UnityEngine;

/*
 * Algorithm works by exploring nodes with the least travel weight to find the end node
 * 
 * Key differences between for Dijkstra: Nodes are reached when 
 */
public class Dijkstras_Algorithm : MonoBehaviour
{
    [SerializeField]
    Vector2Int startCoordinates;

    [SerializeField]
    Vector2Int endCoordinates;

    [SerializeField]
    GridManager_Dijkstra gridManager;

    [SerializeField]
    Material pathMat;

    MinPriorityQueue<TileNode_Dijkstra> frontier = new MinPriorityQueue<TileNode_Dijkstra>();
    Dictionary<Vector2Int, TileNode_Dijkstra> reached = new Dictionary<Vector2Int, TileNode_Dijkstra>();
    Dictionary<Vector2Int, TileNode_Dijkstra> grid;
    Vector2Int[] directions = {
        new Vector2Int(1,0), //right
        new Vector2Int(-1,0), //left
        new Vector2Int(0,1), //up
        new Vector2Int(0,-1), //down
    };

    bool pathExists = false;

    private void Awake()
    {
        if (!CheckIfCoordinatesInGridRange())
        {
            Debug.Log("Start or end coordinates not in range of the grid size. Disabling game object");
            gameObject.SetActive(false);
        }

        if (gridManager == null)
        {
            Debug.Log("No grid manager given. Disabling game object");
            gameObject.SetActive(false);
        }

        grid = gridManager.Grid;
    }

    private void Start()
    {
        List<TileNode_Dijkstra> path = DoDijkstrasAlgorithm();
        if (pathExists)
        {
            Debug.Log("Path generated");
            foreach (TileNode_Dijkstra node in path)
            {
                Debug.Log(node.position);
            }
        }
        else
        {
            Debug.Log("Path does not exist");
        }
    }

    private List<TileNode_Dijkstra> DoDijkstrasAlgorithm()
    {
        //The weight of the start node needs to be set to 0 for algorithm to function
        grid[startCoordinates].weight = 0;

        List<TileNode_Dijkstra> path = new List<TileNode_Dijkstra>();

        /*
         * 1) start at first node. Set node to reached, and add to queue for neighbor exploration.
         *    If the start node is the same as the end node, then the path is just the start node &
         *    you can exit the method early.
         * 2) While the frontier queue is not empty, dequeue the next node as the current node, explore
         *    its neighbors (new method), and connecting the neighbor node to the current node if its weight is smaller.
         *    Add node to reached dictionary. If the node is the end node. Exit early and build the path from the end node
         *    to the current node.
         * 3) If queue is empty and end node has not been reached, then there is no possible path.
         *    If end node has been reached, the order of the path will be created in reversed order
         *    and the path will have to be reversed.
         */

        //1)
        TileNode_Dijkstra currentNode = grid[startCoordinates]; //start node
        TileNode_Dijkstra endNode;

        if (currentNode.position == endCoordinates)
        {
            pathExists = true;
            return BuildPath(currentNode);
        }

        frontier.Enqueue(currentNode);

        //2)
        while (!frontier.IsEmpty())
        {
            currentNode = frontier.Dequeue();

            reached.Add(currentNode.position, currentNode);
            endNode = ExploreNeighbors(currentNode);
            if (endNode != null)
            {
                //Debug.Log("End node found. Building path & returning");
                pathExists = true;
                path = BuildPath(endNode);
                return path;
            }
        }

        //3)
        Debug.Log("No path found.");
        return null;
    }


    TileNode_Dijkstra ExploreNeighbors(TileNode_Dijkstra parentNode)
    {
        //Debug.Log($"Exploring neighbors from position {parentNode.position}");

        //Get neighboring node's coordinates using the directions list from above
        //Check if the node is in the grid (edge nodes will have null neighbors) and if it
        //is walkable.
        //If the coordinates are end coordinates, reutrn the node (end node)

        Vector2Int neighborCoordinates;

        foreach (Vector2Int dir in directions)
        {
            neighborCoordinates = parentNode.position + dir;
            //Debug.Log($"Checking node at position {neighborCoordinates}");

            if (neighborCoordinates == endCoordinates)
            {
                //Debug.Log($"Neighbor coordinates at {neighborCoordinates} is end coordinates.");
                grid[neighborCoordinates].parent = parentNode;
                return grid[neighborCoordinates];
            }

            if (!grid.ContainsKey(neighborCoordinates) || reached.ContainsKey(neighborCoordinates))
            {
                //Debug.Log($"Node at coordinates {neighborCoordinates} doesn't exist, or node has already been explored");
                continue;
            }

            if (grid[neighborCoordinates].tileObj == null)
            {
                //Debug.Log($"Node at position {neighborCoordinates} does not reference a game object");
                continue;
            }

            TileNode_Dijkstra neighborNode = grid[neighborCoordinates];

            //If the neighbor is walkable and moving from parent node to neighbor is less resistant, connect neighbor to parent,
            //add neighbor to queue, & set the weight to the calculated less resistant weight
            if (neighborNode.isWalkable && neighborNode.weight > (parentNode.weight + neighborNode.costToTravel))
            {
                neighborNode.parent = parentNode;
                neighborNode.weight = parentNode.weight + neighborNode.costToTravel;
                frontier.Enqueue(neighborNode);
            }
            else
            {
                //Debug.Log($"Node at position {neighborCoordinates} is not walkable, or it's part of a less-resistant path");
            }
        }

        //End node has not been found
        return null;
    }

    bool CheckIfCoordinatesInGridRange()
    {
        //Check if start & end coordinates is in range of the grid size. Coordinates cannot go below 0
        if (startCoordinates.x < 0 || startCoordinates.x > gridManager.Length ||
            endCoordinates.x < 0 || endCoordinates.x > gridManager.Length ||
            startCoordinates.y < 0 || startCoordinates.y > gridManager.Width ||
            endCoordinates.y < 0 || endCoordinates.y > gridManager.Width)
        {
            return false;
        }
        return true;
    }

    List<TileNode_Dijkstra> BuildPath(TileNode_Dijkstra endNode)
    {
        //Path will be built from end to start. Need to reverse the path to get start to end
        List<TileNode_Dijkstra> path = new List<TileNode_Dijkstra>();

        TileNode_Dijkstra currentNode = endNode;
        path.Add(currentNode);
        currentNode.tileObj.GetComponent<MeshRenderer>().material = pathMat;

        while (currentNode.parent != null)
        {
            currentNode = currentNode.parent;
            path.Add(currentNode);
            currentNode.tileObj.GetComponent<MeshRenderer>().material = pathMat;
        }

        path.Reverse();
        return path;
    }
}
