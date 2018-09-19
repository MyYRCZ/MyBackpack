using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForgePanel : Inventroy
{

    #region 单例模式

    private static ForgePanel instance;
    public static ForgePanel Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.Find("ForgePanel").GetComponent<ForgePanel>();
            }
            return instance;
        }
    }

    #endregion

    private List<Formula> formulaList;

    public override void Start()
    {
        base.Start();
        ParseFormulaJson();
    }

    void ParseFormulaJson()
    {
        formulaList = new List<Formula>();
        TextAsset formulaText = Resources.Load<TextAsset>("Formula");
        string formulaJson = formulaText.text;//配方信息得Json数据
        JSONObject jo = new JSONObject(formulaJson);
        foreach (JSONObject temp in jo.list)
        {
            int item1ID = (int) temp["Item1ID"].n;
            int item1Amount = (int) temp["Item1Amount"].n;
            int item2ID = (int) temp["Item2ID"].n;
            int item2Amount = (int) temp["Item2Amount"].n;
            int resID = (int) temp["ResID"].n;
            Formula formula = new Formula(item1ID,item1Amount,item2ID,item2Amount,resID);
            formulaList.Add(formula);
        }
    }

    // 锻造物品
    public void ForgeItem()
    {
        // 1.得到当前有哪些材料
        // 2.判断满足哪一个秘笈得要求

        List<int> haveMaterialIDList = new List<int>();//存储当前拥有的材料的ID
        foreach (Slot slot in slots)
        {
            if (slot.transform.childCount > 0)
            {
                ItemUI currentItemUI = slot.transform.GetChild(0).GetComponent<ItemUI>();
                for (int i = 0; i < currentItemUI.Amount; i++)
                {
                    haveMaterialIDList.Add(currentItemUI.Item.ID);
                }
            }
        }

        Formula temp = null;
        foreach (Formula formula in formulaList)
        {
            bool isMatch = formula.Match(haveMaterialIDList);
            if (isMatch)
            {
                temp = formula;
                break;
            }
        }

        if (temp != null)
        {
            Knapsack.Instance.StoreItem(temp.ResID);//将生成的物品放入背包 
            //去掉消耗的材料
            foreach (int id in temp.NeedIDList)
            {
                foreach (Slot slot in slots)
                {
                    if (slot.transform.childCount > 0)
                    {
                        ItemUI itemUI = slot.transform.GetChild(0).GetComponent<ItemUI>();
                        if (itemUI.Item.ID == id && itemUI.Amount>0)
                        {
                            itemUI.ReduceAmount();
                            if (itemUI.Amount <= 0)
                            {
                                DestroyImmediate(itemUI.gameObject);
                            }
                            break;
                        }
                    }
                }
            }
        }
    }
}
