using UnityEngine;
using System.Collections;

public class test : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnCollisionEnter(Collision hit){
		if (hit.gameObject.tag == "ground")
			Debug.Log ("hit");
		else
			Debug.Log ("no hit");
	}
}
