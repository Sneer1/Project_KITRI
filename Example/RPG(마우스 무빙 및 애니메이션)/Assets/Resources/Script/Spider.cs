using UnityEngine;
using System.Collections;

public class Spider : MonoBehaviour {

    Animation animation;

	IEnumerator Start()
	{
        animation = this.GetComponent<Animation>();

				AnimationState idleState = animation["iddle"];
				idleState.wrapMode = WrapMode.Loop;
				idleState.normalizedTime = 0.0f;
				idleState.speed = 1.0f;

				Debug.Log ("idle length:" + idleState.length.ToString ());

				animation.Play ("iddle");

				yield return new WaitForSeconds (0.4f);

				Debug.Log ("idle time:" + idleState.time.ToString ());

		AnimationEvent animationEvent = new AnimationEvent ();
		float eventTime = animation ["attack_Melee"].clip.length * 0.9f;
		animationEvent.time = eventTime;
		Debug.Log ("Event Time:" + eventTime);
		animationEvent.messageOptions = SendMessageOptions.DontRequireReceiver;
		animationEvent.functionName = "OnAttack";
		animation["attack_Melee"].clip.AddEvent(animationEvent);
	}

	void Update()
	{
				if (Input.GetKeyDown (KeyCode.A)) {
						animation.Play ("attack_Melee");
						animation.PlayQueued ("iddle", QueueMode.CompleteOthers);
				}
	}

		void OnAttack()
		{
			Debug.LogWarning("=========Attack========");

			AnimationState attackState = animation["attack_Melee"];
			Debug.Log ( "length:" + attackState.length.ToString ());
			Debug.Log ( "time:" + attackState.time.ToString ());
		}
}
