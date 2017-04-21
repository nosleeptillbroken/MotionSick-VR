using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using Rewired.UI.ControlMapper;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(PlayerDeath))]
[RequireComponent(typeof(PlayerAttributes))]


public class PlayerController : MonoBehaviour
{
    

    //movement variables
    [SerializeField] float moveSpeed;
    private Vector2 moveVector;
    private Vector3 FBVector; //forward and back vector

    //camera variables
    private Camera playerCam;
    private Rigidbody CharRB;
    private float cameraPan;
    private float cameraTilt;
    private float joystickCameraPan;
    private float joystickCameraTilt;
    [SerializeField] float lookSensitivity;
    [SerializeField] float joystickLookSensitivity;
    private float minY = -45f;
    private float maxY = 35f;
    float camX;

    //button variables
    private bool interacting;

    //rewired variables
    private Player rewiredPlayer;
    private int playerId = 0;

    //interactable variables
    private GameObject interactableObject;

    //default values
    [SerializeField] private bool useDefaultValues = true;
    private float DefaultLookSensitivity = 15;
    private float DefaultJoystickLookSensitivity = 125;
    private float DefaultMoveSpeed = 10;

    void Awake()
    {
        if (GameObject.Find("Rewired Input Manager") == null)
        {
            Debug.Log(
                "Could not find Input manager, please make sure it is present and active within the scene! Quitting game...");
            Quit();
        }
        else
            rewiredPlayer = ReInput.players.GetPlayer(playerId);

        if (this.GetComponent<PlayerAttributes>().GetHealth() == 0) //may change in the future, adding functionality for now.
        {
            Debug.Log("Player Health is 0! Adjusting to 2...");
            this.GetComponent<PlayerAttributes>().SetHealth(2);
        }

        if (this.GetComponent<Rigidbody>() == null) //redundant now that require component is in place
        {
            Debug.Log("No [active] RigidBody found. Adding one...");
            this.gameObject.AddComponent<Rigidbody>();
            CharRB = this.GetComponent<Rigidbody>();
            CharRB.mass = 5;
            CharRB.constraints = RigidbodyConstraints.FreezeRotation;
        }
        else
        {
            CharRB = this.GetComponent<Rigidbody>();
            CharRB.mass = 5;
            CharRB.constraints = RigidbodyConstraints.FreezeRotation;
        }

        if (this.gameObject.GetComponent<CapsuleCollider>().height != 1.8f)
        {
            Debug.Log("Collider height not set to 2! Adjusting now...");
            this.gameObject.GetComponent<CapsuleCollider>().height = 1.8f;
        }

        if (this.gameObject.GetComponent<CapsuleCollider>().radius != 0.3)
        {
            Debug.Log("Collider radius not set to 0.3! Adjusting now...");
            this.gameObject.GetComponent<CapsuleCollider>().radius = 0.3f;
        }
            
        

        if (this.GetComponentInChildren<Camera>() == null)
        {
            Debug.Log("No [active] Camera found. Adding one...");
            GameObject obj = new GameObject();
            obj.transform.parent = this.gameObject.transform;
            obj.AddComponent<Camera>();
            obj.transform.localPosition = new Vector3(0f, 1f, 0f);
            obj.tag = "MainCamera";
            obj.name = "NewMainCamera";
            playerCam = obj.GetComponent<Camera>();
        }
        else
            playerCam = this.gameObject.GetComponentInChildren<Camera>();

        if (this.gameObject.tag != "Player")
        {
            Debug.Log("Player's tag set incorrectly! Adjusting now...");
            this.gameObject.tag = "Player";
        }

        if (useDefaultValues && moveSpeed == 0)
        {
            Debug.Log("Using default move speed of: " + DefaultMoveSpeed + "...");
            moveSpeed = DefaultMoveSpeed;
        }

        if (useDefaultValues && lookSensitivity == 0)
        {
            Debug.Log("Using default look sensitivity of: " + DefaultLookSensitivity + "...");
            lookSensitivity = DefaultLookSensitivity;
        }

        if (useDefaultValues && joystickLookSensitivity == 0)
        {
            Debug.Log("Using default joystick look sensitivity of: " + DefaultJoystickLookSensitivity + "...");
            joystickLookSensitivity = DefaultJoystickLookSensitivity;
        }

        Cursor.lockState = CursorLockMode.Locked; //so you don't interact with random things. Press escape to show mouse cursor again
        
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
        FBVector = gameObject.transform.forward*moveSpeed*moveVector.y;
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = (gameObject.transform.right * (moveSpeed / 2) * moveVector.x) + FBVector + new Vector3(0, rb.velocity.y, 0); //could be a problem in the future if we need to jump (velocity on the y axis would alway get set to 0


        if (Mathf.Abs(moveVector.y) > 0.1f)
        {
            if (!GetComponent<AudioSource>().isPlaying)
            {
                GetComponent<AudioSource>().Play();
            }
        }
        else
        {
            GetComponent<AudioSource>().Pause();
        }

        if (interacting) //player presses interact
        {
            //Debug.Log("Interacted");
            InteractInput();
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

    void InteractInput()
    {
        if (interactableObject != null)
            interactableObject.SendMessage("Interact");
        else
            Debug.Log("There is nothing to interact with at the moment.");
    }

	// Update is called once per frame
	void Update ()
	{
	    GetInputs();
        ProcessInputs();
	}

    #region Getters

    public float GetLookSensitivity()
    {
        return lookSensitivity;
    }

    public GameObject GetInteractable()
    {
        return interactableObject;
    }
    #endregion

    #region Setters

    public void SetLookSensitivity(int sensitivity)
    {
        lookSensitivity = sensitivity;
    }

    public void SetInteractableObject(GameObject newObj)
    {
        interactableObject = newObj;
    }
    #endregion

    public static void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }
}
