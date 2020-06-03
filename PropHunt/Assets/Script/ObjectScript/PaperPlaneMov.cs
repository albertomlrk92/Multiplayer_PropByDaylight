using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperPlaneMov : MonoBehaviour
{
    // Start is called before the first frame update
    public float maxSpeed = 20.0f;
    public float acceleration = 20.0f;
    public float deceleration = 5.0f;
    public float speed = 20.0f;
    public float angularSpeed = 90.0f;
    public float angularAcceleration = 90.0f;
    public bool canFly = true;


    private float inputX;
    private float inputY;
    private float inputZ;

    private Vector3 vel = new Vector3(0.0f, 0.0f, 0.0f);
    private Vector3 angularVel = new Vector3(0.0f, 0.0f, 0.0f);
    private Rigidbody rb;
    private bool jumping = false;
    private float lastY;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.mass = 1;
        rb.useGravity = true;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        lastY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        float dt = Time.deltaTime;
        //Get axis
        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");

        if (canFly)
        {

            //Rotation with acceleration
            Vector3 targetRotationSpeed = new Vector3(angularSpeed * inputY, angularSpeed * inputX, 0f);
            Vector3 angularVelOffset = targetRotationSpeed - angularVel;
            angularVelOffset = Vector3.ClampMagnitude(angularVelOffset, angularAcceleration * dt);
            angularVel += angularVelOffset;
            transform.eulerAngles += angularVel;


            //Movement with acceleration  
            Vector3 targetSpeed = transform.forward * speed;
            Vector3 velOffset = targetSpeed - vel;
            velOffset = Vector3.ClampMagnitude(velOffset, acceleration * dt);
            vel += velOffset;
            rb.velocity = vel;

        }
        else
        {
            //Jumping state
            if (jumping)
            {
                //if the gameObject stoped to jump and start to fall, then start to move again
                if(lastY>transform.position.y)
                {
                    canFly = true;
                    jumping = false;
                    //contrain rotation, not let the plane go crazy if colides
                    rb.constraints = RigidbodyConstraints.FreezeRotation;
                }
            }
            else
            {
                //start to decelerate
                if (rb.velocity.magnitude > 0.1)
                {
                    rb.AddForce(-rb.velocity * 1 / maxSpeed);
                    vel = rb.velocity;
                }
                else
                {
                    rb.velocity = Vector3.zero;
                    vel = Vector3.zero;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(!canFly && !jumping)
            {
                jump();
            }
        }
        lastY = transform.position.y;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (this.enabled)
        {
            canFly = false;
            //free constraints for let the plane falls by natural gravity
            rb.constraints = RigidbodyConstraints.None;
        }
    }
    public void jump()
    {
        rb.AddForce(1000 * Vector3.up);
        jumping = true;
    }


}
