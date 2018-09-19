using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 物品基类
/// </summary>
public class Item
{
    // 物品类型
    public enum itemType
    {
        Consumable,  //消耗品
        Equipment,   //装备
        Weapon,      //武器
        Material     //材料
    }

    // 品质类型
    public enum quality
    {
        Common,      //一般
        Unmmon,      //不一般
        Rare,        //稀有
        Epic,        //史诗
        Legendary,   //传说
        Artifact     //远古
    }

    // ID
	public int ID { get; set; }
    // 名字
    public string Name { get; set; }
    // 物品类型
    public itemType ItemType { get; set; }
    // 品质类型
    public quality Quality { get; set; }
    // 描述
    public string Description { get; set; }
    // 最大容量
    public int Capacity { get; set; }
    // 购买价格
    public int BuyPrice { get; set; }
    // 出售价格
    public int SellPrice { get; set; }
    // 图标路径
    public string Sprite { get; set; }

    public Item()
    {
        this.ID = -1;
    }


    public Item(int id, string name, itemType itemType, quality quality, string description, int capacity, int buyPrice, int sellPrice, string sprite)
    {
        this.ID = id;
        this.Name = name;
        this.ItemType = itemType;
        this.Quality = quality;
        this.Description = description;
        this.Capacity = capacity;
        this.BuyPrice = buyPrice;
        this.SellPrice = sellPrice;
        this.Sprite = sprite;
    }

    // 得到提示面板应该显示的内容
    public virtual string GetTooltipText()
    {
        string color = "";
        switch (Quality)
        {
            case quality.Common:
                color = "white";
                break;
            case quality.Unmmon:
                color = "lime";
                break;
            case quality.Rare:
                color = "navy";
                break;
            case quality.Epic:
                color = "magenta";
                break;
            case quality.Legendary:
                color = "orange";
                break;
            case quality.Artifact:
                color = "red";
                break;
        }
        //string text = "";
        //text += "名字:" + Name + "\n";
        //text += "描述:" + Description + "\n";
        //text += "购买价格:" + BuyPrice + "  出售价格:" + SellPrice;
        string text = string.Format("<color={4}><size=25>{0}</size></color>\n<color=yellow>{1}</color>\n购买价格:<color=red>{2}</color> 出售价格:<color=green>{3}</color>", Name,Description, BuyPrice, SellPrice,color);
        return text;
    }
}
