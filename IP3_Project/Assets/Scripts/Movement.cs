using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{
	enum movementType
	{
		stand,
		walk,
		run,
		jump,
		fall
	};


	private float normalSpeed = 5.0f;
	public float walkSpeed = 5.0f;
	public float runSpeed = 10.0f;
	public float JumpPower = 3.0f;

	public bool isGrounded;
	private bool faceRight = true;
	bool toggleRun = false;

	movementType defaultType, currentMovement;

	private float distanceToGround;
	RaycastHit hit;

	// Use this for initialization
	void Start ()
	{
		distanceToGround = collider.bounds.extents.y;
		defaultType = movementType.stand;
	}
	
	// Update is called once per frame
	void Update ()
	{
		CheckMovement ();
		Move ();

		Debug.Log (currentMovement);
	}

	void FixedUpdate ()
	{
		CheckGrounded ();
		ChangeFacingDirection ();
	}

	void check ()
	{
		//Android/IOS touch code here
		//iPhoneTouch touch;
	}

	void ChangeFacingDirection(){

		if (Input.GetAxis ("Horizontal") > 0) 
			faceRight = true;
		else
			if(Input.GetAxis("Horizontal") < 0)
			faceRight = false;

		if (faceRight)
			transform.forward = new Vector3(1f, 0f, 0f);

		if (!faceRight)
			transform.forward = new Vector3(-1f, 0f, 0f);
	}
	
	void CheckMovement ()
	{
		var horizontalMovement = 
			Input.GetAxis ("Horizontal") <= 1 && 
			Input.GetAxis ("Horizontal") >= -1 && 
			Input.GetAxis ("Horizontal") != 0;


		if (Input.GetKeyDown (KeyCode.LeftShift) || Input.GetKeyDown (KeyCode.RightShift)) {
			toggleRun = !toggleRun;

			if (toggleRun)
				normalSpeed = runSpeed;
			else
				normalSpeed = walkSpeed;
		}

		if (isGrounded == true) {
			if (horizontalMovement)
				currentMovement = movementType.walk;
			else 
			if (Input.GetKeyDown (KeyCode.Space))
				currentMovement = movementType.jump;
			else 
				if (horizontalMovement && toggleRun)
				currentMovement = movementType.run;
		}
	}

	/// <summary>
	/// Left/Right & Jump Movement
	/// </summary>
	public void Move ()
	{
		float horizontal = Input.GetAxis ("Horizontal");

		//Horizontal movement code:
		if (currentMovement == movementType.walk || currentMovement == movementType.run) {
			Vector3 movement = new Vector3 (horizontal, 0.0f, 0.0f);
			rigidbody.velocity = movement * normalSpeed;
		}

		//Jump code:
		if (currentMovement == movementType.jump && isGrounded == true) {
			rigidbody.AddForce (Vector3.up * JumpPower, ForceMode.Impulse);
		}
	}
	
	void CheckGrounded ()
	{
		Debug.DrawRay (transform.position, -Vector3.up); //remove when done testing

		if (Physics.Raycast (transform.position, -Vector3.up, distanceToGround + 0.1f)) {
			isGrounded = true;
		} else
			isGrounded = false;
	}

}
