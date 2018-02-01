using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Lobby : BaseObject
{
    UIButton InvenBtn = null;

    private void Awake()
    {
        Transform trans = FindInChild("InvenBtn");
        if(trans == null)
        {
            Debug.LogError("not found inv");
            return;
        }

        InvenBtn = trans.GetComponent<UIButton>();
        //#1
        //InvenBtn.onClick.Add(new EventDelegate(this, "OnClickedInvenBtn"));

        //#2
        EventDelegate.Add(InvenBtn.onClick, new EventDelegate(this, "OnClickedInvenBtn"));

        //#3
        //EventDelegate.Add(InvenBtn.onClick, () => { });
    }


    public void OnClickedInvenBtn()
    {
        GameObject go = UI_Tools.Instance.ShowUI(EUIType.PF_UI_Inventory);
        UI_Inventory inven = go.GetComponent<UI_Inventory>();
        inven.Init();
        inven.Reset();
    }
}
