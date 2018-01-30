using UnityEngine;
using System.Collections;

public class ParkPrologue : MonoBehaviour
{

		public UnityEngine.UI.Text lines;
		public GameObject U;
		public GameObject Mouse;
		bool seclect = true;
		GameObject tempParent;
		//	public GUIText lines;
		int length = 1;
		float SatrtTimes;
		float times;
		float Endtimes = 0;
		float count = 0;
		bool reading = true;
		string[] str = {"아니... ","니네만 사람이냐 나도 사람이다", "니네들 다 죽었어", "소녀들은 이리와 흐흐"};
		int strNum = 0;
		float temp = 0;
		// Use this for initialization
		void Start ()
		{
				SatrtTimes = Time.time;
				tempParent = GameObject.Find ("Canvas");
				Attack.Last = false;
				
		}
	
		// Update is called once per frame
		void Update ()
		{
				times = Time.time - SatrtTimes - count;
		
		
				if (!reading) {
						temp = Time.time - Endtimes;
			
				}
				if (seclect) {
						if ((int)strNum >= (int)str.Length) {


//				seclect = false;
								Instantiate (U, new Vector3 (-1.0f, -0.3f, 0f), transform.rotation);
								GameObject child = Instantiate (Mouse, new Vector3 (0.937f, 0.544f, 0f), transform.rotation) as GameObject;
								child.transform.parent = tempParent.transform;
								Destroy (GameObject.Find ("Image"));
								Destroy (GameObject.Find ("Text"));
								Destroy (GameObject.Find ("Face"));
								
						}
				}
				readText ();
				if (Input.GetMouseButtonDown (0)) {
						if (reading)
								count = count - (str [strNum].Length - length) * 0.3f;
						else {
								//				if(str[strNum+1] != null){
								strNum++;
								length = 1;
								reading = true;
								count = count + temp;
						}
			
				}
		
		}
	
		void readText ()
		{
				if (reading) {
			
						if (times > 0.3f) {
								lines.text = str [strNum].Substring (0, length);
								length ++;
								if (length > str [strNum].Length) {
										reading = false;
										Endtimes = Time.time;
					
								}
								count = count + 0.3f;
						}
			
				}
		}
}
