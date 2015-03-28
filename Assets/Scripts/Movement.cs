using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {
	public float speed = 10.0f;
	public float rotationSpeed = 10.0f;
	public float jumpSpeed = 10.0f;
	public float gravity = 20.0f;
	private Vector3 movement = Vector3.zero;
	private float fallStartLevel;
	private bool falling;
	private bool grounded = false;
	private CharacterController controller;
	private Camera _camera;

	// Use this for initialization
	void Start () {
		controller = GetComponent<CharacterController>();
		_camera = GetComponentInChildren<Camera> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(controller.isGrounded)
		{
			movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis ("Vertical"));
			movement = _camera.transform.TransformDirection(movement);
			movement.y = 0;
			//movement = transform.TransformDirection(movement);
			movement *= speed;
			if(Input.GetButton("Jump"))
			{
				movement.y = jumpSpeed;
			}
		}

		movement.y -= gravity * Time.deltaTime;
		controller.Move (movement * Time.deltaTime);

	}
}
