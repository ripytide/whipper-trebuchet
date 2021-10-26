using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Main : MonoBehaviour
{

    public GameObject pivot1;
    public GameObject pivot2;
    public GameObject weight;
    public GameObject firingPin;


    public float mainArm;
    public float secondaryArm;
    public float firingPinLength;


    private Transform pivot2Transform;
    private Transform weightTransform;



    static float Theta(float a, float b)
    {
        //equation defined in my whipper trebuceth paper - slight difference in that theat is measured from the x-axis anticlockwise
        float theta = Convert.ToSingle(Math.Acos((2*(Math.Pow(b,2))-(a)*(b)*Math.Sqrt(2))/(2*(b)*Math.Sqrt((Math.Pow(a,2))+(Math.Pow(b,2))-(a)*(b)*Math.Sqrt(2)))));
        return theta;

    }

    static float Corule(float a, float b)
    {
        float c = Convert.ToSingle(Math.Sqrt(Math.Pow(a, 2) + Math.Pow(b, 2) - a * b * Math.Sqrt(2)));
        return c;
    }

    // Start is called before the first frame update
    void Awake()
    {
        //overrule the firingpin length parameter
        firingPinLength = mainArm;


        //find the coords of the pivot2 and the counterweight and the firing pin
        float theta = Theta(secondaryArm, mainArm);

        print(theta * 180/Math.PI);

        float x2 = Convert.ToSingle(mainArm * Math.Sin(-theta));
        float y2 = Convert.ToSingle(mainArm * Math.Cos(theta));

        float x3 = 0f;
        float y3 = Corule(mainArm, secondaryArm);

        float x4 = Convert.ToSingle(firingPinLength * Math.Sin(theta));
        float y4 = Convert.ToSingle(firingPinLength * -Math.Cos(theta));

        //make a vector out of the coords
        Vector2 position2 = new Vector2(x2,y2);
        Vector2 position3 = new Vector2(x3,y3);
        Vector2 position4 = new Vector2(x4, y4);

        print("PIVOT: " + Convert.ToString(position2));
        print("WEIGHT: " + Convert.ToString(position3));


        // Make pivot1, pivot2, firngPin and the counterweight at the set coords
        GameObject pivot1Instance = Instantiate(pivot1, new Vector2(0,0), Quaternion.identity);
        GameObject pivot2Instance = Instantiate(pivot2, position2, Quaternion.identity);
        GameObject weightInstance = Instantiate(weight, position3, Quaternion.identity);
        GameObject firingPinInstance = Instantiate(firingPin, position4, Quaternion.identity);

        //connect the secondary pivot to the main pivot
        HingeJoint2D joint = pivot1Instance.GetComponent<HingeJoint2D>();
        joint.connectedBody = pivot2Instance.GetComponent<Rigidbody2D>();

        //connect the counterweight to the secondary pivot
        HingeJoint2D hinge = pivot2Instance.GetComponent<HingeJoint2D>();
        hinge.connectedBody = weightInstance.GetComponent<Rigidbody2D>();
        
        //connec the firing pin to the secondary pivot
        FixedJoint2D fixedJointy = firingPinInstance.GetComponent<FixedJoint2D>();
        fixedJointy.connectedBody = pivot2Instance.GetComponent<Rigidbody2D>();


        //connect the tranform components to the private variables for use in update
        pivot2Transform = pivot2Instance.GetComponent<Transform>();
        weightTransform = weightInstance.GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetButton("space"))
        // {
        //     pivot2Transform.position = position2;
        //     pivot2Transform.rotation = Quaternion.identity;
        //     weightTransform.position = position3;
        //     weightTransform.rotation = Quaternion.identity;
        // }
    }
}
