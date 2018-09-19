using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Formula { 

	public int Item1ID { get; private set; }
    public int Item1Amount { get; private set; }
    public int Item2ID { get; private set; }
    public int Item2Amount { get; private set; }

    // 锻造结果的ID
    public int ResID { get; private set; }
    private List<int> needIDList = new List<int>();//锻造物品需要得IDlist

    public List<int> NeedIDList
    {
        get { return needIDList; }
    }

    public Formula(int item1ID, int item1Amount, int item2ID, int item2Amount, int resID)
    {
        this.Item1ID = item1ID;
        this.Item1Amount = item1Amount;
        this.Item2ID = item2ID;
        this.Item2Amount = item2Amount;
        this.ResID = resID;

        //初始化needIDList
        for (int i = 0; i < Item1Amount; i++)
        {
            needIDList.Add(Item1ID);
        }
        for (int i = 0; i < Item2Amount; i++)
        {
            needIDList.Add(Item2ID);
        }
    }


    // 传入的List是否匹配锻造秘笈
    public bool Match(List<int> idList)
    {
        List<int> tempList = new List<int>(idList);
        foreach (int id in needIDList)
        {
            bool isSuccess = tempList.Remove(id);
            if (!isSuccess)
            {
                return false;
            }
        }
        return true;
    }
}
