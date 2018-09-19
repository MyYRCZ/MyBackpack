using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Inventroy : MonoBehaviour
{
    // 格子
    protected Slot[] slots;
    // 目标透明度
    private float targetAlpha = 1.0f;
    // 渐变速度
    public float smoothing = 4.0f;

    private CanvasGroup canvasGroup;

    // Use this for initialization
    public virtual void Start()
    {
        slots = GetComponentsInChildren<Slot>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    void Update()
    {
        if (canvasGroup.alpha != targetAlpha)
        {
            canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, targetAlpha, smoothing*Time.deltaTime);
            if (Mathf.Abs(canvasGroup.alpha - targetAlpha) < 0.01f)
            {
                canvasGroup.alpha = targetAlpha;
            }
        }
    }

    public bool StoreItem(int id)
    {
        Item item = InventoryManager.Instance.GetItemById(id);
        return StoreItem(item);
    }

    public bool StoreItem(Item item)
    {
        //ID不存在
        if (item == null)
        {
            Debug.LogWarning("要存储的物品的Id不存在");
            return false;
        }

        //ID存在,就存储
        if (item.Capacity == 1)
        {
            Slot slot = FindEmptySlot();
            if (slot == null)
            {
                Debug.Log("没有空的物品槽");
                return false;
            }
            else
            {
                slot.StoreItem(item);
            }
        }
        else
        {
            Slot slot = FindSameIdSlot(item);//找一个相同类型的物品槽
            if (slot != null)
            {
                slot.StoreItem(item);
            }
            else
            {
                Slot emptySlot = FindEmptySlot();
                if (emptySlot != null)
                {
                    emptySlot.StoreItem(item);
                }
                else
                {
                    Debug.Log("没有空的物品槽");
                    return false;
                }
            }
        }
        return true;
    }

    // 查找空的物品槽
    private Slot FindEmptySlot()
    {
        foreach (Slot slot in slots)
        {
            if (slot.transform.childCount == 0)//如果当前物品槽没有任何子物体
            {
                return slot;
            }
        }
        return null;
    }

    // 查找相同类型的物品槽
    private Slot FindSameIdSlot(Item item)
    {
        foreach (Slot slot in slots)
        {
            if (slot.transform.childCount >= 1 && slot.GetItemId() == item.ID && slot.IsFilled() == false)
            {
                return slot;
            }
        }
        return null;
    }

    // 显示
    public void Show()
    {
        canvasGroup.blocksRaycasts = true;//可以进行交互
        targetAlpha = 1.0f;
    }


    // 隐藏
    public void Hide()
    {
        canvasGroup.blocksRaycasts = false;
        targetAlpha = 0.0f;
    }

    // 显示隐藏开关
    public void DisPlayerSwitch()
    {
        if (targetAlpha == 1.0f)
        {
            Hide();
        }
        else
        {
            Show();
        }
    }

    #region 保存和加载

    // 保存数据
    public void SaveInventory()
    {
        StringBuilder sb = new StringBuilder();
        foreach (Slot slot in slots)
        {
            if (slot.transform.childCount > 0)
            {
                ItemUI itemUI = slot.transform.GetChild(0).GetComponent<ItemUI>();
                sb.Append(itemUI.Item.ID + "," + itemUI.Amount + "-");
            }
            else
            {
                sb.Append("0-");
            }
        }
        PlayerPrefs.SetString(this.gameObject.name,sb.ToString());
    }


    // 加载数据
    public void LoadInventory()
    {
        if (PlayerPrefs.HasKey(this.gameObject.name) == false) return;
        string str = PlayerPrefs.GetString(this.gameObject.name);
        string[] itemArray = str.Split('-');
        for (int i = 0; i < itemArray.Length - 1; i++) 
        {
            string itemStr = itemArray[i];
            if (itemStr != "0")
            {
                string[] temp = itemStr.Split(',');
                int id = int.Parse(temp[0]);
                Item item = InventoryManager.Instance.GetItemById(id);
                int amount = int.Parse(temp[1]);
                for (int j = 0; j < amount; j++)
                {
                    slots[i].StoreItem(item);
                }
            }
        }
    }

    public void Sort()
    {
        int index = 0;
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].transform.childCount != 0)
            {
                slots[i].transform.GetChild(0).GetComponent<ItemUI>().transform.position = slots[index].transform.position;
                slots[i].transform.GetChild(0).GetComponent<ItemUI>().transform.SetParent(slots[index].transform);
                index++;
            }
        }
    }

    #endregion
}
