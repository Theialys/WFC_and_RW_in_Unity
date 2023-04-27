using System;
using System.Collections.Generic;
using UnityEngine;

public class RW_rooms : MonoBehaviour
{
    public Sprite proxy;
    public int upperbound;
    public int segments;
    public int roomCollumns, roomRows;
    public List<Vector4> bounds = new List<Vector4>();
    public List<RW_Parents> parents;
    public int steps;

    private void Awake()
    {
        SetBounds(new Vector2(0, 0), upperbound, segments);
        foreach (var a in bounds)
        {
            Setup_parent(a);
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

    public void Setup_parent(Vector4 vec)
    {
        GameObject o = new GameObject();
        var data = GetBoundData(vec);
        float width = data.Item1;
        float height = data.Item2;
        Vector3 pos = data.Item3;
        o.transform.position = pos;
        RW_Parents parentScript = o.AddComponent<RW_Parents>();
        parentScript.FillScript(width, height, pos,roomCollumns,roomRows);
        parentScript.SetupGrid();
        parentScript.rw.tiles = parentScript.tiles;
        parentScript.rw.defaultSprite = proxy;
        parentScript.rw.collumns = roomCollumns;
        parentScript.rw.rows = roomRows;
        
        parentScript.rw.randomWalk(steps, o.GetComponent<RW_Parents>().tiles[0,0], 0, 0);
    }

    public Tuple<float, float, Vector3> GetBoundData(Vector4 vec)
    {
        float xDistance = vec.z - vec.x;
        float yDistance = vec.w - vec.y;
        float x = (float) (vec.x + 0.5 * xDistance);
        float y = (float) (vec.y + 0.5 * yDistance);
        return new Tuple<float, float, Vector3>(xDistance, yDistance, new Vector3(x, y, 0));
    }
    

    public void instantiate_images(GameObject empty, int x, int y)
    {
        empty.name = x.ToString() + y.ToString();
        empty.transform.position = new Vector3(x - (0.5f), y - (0.5f), 0);
    }
}