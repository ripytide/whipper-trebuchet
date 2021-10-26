using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getStarted : MonoBehaviour
{

    private HingeJoint2D hinge;


    // Start is called before the first frame update
    void Start()
    {
        hinge = gameObject.GetComponent<HingeJoint2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hinge.jointSpeed == 0f)
        {
            hinge.useMotor = true;
            print("motoe used");
        } else
        {
            hinge.useMotor = false;
        }
    }
}
