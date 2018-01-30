using UnityEngine;
using System.Collections;

public class QMoveTarget : MonoBehaviour
{
		public GameObject Target;
		public AudioClip Punch;
		public AudioClip Pict;
		Animator Ani;
		Vector3 Vec;
		float speed = 1;
		Attack attack;
		float StartTime;
		float ToTime;
		// Use this for initialization
		void Start ()
		{
				Target = GameObject.Find ("EventSpawn");
				Vec = (Target.transform.position - transform.position).normalized;
				Ani = gameObject.GetComponent<Animator> ();
				attack = GameObject.FindWithTag ("U").GetComponent<Attack> ();
				StartTime = Time.time;
		}
	
		// Update is called once per frame
		void Update ()
		{
		ToTime = Time.time - StartTime;
				if (!Attack.Last) {
			speed = (3.0f + (ToTime * 2.0f) * Time.deltaTime) * 2;
						transform.Translate (Vec * Time.deltaTime * speed);
				}
		
		}

		void OnTriggerEnter (Collider other)
		{
				if (other.tag == "U") {
						if (attack.Att) {
								other.GetComponent<AudioSource>().PlayOneShot (Punch);
			
						}
						if (attack.Pic) {
								other.GetComponent<AudioSource>().PlayOneShot (Pict);
						}
						Ani.SetBool ("Death", true);
				}
		}
}
