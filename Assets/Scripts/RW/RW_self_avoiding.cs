using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RW_self_avoiding : MonoBehaviour
{
    public RW_entry[,] tiles; //= new Entry[4,4];
    public int collumns = 5, rows = 5;
    public Sprite defaultSprite;

    private int horizontal, vertical;
    //  public int steps;

    private List<Vector2> dirs = new List<Vector2>()
        {new Vector2(0, 1), new Vector2(1, 0), new Vector2(0, -1), new Vector2(-1, 0)};

    /*   public void Start()
       {
           setupGrid();
           randomWalk(steps, tiles[0, 0], 0, 0);
       }*/


    /// <summary>
    /// set up the Grid;
    /// for the predefined grid we assign each square to a spot in the "tiles" array
    /// and get the according entry
    /// in a second doubled for loop find neighbours for each entry
    /// </summary>
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
        empty.transform.position = new Vector3(x - (horizontal - 0.5f), y - (vertical - 0.5f), 0);
    }

    //prolly make recursive
    /*public bool randomWalk(int steps, GameObject starter, int x, int y)
    {
        if (steps >= collumns * rows)
        {
            print("oh no");
            return false;
        }

        if (steps <= 0) return true;
        var a = starter.GetComponent<SpriteRenderer>();
        if (a == null)
        {
            starter.AddComponent<SpriteRenderer>();
            // paint_tile(starter);
            --steps;
        }

        Vector2 vec = randomDir();
        Vector3 newPos = new Vector3(vec.x + x, vec.y + y, 0);
        int index = 0;
        while (!checkBounds(newPos))
        {
            if (index >= dirs.Count) break;
            vec = getDir(index);
            newPos = new Vector3(vec.x + x, vec.y + y, 0);
            ++index;
            //do shit -> reroll the   direction to go to
            --steps;
        }

        return randomWalk(steps, tiles[(int) newPos.x, (int) newPos.y], (int) newPos.x, (int) newPos.y);
    }*/

    //
    public bool randWak2l(int steps, int x, int y)
    {
        for (int i = 0; i < steps; i++)
        {
            var entry = tiles[x, y];
            if (!entry.painted)
            {
                paint_tile(entry);
                --i;
            }

            Vector2 vec = randomDir();
            Vector3 newPos = new Vector3(vec.x + x, vec.y + y, 0);
            int index = 0;
            while (!checkBounds(newPos))
            {
                if (index >= dirs.Count) break;
                vec = getDir(index);
                newPos = new Vector3(vec.x + x, vec.y + y, 0);
                ++index;
            }

            x = (int) newPos.x;
            y = (int) newPos.y;
        }

        return true;
    }

    public Vector2 randomDir()
    {
        return dirs[Random.Range(0, dirs.Count)];
    }

    public Vector2 getDir(int index)
    {
        return dirs[index];
    }

    public bool checkBounds(Vector3 vec)
    {
        int x = (int) vec.x;
        int y = (int) vec.y;
        if (x < 0 || x >= collumns) return false;
        if (y < 0 || y >= rows) return false;
        return true;
    }

    public void paint_tile(RW_entry pointer)
    {
        pointer.painted = true;
        pointer.rendererComp.sprite = defaultSprite;
    }
}