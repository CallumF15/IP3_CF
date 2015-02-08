using UnityEngine;
using System.Collections;
using Leap;

public class LeapBehaviour : MonoBehaviour
{

	Controller controller;

	// Use this for initialization
	void Start ()
	{
	
		controller = new Controller ();

		//controller.EnableGesture(Gesture.GestureType.TYPECIRCLE);
		//controller.EnableGesture(Gesture.GestureType.TYPEINVALID);
		//controller.EnableGesture(Gesture.GestureType.TYPEKEYTAP);
		//controller.EnableGesture(Gesture.GestureType.TYPESCREENTAP);
		controller.EnableGesture (Gesture.GestureType.TYPESWIPE);
	}
	
	// Update is called once per frame
	void Update ()
	{
		OnFrame (controller);

	}

	public void OnFrame (Controller controller)
	{
		Frame frame = controller.Frame ();
		GestureList gestures = frame.Gestures ();

		for (int i = 0; i < gestures.Count; i++) {
			Gesture gesture = gestures [0];
			switch (gesture.Type) {
			case Gesture.GestureType.TYPECIRCLE:
				Debug.Log ("Circle");
				break;
			default:
				Debug.Log ("Bad gesture type");
				break;
			}
		}
	}


	
//	void OnApplicationQuit ()
//	{
//		controller.Dispose ();
//		listener.Dispose ();
//		#if UNITY_STANDALONE_WIN && !UNITY_EDITOR && UNITY_3_5
//		mono_thread_exit ();
//		#endif
//	}
}
