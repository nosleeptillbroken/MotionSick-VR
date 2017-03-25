using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using Rewired.UI.ControlMapper;

public class PlayerController : MonoBehaviour
{
    //movement variables
    [SerializeField] int moveSpeed;
    private Vector2 moveVector;

    //camera variables
    private Camera playerCam;
    private CharacterController CharController;
    private float cameraPan;
    private float cameraTilt;
    private float joystickCameraPan;
    private float joystickCameraTilt;
    [SerializeField] int lookSensitivity;
    [SerializeField] int joystickLookSensitivity;
    private float minY = -45f;
    private float maxY = 35f;
    float camX;

    //button variables
    private bool interacting;

    //rewired variables
    private Player rewiredPlayer;
    private int playerId = 0;

    void Awake()
    {
        rewiredPlayer = ReInput.players.GetPlayer(playerId);
        playerCam = this.gameObject.GetComponentInChildren<Camera>();
        CharController = this.GetComponent<CharacterController>();
    }

    void GetInputs()
    {
        moveVector.x = rewiredPlayer.GetAxis("Move Horizontal"); // get input by name or action id
        moveVector.y = rewiredPlayer.GetAxis("Move Vertical");

        cameraPan = rewiredPlayer.GetAxis("Look Horizontal") * lookSensitivity * Time.deltaTime;
        cameraTilt = rewiredPlayer.GetAxis("Look Vertical") * lookSensitivity * Time.deltaTime;

        joystickCameraPan = rewiredPlayer.GetAxis("Joystick Look Horizontal") * joystickLookSensitivity * Time.deltaTime;
        joystickCameraTilt = rewiredPlayer.GetAxis("Joystick Look Vertical") * joystickLookSensitivity * Time.deltaTime;

        interacting = rewiredPlayer.GetButtonDown("Interact");

    }

    void ProcessInputs()
    {
        if (moveVector.y != 0) //moving forward or back
        {
            CharController.SimpleMove(gameObject.transform.forward * moveSpeed * moveVector.y);
        }

        if (rewiredPlayer.GetAxis("Move Horizontal") != 0) //strafing left or right
        {
            CharController.SimpleMove(gameObject.transform.right*(moveSpeed/2)*moveVector.x);
        }

        if (interacting) //player presses interact
        {
            Debug.Log("Interacted");
        }

        else if (cameraPan > 1 || cameraPan < 1) //looking up or down
        {
            //Debug.Log("panning left");
            playerCam.transform.Rotate(new Vector3(0f,cameraPan,0f));
            gameObject.transform.Rotate(new Vector3(0f, cameraPan, 0f));
        }

        if (cameraTilt > 1 || cameraTilt < 1) //looking up or down
        {
            camX += cameraTilt;
            camX = Mathf.Clamp(camX, minY, maxY);
            playerCam.transform.localEulerAngles = new Vector3(-camX,0f,0f);
        }

        if (joystickCameraPan > 1 || joystickCameraPan < -1) //joystick look left or right
        {
            playerCam.transform.Rotate(new Vector3(0f, joystickCameraPan, 0f));
            gameObject.transform.Rotate(new Vector3(0f, joystickCameraPan, 0f));
        }

        if (joystickCameraTilt > 1 || joystickCameraTilt < -1)
        {
            camX += joystickCameraTilt;
            camX = Mathf.Clamp(camX, minY, maxY);
            playerCam.transform.localEulerAngles = new Vector3(-camX, 0f, 0f);
        }

    }

	// Update is called once per frame
	void Update ()
	{
	    GetInputs();
        ProcessInputs();
	}

    #region Getters

    public int GetLookSensitivity()
    {
        return lookSensitivity;
    }
    #endregion

    #region Setters

    public void SetLookSensitivity(int sensitivity)
    {
        lookSensitivity = sensitivity;
    }
    #endregion
}
