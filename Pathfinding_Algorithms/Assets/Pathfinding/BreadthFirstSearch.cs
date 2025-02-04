using System;
using System.Collections.Generic;
using UnityEngine;

public class BreadthFirstSearch : MonoBehaviour
{
    [SerializeField]
    Vector2Int startCoordinates;

    [SerializeField]
    Vector2Int endCoordinates;

    [SerializeField]
    GridManager gridManager;

    [SerializeField]
    Color pathColor = Color.green;

    //Breadth-First Search requires a queue of nodes to explore, a dictionary of nodes that have been
    //reached, and the grid it will be exploring. Exploration happens based on given criteria
    //(i.e. left, right, up, down).

    Queue<TileNode> frontier = new Queue<TileNode>();
    Dictionary<Vector2Int, TileNode> reached = new Dictionary<Vector2Int, TileNode>();
    Dictionary<Vector2Int, TileNode> grid;
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

    void Start()
    {
        List<TileNode> path = DoBreadthFirstSearch();
        if (pathExists)
        {
            Debug.Log("Path generated");
            foreach (TileNode node in path)
            {
                Debug.Log(node.position);
            }
        }
        else
        {
            Debug.Log("Path does not exist");
        }
    }

    List<TileNode> DoBreadthFirstSearch()
    {
        List<TileNode> path = new List<TileNode>();

        /*
         * 1) start at first node. Set node to reached, and add to queue for neighbor exploration.
         *    If the start node is the same as the end node, then the path is just the start node &
         *    you can exit the method early.
         * 2) While the frontier queue is not empty, dequeue the next node as the current node, explore
         *    its neighbors (new method) and connect those nodes to the current node. Add node to reached
         *    dictionary. If the node is the end node. Exit early and build the path from the end node
         *    to the current node.
         * 3) If queue is empty and end node has not been reached, then there is no possible path.
         *    If end node has been reached, the order of the path will be created in reversed order
         *    and the path will have to be reversed.
         */

        //1)
        TileNode currentNode = grid[startCoordinates]; //start node
        TileNode endNode;

        reached.Add(startCoordinates, currentNode);

        if (currentNode.position == endCoordinates)
        {
            path.Add(currentNode);
            return path;
        }

        frontier.Enqueue(currentNode);

        //2)
        while (frontier.Count > 0)
        {
            currentNode = frontier.Dequeue();
            endNode = ExploreNeighbors(currentNode);
            if (endNode != null)
            {
                //End node found
                pathExists = true;
                Debug.Log("End node found. Building path & returning");
                path = BuildPath(endNode);
                return path;
            }
        }

        //3)
        Debug.Log("No path found.");
        return null;
    }

    TileNode ExploreNeighbors(TileNode parentNode)
    {
        //Get neighboring node's coordinates using the directions list from above
        //Check if the node is in the grid (edge nodes will have null neighbors) and if it
        //is walkable.
        //If the coordinates are end coordinates, reutrn the node (end node)

        Vector2Int neighborCoordinates;
        
        foreach (Vector2Int dir in directions)
        {
            neighborCoordinates = parentNode.position + dir;
            
            if (neighborCoordinates == endCoordinates)
            {
                //End node has been found
                grid[neighborCoordinates].parent = parentNode;
                return grid[neighborCoordinates];
            }

            if (!grid.ContainsKey(neighborCoordinates) || reached.ContainsKey(neighborCoordinates))
            {
                //Node doesn't exists in grid, or it has already been added to queue
                continue;
            }

            if (grid[neighborCoordinates].tileObj == null)
            {
                //Node is empty (gap in the grid)
                continue;
            }

            if (grid[neighborCoordinates].isWalkable)
            {
                TileNode neighborNode = grid[neighborCoordinates];
                reached.Add(neighborCoordinates, neighborNode);
                neighborNode.parent = parentNode;
                frontier.Enqueue(neighborNode);
            }
        }

        //End node has not been found
        return null;
    }

    List<TileNode> BuildPath(TileNode endNode)
    {
        //Path will be built from end to start. Need to reverse the path to get start to end
        List<TileNode> path = new List<TileNode>();

        TileNode currentNode = endNode;
        path.Add(currentNode);
        currentNode.tileObj.GetComponent<MeshRenderer>().material.color = pathColor;

        while (currentNode.parent != null)
        {
            currentNode = currentNode.parent;
            path.Add(currentNode);
            currentNode.tileObj.GetComponent<MeshRenderer>().sharedMaterial.color = pathColor;
        }

        path.Reverse();
        return path;
    }
}
