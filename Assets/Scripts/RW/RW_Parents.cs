using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RW_Parents : MonoBehaviour
{
    public float height, width;
    public Vector3 position;
    public List<Vector3> neighbours;
    public RW_entry[,] tiles;
    public int collumns, rows;
    public RW_self_avoiding rw;
    public GameObject prefab;

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
        tiles = new RW_entry[collumns, rows];
        for (int i = 0; i < collumns; ++i)
        {
            for (int j = 0; j < rows; ++j)
            {
                GameObject temp = Instantiate(prefab);
                var a=temp.GetComponent<RW_entry>();
                a.rendererComp = temp.GetComponent<SpriteRenderer>();
                tiles[i, j] = a;
               // print("entry created at: " + j + " " + i);
                instantiate_images( temp, i, j);
            }
        }
    }

    public void instantiate_images(GameObject empty, int x, int y)
    {
        empty.name = x.ToString() + y.ToString();
        empty.transform.position = new Vector3(position.x + (x - 0.5f), position.y + (y - 0.5f), 0);
        empty.transform.parent = this.transform;
    }
}