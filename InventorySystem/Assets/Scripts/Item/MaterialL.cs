using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialL : Item
{
    public MaterialL(int id, string name, itemType itemType, quality quality, string description, int capacity,
        int buyPrice, int sellPrice, string sprite)
        : base(id, name, itemType, quality, description, capacity, buyPrice, sellPrice, sprite)
    {}
}
