using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestCode : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        Material skymaterial = Resources.Load("Materials/SkyBox_Night") as Material;
        Camera.main.GetComponent<Skybox>().material = skymaterial;
    }
}
