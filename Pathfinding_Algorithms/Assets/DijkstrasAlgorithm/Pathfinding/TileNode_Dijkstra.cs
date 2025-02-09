using System;
using UnityEngine;

public class TileNode_Dijkstra : IComparable<TileNode_Dijkstra>
{
    //TileNode_Dijkstra has a travel cost, a weight (represents the minimum path distance to travel to this node)
    //parent node, walkable status, position, and reference to the game object.

    public float costToTravel;
    public float weight;
    public bool isWalkable;

    public Vector2Int position;

    public TileNode_Dijkstra parent;
    public GameObject tileObj;

    public TileNode_Dijkstra(Vector2Int position, bool isWalkable)
    {
        this.costToTravel = 0;

        //Weight value set to infinity b/c of first-time traversal to node.
        this.weight = Mathf.Infinity; 
        this.isWalkable = isWalkable;

        this.position = position;

        parent = null;
        tileObj = null;
    }


    //CompareTo method will return the smaller weight value between two tile nodes. Will be used to
    //Sort the min priority queue.
    public int CompareTo(TileNode_Dijkstra other)
    {
        /*
         * Return -1: this node is smaller than other node.
         * Return 0: this node is equal to other node (order is irrelevant)
         * Return 1: this node is greater than other node.
         */

        if (other == null) return 1;
        return this.weight.CompareTo(other.weight);
    }
}
