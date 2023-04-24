using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WFC_predef_array : MonoBehaviour
{
    public static WFC_predef_array Instance { get; private set; }


    [SerializeField] public GameObject parent;
    public GameObject[,] tiles; //= new Entry[4,4];
    public int collumns = 5, rows = 5;
    public Sprite defaultSprite;

    private int horizontal, vertical;

    //the list will be sorted by degree
    public List<Entry> entries;

    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void Start()
    {
        //StartCoroutine(stugg());
        setupGrid();
    }

    private IEnumerator stugg()
    {
        yield return new WaitForSeconds(2);
    }


    /// <summary>
    /// set up the Grid;
    /// for the predefined grid we assign each square to a spot in the "tiles" array
    /// and get the according entry
    /// in a second doubled for loop find neighbours for each entry
    /// </summary>
    public void setupGrid()
    {
        tiles = new GameObject[collumns, rows];
        for (int i = 0; i < collumns; ++i)
        {
            for (int j = 0; j < rows; ++j)
            {
                tiles[i, j] = new GameObject();
              //  print("entry created at: " + j + " " + i);
                instantiate_images(tiles[i, j], i, j);
                // print("entry created at: " + i + " " + j);
                setEntries(tiles[i, j], i, j);
            }
        }

        for (int i = 0; i < collumns; ++i)
        {
            for (int j = 0; j < rows; ++j)
            {
                find_neighbours(i, j);
            }
        }

        foreach (var a in entries)
        {
            a.count_remaining_values();
        }

        sort_Entries_remaining();
        wfcFunction(entries);
    }

    public void instantiate_images(GameObject empty, int x, int y)
    {
        empty.name = x.ToString() + y.ToString();
        empty.transform.position = new Vector3(x - (horizontal - 0.5f), y - (vertical - 0.5f), 0);
        //add a sprite renderer and set a default sprite
        var sprite = empty.AddComponent<SpriteRenderer>();
        sprite.sprite = defaultSprite;
        //add an entryScript and add it to the list
        var entry = empty.AddComponent<Entry>();
    }

    public void setEntries(GameObject empty, int x, int y)
    {
        var entry = empty.GetComponent<Entry>();
        empty.GetComponent<SpriteRenderer>().sprite =
            AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Sprites/WFC/tube1.png");

        entry.x = x;
        entry.y = y;
        entries.Add(entry);
    }

    public void paint_tile(Entry entry, int x, int y)
    {
        string load = Constraint_Singelton.ConSin.map[entry.selectedTile];
        tiles[x, y].GetComponent<SpriteRenderer>().sprite = AssetDatabase.LoadAssetAtPath<Sprite>(load);
    }

    /// <summary>
    /// neighbours have the following order: 0:up 1:right 2:down 3:left
    /// if it is a corner piece then the according entry will be null
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public void find_neighbours(int x, int y)
    {
        Entry entry = tiles[x, y].GetComponent<Entry>();
        entry.neighbours.Clear();
        entry.neighbours.Capacity = 4;
        //up
        entry.neighbours.Add((y + 1 < rows) ? tiles[x, y + 1].GetComponent<Entry>() : null);
        //right
        entry.neighbours.Add((x + 1 < collumns) ? tiles[x + 1, y].GetComponent<Entry>() : null);
        //down
        entry.neighbours.Add((y - 1 >= 0) ? tiles[x, y - 1].GetComponent<Entry>() : null);
        //left
        entry.neighbours.Add((x - 1 >= 0) ? tiles[x - 1, y].GetComponent<Entry>() : null);
    }

    /// <summary>
    /// called once at the start and after every iteration
    /// sorts entries after degree and once after 
    /// </summary>
    public void sort_Entries_remaining()
    {
        entries.Sort((x, y) => x.remaining_values.CompareTo(y.remaining_values));
    }

    public void sort_degree()
    {
        entries.Sort((x, y) => y.degree.CompareTo(x.degree));
    }

    /// <summary>
    /// notify each neighbour and call updateOptions on each of them
    /// </summary>
    public void notify_neighbours(Entry current)
    {
        for (int i = 0; i < 4; ++i)
        {
            if (current.neighbours[i] == null || current.neighbours[i].collapsed)
            {
                continue;
            }

            //0 is up 1 is right 2 is down 3 is left 
            print("updating neighbour: " + i);
            current.neighbours[i].updateOptions2(current.selectedTile, i);
        }
    }

    public bool wfcFunction(List<Entry> contents)
    {
        print("entered");
        if (contents.Count == 0)
        {
            print("noice");
            return true;
        }

        Entry current = contents[0];
        if (current.remaining_values == 0)
        {
            print(current.gameObject.name);
            print("none remain.");
            current.collapsed = true;
            current.selectedTile = Constraint_Singelton.Tiles.none;
            //return false;
        }
        else
        {
            current.collapse();
            notify_neighbours(current);
        }

        entries.Remove(current);
        paint_tile(current, current.x, current.y);
        sort_Entries_remaining();
        if (!wfcFunction(contents))
        {
            print("aaaaa");
            return wfcFunction(contents);
        }

        return true;
        //return wfcFunction(entries);
        //is the entry list empty

        //collapse a tile
        //update the neighbours
        //remove it from the entry list
        //set the correct tile;
    }
}