using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour
{
		static public bool Last;
		public Sprite sPicture;
		public Sprite sStend;
		public Sprite sAttack;
		SpriteRenderer spriteRenderer;
		float Timer;
		float Delay;
		float KeepTime;
		public bool motion = false;
		public bool Pic = false;
		public bool Att = false;
		float speed;
		public AudioClip swing;
		public GameObject hego;
		float StartTime;
		float ToTime;
		
	
		// Use this for initialization
		void Start ()
		{
				spriteRenderer = gameObject.GetComponent<SpriteRenderer> ();
				Timer = Time.time;
				Delay = 0.2f;
				KeepTime = 0.5f;
				StartTime = Time.time;
		}
	
		// Update is called once per frame
		void Update ()
		{
		ToTime = Time.time - ToTime;
				speed = 1.0f + (StartTime * Time.deltaTime);

				if (!motion) {
//						if (Time.time > Timer + Delay/speed) {
				
						if (Input.GetMouseButtonDown (0)) {
								spriteRenderer.sprite = sPicture;
								Pic = true;
								StartCoroutine (WaitAndPrint ());
					
						}
						if (Input.GetMouseButtonDown (1)) {
								GetComponent<AudioSource>().PlayOneShot (swing);
								spriteRenderer.sprite = sAttack;
								Att = true;
								StartCoroutine (WaitAndPrint ());
						}
						//				}
				}
		}
	
		IEnumerator WaitAndPrint ()
		{
				motion = true;
				yield return new WaitForSeconds (KeepTime / speed);
				spriteRenderer.sprite = sStend;
				Timer = Time.time;
				motion = false;
				Pic = false;
				Att = false;
		}

		void OnTriggerEnter (Collider other)
		{
				if (Pic) {
						if (other.gameObject.tag == "BOY") {
								//		Time.timeScale = 0;
								Instantiate (hego, transform.position, transform.rotation);
								StartCoroutine (End ());
								//		Pic = false;
						} else if (other.gameObject.tag == "GIRL") {
								GameObject.Destroy (other.gameObject);
								//		Pic = false;
						}
				} else if (Att) {
						if (other.gameObject.tag == "BOY") {
								GameObject.Destroy (other.gameObject);
								//		Att = false;

						} else if (other.gameObject.tag == "GIRL") {
								//			Time.timeScale = 0;
								Instantiate (hego, transform.position, transform.rotation);
								//	yield return StartCoroutine (Sleep (1.0f));
								StartCoroutine (End ());
								//			Att = false;
						}
				} else {
						//			Time.timeScale = 0;
						Instantiate (hego, transform.position, transform.rotation);
						//	yield return StartCoroutine (Sleep (1.0f));
						StartCoroutine (End ());
						//						Debug.Log (Pic);
//						Debug.Log (Att);
				}
		}

		IEnumerator End ()
		{
				Last = true;
				yield return new WaitForSeconds (3);
				Application.LoadLevel ("End");
		}
}