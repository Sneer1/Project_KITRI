using UnityEngine;
using System.Collections;

public class Tests : MonoBehaviour {

	public UnityEngine.UI.Text lines;
	public GameObject thePrefab;
	bool seclect = true;
	GameObject tempParent;
//	public GUIText lines;
	int length = 1;
	float SatrtTimes;
	float times;
	float Endtimes=0;
	float count = 0;
	bool reading = true;
	string[] str = {"SNL작가일을 잘리고...","당장 쓰레기통에 들어가지 않으려면 알바를 해야한다..", "알바를 선택하시오."};
	int strNum = 0;
	float temp = 0;
	// Use this for initialization
	void Start () {
		SatrtTimes = Time.time;
		tempParent = GameObject.Find ("Canvas");
		ShrimpJump.GameOver = 0;

	}
	
	// Update is called once per frame
	void Update () {
		times = Time.time - SatrtTimes - count;
		
		
		if (!reading) {
			temp= Time.time - Endtimes;

		}
		if(seclect){
			if((int)strNum >= (int)str.Length)
			{
				GameObject child = Instantiate(thePrefab, transform.position, transform.rotation) as GameObject;
				child.transform.parent = tempParent.transform;
				seclect = false;
			}
		}

		readText();
		if(Input.GetMouseButtonDown(0)){
			if(reading)
				count = count - (str[strNum].Length - length) * 0.3f;
			else
			{
//				if(str[strNum+1] != null){
					strNum++;
					length = 1;
					reading = true;
					count = count + temp;
			}
			
		}
		
	}
	
	void readText()
	{
		if(reading){
			
			if(times > 0.3f){
				lines.text = str[strNum].Substring(0, length);
				length ++;
				if(length > str[strNum].Length){
					reading = false;
					Endtimes = Time.time;

				}
				count = count +0.3f;
			}
			
		}
	}
}
