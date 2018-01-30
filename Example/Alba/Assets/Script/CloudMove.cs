using UnityEngine;
using System.Collections;

public class CloudMove : MonoBehaviour
{
	float x = 10.0f;
	float Spawn_y = 0.0f;
	
	void Start()
	{
		// 생성하 는 두좌 표지 점ㅋㅋ
		Vector3 pos = gameObject.transform.position;
		Spawn_y = (Random.Range (0, 3) * 0.2f);
		pos.y = Spawn_y + 4;
		gameObject.transform.position = pos;
	}
	void Update()
	{
		Move();
		// y값을 gameObject에 적용하세요.
		Vector3 pos = gameObject.transform.position;
		pos.x = x;
		
		gameObject.transform.position = pos;
	}
	void Move() // 점프키 누를때 1회만 호출
	{
		x -= 0.01f;
		//Debug.Log (x);
		if (x < -3)
			Destroy(gameObject);
	}
}
