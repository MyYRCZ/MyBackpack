using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 物品
/// </summary>
public class ItemUI : MonoBehaviour
{
    // Item物品
	public Item Item { get; set; }
    // Item的容量
    public int Amount { get; set; }
    // Item图标
    private Image itemImage;
    // Item数量
    private Text amountText;

    private float targetScale = 1.0f;
    private Vector3 animationScale = new Vector3(1.2f, 1.2f, 1.2f);
    private float smoothing = 5.0f;

    private Image ItemImage
    {
        get
        {
            if (itemImage == null)
            {
                itemImage = GetComponent<Image>();
            }
            return itemImage;
        }
    }

    private Text AmountText
    {
        get
        {
            if (amountText == null)
            {
                amountText = GetComponentInChildren<Text>();
            }
            return amountText;
        }
    }

    void Update()
    {
        if (transform.localScale.x != targetScale)
        {
            //动画
            float scale = Mathf.Lerp(transform.localScale.x, targetScale, smoothing * Time.deltaTime);
            transform.localScale = new Vector3(scale, scale, scale);
            if (Mathf.Abs(transform.localScale.x - targetScale) < 0.03f)//节约性能
            {
                transform.localScale = new Vector3(targetScale, targetScale, targetScale);
            }
        }
    }

    // 设置物品
    public void SetItem(Item item, int amount = 1)
    {
        transform.localScale = animationScale;
        this.Item = item;
        this.Amount = amount;
        //更新UI 
        ItemImage.sprite = Resources.Load<Sprite>(item.Sprite);
        if (Item.Capacity > 1)
        {
            AmountText.text = Amount.ToString();
        }
        else
        {
            AmountText.text = "";
        }
    }

    // 增加物品数量
    public void AddAmount(int amount = 1)
    {
        transform.localScale = animationScale;
        this.Amount += amount;
        if (Item.Capacity > 1)
        {
            AmountText.text = Amount.ToString();
        }
        else
        {
            AmountText.text = "";
        }
    }

    // 减少物品数量
    public void ReduceAmount(int amount = 1)
    {
        transform.localScale = animationScale;
        this.Amount -= amount;
        if (Item.Capacity > 1)
        {
            AmountText.text = Amount.ToString();
        }
        else
        {
            AmountText.text = "";
        }
    }

    // 设置物品数量
    public void SetAmount(int amount)
    {
        transform.localScale = animationScale;
        this.Amount = amount;
        if (Item.Capacity > 1)
        {
            AmountText.text = Amount.ToString();
        }
        else
        {
            AmountText.text = "";
        }
    }

    // 当前物品和传入物品交换显示
    public void Exchange(ItemUI itemUI)
    {
        Item itemTemp = itemUI.Item;
        int amountTemp = itemUI.Amount;
        itemUI.SetItem(this.Item,this.Amount);
        this.SetItem(itemTemp,amountTemp);
    }

    // 显示
    public void Show()
    {
        gameObject.SetActive(true);
    }

    // 隐藏
    public void Hide()
    {
        gameObject.SetActive(false);
    }

    // 设置位置
    public void SetLocalPosition(Vector3 position)
    {
        transform.localPosition = position;
    }
}
