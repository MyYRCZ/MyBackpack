using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquipmentSlot : Slot
{
    // 装备类型
    public Equipment.EquipmentType equipmentType;
    // 武器类型
    public Weapon.WeaponType weponType;

    public override void OnPointerDown(PointerEventData eventData)
    {
        #region 分析
        //1.鼠标上有东西 
        //当前装备槽有装备
        //当前装备槽无装备

        //2.鼠标上无东西
        //当前装备槽有装备
        //当前装备槽无装备(不做处理)
        #endregion

        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (transform.childCount > 0 && !InventoryManager.Instance.IsPickedItem)
            {
                ItemUI currentItemUI = transform.GetChild(0).GetComponent<ItemUI>();
                ItemUI itemUITemp = currentItemUI;
                DestroyImmediate(currentItemUI.gameObject);
                transform.parent.SendMessage("PutOff", itemUITemp.Item);
                InventoryManager.Instance.HideToolTip();
            }
        }

        if (eventData.button != PointerEventData.InputButton.Left) return;//如果不是鼠标左键点击return;

        bool isUpdateProperty = false;//是否更新属性信息
        if (InventoryManager.Instance.IsPickedItem)
        {
            ItemUI pickedItem = InventoryManager.Instance.PickedItem;
            if (transform.childCount > 0)
            {
                ItemUI currentItemUI = transform.GetChild(0).GetComponent<ItemUI>();
                if (IsRightItem(pickedItem.Item))
                {
                    InventoryManager.Instance.PickedItem.Exchange(currentItemUI);
                    isUpdateProperty = true;
                }
            }
            else//当前装备槽无装备
            {
                if (IsRightItem(pickedItem.Item))
                {
                    this.StoreItem(pickedItem.Item);
                    InventoryManager.Instance.RemoveItem(1);
                    isUpdateProperty = true;
                }
            }
        }
        else
        {
            if (transform.childCount > 0)
            {
                ItemUI currentItem = transform.GetChild(0).GetComponent<ItemUI>();//获得当前物品槽物品
                InventoryManager.Instance.PickUpItem(currentItem.Item, currentItem.Amount);//拿起当前物品槽物品
                Destroy(currentItem.gameObject);
                isUpdateProperty = true;
            }
        }

        if (isUpdateProperty)
        {
            Invoke("UpdataMsg", 0.1f);
        }
    }

    public void UpdataMsg()
    {
        transform.parent.SendMessage("UpdatePropertyText");
    }


    // 传入的物品是否适合放在这个物品
    public bool IsRightItem(Item item)
    {
        if ((item is Equipment && ((Equipment)(item)).EquipType == this.equipmentType) || (item is Weapon && ((Weapon)(item)).WpType == this.weponType))
        {
            return true;
        }
        return false;
    }
}
