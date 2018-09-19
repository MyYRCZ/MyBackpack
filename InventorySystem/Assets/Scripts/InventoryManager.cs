using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour {

    #region 单例模式

    private static InventoryManager instance;

    public static InventoryManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
            }
            return instance;
        }
    }

    #endregion

    // 用于存储Item物品信息
    private List<Item> itemList;
    // 提示信息
    private ToolTip toolTip;
    // 提示信息是否在显示
    private bool IsToolTipShow = false;
    // 画布
    private Canvas canvas;
    // 提示信息框的偏移
    private Vector2 toolTipPosionOffset;
    // 跟随鼠标移动的Item
    private ItemUI pickedItem;
    // 鼠标是否选中物品
    private bool isPickedItem = false;
    

    public ItemUI PickedItem
    {
        get { return pickedItem; }
    }

    public bool IsPickedItem
    {
        get { return isPickedItem; }
        set { isPickedItem = value; }
    }

    void Awake()
    {
        ParseItemJson();
    }

    void Start()
    {
        toolTip = GameObject.FindObjectOfType<ToolTip>();//通过查找类型得到
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        toolTipPosionOffset = new Vector2(20,-20);
        pickedItem = GameObject.Find("PickedItem").GetComponent<ItemUI>();

        pickedItem.Hide();
    }

    void Update()
    {
        if (isPickedItem)
        {
            //如果捡起了物品就让物品跟随鼠标移动
            Vector2 position;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, null, out position);
            pickedItem.SetLocalPosition(position);
        }
        else if (IsToolTipShow)
        {
            //提示信息跟随鼠标移动
            Vector2 position;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, null, out position);
            toolTip.SetLocalPotion(position + toolTipPosionOffset);
        }

        //物品丢弃的处理    
        if (isPickedItem && Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject(-1))
        {
            isPickedItem = false;
            pickedItem.Hide();
        }
    }
    
    // 解析Json文件
    private void ParseItemJson()
    {
        itemList = new List<Item>();
        TextAsset itemText = Resources.Load<TextAsset>("itemJson");
        string itemsJson = itemText.text;
        JSONObject j = new JSONObject(itemsJson);
        foreach (JSONObject temp in j.list)
        {
            Item.itemType type = (Item.itemType)System.Enum.Parse(typeof(Item.itemType), temp["type"].str);
            //解析temp对象的共有属性
            int id = (int)(temp["id"].n);
            string name = temp["name"].str;
            Item.quality qual = (Item.quality) System.Enum.Parse(typeof(Item.quality), temp["quality"].str);
            string description = temp["description"].str;
            int capacity = (int)(temp["capacity"].n);
            int buyPrice = (int)(temp["buyPrice"].n);
            int sellPrice = (int)(temp["sellPrice"].n);
            string sprite = temp["sprite"].str;

            Item item = null;
            switch (type)
            {
                case Item.itemType.Consumable:
                    int hp = (int)(temp["hp"].n);
                    int mp = (int)(temp["mp"].n);
                    item= new Consumable(id,name,type,qual,description,capacity,buyPrice,sellPrice,sprite,hp,mp);
                    break;
                case Item.itemType.Equipment:
                    int stength = (int)(temp["stength"].n);
                    int intellect = (int)(temp["intellect"].n);
                    int agility = (int)(temp["agility"].n);
                    int stamina = (int)(temp["stamina"].n);
                    Equipment.EquipmentType equipment = (Equipment.EquipmentType)System.Enum.Parse(typeof(Equipment.EquipmentType), temp["equipType"].str);
                    item = new Equipment(id, name, type, qual, description, capacity, buyPrice, sellPrice, sprite,stength,intellect,agility,stamina,equipment);
                    break;
                case Item.itemType.Weapon:
                    int demage = (int) (temp["damage"].n);
                    Weapon.WeaponType weapon = (Weapon.WeaponType)System.Enum.Parse(typeof(Weapon.WeaponType), temp["weapon"].str);
                    item = new Weapon(id, name, type, qual, description, capacity, buyPrice, sellPrice, sprite,demage,weapon);
                    break;
                case Item.itemType.Material:
                    item = new MaterialL(id, name, type, qual, description, capacity, buyPrice, sellPrice, sprite);
                    break;
            }
            itemList.Add(item);//添加到集合
        }
    }

    // 通过id得到Item对象
    public Item GetItemById(int id)
    {
        foreach (Item item in itemList)
        {
            if (item.ID == id)
            {
                return item;
            }
        }
        return null;
    }

    // 显示提示信息
    public void ShowToolTip(string content)
    {
        if (isPickedItem) return; 
        IsToolTipShow = true;
        toolTip.Show(content);
    }

    // 隐藏提示信息
    public void HideToolTip()
    {
        IsToolTipShow = false;
        toolTip.Hide();
    }

    //捡起物品物品槽指定的物品数量
    public void PickUpItem(Item item,int amount)
    {
        PickedItem.SetItem(item,amount);
        isPickedItem = true;
        PickedItem.Show();
        this.toolTip.Hide();
    }

    /// 从鼠标上移除指定数量的物品
    public void RemoveItem(int amount=1)
    {
        PickedItem.ReduceAmount(amount);
        if (pickedItem.Amount <= 0)
        {
            isPickedItem = false;
            PickedItem.Hide();
        }
    }

    // 保存
    public void SaveInventory()
    {
        Knapsack.Instance.SaveInventory();         //背包
        Chest.Instance.SaveInventory();            //箱子
        CharacterPanel.Instance.SaveInventory();   //角色
        ForgePanel.Instance.SaveInventory();       //锻造
        PlayerPrefs.SetInt("GoldAmount",GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().GoldAmount);//金币
        PlayerPrefs.SetString("PropertyText",CharacterPanel.Instance.PropertyText.text);
    }

    // 加载
    public void LoadInventory()
    {
        Knapsack.Instance.LoadInventory();
        Chest.Instance.LoadInventory();
        CharacterPanel.Instance.LoadInventory();
        ForgePanel.Instance.LoadInventory();
        if (PlayerPrefs.HasKey("GoldAmount"))
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().GoldAmount = PlayerPrefs.GetInt("GoldAmount");
        }

        CharacterPanel.Instance.PropertyText.text = PlayerPrefs.GetString("PropertyText");
    }

    public void SortInventory()
    {
        Knapsack.Instance.Sort();
    }
  
}
