using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{

	//private CharacterController player;
	private Rigidbody player;
	public GameObject cam;
	public Legs legs;
	public Body body;
	//public Vector3 gravity;
	public Vector3 camOffset; // target position for camera
	public Quaternion camRotation; // target rotation for camera
	
	private bool cursorLocked = false;
	private float camSpeed = 10; // sensitivity
	private float rotateSpeed = 5.0f; // how fast you rotate to a camera
	
	private Vector3 camCurrentOffset;
	/* public Body body; // need to assign this & create class
	 * public Legs legs; // need to assign this & create class
	 * public Arm right; // need to assign this & create class
	 * public Arm left; // need to assign this & create class
	 */
	void Start()
	{
		//player = GetComponent<CharacterController>();
		player = GetComponent<Rigidbody>();
		cam = GameObject.Find("Camera");
		//gravity = Physics.gravity; // need to figure out gravity eventually
		camOffset = cam.transform.position;
		camCurrentOffset = camOffset;
		legs = GetComponentInChildren<Legs>();
        body = GetComponentInChildren<Body>();
        //Cursor.lockState = CursorLockMode.Locked;
    }

	// Update is called once per frame
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape)) // toggling locked mode on cursor with escape key **open pause menu**
		{
			cursorLocked = !cursorLocked;
			if (cursorLocked)
			{
				Cursor.lockState = CursorLockMode.Locked;
				print("locking cursor");
				// show pause menu
				//Time.timeScale = 0.01f; // hella broken
				//print(Time.timeScale);
				//Time.fixedDeltaTime = .02f;
				
			}
			else
			{
				Cursor.lockState = CursorLockMode.None;
				print("unlocking cursor");
				// close pause menu
				//Time.timeScale = 1;
				//print(Time.timeScale);
				//Time.fixedDeltaTime = 0f;
			}
		}
	}

	void FixedUpdate()
	{
		
		if (cursorLocked) // aka not paused
		{
			// using rotateAround to rotate around the player's body
			cam.transform.RotateAround(body.transform.position, Vector3.up, Input.GetAxis("Mouse X") * camSpeed);
			cam.transform.RotateAround(body.transform.position, cam.transform.right, Input.GetAxis("Mouse Y") * -camSpeed);

			// stopping it from going upside down -- didn't work rip
			//Vector3 rotation = cam.transform.eulerAngles;
			//rotation.x = Mathf.Clamp(rotation.x, 0, 180);
			//cam.transform.eulerAngles = rotation;
			//print(cam.transform.rotation);

		}
		else
		{
			return;
		}

		camCurrentOffset = cam.transform.position - transform.position;

		if (!Input.GetKey(KeyCode.LeftAlt)) // rotate body if you're not pressing alt
		{
			Quaternion targetRotation = Quaternion.Euler(0f, cam.transform.rotation.eulerAngles.y, 0f); // y rotation of cam
			player.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotateSpeed);

        }
		//player.MoveRotation(Quaternion.Euler(0f, cam.transform.rotation.eulerAngles.y, 0f)); // change to head to instantly snap
		

		legs.move(); 

		/*Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), player.velocity.y, Input.GetAxis("Vertical"));
		print("movement not normal: " + movement);
		movement.Normalize();
		print("movement normal: " + movement);
		movement.x *= speed;
		movement.z *= speed;

		player.velocity = Vector3.Lerp(player.velocity, movement, movementSnapping);
		print("movement: " + movement);*/


		/*if (!Physics.Raycast(transform.position, Vector3.down, .1f)){ // gravity
			player.AddForce(Physics.gravity, ForceMode.Acceleration);
			print("gravity moment");
		}
		else
		{
			player.velocity = new Vector3(player.velocity.x, 0, player.velocity.z);
		}*/
		//Debug.DrawRay(transform.position, Vector3.down * .1f, Color.yellow);
		//print(player.velocity);

		/*if (player.isGrounded)
		{
			//read for jump input
		}
		else
		{
			//movement += gravity;
		}
		//print(movement);
		player.Move(movement);*/



	}

	private void LateUpdate()
	{
		if (cursorLocked && Time.timeScale==1)
		{
			cam.transform.position = transform.position + camCurrentOffset;
			//raycast from origin point to camera location, bring it closer as needed, if ray doesn't hit anything then bring back to original position
		}
	}
}
