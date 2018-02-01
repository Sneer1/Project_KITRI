using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Inventory : MonoBehaviour
{
    bool IsInit = false;
    GameObject ItemPrefab;
    UIGrid Grid;

    public void Init()
    {
        if (IsInit == true)
            return;

        ItemPrefab = Resources.Load("Prefabs/UI/" + EUIType.PF_UI_Item.ToString()) as GameObject;

        Grid = GetComponentInChildren<UIGrid>();

        IsInit = true;
    }

    public void Reset()
    {
        for(int i = 0; i < Grid.transform.childCount; ++i)
        {
            Destroy(Grid.transform.GetChild(i).gameObject);
        }
        AddItem();
    }

    void AddItem()
    {
        //ItemInfo / ItemInstance(ItemInfo[Have])
        //List
        //for()
        for (int i = 0; i < Random.Range(5,25); ++i)
        {
            //
            GameObject go = Instantiate(ItemPrefab, Grid.transform) as GameObject;
            go.transform.localPosition = Vector3.zero;
            //go.GetComponent<UI_ITEM>.
        }

        //Grid.Reposition();
        Grid.repositionNow = true;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Backspace))
        {
            UI_Tools.Instance.HideUI(EUIType.PF_UI_Inventory);
        }
    }

}
