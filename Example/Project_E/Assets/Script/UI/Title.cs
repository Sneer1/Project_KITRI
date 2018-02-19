using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour
{
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Scene_Manager.Instance.LoadScene(E_SCENETYPE.SCENE_INTRO, false);
            Scene_Manager.Instance.UpdateScene();
        }
    }
}
