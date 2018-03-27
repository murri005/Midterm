using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{

	public float speed;
	public float gravity;
	public float jumpHeight;
	public Text countText;
	public Text winText;
	public LayerMask ground;
	public Transform feet;

	private Vector3 direction;
	private Vector3 walkingVelocity;
	private Vector3 fallingVelocity;
	// private references to CContr
	private CharacterController controller;
	private int count;
	// Use this for initialization
	void Start()
	{
		speed = 5.0f;
		gravity = 9.8f;
		jumpHeight = 3.0f;
		direction = Vector3.zero;
		fallingVelocity = Vector3.zero;
		controller = GetComponent<CharacterController>();
		// Set the count to zero 
		count = 0;

		// Run the SetCountText function to update the UI (see below)
		SetCountText();

		// Set the text property of our Win Text UI to an empty string, making the 'You Win' (game over message) blank
		winText.text = "";
	}

	// Update is called once per frame
	void Update()
	{
		direction.x = Input.GetAxis("Horizontal");
		direction.z = Input.GetAxis("Vertical");
		direction = direction.normalized;
		walkingVelocity = direction * speed;
		controller.Move(walkingVelocity * Time.deltaTime);
		if (direction != Vector3.zero)
		{
			transform.forward = direction;
			Debug.Log(direction);
		}
		bool isGrounded = Physics.CheckSphere(feet.position, 0.1f, ground, QueryTriggerInteraction.Ignore);
		if (isGrounded)
			fallingVelocity.y = 0f;
		else
			fallingVelocity.y -= gravity * Time.deltaTime;
		if (Input.GetButtonDown("Jump") && isGrounded)
		{
			fallingVelocity.y = Mathf.Sqrt(gravity * jumpHeight);
		}
		controller.Move(fallingVelocity * Time.deltaTime);
	}


	// When this game object intersects a collider with 'is trigger' checked, 
	// store a reference to that collider in a variable named 'other'..
	void OnTriggerEnter(Collider other)
	{
		// ..and if the game object we intersect has the tag 'Pick Up' assigned to it..
		if (other.gameObject.CompareTag("Pick Up"))
		{
			// Make the other game object (the pick up) inactive, to make it disappear
			other.gameObject.SetActive(false);

			// Add one to the score variable 'count'
			count = count + 1;

			// Run the 'SetCountText()' function (see below)
			SetCountText();
		}
	}

	// Create a standalone function that can update the 'countText' UI and check if the required amount to win has been achieved
	void SetCountText()
	{
		// Update the text field of our 'countText' variable
		countText.text = "Count: " + count.ToString();

		// Check if our 'count' is equal to or exceeded 12
		if (count >= 12)
		{
			// Set the text value of our 'winText'
			winText.text = "You Win!";
		}
	}










}





