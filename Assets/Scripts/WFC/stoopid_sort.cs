using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class stoopid_sort : MonoBehaviour
{
    public GameObject[,] tiles; //= new Entry[4,4];
    public Sprite defaultSprite;
    public int rows=4;
    public int collumns=4;
   
    //Dictuionary key:tile -> values:[options_up],[options_right],[options_down],[options_left]
    private Dictionary<Constraint_Singelton.Tiles, List<Constraint_Singelton.Tiles>[]> cont =
        new Dictionary<Constraint_Singelton.Tiles, List<Constraint_Singelton.Tiles>[]>();

    public List<Entry> entries;

    void Start()
    {tiles = new GameObject[collumns, rows];
        for (int i = 0; i < collumns; ++i)
        {
            for (int j = 0; j < rows; ++j)
            {
                tiles[i, j] = new GameObject();
                print("entry created at: " + i + " " + j);
                instantiateImages(tiles[i, j], i, j);
            }
        }
        
        
        
        /*
        cont[Constraint_Singelton.Tiles.middown] = new List<Constraint_Singelton.Tiles>[4];
        cont[Constraint_Singelton.Tiles.midup] = new List<Constraint_Singelton.Tiles>[4];
        cont[Constraint_Singelton.Tiles.midleft] = new List<Constraint_Singelton.Tiles>[4];
        cont[Constraint_Singelton.Tiles.midright] = new List<Constraint_Singelton.Tiles>[4];
        //UP
        fill_dict2(Constraint_Singelton.Tiles.middown, 0, Constraint_Singelton.Tiles.midup);
        //RIGHT
        fill_dict2(Constraint_Singelton.Tiles.middown, 1, Constraint_Singelton.Tiles.midup,
            Constraint_Singelton.Tiles.midright, Constraint_Singelton.Tiles.midleft);
        //DOWN
        fill_dict2(Constraint_Singelton.Tiles.middown, 2, Constraint_Singelton.Tiles.midup,
            Constraint_Singelton.Tiles.middown, Constraint_Singelton.Tiles.midright);
        //LEFT
        fill_dict2(Constraint_Singelton.Tiles.middown, 3, Constraint_Singelton.Tiles.middown,
            Constraint_Singelton.Tiles.midleft, Constraint_Singelton.Tiles.midup);

        //for midup flip 0 and 2
        //UP
        fill_dict2(Constraint_Singelton.Tiles.midup, 0, Constraint_Singelton.Tiles.midup,
            Constraint_Singelton.Tiles.middown, Constraint_Singelton.Tiles.midright);
        //RIGHT
        fill_dict2(Constraint_Singelton.Tiles.midup, 1, Constraint_Singelton.Tiles.midup,
            Constraint_Singelton.Tiles.midright, Constraint_Singelton.Tiles.midleft);
        //DOWN
        fill_dict2(Constraint_Singelton.Tiles.midup, 2, Constraint_Singelton.Tiles.midup);
        //LEFT
        fill_dict2(Constraint_Singelton.Tiles.midup, 3, Constraint_Singelton.Tiles.middown,
            Constraint_Singelton.Tiles.midleft, Constraint_Singelton.Tiles.midup);

        //midleft

        //UP
        fill_dict2(Constraint_Singelton.Tiles.midleft, 0, Constraint_Singelton.Tiles.midleft,
            Constraint_Singelton.Tiles.middown, Constraint_Singelton.Tiles.midright);
        //RIGHT
        fill_dict2(Constraint_Singelton.Tiles.midleft, 1, Constraint_Singelton.Tiles.midright);
        //DOWN
        fill_dict2(Constraint_Singelton.Tiles.midleft, 2, Constraint_Singelton.Tiles.midleft,
            Constraint_Singelton.Tiles.midright, Constraint_Singelton.Tiles.midup);
        //LEFT
        fill_dict2(Constraint_Singelton.Tiles.midleft, 3, Constraint_Singelton.Tiles.middown,
            Constraint_Singelton.Tiles.midleft, Constraint_Singelton.Tiles.midup, Constraint_Singelton.Tiles.midleft,
            Constraint_Singelton.Tiles.middown);
        //midright flip left and right
        //UP
        fill_dict2(Constraint_Singelton.Tiles.midright, 0, Constraint_Singelton.Tiles.midleft,
            Constraint_Singelton.Tiles.middown, Constraint_Singelton.Tiles.midright);
        //RIGHT
        fill_dict2(Constraint_Singelton.Tiles.midright, 1, Constraint_Singelton.Tiles.middown,
            Constraint_Singelton.Tiles.midleft, Constraint_Singelton.Tiles.midup, Constraint_Singelton.Tiles.midleft,
            Constraint_Singelton.Tiles.middown);
        //DOWN
        fill_dict2(Constraint_Singelton.Tiles.midright, 2, Constraint_Singelton.Tiles.midleft,
            Constraint_Singelton.Tiles.midright, Constraint_Singelton.Tiles.midup);
        //LEFT
        fill_dict2(Constraint_Singelton.Tiles.midright, 3, Constraint_Singelton.Tiles.midright);


        print(cont[Constraint_Singelton.Tiles.middown]);
        print_array();*/
        /*entries = new List<Entry>(5);
       
        print(entries.Capacity);
        for (int i = 0; i < 5; ++i)
        {
            entries.Add(new Entry());
            entries.ElementAt(i).degree =Random.Range(0, 10);
            print("assigned: "+entries[i].degree);
        }

        entries.OrderBy(i => i.degree);
        entries.Sort((x,y)=>y.degree.CompareTo(x.degree));
        foreach (Entry e in entries)
        {
            print(e.degree);
        }
      //print(Constraint_Singelton.ConSin.constraints[0]); */
    }
    
    public void fill_dict2(Constraint_Singelton.Tiles key, int index, params Constraint_Singelton.Tiles[] options)
    {
        //new Odering: Dictionary key:collapsedTile -> value: [opt_up][opt_right][opt_down][opt_left]
        List<Constraint_Singelton.Tiles> opts = new List<Constraint_Singelton.Tiles>();
        foreach (var a in options)
        {
            opts.Add(a);
        }

        cont[key][index] = opts;
    }

    public void print_array()
    {
        foreach (var a in cont[Constraint_Singelton.Tiles.middown])
        {
            foreach (var b in a)
            {
                print(b);
            }
        }
    }
    public void instantiateImages(GameObject empty, int x, int y)
    {
        //set position of the empty
        empty.transform.position = new Vector3(x - (rows - 0.5f), y - (collumns - 0.5f), 0);
        //add a sprite renderer and set a default sprite
        var sprite = empty.AddComponent<SpriteRenderer>();
        sprite.sprite = defaultSprite;
        //add an entryScript and add it to the list
        var entry = empty.AddComponent<Entry>();
        entry.x = x;
        entry.y = y;
        entries.Add(entry);
    }
}