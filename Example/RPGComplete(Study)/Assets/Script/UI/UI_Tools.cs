using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Tools : MonoSingleton<UI_Tools>
{
    Dictionary<EUIType, GameObject> DicUI = new Dictionary<EUIType, GameObject>();

    Camera UICam = null;

    private void Awake()
    {
        UICam = NGUITools.FindCameraForLayer(LayerMask.NameToLayer("UI"));
    }

    GameObject GetUI(EUIType uiType, bool isDontDestroy)
    {
        if (isDontDestroy == false)
        {
            if (DicUI.ContainsKey(uiType) == true)
                return DicUI[uiType];
        }

        GameObject makeUI = null;
        GameObject PrefabUI = Resources.Load("Prefabs/UI/" + uiType.ToString()) as GameObject;

        if (PrefabUI != null)
        {
            makeUI = NGUITools.AddChild(UICam.gameObject, PrefabUI);
            DicUI.Add(uiType, makeUI);

            makeUI.SetActive(false);
        }
        return makeUI;
    }

    public GameObject ShowUI(EUIType uiType)
    {
        GameObject showObject = GetUI(uiType, false);
        if (showObject != null && showObject.activeSelf == false)
        {
            showObject.SetActive(true);
        }
        return showObject;
    }

    public void HideUI(EUIType uiType)
    {
        GameObject showObject = GetUI(uiType, false);
        if (showObject != null && showObject.activeSelf == true)
        {
            showObject.SetActive(false);
        }
    }

}
