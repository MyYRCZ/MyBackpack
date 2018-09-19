using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPanel : Inventroy
{
    #region 单例模式

    private static ShopPanel instance;

    public static ShopPanel Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.Find("ShopPanel").GetComponent<ShopPanel>();
            }
            return instance;
        }
    }

    #endregion

    // ID数组
    public int[] itemIdArray;

    private Player player;

    public override void Start()
    {
        base.Start();
        InitShop();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }


    // 初始化商店
    private void InitShop()
    {
        foreach (int itemID in itemIdArray)
        {
            StoreItem(itemID);
        }
    }


    // 购买物品
    public void BuyItem(Item item)
    {
        bool isSuccess = player.ConsumeGold(item.BuyPrice);//是否可以购买
        if (isSuccess)
        {
            Knapsack.Instance.StoreItem(item);
        }
    }

    // 出售物品
    public void SellItem()
    {
        //计算出售后应得金币数量(按下Ctrl:一个一个出售,否则全部出售)
        int sellAmount = 1;
        if (Input.GetKey(KeyCode.LeftControl))
        {
            sellAmount = 1;
        }
        else
        {
            sellAmount = InventoryManager.Instance.PickedItem.Amount;
        }
        int goldAmount = InventoryManager.Instance.PickedItem.Item.SellPrice * sellAmount;
        player.GetGold(goldAmount);
        InventoryManager.Instance.RemoveItem(sellAmount);
    }
}
