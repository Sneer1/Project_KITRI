using UnityEngine;
using System.Collections;

public class ShrimpHunter : MonoBehaviour {
	public GameObject MrYouLeft;
	public GameObject MrYouRight;
	public GameObject End;
	public AudioClip hook;
	public int CountNum = 0;
	Camera camera;
	// Use this for initialization
	void Start () {
		camera = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera> ();
			
	}
	
	// Update is called once per frame
	void Update () {
		if (ShrimpJump.GameOver == 0)
						Update_Inputs ();
				else if (ShrimpJump.GameOver == 1) {
						if(CountNum == 0)
						Instantiate(End);
		            	CountNum = 1;
			            ShrimpJump.GameOver =2;
				} //else if (ShrimpJump.GameOver == 2)
					//	Debug.Log (ShrimpJump.GameOver);
	}

	private void Update_Inputs()
	{

		if (Input.GetMouseButtonDown (0)) {
			Instantiate(MrYouLeft);
			camera.GetComponent<AudioSource>().PlayOneShot(hook);
			Destroy (gameObject);
				}
		else if (Input.GetMouseButtonDown (1)) {
			Instantiate(MrYouRight);
			camera.GetComponent<AudioSource>().PlayOneShot(hook);
			Destroy (gameObject);
				}
	}
}
