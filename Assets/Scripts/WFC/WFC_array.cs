using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WFC_array : MonoBehaviour
{
    [SerializeField] public GameObject[,] tiles; //= new Entry[4,4];
    public int rows, collumns = 3;
    public Sprite defaultSprite;

    private int horizontal, vertical;

    //the list will be sorted by degree
    public List<Entry> entries;

    public void Awake()
    {
        vertical = (int) Camera.main.orthographicSize;
        horizontal = vertical * (Screen.width / Screen.height);
        rows = vertical;
        collumns = horizontal;
        setupGrid();
    }

    public void setupGrid()
    {
        tiles = new GameObject[collumns, rows];
        for (int i = 0; i < collumns; ++i)
        {
            for (int j = 0; j < rows; ++j)
            {
                tiles[i, j] = new GameObject();
                print("entry created at: " + i + " " + j);
                instantiateImages(tiles[i, j], i, j);
            }
        }

        sort_Entries();
    }

    public void instantiateImages(GameObject empty, int x, int y)
    {
        //set position of the empty
        empty.transform.position = new Vector3(x - (horizontal - 0.5f), y - (vertical - 0.5f), 0);
        //add a sprite renderer and set a default sprite
        var sprite = empty.AddComponent<SpriteRenderer>();
        sprite.sprite = defaultSprite;
        //add an entryScript and add it to the list
        var entry = empty.AddComponent<Entry>();
        entry.x = x;
        entry.y = y;
        entries.Add(entry);
    }

    //called once at the start and after every iteration
    public void sort_Entries()
    {
        entries.Sort((x, y) => y.degree.CompareTo(x.degree));
    }

    public void notify_neighbours()
    {
        //top
        //right
        //down
        //left
        //for each check boundaries since neighbours are not saved anymore 
        //and check if collapsed or already looked at
        //the checks are: if i-1 <0 or i+1 =collumns
        //j+1>=rows or j-1<0
    }

    public bool wfcFunction()
    {
        return false;
    }
}