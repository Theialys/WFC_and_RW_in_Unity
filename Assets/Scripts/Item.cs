using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public Sprite itemImage;
    public String name;

    //make this reactive! for now E is the intgeraction button!
    public void OnUse()
    {
    }

    public void equip()
    {
        //later done by keyboard input, for now it will be pressed
    }
}