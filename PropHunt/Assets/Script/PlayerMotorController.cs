using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotorController : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private Vector3 cameraRotation = Vector3.zero;


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
        MakeItRotate();
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

    public void Rotate(Vector3 _rotation)
    {
        rotation = _rotation;
    }

    void MakeItRotate()
    {
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));

        if(cam != null)
        {
            cam.transform.Rotate(-cameraRotation);
        }
    }

    public void RotateCamera(Vector3 _camera)
    {
        cameraRotation = _camera;
    }
}
