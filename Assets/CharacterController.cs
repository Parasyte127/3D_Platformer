using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
 
public class CharacterController : MonoBehaviour
{
    public float maxSpeed = 0.5f;
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
 
        if (isOnGround == true && Input.GetKeyDown(KeyCode.Space))
        {
            myRigidbody.AddForce(transform.up * jumpForce);
        }
 
    //Double Jump
        if (isOnGround == false && doubleJump == true && Input.GetKeyDown(KeyCode.Space))
        {
            myRigidbody.AddForce(transform.up * jumpForce);
            doubleJump = false;
        }
 
        if (isOnGround == true)
        {
            doubleJump = true;
        }
    //Run
        if (Input.GetKey(KeyCode.LeftShift))
        {
            maxSpeed = 7.5f;
        }else  
        {
            maxSpeed = 5.0f;
        }

    //Wall Hang
        isOnWall = Physics.CheckSphere(wallChecker.transform.position, 30.0f, wallLayer);

        

        if (isOnWall == true && Input.GetKeyDown(KeyCode.Space) && Input.GetKeyDown(KeyCode.W))
        {
            myRigidbody.AddForce(transform.up * jumpForce);
            myRigidbody.AddForce(transform.forward * jumpForce);
        }

    //Movement
        //transform.position = transform.position + (transform.forward * Input.GetAxis("Vertical") * maxSpeed);
        Vector3 newVelocity = transform.forward * Input.GetAxis("Vertical") * maxSpeed;
        myRigidbody.velocity = new Vector3(newVelocity.x, myRigidbody.velocity.y, newVelocity.z);

    //Rotation
        rotation = rotation + Input.GetAxis("Mouse X") * rotationSpeed;
        transform.rotation = Quaternion.Euler(new Vector3(0.0f, rotation, 0.0f));
 
    //Camera Rotation
        camRotation = camRotation + Input.GetAxis("Mouse Y") * camRotationSpeed;

        camRotation = Mathf.Clamp(camRotation, -40.0f, 40.0f);

        camLock.transform.localRotation = Quaternion.Euler(new Vector3(-camRotation, 0.0f, 0.0f));
       
    }
}
 
