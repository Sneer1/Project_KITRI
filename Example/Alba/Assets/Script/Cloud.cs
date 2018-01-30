using UnityEngine;
using System.Collections;

public class Cloud : MonoBehaviour
{


		// Use this for initialization
		void Start ()
		{

		}
	
		// Update is called once per frame
		void Update ()
		{
				if (!Attack.Last) {
						transform.Translate (Vector3.left * Time.deltaTime);
				}

				if (transform.position.x < -13) {
						Vector3 v;
						v = transform.position;
						v.x = 15.0f;
						transform.position = v;
				}
	
		}
}
