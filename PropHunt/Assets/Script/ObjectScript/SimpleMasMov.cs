using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMasMov : MonoBehaviour
{
    // Start is called before the first frame update

    public float speed = 20.0f;
    public float jumpSpeed = 8.0f;

    public float armarioMass;
    public float barril1Mass;
    public float barril2Mass;
    public float basuraMass;
    public float bombonaMass;
    public float botella1Mass;
    public float botella2Mass;
    public float cajaMass;
    public float camaMass;
    public float colchonMass;
    public float conoMass;
    public float estanteriaMass;
    public float jarraMass;
    public float ladrilloMass;
    public float lataArrugadaMass;
    public float lataEnteraMass;
    public float libroMass;
    public float lamparaMass;
    public float macetaMass;
    public float mesaMass;
    public float mesillaMass;
    public float miniArmarioMass;
    public float fridgeMass;
    public float noodlesMass;
    public float paleMass;
    public float sillaMass;
    public float sillonMass;
    public float sofaMass;
    public float tabureteMass;
    public float taquillaMass;

    private float inputX;
    private float inputZ;

    public bool canTotem = false;
    public GameObject currentTotem;
    public LoadTotem myLoadingBar;

    private Vector3 vel = new Vector3(0.0f, 0.0f, 0.0f);

    private Rigidbody rb;
    private float mass;
    private PlayerMotorController motor;
    private float lookSensivility = 3f;
    void Start()
    {
        motor = GetComponent<PlayerMotorController>();
    }

    // Update is called once per frame
    void Update()
    {
        float xMov = Input.GetAxis("Horizontal");
        float zMov = Input.GetAxis("Vertical");

        Vector3 movHorizontal = transform.right * xMov;
        Vector3 movVertical = transform.forward * zMov;

        //final movement vector
        Vector3 velocity = (movHorizontal + movVertical) * speed/rb.mass;

        //apply movement
        motor.Move(velocity);
        float yRot = Input.GetAxisRaw("Mouse X");
        Vector3 rotationPlayer = new Vector3(0f, yRot, 0f) * lookSensivility;

        //Apply
        motor.Rotate(rotationPlayer);

        //Rotation VERTICAL, we will turn the camera in a vertical axis, why? We dont wanna turn the player vertically only camera.
        float xRot = Input.GetAxisRaw("Mouse Y");
        float cameraRotation = xRot * lookSensivility;

        //Apply 
        motor.RotateCamera(cameraRotation);

    }
    public void changeRbattributes(string prefab)
    {
        switch (prefab)
        {
            case "Armario":
                mass = armarioMass;
                break; 
            case "Barril1":
                mass = barril1Mass;
                break;            
            case "Barril2":
                mass = barril2Mass;
                break;
            case "Basura":
                mass = basuraMass;
                break;
            case "Bombona":
                mass = bombonaMass;
                break;
            case "Botella1":
                mass = botella1Mass;
                break;
            case "Botella2":
                mass = botella2Mass;
                break;
            case "Caja":
                mass = cajaMass;
                break;
            case "Cama":
                mass = camaMass;
                break;            
            case "Colchon":
                mass = colchonMass;
                break;            
            case "Cono":
                mass = conoMass;
                break;
            case "Estanteria":
                mass = estanteriaMass;
                break;
            case "Jarra":
                mass = jarraMass;
                break;
            case "Ladrillo":
                mass = ladrilloMass;
                break;            
            case "LataArrugada":
                mass = lataArrugadaMass;
                break;            
            case "LataEntera":
                mass = lataEnteraMass;
                break;            
            case "Libro":
                mass = libroMass;
                break;
            case "Lampara":
                mass = lamparaMass;
                break;
            case "Maceta":
                mass = macetaMass;
                break;
            case "Mesa":
                mass = mesaMass;
                break;
            case "Mesilla":
                mass = mesillaMass;
                break;            
            case "MiniArmario":
                mass = miniArmarioMass;
                break;           
            case "Fridge":
                mass = fridgeMass;
                break;            
            case "Noodles":
                mass = noodlesMass;
                break;           
            case "Pale":
                mass = paleMass;
                break;            
            case "Silla":
                mass = sillaMass;
                break;            
            case "Sillon":
                mass = sillonMass;
                break;            
            case "Sofa":
                mass = sofaMass;
                break;            
            case "Taburete":
                mass = tabureteMass;
                break;            
            case "Taquilla":
                mass = taquillaMass;
                break;


        }
        rbApplication();
    }
    private void rbApplication()
    {
        rb.mass = mass;
        rb.useGravity = true;
    }
    private void OnEnable()
    {
        rb = this.GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }
}
