using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RW_Parents : MonoBehaviour
{
    public float height, width;
    public Vector3 position;
    public List<Vector3> neighbours;
    public GameObject[,] tiles;
    public int collumns, rows;
    public RW_self_avoiding rw;

    public void FillScript(float w, float h, Vector3 pos, int coll, int row)
    {
        height = h;
        width = w;
        collumns = coll;
        rows = row;
        position = pos;
        rw = this.AddComponent<RW_self_avoiding>();
        
    }

    public void SetupGrid()
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
    }

    public void instantiate_images(GameObject empty, int x, int y)
    {
        empty.name = x.ToString() + y.ToString();
        empty.transform.position = new Vector3(position.x - (x-0.5f), position.y - (y-0.5f), 0);
        empty.transform.parent = this.transform;
    }
    
    
}