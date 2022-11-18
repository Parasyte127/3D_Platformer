using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
 
public class CharacterController : MonoBehaviour
{
    public float maxSpeed = 1.0f;
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


    Rigidbody myRigidbody;
   
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        camLock = GameObject.Find("CamLock");
        myRigidbody = GetComponent<Rigidbody>();
    }
 
    // Update is called once per frame
    void Update()
    {
    //Jump
        isOnGround = Physics.CheckSphere(groundChecker.transform.position, 0.1f, groundLayer);
 
        if (isOnGround == true && Input.GetKeyDown(KeyCode.Space) && isOnWall == false)
        {
            myRigidbody.AddForce(transform.up * jumpForce);
        }
 
    //Double Jump
        if (isOnGround == false && doubleJump == true && Input.GetKeyDown(KeyCode.Space) && isOnWall == false)
        {
            myRigidbody.AddForce(transform.up * jumpForce);
            doubleJump = false;
        }
 
        if (isOnGround == true)
        {
            doubleJump = true;
        }
    //Run
        if (Input.GetKey(KeyCode.LeftShift) && ! Input.GetKey(KeyCode.S))
        {
            maxSpeed = 8.5f;
        }else  
        {
            maxSpeed = 5.0f;
        }

    //Wall Hang

    //Movement
        //transform.position = transform.position + (transform.forward * Input.GetAxis("Vertical") * maxSpeed);
        Vector3 newVelocity = transform.forward * Input.GetAxis("Vertical") * maxSpeed + transform.right * Input.GetAxis("Horizontal") * maxSpeed;
        myRigidbody.velocity = new Vector3(newVelocity.x, myRigidbody.velocity.y, newVelocity.z);
        

    //Rotation
        rotation = rotation + Input.GetAxis("Mouse X") * rotationSpeed;
        transform.rotation = Quaternion.Euler(new Vector3(0.0f, rotation, 0.0f));
 
    //Camera Rotation
        camRotation = camRotation + Input.GetAxis("Mouse Y") * camRotationSpeed;

        camRotation = Mathf.Clamp(camRotation, -40.0f, 40.0f);

        camLock.transform.localRotation = Quaternion.Euler(new Vector3(-camRotation, 0.0f, 0.0f));
       
       //Wall Hang V2
       isOnWall = Physics.CheckSphere(wallChecker.transform.position, 0.5f, wallLayer);
        

        if (isOnWall && Input.GetKeyDown(KeyCode.Space))
        {
            myRigidbody.constraints = RigidbodyConstraints.None;
            myRigidbody.AddForce(new Vector3(0, 10, 0), ForceMode.Impulse);

        }
        else if (isOnWall && Input.GetKey(KeyCode.LeftControl) && !Input.GetKey(KeyCode.Space))
        {
            myRigidbody.constraints = RigidbodyConstraints.FreezePosition;
            //myRigidbody.velocity = Vector3.zero;
            //myRigidbody.constraints = RigidbodyConstraints.FreezePositionX & RigidbodyConstraints.FreezePositionY & RigidbodyConstraints.FreezePositionZ;
        }
        else if (isOnWall == false)
        {
            myRigidbody.constraints = RigidbodyConstraints.None;
        }
       
    }
}
 
