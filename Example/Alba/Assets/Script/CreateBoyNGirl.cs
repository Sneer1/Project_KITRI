using UnityEngine;
using System.Collections;

public class CreateBoyNGirl : MonoBehaviour
{
	public GameObject Boy;
	public GameObject Girl;
	public GameObject QBoy;
	public GameObject QGirl;
	public GameObject Spawn2;
	public GameObject QSpawn1;
	public GameObject QSpawn2;
	float timer;
	float SpawnTime = 2.0f;
	float speed;
	int sec;
	float StartTime;
	float ToTime;
	// Use this for initialization
	void Start ()
	{
		Spawn2 = GameObject.Find ("Spawn2");
		QSpawn1 = GameObject.Find ("QSpawn1");
		QSpawn2 = GameObject.Find ("QSpawn2");
		StartTime = Time.time;
	}
	
	// Update is called once per frame
	void Update ()
	{
		ToTime = Time.time - StartTime;
		speed = 1.0f + (ToTime * 3.0f) * Time.deltaTime;
		if (Time.time > timer + SpawnTime / speed) {
			sec = Random.Range (0, 3);
			if (sec == 0) {
				sec = Random.Range (0, 2);
				if (sec == 0)
					Instantiate (Boy, transform.position, transform.rotation);
				else if (sec == 1)
					Instantiate (Girl, transform.position, transform.rotation);

			} else if (sec == 1) {
				sec = Random.Range (0, 2);
				if (sec == 0)
					Instantiate (Boy, Spawn2.transform.position, transform.rotation);
				else if (sec == 1)
					Instantiate (Girl, Spawn2.transform.position, transform.rotation);
			} else if (sec == 2) {
				sec = Random.Range (0, 2);
				if (sec == 0) {
					sec = Random.Range (0, 2);
					if (sec == 0) {
						Instantiate (QGirl, QSpawn1.transform.position, QSpawn1.transform.rotation);
					} else if (sec == 1) {
						Instantiate (QBoy, QSpawn1.transform.position, QSpawn1.transform.rotation);
					}
				} else if (sec == 1) {
					sec = Random.Range (0, 2);
					if (sec == 0) {
						Instantiate (QGirl, QSpawn2.transform.position, QSpawn2.transform.rotation);
					} else if (sec == 1) {
						Instantiate (QBoy, QSpawn2.transform.position, QSpawn2.transform.rotation);
					}
				}
										
			}
			timer = Time.time;
		}
	}
}
