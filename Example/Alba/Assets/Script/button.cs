using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class button : MonoBehaviour {

	Image image;

	void Start()
	{
		image = GetComponent<Image>();
	}

	public void Shimp()
	{
		Application.LoadLevel("shrimp");
	}

	public void Doll()
	{
		Application.LoadLevel("ParkPrologue");
	}

	public void GoHome()
	{
		Application.LoadLevel("Prologue");
	}

	public void Exit()
	{
		Application.Quit();
	}
	// Use this for initialization

}
