using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestCode : MonoBehaviour
{
    Renderer[] render;
    // Use this for initialization
    void Start()
    {
        render = this.gameObject.GetComponentsInChildren<Renderer>();
        for(int i = 0; i < render.Length; i++)
        {
            render[i].material.color = new Color(render[i].material.color.r, render[i].material.color.g, render[i].material.color.b, 0.5f);
        }
        
    }
}
