using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class Test : MonoBehaviour
{

    List<int> testInt = new List<int>();

    void Start()
    {
        testInt.Add(1);
        testInt.Add(2);
        testInt.Insert(0, 3);

        foreach (int i in testInt)
        {
            Debug.Log(i);
        }
    }


    void Update()
    {
        
    }
}
