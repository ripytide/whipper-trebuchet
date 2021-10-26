using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainReset : MonoBehaviour
{
    private int curr;

    private Transform pivot2Tran;
    private Transform weightTran;
    private Transform firingPinTran;

    private Rigidbody2D pivot2Rig;
    private Rigidbody2D weightRig;
    private Rigidbody2D firingPinRig;

    private string line;

    private List<string[]> setups;

    public float mainArm;
    public float secondaryArm;



    static float Corule(float a, float b)
    {
        float c = Convert.ToSingle(Math.Sqrt(Math.Pow(a, 2) + Math.Pow(b, 2) - a * b * Math.Sqrt(2)));
        return c;
    }

    static float Theta(float a, float b)
    {
        //equation defined in my whipper trebuceth paper - slight difference in that theat is measured from the x-axis anticlockwise
        float theta = Convert.ToSingle(Math.Acos((2*(Math.Pow(b,2))-(a)*(b)*Math.Sqrt(2))/(2*(b)*Math.Sqrt((Math.Pow(a,2))+(Math.Pow(b,2))-(a)*(b)*Math.Sqrt(2)))));
        return theta;

    }


    // Start is called before the first frame update
    void Start()
    {
        curr = 0;

        pivot2Tran = GameObject.FindWithTag("pivot2").GetComponent<Transform>();
        weightTran = GameObject.FindWithTag("weight").GetComponent<Transform>();
        firingPinTran = GameObject.FindWithTag("firingPin").GetComponent<Transform>();

        pivot2Rig = GameObject.FindWithTag("pivot2").GetComponent<Rigidbody2D>();
        weightRig = GameObject.FindWithTag("weight").GetComponent<Rigidbody2D>();
        firingPinRig = GameObject.FindWithTag("firingPin").GetComponent<Rigidbody2D>();


        setups = new List<string[]>();
        using (System.IO.StreamReader file = new System.IO.StreamReader(@Application.dataPath+"/CSV/"+"setups.txt", true))
        {
            while ((line = file.ReadLine()) != null)
            {
                string[] lengths = line.Split(',');
                setups.Add(lengths);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        if ((weightTran.position.x < -0.15) & (curr < setups.Count))
        {
            secondaryArm = Convert.ToSingle(setups[curr][0]);
            mainArm = Convert.ToSingle(setups[curr][1]);

            //find the coords of the pivot2 and the counterweight and the firing pin
            float theta = Theta(secondaryArm, mainArm);

            print(theta * 180/Math.PI);

            //calculate coords
            float x2 = Convert.ToSingle(mainArm * Math.Sin(-theta));
            float y2 = Convert.ToSingle(mainArm * Math.Cos(theta));

            float x3 = 0f;
            float y3 = Corule(mainArm, secondaryArm);

            float x4 = Convert.ToSingle(mainArm * Math.Sin(theta));
            float y4 = Convert.ToSingle(mainArm * -Math.Cos(theta));

            //make a vector out of the coords
            Vector2 position2 = new Vector2(x2,y2);
            Vector2 position3 = new Vector2(x3,y3);
            Vector2 position4 = new Vector2(x4, y4);

            //print coords
            print("PIVOT: " + Convert.ToString(position2));
            print("WEIGHT: " + Convert.ToString(position3));
            print("FIRING PIN: " + Convert.ToString(position4));

            //move object to their position
            pivot2Tran.position = position2;
            pivot2Tran.rotation = Quaternion.identity;

            pivot2Rig.velocity = Vector2.zero;
            pivot2Rig.angularVelocity = 0f;


            weightTran.position = position3;
            weightTran.rotation = Quaternion.identity;

            weightRig.velocity = Vector2.zero;
            weightRig.angularVelocity = 0f;


            firingPinTran.position = position4;
            firingPinTran.rotation = Quaternion.identity;

            firingPinRig.velocity = Vector2.zero;
            firingPinRig.angularVelocity = 0f;



            curr += 1;
        }

        //print("Main Arm:" + Convert.ToString(mainArm));
        //print("Secondary Arm:" + Convert.ToString(secondaryArm));
    }
}
;