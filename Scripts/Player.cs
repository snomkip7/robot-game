using System.IO;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

	//private CharacterController player;
	public Rigidbody player;
	public GameObject cam;
	public Legs legs;
	public Body body;
	public Arm leftArm;
	public Arm rightArm;
	public Transform head;
	
	//public Vector3 gravity;
	public Vector3 camOffset; // target position for camera
	public Quaternion camRotation; // target rotation for camera
	public float camDistance = 30.82f;
	public Quaternion camLastRotation; // last rotation for camera, matters if alt is pressed
	public bool canWalk = true;

	[SerializeField] private LayerMask layermask;
	
	public bool cursorLocked = false;
	private float camSpeed = 10; // sensitivity
	public float rotateSpeed = 7.0f; // how fast you rotate to a camera
	
	private Vector3 camCurrentOffset;
	private Vector3 camPositionChange = Vector3.up * 4;
	private Quaternion headAngle = Quaternion.Euler(new Vector3(-90, 0, 90));
	private float health = 100;
	public GameObject pauseScreen;
	private GameObject healthBg;
	public GameObject messageBox;
	public GameObject messageText;
	public GameObject hint;
	public bool fixedShip = false;
	public bool hintWasActive = false;


	void Start()
	{
		
		player = GetComponent<Rigidbody>();
		cam = GameObject.Find("Camera");
		head = transform.Find("Head");
		//gravity = Physics.gravity; // need to figure out gravity eventually
		camOffset = cam.transform.position;
		camCurrentOffset = camOffset;
		//Cursor.lockState = CursorLockMode.Locked;
		pauseScreen = GameObject.Find("PauseScreen");
		healthBg = GameObject.Find("healthbg");

		messageBox = GameObject.Find("messageBox");
		messageText = GameObject.Find("messageText");
		hint = GameObject.Find("hintText");
		hint.SetActive(false);
		messageText.SetActive(false);
		messageBox.SetActive(false);

		string leftArmType = "boxingArm";
		string rightArmType = "boxingArm";
		string legType = "mechLegs";
		string bodyType = "genericBody";

		// loading save
		string savePath = Application.persistentDataPath + "/saveFile.json";
		if (System.IO.File.Exists(savePath))
		{
			string json = System.IO.File.ReadAllText(savePath);
			SaveFile save = JsonUtility.FromJson<SaveFile>(json);
			leftArmType = save.leftArmType;
			rightArmType = save.rightArmType;
			bodyType = save.bodyType;
			legType = save.legType;
		}
		else
		{
			SaveFile save = new(leftArmType, rightArmType, bodyType, legType);
			string json = JsonUtility.ToJson(save);
			print("saving to: " + savePath);
			System.IO.File.WriteAllText(savePath, json);
		}

		leftArm = (Instantiate(Resources.Load(leftArmType, typeof(GameObject)), transform) as GameObject).GetComponent<Arm>();
		leftArm.init();

		rightArm = (Instantiate(Resources.Load(rightArmType, typeof(GameObject)), transform) as GameObject).GetComponent<Arm>();
		rightArm.init();
		rightArm.transform.position = new Vector3(-rightArm.transform.position.x, rightArm.transform.position.y, rightArm.transform.position.z);
		rightArm.transform.localScale = new Vector3(3,-3,3);

		legs = (Instantiate(Resources.Load(legType, typeof(GameObject)), transform) as GameObject).GetComponent<Legs>();
		legs.init(this);

		body = (Instantiate(Resources.Load(bodyType, typeof(GameObject)), transform) as GameObject).GetComponent<Body>();
		body.init(this);
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
				// close pause menu
				//Time.timeScale = 0.01f;
				//print(Time.timeScale);
				//Time.fixedDeltaTime = .02f;
				pauseScreen.SetActive(false);
				if (hintWasActive)
				{
					hint.SetActive(true);
					hintWasActive = false;
				}
				messageBox.SetActive(false);
				messageText.SetActive(false);
				Time.timeScale = 1;
			}
			else
			{
				Cursor.lockState = CursorLockMode.None;
				print("unlocking cursor");
				// open pause menu
				//Time.timeScale = 1;
				//print(Time.timeScale);
				//Time.fixedDeltaTime = 0f;
				pauseScreen.SetActive(true);
				if (hint.activeInHierarchy)
				{
					hintWasActive = true;
					hint.SetActive(false);
				}
				Time.timeScale = 0;
			}
		}
	}

	void FixedUpdate()
	{
		
		if (cursorLocked) // aka not paused
		{ // CAMERA STUFF
			cam.transform.position = transform.position + camCurrentOffset;
			//raycast from origin point to camera location, bring it closer as needed, if ray doesn't hit anything then bring back to original position
			//Ray ray = new Ray(body.transform.position, (cam.transform.position - body.transform.position).normalized);
			Ray ray = new Ray(body.transform.position+ camPositionChange, cam.transform.forward * -1);
			//print(ray.direction);
			Debug.DrawRay(ray.origin, ray.direction * camDistance, Color.yellow);

			RaycastHit hit = new();
			bool hitSmth = Physics.Raycast(ray, out hit, camDistance, layermask);
			if (hitSmth) // set to hit point if collides with smth
			{
				cam.transform.position = hit.point;
				//print("hitSmth: " + hit.collider.gameObject.name);
				Debug.DrawLine(ray.origin, hit.point, Color.cyan);

			}
			else // otherwise just set it to camdistance in the correct angle
			{
				cam.transform.position = body.transform.position+ camPositionChange + ray.direction * camDistance;
			}

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
			player.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.fixedDeltaTime * rotateSpeed);
			camLastRotation = cam.transform.rotation;

			head.rotation = targetRotation * headAngle;
		}
		//player.MoveRotation(Quaternion.Euler(0f, cam.transform.rotation.eulerAngles.y, 0f)); // change to head to instantly snap

		legs.move();

		if (!leftArm.attacking && Input.GetMouseButton((int)MouseButton.Left))
		{
			leftArm.attack();
		}

		if (!rightArm.attacking && Input.GetMouseButton((int)MouseButton.Right))
		{
			rightArm.attack();
		}

		if (!body.onCooldown && Input.GetKey(KeyCode.LeftControl))
		{
			body.special();
		}
		
		if (Input.GetKey(KeyCode.KeypadEnter)) // REMOVE THIS ***************************************************************************
		{
			body.onCooldown = false;
		}

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

	public void takeDmg(int amount)
	{
		health -= amount;
		print("ouch only " + health + " hp left");
		if (health <= 0)
		{
			print("OH SHOOT YOU DIED L");
			// DO SMTH
			SceneManager.LoadScene("DeathScreen");
		}
		float healthPct = 1- health / 100;
		healthBg.transform.localScale = new Vector3(healthPct * 80, 80, 80);
		healthBg.transform.localPosition = new Vector3(-380-120*healthPct, 300, 0);
	}

}
