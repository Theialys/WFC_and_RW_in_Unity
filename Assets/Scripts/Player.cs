using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //because why not
    public int health;

    //this is changed if the player changes the actively equipped item.
    //mostly just for rendering and item effect.
    public Item equippedItem;
    public string name = "Quinqi";
    public Item[] inventory = new Item[4];

    public string OnSafe()
    {
        return JsonUtility.ToJson(this);
    }

    public void OnLoad(string safe)
    {
        JsonUtility.FromJsonOverwrite(safe, this);
    }
}