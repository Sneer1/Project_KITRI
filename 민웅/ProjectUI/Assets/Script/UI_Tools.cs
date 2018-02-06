using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UI_Tools : MonoSingleton<UI_Tools>
{
    Dictionary<E_UITYPE, GameObject> DicUI = new Dictionary<E_UITYPE, GameObject>();


    GameObject GetUI(E_UITYPE _uiType, bool isDontDestroy)
    {
        if (isDontDestroy == false)
        {
            if (DicUI.ContainsKey(_uiType) == true)
                return DicUI[_uiType];
        }

        GameObject makeUI = null;
        GameObject prefabUI = Resources.Load("Prefabs/UI/" + _uiType.ToString()) as GameObject;

        if(prefabUI != null)
        {
            Canvas canvas = GameObject.FindGameObjectWithTag("UI_Canvas").GetComponent<Canvas>();
            makeUI = Instantiate(prefabUI, canvas.transform) as GameObject;            
            makeUI.transform.parent = canvas.transform;
            RectTransform RectPos = makeUI.GetComponent<RectTransform>();

            makeUI.SetActive(false);

            DicUI.Add(_uiType, makeUI);
        }
        return makeUI;
    }

    public GameObject ShowUI(E_UITYPE _uiType)
    {
        GameObject showObject = GetUI(_uiType, false);
        if (showObject != null && showObject.activeSelf == false)
        {
            showObject.SetActive(true);
        }
        return showObject;
    }

    public void HideUI(E_UITYPE _uiType)
    {
        GameObject showObject = GetUI(_uiType, false);
        if (showObject != null && showObject.activeSelf == true)
        {
            showObject.SetActive(false);
        }
    }

    public void Clear()
    {
        foreach (KeyValuePair<E_UITYPE, GameObject> pair in DicUI)
        {
            Destroy(pair.Value);
        }

        DicUI.Clear();
    }

}
