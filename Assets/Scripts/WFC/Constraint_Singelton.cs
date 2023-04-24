using System;
using System.Collections.Generic;
using UnityEngine;

public class Constraint_Singelton : MonoBehaviour
{
    public enum Tiles
    {
        middown,
        midleft,
        midup,
        midright,
        edgeright,
        edgedown,
        edgeleft,
        edgeup,
        cross,
        none
    };

    //with a as 
    public static Constraint_Singelton ConSin { get; private set; }

    public List<Tiles> values = new List<Tiles>
    {
        Tiles.middown, Tiles.midleft, Tiles.midright,
        Tiles.midup
    };

    public List<Tiles> values2 = new List<Tiles>();

    //tile and the look up (can be loaded from Json)
    public Dictionary<Tiles, String> map = new Dictionary<Tiles, String>()
    {
        {Tiles.midup, "Assets/Sprites/WFC/tube1.png"}, {Tiles.midright, "Assets/Sprites/WFC/tube2.png"},
        {Tiles.middown, "Assets/Sprites/WFC/tube3.png"}, {Tiles.midleft, "Assets/Sprites/WFC/tube4.png"},
        {Tiles.cross, "Assets/Sprites/WFC/tube5.png"},
        {Tiles.none, "Assets/Sprites/WFC/tube6.png"}, {Tiles.edgeright, "Assets/Sprites/WFC/edge1.png"},
        {Tiles.edgedown, "Assets/Sprites/WFC/edge2.png"}, {Tiles.edgeleft, "Assets/Sprites/WFC/edge3.png"},
        {Tiles.edgeup, "Assets/Sprites/WFC/edge4.png"}
    };

    public Dictionary<Tiles, int[]> constraints = new Dictionary<Tiles, int[]>();

    private void Awake()
    {
        if (ConSin != null && ConSin != this)
        {
            Destroy(this);
        }
        else
        {
            ConSin = this;
        }

        //this can be shrunken down by taking one sprite and rotating it.
        //(But I do not know how to initialize a sprite from another)
        add_con(Tiles.midup, new int[] {1, 1, 0, 1});
        add_con(Tiles.none, new int[] {0, 0, 0, 0});
        add_con(Tiles.cross, new int[] {1, 1, 1, 1});
        add_con(Tiles.midright, new int[] {1, 1, 1, 0});
        add_con(Tiles.middown, new int[] {0, 1, 1, 1});
        add_con(Tiles.midleft, new int[] {1, 0, 1, 1});
        add_con(Tiles.edgeright, new int[] {1, 1, 0, 0});
        add_con(Tiles.edgedown, new int[] {0, 1, 1, 0});
        add_con(Tiles.edgeleft, new int[] {0, 0, 1, 1});
        add_con(Tiles.edgeup, new int[] {1, 0, 0, 1});
    }

    public void add_con(Tiles tile, int[] edges)
    {
        constraints.Add(tile, edges);
        values2.Add(tile);
    }
}