using AE.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    public string Name;
    public ItemClass Class;

    public Enemy(string _name, ItemClass _class) : base()
    {
        Name = _name;
        Class = _class;
    }
}
