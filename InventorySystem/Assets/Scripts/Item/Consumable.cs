using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 消耗品类
/// </summary>
public class Consumable : Item{

	public int Hp { get; set; }
    public int Mp { get; set; }

    public Consumable(int id, string name, itemType itemType, quality quality, string description, int capacity,
        int buyPrice, int sellPrice,string sprite, int hp, int mp)
        : base(id, name, itemType, quality, description, capacity, buyPrice, sellPrice,sprite)
    {
        this.Hp = hp;
        this.Mp = mp;
    }

    public override string ToString()
    {
        return string.Format("ID:{0} 名字:{1} 物品类型:{2} 品质类型:{3} 描述:{4} 容量:{5} 购买价格:{6} 出售价格:{7} 路径:{8} Hp:{9} Mp:{10}", ID, Name,
            ItemType, Quality, Description, Capacity, BuyPrice, SellPrice, Sprite, Hp, Mp);
    }

    public override string GetTooltipText()
    {
        string text = base.GetTooltipText();
        string newText = string.Format("{0}\n\n <color=blue>加血:{1}\n 加蓝:{2}</color>", text, Hp, Mp);
        return newText;
    }
}
