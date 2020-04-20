using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotorController : MonoBehaviour
{
    private Vector3 velocity = Vector3.zero;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        MakeItMove();
    }

    //Gets movement vector
    public void Move(Vector3 _velocity)
    {
        velocity = _velocity;
    }

    //Performs move based on velocity
    void MakeItMove()
    {
        if(velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + velocity * Time.deltaTime);
        }
    }
}
