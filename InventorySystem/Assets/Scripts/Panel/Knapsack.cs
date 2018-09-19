using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knapsack : Inventroy {

    #region 单例模式

    private static Knapsack instance;

    public static Knapsack Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.Find("KnapsackPanel").GetComponent<Knapsack>();
            }
            return instance;
        }
    }

    #endregion

    public override void Start()
    {
        base.Start();
    }
}
