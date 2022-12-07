using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
 
public class CharacterController : MonoBehaviour
{   
    public AudioClip jump;
    public AudioClip run;
    public AudioClip backgroundMusic;

    public AudioSource sfxPlayer;
    public AudioSource musicPlayer;

    public GameObject cam1;
    public GameObject cam2;
    bool usingBox = false;
    bool atBox;
    public GameObject boxChecker;
    public LayerMask boxLayer;

    public float maxSpeed = 1.0f;
    public float runSpeed = 10.0f;
    float rotation = 0.0f;
    float camRotation = 0.0f;
    float rotationSpeed = 2.0f;
    float camRotationSpeed = 1.5f;
    GameObject camLock;
 
    bool isOnGround;
    public GameObject groundChecker;
    public LayerMask groundLayer;
    public float jumpForce = 300.0f;
    bool doubleJump = true;

    bool isOnWall;
    public GameObject wallChecker;
    public LayerMask wallLayer;
    public float maxJumpTime = 1.5f;
    public bool canAttach;
    bool hanging;

    Animator myAnim;

    Rigidbody myRigidbody;
   
    void Start()
    {
        cam1.SetActive(true);
        cam2.SetActive(false);

        myAnim = GetComponentInChildren<Animator>();

        Cursor.lockState = CursorLockMode.Locked;
        camLock = GameObject.Find("CamLock");
        myRigidbody = GetComponent<Rigidbody>();

        musicPlayer.clip = backgroundMusic;
        musicPlayer.loop = true;
        musicPlayer.Play();
    }
 
    // Update is called once per frame
    void Update()
    {
       
    //Jump
        isOnGround = Physics.CheckSphere(groundChecker.transform.position, 0.1f, groundLayer);
        myAnim.SetBool("isOnGround", isOnGround);
 
        if (isOnGround == true && Input.GetKeyDown(KeyCode.Space) && isOnWall == false)
        {
            sfxPlayer.PlayOneShot(jump);
            myAnim.SetTrigger("Jumped");
            myRigidbody.AddForce(transform.up * jumpForce);
        }
 
    //Double Jump
        //if (isOnGround == false && doubleJump == true && Input.GetKeyDown(KeyCode.Space) && isOnWall == false)
        //{
            //myRigidbody.AddForce(transform.up * jumpForce);
            //doubleJump = false;
        //}
 
        //if (isOnGround == true)
        //{
            //doubleJump = true;
        //}
    //Run
        if (Input.GetKey(KeyCode.LeftShift) && ! Input.GetKey(KeyCode.S))
        {
            maxSpeed = runSpeed;
        }else  
        {
            maxSpeed = 5.0f;
        }

    //Wall Hang (Fail)

    //Movement
        //transform.position = transform.position + (transform.forward * Input.GetAxis("Vertical") * maxSpeed);
        Vector3 newVelocity = transform.forward * Input.GetAxis("Vertical") * maxSpeed + transform.right * Input.GetAxis("Horizontal") * maxSpeed;
        myRigidbody.velocity = new Vector3(newVelocity.x, myRigidbody.velocity.y, newVelocity.z);

        myAnim.SetFloat("speed", newVelocity.magnitude);


        //Rotation
        rotation = rotation + Input.GetAxis("Mouse X") * rotationSpeed;
        transform.rotation = Quaternion.Euler(new Vector3(0.0f, rotation, 0.0f));
 
    //Camera Rotation
        camRotation = camRotation + Input.GetAxis("Mouse Y") * camRotationSpeed;

        camRotation = Mathf.Clamp(camRotation, -80.0f, 60.0f);

        camLock.transform.localRotation = Quaternion.Euler(new Vector3(-camRotation, 0.0f, 0.0f));
       
    //Wall Hang V2
       isOnWall = Physics.CheckSphere(wallChecker.transform.position, 0.6f, wallLayer);
        

        if (isOnWall && Input.GetKeyDown(KeyCode.Space) && hanging == true)
        {
            myRigidbody.constraints = RigidbodyConstraints.None;
            myRigidbody.AddForce(new Vector3(0, 10, 0), ForceMode.Impulse);
            hanging = false;
            sfxPlayer.PlayOneShot(jump);
        }
        else if (isOnWall && Input.GetKey(KeyCode.LeftControl) && !Input.GetKey(KeyCode.Space))
        {
            myRigidbody.constraints = RigidbodyConstraints.FreezePosition;
            hanging = true;
            //myRigidbody.velocity = Vector3.zero;
            //myRigidbody.constraints = RigidbodyConstraints.FreezePositionX & RigidbodyConstraints.FreezePositionY & RigidbodyConstraints.FreezePositionZ;
        }
        else if (isOnWall == false)
        {
            myRigidbody.constraints = RigidbodyConstraints.None;
        }

     //cam use

        atBox = Physics.CheckSphere(boxChecker.transform.position, 2.0f, boxLayer);

        

        //Switch to complaint
        if (Input.GetKeyDown(KeyCode.E) && atBox == true && usingBox == false)
        {
            cam1.SetActive(false);
            cam2.SetActive(true);
            usingBox = true;
        }
        //Switch back to third-person
        else if (Input.GetKeyDown(KeyCode.E) && usingBox == true)
        {
            cam1.SetActive(true);
            cam2.SetActive(false);
            usingBox = false;
        }

        //Audio
        
    }
}
 
