using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Entry : MonoBehaviour
{
    //has the entry been collapsed?
    public bool collapsed;

    //my heuristics
    public int degree;

    public int remaining_values;

    //Do I ever use this? YES
    public int x, y;

    //list of all the direct neighbours 
    public List<Entry> neighbours = new List<Entry>(4);

    //what tile has been selected -> important for later lookup
    public Constraint_Singelton.Tiles selectedTile;

    //list of all possible values(preinitialized)
    //this can be loaded from a Json
    //this can only be done because the values in the Constraint singelton are set in Awake and values
    //are first used in Start
    public List<Constraint_Singelton.Tiles> values = Constraint_Singelton.ConSin.values2;
   
    /// <summary>
    /// remaining values from the CSP
    /// </summary>
    public void count_remaining_values()
    {
        remaining_values = values.Count;
    }

    /// <summary>
    /// collapse a cell by randomly choosing a valid option
    /// </summary>
    public void collapse()
    {
        selectedTile = values[Random.Range(0, remaining_values)];
        print("collapsed to: " + selectedTile);
        collapsed = true;
    }


    /// <summary>
    /// update the remaining options
    /// </summary>
    /// <param name="tile"></param>
    /// <param name="dir"></param>
    public void updateOptions2(Constraint_Singelton.Tiles tile, int dir)
    {
        if (remaining_values == 0) return;
        //we want to compare the opposite side: used_tile:[1,0,0,0]-> [0,0,1,0] up matches down! dir=0 => mydir=2
        int mydir = (dir + 2) % 4;
        int[] used = Constraint_Singelton.ConSin.constraints[tile];
        values = get_intersection(used, dir, mydir);
        if (values.Count == 0)
        {
            remaining_values = 0;
            return;
        }

        count_remaining_values();
    }

    public List<Constraint_Singelton.Tiles> get_intersection(int[] reference, int direction, int mydir)
    {
        List<Constraint_Singelton.Tiles> temp = new List<Constraint_Singelton.Tiles>();
        foreach (var tile in values)
        {
            var a = Constraint_Singelton.ConSin.constraints[tile][mydir];
            if (a == reference[direction])
            {
                temp.Add(tile);
                print("still possible: " + tile);
            }
        }

        return temp;
    }
}