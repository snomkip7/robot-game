using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{

    //private CharacterController player;
    private Rigidbody player;
    public GameObject cam;
    //public Vector3 gravity;
    public Vector3 camOffset; // target position for camera
    public Quaternion camRotation; // target rotation for camera
    private float speed = 10; // temp
    private bool cursorLocked = false;
    private float camSpeed = 10; // sensitivity
    private float movementSnapping = .7f; // for the lerp in velocity
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
        //gravity = Physics.gravity; // need to figure out gravity eventually
        camOffset = cam.transform.position;
        //Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (Input.GetKeyDown(KeyCode.Escape)) // toggling locked mode on cursor with escape key **open pause menu**
        {
            cursorLocked = !cursorLocked;
            if (cursorLocked)
            {
                Cursor.lockState = CursorLockMode.Locked;
                print("locking cursor");
                // show pause menu
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                print("unlocking cursor");
                // close pause menu
            }
        }

        if (cursorLocked)
        {
            cam.transform.RotateAround(transform.position, Vector3.up, Input.GetAxis("Mouse X") * camSpeed);
            cam.transform.RotateAround(transform.position, cam.transform.right, Input.GetAxis("Mouse Y") * -camSpeed);

        }
        else
        {
            return;
        }

        camCurrentOffset = cam.transform.position - transform.position;

        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), player.velocity.y, Input.GetAxis("Vertical"));
        movement.Normalize();
        movement.x *= speed;
        movement.z *= speed;

        player.velocity = Vector3.Lerp(player.velocity, movement, movementSnapping);

        if (!Physics.Raycast(transform.position, Vector3.down, .1f)){ // gravity
            player.AddForce(Physics.gravity, ForceMode.Acceleration);
            print("gravity moment");
        }
        else
        {
            player.velocity = new Vector3(player.velocity.x, 0, player.velocity.z);
        }
        Debug.DrawRay(transform.position, Vector3.down * .1f, Color.yellow);
        print(player.velocity);

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
        if (cursorLocked)
        {
            cam.transform.position = transform.position + camCurrentOffset;
            //raycast from origin point to camera location, bring it closer as needed, if ray doesn't hit anything then bring back to original position
        }
    }
}
