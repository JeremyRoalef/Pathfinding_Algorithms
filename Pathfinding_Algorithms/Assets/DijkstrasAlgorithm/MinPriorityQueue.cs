using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

//This minimum priority queue uses the old structure for Dijkstra's algorithm. To make more efficient, implement heap data structure
//For purposes of getting this pathfinding algorithm working in reasonable time, I will focus on the old structure.
public class MinPriorityQueue<T> where T : IComparable<T>
{
    private List<T> queue;

    public MinPriorityQueue() : this(new List<T>())
    {

    }
    public MinPriorityQueue (List<T> queue)
    {
        this.queue = queue;
    }

    /*
     * Methods for queue:
     *  1) Enqueue
     *  2) Dequeue
     *  3) IsEmpty
     */

    public void Enqueue(T item)
    {
        //Add item such that the smallest item is at index 0 and largest item is at the last index

        //No items in queue condition
        if (queue.Count == 0)
        {
            queue.Add(item); //Nothing in queue
            return;
        }

        //Condition to insert between items in queue
        for (int i = 0; i < queue.Count; i++)
        {
            if (item.CompareTo(queue[i]) <= 0)
            {
                queue.Insert(i, item);
                return;
            }
        }

        //Looped through every item and never inserted item. Add at end
        queue.Add(item);
    }

    public T Dequeue()
    {
        if (queue.Count == 0)
        {
            //Return the default data type of T (should be null)
            return default(T);
        }

        //C# list does not have pop method. I hate this language.
        T item = queue[0];
        queue.RemoveAt(0);
        return item;
    }

    public bool IsEmpty()
    {
        return queue.Count == 0;
    }
}