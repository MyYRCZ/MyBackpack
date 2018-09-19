using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Inventroy {

    #region 单例模式

    private static Chest instance;

    public static Chest Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.Find("ChestPanel").GetComponent<Chest>();
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
