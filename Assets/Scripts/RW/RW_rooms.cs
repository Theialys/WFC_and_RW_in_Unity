using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RW_rooms : MonoBehaviour
{
    public Sprite proxy;
    public int upperbound;
    public int segments;
    public int gridsteps;
    public List<Vector4> bounds = new List<Vector4>();

    private void Awake()
    {
        SetBounds(new Vector2(0, 0), upperbound, segments);
        foreach (var a in bounds)
        {
            print(a);
           // DrawImage(a);
            //var a = new Vector4(1, 2, 3, 4);
        }
    }

    /// <summary>
    /// fill a list with bounds. Later on this will be for spatial partitioning with Octree prolly
    /// </summary>
    /// <param name="leftDown"></param>
    /// <param name="maxVal"></param>
    /// <param name="segments"></param>
    public void SetBounds(Vector2 leftDown, int maxVal, int segments)
    {
        
        Vector2 leftCorner = leftDown;
        for (int i = 0; i < maxVal; i += gridsteps)
        {
            print(maxVal*i/segments);
            int newX = maxVal * i / segments;
            for (int j = 0; j < maxVal; j += gridsteps)
            {
                int newY = maxVal * j / segments;
                bounds.Add(new Vector4(leftCorner.x, leftCorner.y, newX, newY));
                leftCorner.y = newY;
            }

            leftCorner.x = newX;
        }
    }

    public void DrawImage(Vector4 vec)
    {
        GameObject o = new GameObject();
        var h = vec.z-vec.x;
        var i = vec.w-vec.y;
        var a = vec.x + 0.5*h;
        var b = vec.y +  0.5*i;
        print("x: " + a + "y: " + b);
        var j = new Vector3((float)a, (float)b, 0);
        o.transform.position = j;
        var g = o.AddComponent<SpriteRenderer>();
        g.sprite = proxy;
    }
}