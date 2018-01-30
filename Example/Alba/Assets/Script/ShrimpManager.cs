using UnityEngine;
using System.Collections;

public class ShrimpManager : MonoBehaviour {
	public GameObject cube;
	public GameObject Fish;
	public GameObject Cloud;

	float CUBE_DELAY = 1.5f; //큐브 생성시간 초기값
	float CLOUD_DELAY = 10.0f;//cloud edit
	float cubeDelay;//큐브 생성시간
	float cloudelay;
	// Use this for initialization

	void Start () {
		Instantiate(Cloud);
		cubeDelay = CUBE_DELAY;
		cloudelay = CLOUD_DELAY;
	}
	
	// Update is called once per frame
	void Update () {
		cubeDelay -= Time.deltaTime;
		cloudelay -= Time.deltaTime;
		if (ShrimpJump.GameOver == 0) {
						if (cloudelay <= 0) {
								Instantiate (Cloud);
								cloudelay = CLOUD_DELAY;
						}

						if (cubeDelay <= 0) {
								int i = Random.Range (0, 2);
								if (i == 0)
										Instantiate (cube);
								else if (i == 1)
										Instantiate (Fish);

								CUBE_DELAY = Mathf.Clamp (CUBE_DELAY * 0.99f, 0.3f, 1.5f);
								//1.5f 에서 점점 생성시간이 줄어듭니다. 최대 0.2초 까지 줄어듬
								cubeDelay = CUBE_DELAY;
						}
				}
	}
}
