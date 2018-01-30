using UnityEngine;
using System.Collections;

public class End : MonoBehaviour {
	public GameObject You;
	public GameObject Sweat;
	float Speed = 0.2f;
	public int YouFace = 0;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		EndSin ();
	}

	void EndSin() {
		Vector3 pos = gameObject.transform.position;
		if (pos.z < 0) {
						Speed = 0.0f;
			            pos.z = 0;
		             	YouFace = 1;
				}
		else
						pos.z -= Speed;
		Debug.Log (pos.z);
		gameObject.transform.position = pos;

		if (YouFace == 1) {
			YouFace = 2;
			Instantiate (You);
			Instantiate(Sweat); }
		if (YouFace == 2) {
			if(Input.GetMouseButtonDown (0))
				Application.LoadLevel("End"); }

	}
}
