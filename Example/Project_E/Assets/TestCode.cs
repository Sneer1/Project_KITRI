using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestCode : MonoBehaviour
{
    public GameObject prefabUI;
    public RectTransform position;
    public Canvas canvas;
    public Button button;
    // Use this for initialization
    void Start ()
    {
        canvas = GetComponent<Canvas>() as Canvas;
        prefabUI = Resources.Load("Prefabs/UI/Button") as GameObject;
        prefabUI = Instantiate(prefabUI, canvas.transform) as GameObject;
        prefabUI.transform.parent = canvas.transform;
        position = prefabUI.GetComponent<RectTransform>();
//        position.anchoredPosition = Vector3.zero;

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
