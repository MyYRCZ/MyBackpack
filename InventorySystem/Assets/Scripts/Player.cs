using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    #region 基础属性
    private int basicStrength = 10;//力量
    private int basicIntellect = 10;//智力
    private int basicAgility = 10;//敏捷
    private int basicStamina = 10;//体力
    private int basicDamage = 10;//攻击力

    public int BasicStrength
    {
        get { return basicStrength; }
    }

    public int BasicIntrllect
    {
        get { return basicIntellect; }
    }

    public int BasicAgility
    {
        get { return basicAgility; }
    }

    public int BasicStamina
    {
        get { return basicStamina; }
    }

    public int BasicDamage
    {
        get { return basicDamage; }
    }

    #endregion


    // 金币数量
    private int goldAmount = 1000;
    // 金币显示
    private Text goldText;

    public int GoldAmount
    {
        get { return goldAmount; }
        set
        {
            goldAmount = value;
            goldText.text = goldAmount.ToString();
        }
    }

    void Start()
    {
        goldText = GameObject.Find("Gold").GetComponentInChildren<Text>();
        goldText.text = goldAmount.ToString();
    }

    void Update () {
        //G.  随机获得物品
	    if (Input.GetKeyDown(KeyCode.G))
	    {
	        int id = Random.Range(1, 18);
	        Knapsack.Instance.StoreItem(id);
	    }

        //B. 显示隐藏背包面板
	    if (Input.GetKeyDown(KeyCode.B))
	    {
            Knapsack.Instance.DisPlayerSwitch(); 
	    }

        //N. 显示隐藏箱子面板
	    if (Input.GetKeyDown(KeyCode.N))
	    {
            Chest.Instance.DisPlayerSwitch();
	    }

        //M. 显示隐藏角色面板
	    if (Input.GetKeyDown(KeyCode.M))
	    {
            CharacterPanel.Instance.DisPlayerSwitch();
	    }

        //K. 显示隐藏商店面板
        if (Input.GetKeyDown(KeyCode.K))
        {
            ShopPanel.Instance.DisPlayerSwitch();
        }

        //L. 显示隐藏锻造面板
        if (Input.GetKeyDown(KeyCode.L))
        {
            ForgePanel.Instance.DisPlayerSwitch();
        }

        //Esc. 退出程序
        if (Input.GetKeyDown(KeyCode.Escape))
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
        }
    }
    
    // 消费金币
    public bool ConsumeGold(int amount)
    {
        if (goldAmount >= amount)
        {
            goldAmount -= amount;
            goldText.text = goldAmount.ToString();
            return true;
        }
        return false;
    }

    // 得到金币
    public void GetGold(int amount)
    {
        goldAmount += amount;
        goldText.text = goldAmount.ToString();
    }
}
