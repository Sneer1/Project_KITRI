using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Actor
{
    public override void ThrowEvent(string keyData, params object[] datas)
    {
        switch (keyData)
        {
            default:
                base.ThrowEvent(keyData, datas);
                break;
        }
    }

    private new void OnDisable()
    {
        base.OnDisable();
    }

    private new void OnDestroy()
    {
        base.OnDestroy();
    }


}
