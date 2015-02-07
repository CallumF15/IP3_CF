using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{
	enum movementType
	{
		stand,
		walk,
		run,
		jump }
	;

	public float PlayerSpeed = 5.0f;
	public float JumpPower = 3.0f;
	public float JumpHeight = 10.0f;
	private double acceleration = 0.2;
	private Vector2 moveDirection = Vector2.zero;
	public bool isGrounded = true;
	float maximumSpeed = 15.0f;
	bool toggleRun = false;
	movementType defaultType, currentMovement;



	// Use this for initialization
	void Start ()
	{
		defaultType = movementType.stand;
	}
	
	// Update is called once per frame
	void Update ()
	{
		Move ();
	}

	void checkMovement ()
	{
		var horizontalMovement = 
			Input.GetAxis ("Horizontal") <= 1 && 
			Input.GetAxis ("Horizontal") >= -1 && 
			Input.GetAxis ("Horizontal") != 0;


		if (Input.GetKey (KeyCode.LeftShift) || Input.GetKey (KeyCode.RightShift))
			toggleRun = true;

		if (horizontalMovement)
			currentMovement = movementType.walk;
		else if (Input.GetKey (KeyCode.Space))
			currentMovement = movementType.jump;
		else if (horizontalMovement && toggleRun)
			currentMovement = movementType.run;
	}

	/// <summary>
	/// Left/Right Horizontal Movement
	/// Jump Vertical Movement
	/// </summary>
	public void Move ()
	{

		float horizontal = Input.GetAxis ("Horizontal");
		Debug.Log ("Axis value: " + horizontal);

		Vector3 movement = new Vector3 (horizontal, 0.0f, 0.0f);
		rigidbody.velocity = movement * PlayerSpeed;


		//Jump code below:
		
		if (Input.GetKey (KeyCode.Space) && isGrounded == true) {
			//rigidbody.AddForce(transform.up * JumpPower);

			rigidbody.AddForce (transform.up * JumpPower, ForceMode.Force);

			if (acceleration < maximumSpeed) {


			}


			//rigidbody.AddForce(new Vector3(0, 10, 0), ForceMode.Impulse);
			isGrounded = false;
		}

		//need to check for ground collision
	}

	void OnCollisionEnter (Collision hit)
	{
		if (hit.gameObject.tag == "ground") {
			isGrounded = true;
			Debug.Log ("hit");
		} else {
			Debug.Log ("no hit");
			isGrounded = false;
		}
			
	}

}
