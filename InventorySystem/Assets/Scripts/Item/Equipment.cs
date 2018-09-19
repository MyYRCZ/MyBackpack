using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 装备类
/// </summary>
public class Equipment : Item {

    // 装备类型
    public enum EquipmentType
    {
        None,
        Head,         //头部
        Neck,         //脖子
        Ring,         //戒指
        Leg,          //腿部
        Bracer,       //护腕
        Boots,        //靴子
        Shoulder,     //肩膀
        Belt,         //腰带
        OffHand,      //副手
        Chest         //胸部
    }

    // 力量
	public int Strength { get; set; }
    // 智力
    public int Intellect { get; set; }
    // 敏捷
    public int Agility { get; set; }
    // 体力
    public int Stamina { get; set; }
    // 装备类型
    public EquipmentType EquipType { get; set; }

    public Equipment(int id, string name, itemType itemType, quality quality, string description, int capacity,
        int buyPrice, int sellPrice,string sprite, int strength, int intellect, int agility, int stamina, EquipmentType equipType)
        : base(id, name, itemType, quality, description, capacity, buyPrice, sellPrice, sprite)
    {
        this.Strength = strength;
        this.Intellect = intellect;
        this.Agility = agility;
        this.Stamina = stamina;
        this.EquipType = equipType;
    }

    public override string GetTooltipText()
    {
        string text = base.GetTooltipText();
        string equipTypeText = "";
        switch (EquipType)
        {
            case EquipmentType.Belt:
                equipTypeText = "腰带";
                break;
            case EquipmentType.Boots:
                equipTypeText = "靴子";
                break;
            case EquipmentType.Bracer:
                equipTypeText = "护腕";
                break;
            case EquipmentType.Chest:
                equipTypeText = "胸部";
                break;
            case EquipmentType.Head:
                equipTypeText = "头部";
                break;
            case EquipmentType.Leg:
                equipTypeText = "腿部";
                break;
            case EquipmentType.Neck:
                equipTypeText = "脖子";
                break;
            case EquipmentType.OffHand:
                equipTypeText = "副手";
                break;
            case EquipmentType.Ring:
                equipTypeText = "戒指";
                break;
            case EquipmentType.Shoulder:
                equipTypeText = "护肩";
                break;
        }
        string newText = string.Format("{0}\n\n<color=blue>装备类型:{5}\n力量:{1}\n智力:{2}\n敏捷:{3}\n体力:{4}</color>", text, Strength,Intellect,Agility,Stamina, equipTypeText);
        return newText;
    }
}
