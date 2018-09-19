using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPanel : Inventroy
{
    #region 单例模式

    private static CharacterPanel instance;
    public static CharacterPanel Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.Find("CharacterPanel").GetComponent<CharacterPanel>();
            }

            return instance;
        }
    }

    #endregion

    // 属性信息文本
    private Text propertyText;
    private Player player;

    public Text PropertyText
    {
        get { return propertyText; }
        set
        {
            propertyText = value;
        }
    }

    public override void Start()
    {
        base.Start();
        propertyText = transform.Find("PropertyPanel/PropertyText").GetComponent<Text>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        UpdatePropertyText();
    }

    // 右键穿上装备
    public void PutOn(Item item)
    {
        Item exitItem = null;
        foreach (Slot slot in slots)
        {
            EquipmentSlot equipmentSlot = (EquipmentSlot) slot;
            if (equipmentSlot.IsRightItem(item))
            {
                if (equipmentSlot.transform.childCount > 0)//如果对应的角色面板物品槽原先有装备
                {
                    ItemUI currentItemUI = equipmentSlot.transform.GetChild(0).GetComponent<ItemUI>();
                    exitItem = currentItemUI.Item;
                    currentItemUI.SetItem(item,1);
                    UpdatePropertyText();
                }
                else//对应的角色面板物品槽没有装备
                {
                    equipmentSlot.StoreItem(item);//直接放入角色面板物品槽
                    UpdatePropertyText();
                }
                break;
            }
        }

        if (exitItem != null)
        {
            Knapsack.Instance.StoreItem(exitItem);
        }
    }

    // 右键脱下装备
    public void PutOff(Item item)
    {
        Knapsack.Instance.StoreItem(item);
        UpdatePropertyText();
    }

    // 更新属性信息
    public void UpdatePropertyText()
    {
        int strength = 0, intellect = 0, agility = 0, stamina = 0, damage = 0;
        foreach (EquipmentSlot slot in slots)
        {
            if (slot.transform.childCount > 0)
            {
                Item item = slot.transform.GetChild(0).GetComponent<ItemUI>().Item;
                if (item is Equipment)
                {
                    Equipment e = (Equipment) item;
                    strength += e.Strength;
                    intellect += e.Intellect;
                    agility += e.Agility;
                    stamina += e.Stamina;
                }else if (item is Weapon)
                {
                    Weapon w = (Weapon) item;
                    damage += w.Damage;
                }
            }
        }
        strength += player.BasicStrength;
        intellect += player.BasicIntrllect;
        agility += player.BasicAgility;
        stamina += player.BasicStamina;
        damage += player.BasicDamage;
        string text = string.Format("力量:{0}\n智力:{1}\n敏捷:{2}\n体力:{3}\n攻击力:{4}", strength, intellect, agility, stamina,damage);
        propertyText.text = text;


    }
}
