using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 武器类
/// </summary>
public class Weapon : Item {

    // 武器类型
    public enum WeaponType
    {
        None, 
        MainHand,      //主手
        OffHand        //副手
    }

    // 伤害
    public int Damage { get; set; }
    // 武器类型
    public WeaponType WpType { get; set; }

    public Weapon(int id, string name, itemType itemType, quality quality, string description, int capacity,
        int buyPrice, int sellPrice, string sprite,int damage, WeaponType wpType)
        : base(id, name, itemType, quality, description, capacity, buyPrice, sellPrice, sprite)
    {
        this.Damage = damage;
        this.WpType = wpType;
    }

    public override string GetTooltipText()
    {
        string text = base.GetTooltipText();
        string weaponTypeText = "";
        switch (WpType)
        {
            case WeaponType.MainHand:
                weaponTypeText = "主手";
                break;
            case WeaponType.OffHand:
                weaponTypeText = "副手";
                break;
        }
        string newText = string.Format("{0}\n\n<color=blue>武器类型:{1}\n伤害:{2}</color>", text, weaponTypeText, Damage);
        return newText;
    }
}
