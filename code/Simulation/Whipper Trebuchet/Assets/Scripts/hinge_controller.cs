using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hinge_controller : MonoBehaviour
{
    public GameObject indicator;
    private HingeJoint2D hingey;
    private JointMotor2D motor;
    void Awake()
    {
        hingey = GetComponent<HingeJoint2D>();
        motor = hingey.motor;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("left")) {
            hingey.useMotor = true;
            motor.motorSpeed = -100;
            hingey.motor = motor;
            indicator.transform.localScale = new Vector3(0.5f,0.5f,0f);
        } else if (Input.GetButton("right")) {
            hingey.useMotor = true;
            motor.motorSpeed = 100;
            hingey.motor = motor;
            indicator.transform.localScale = new Vector3(0.1f,0.1f,0f);
        } else {
            hingey.useMotor = false;
            indicator.transform.localScale = new Vector3(1f,1f,0f);
        }

    }
}

