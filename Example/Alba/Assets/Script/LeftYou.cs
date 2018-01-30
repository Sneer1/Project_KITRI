using UnityEngine;
using System.Collections;

public class LeftYou : MonoBehaviour {
	public GameObject ShrimpHunter;
	public AudioClip hook;
	Camera camera;
	// Use this for initialization
	void Start () {
		camera = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera> ();
	}
	
	// Update is called once per frame
	void Update () {
			GameOver();
		//Update_Inputs ();
	}
	
	private void Update_Inputs()
	{
		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			Instantiate(ShrimpHunter);
			//Destroy (gameObject);
		}
	}
	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.name == "Shrimp(Clone)")
		{
			Instantiate(ShrimpHunter);
			camera.GetComponent<AudioSource>().PlayOneShot(hook);
			GameObject.Destroy(other.gameObject);
			GameObject.Destroy(this.gameObject);
		}
		else if (other.gameObject.name == "Fish(Clone)")
		{
			ShrimpJump.GameOver = 1;
			Instantiate(ShrimpHunter);
			camera.GetComponent<AudioSource>().PlayOneShot(hook);
			GameObject.Destroy(other.gameObject);
			GameObject.Destroy(this.gameObject);
		}
	}
	void GameOver()
	{
		if (ShrimpJump.GameOver == 1) {
			//ShrimpJump.GameOver = 2;
			Instantiate (ShrimpHunter);
			GameObject.Destroy (this.gameObject);
		}
	}
}

