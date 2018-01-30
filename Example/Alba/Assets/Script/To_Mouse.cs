using UnityEngine;
using System.Collections;

public class To_Mouse : MonoBehaviour
{
		public UnityEngine.UI.Text line;
		public Sprite Mouse0;
		public Sprite Mouse1;
		public Sprite Mouse2;
		SpriteRenderer spriteRenderer;
		float Timer;
		float StartTime;
		float Delay;
		bool Idle;
		bool SelecMouse;
		bool ST;
		float stage;
		int count;
		// Use this for initialization
		void Start ()
		{
				spriteRenderer = gameObject.GetComponent<SpriteRenderer> ();
				spriteRenderer.sprite = Mouse0;
				line.text = "Left Button Click!";
				Delay = 0.5f;
				Timer = Time.time;
				Idle = true;
				stage = 0;
				count = 3;
				ST = true;
		}
	
		// Update is called once per frame
		void Update ()
		{
		if(count < 0){
			Application.LoadLevel("Park");
		}
				if (Input.GetMouseButtonDown (0)) {
						if (stage == 0) {
								line.text = "소녀가 오면 같이 사진을 찍습니다.";
								stage++;
						} else if (stage == 1) {
								line.text = "소년이 오면 Game Over";
								stage++;
						} else if (stage == 2) {
								line.text = "소년이 오면 Right Button Click!";
								SelecMouse = true;
								stage++;
						}
				}
				if (Input.GetMouseButtonDown (1)) {
						if (stage == 3) {
								line.text = "소년을 때릴 수 있다.";
								SelecMouse = true;
								stage++;
						} else if (stage == 4) {
								line.text = "소녀를 때리면 Game Over!";
								SelecMouse = true;
								stage++;
						}
				}
				if (stage == 5) {
						line.text = "아이들이 몰려옵니다. " + count;
						if (ST) {
								StartTime = Time.time;
								ST = false;
								
						}
						int i;
						count =3 + (int)StartTime - (int)Time.time;
						Debug.Log (count);
				}

				


				if (Time.time > Timer + Delay) {
						if (Idle) {
								spriteRenderer.sprite = Mouse0;
								Idle = false;
						} else if (!Idle) {
								if (SelecMouse && stage > 2) {
										spriteRenderer.sprite = Mouse2;
										Idle = true;
								} else if (!SelecMouse) {
										spriteRenderer.sprite = Mouse1;
										Idle = true;
								}
						}
						Timer = Time.time;
				}
		}
}
