using UnityEngine;

public class Player : MonoBehaviour
{

    private CharacterController player;
    public GameObject cam;
    public Vector3 gravity;
    public Vector3 camOffset; // target position for camera
    public Quaternion camRotation; // target rotation for camera
    private float speed = 1; // temp
    private bool cursorLocked = true;
    /* public Body body; // need to assign this & create class
     * public Legs legs; // need to assign this & create class
     * public Arm right; // need to assign this & create class
     * public Arm left; // need to assign this & create class
     */
    void Start()
    {
        player = GetComponent<CharacterController>();
        gravity = Physics.gravity; // need to figure out gravity eventually
        camOffset = cam.transform.position;
        //camRotation = cam.transform.r
    }

    // Update is called once per frame
    void Update()
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
        if (!cursorLocked)
        {
            return;
        }


        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        movement.Normalize();
        movement.x *= speed;
        movement.y *= speed;

        if (player.isGrounded)
        {
            //read for jump input
        }
        else
        {
            //movement += gravity;
        }
        //print(movement);
        player.Move(movement);

        if (cursorLocked)
        {
            cam.transform.position = transform.position + camOffset;
            cam.transform.RotateAround(transform.position, Vector3.up, 0);
            //cam.transform.position = Vector3.MoveTowards(cam.transform.position, transform.position, speed);
        }

    }
}
