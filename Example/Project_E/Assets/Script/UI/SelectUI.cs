using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectUI : MonoBehaviour
{
    Transform myTransform;
    List<GameObject> mylistGameObject = new List<GameObject>();

    void SelectMusic()
    {

    }

    void SelectCharacter()
    {
        //for (int i = 0; i < )
        //{
        //    mylistGameObject.Add(Instantiate(Resources.Load<GameObject>("Prefabs/UI/SELECT/SelectButton"), myTransform));
        //}

    }

    void Init()
    {
        Instantiate(Resources.Load<GameObject>("Prefabs/UI/SELECT/Select_Canvas"), myTransform);
        myTransform = GameObject.Find("Select_Panel").transform;
        SelectCharacter();
    }

    private void Awake()
    {
        Init();
    }


}
