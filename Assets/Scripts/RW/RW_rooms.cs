using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RW_rooms : MonoBehaviour
{
    public Sprite proxy;
    public int upperbound;
    public int segments;
    public List<Vector4> bounds = new List<Vector4>();

    private void Awake()
    {
        SetBounds(new Vector2(0, 0), upperbound, segments);
        foreach (var a in bounds)
        {
            DrawImage(a);
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
        for (int i = 1; i <= segments; i++)
        {
            int newX = maxVal * i / segments;
            for (int j = 1; j <= segments; j++)
            {
                int newY = maxVal * j / segments;
                bounds.Add(new Vector4(leftDown.x, leftDown.y, newX, newY));
                leftDown.y = newY;
            }

            leftDown.x = newX;
            leftDown.y = 0;
        }
    }

    public void DrawImage(Vector4 vec)
    {
        GameObject o = new GameObject();
        var h = vec.z - vec.x;
        var i = vec.w - vec.y;
        var a = vec.x + 0.5 * h;
        var b = vec.y + 0.5 * i;
        //print("x: " + a + "y: " + b);
        var j = new Vector3((float) a, (float) b, 0);
        o.transform.position = j;
        var g = o.AddComponent<SpriteRenderer>();
        g.sprite = proxy;
    }

    public Vector3 GetCentre(Vector4 vec)
    {
        float xDistance = vec.z - vec.x;
        float yDistance = vec.w - vec.y;
        float x = (float) (vec.x + 0.5 * xDistance);
        float y = (float) (vec.y + 0.5 * yDistance);
        return new Vector3(x, y, 0);
    }

    /*public void setupGrid()
    {
        tiles = new GameObject[collumns, rows];
        for (int i = 0; i < collumns; ++i)
        {
            for (int j = 0; j < rows; ++j)
            {
                tiles[i, j] = new GameObject();
                //  print("entry created at: " + j + " " + i);
                instantiate_images(tiles[i, j], i, j);
            }
        }
    }*/

    public void instantiate_images(GameObject empty, int x, int y)
    {
        empty.name = x.ToString() + y.ToString();
        empty.transform.position = new Vector3(x - (0.5f), y - (0.5f), 0);
    }
}