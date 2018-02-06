using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Actor
{

    public override void ThrowEvent(string keyData, params object[] datas)
    {
        switch (keyData)
        {
            //case ConstValue.EventKey_EnemyInit:
            //    Generator = datas[0] as EnemyRegenerator;
            //    break;
            default:
                base.ThrowEvent(keyData, datas);
                break;
        }
    }

    private new void OnDisable()
    {
        //if (Generator != null)
        //    Generator.RemoveActor(this);

        base.OnDisable();
    }

    private new void OnDestroy()
    {
        base.OnDestroy();
    }


}
