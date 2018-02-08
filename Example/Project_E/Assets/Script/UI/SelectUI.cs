using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectUI : MonoBehaviour
{
    Transform myTransform;
    GameObject myGameObject;

    void SelectMusic()
    {

    }

    void SelectCharacter()
    {

    }

    void Init()
    {
        myTransform = GameObject.Find("Select_Panel").transform;
        myGameObject = Instantiate(Resources.Load<GameObject>("Prefabs/UI/SELECT/Select_Canvas"), myTransform);
    }

    private void Awake()
    {
        SelectCharacter();
    }


}
