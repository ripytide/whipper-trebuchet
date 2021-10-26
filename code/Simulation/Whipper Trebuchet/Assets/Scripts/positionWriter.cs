using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class positionWriter : MonoBehaviour
{

    private MainReset resetter;

    private Transform trans;
    // Start is called before the first frame update
    void Start()
    {
        trans = gameObject.GetComponent<Transform>();

        resetter = GameObject.Find("Scene Reseter").GetComponent<MainReset>();

    }

    // Update is called once per frame
    void Update()
    {
        string filename = "coords" + Convert.ToString(resetter.mainArm) + "-" + Convert.ToString(resetter.secondaryArm) + ".csv";
        addRecord(trans.position.x, trans.position.y, Application.dataPath + "/CSV/coords/" + filename);
    }

    public static void addRecord(float x, float y, string filepath)
    {
        using (System.IO.StreamWriter file = new System.IO.StreamWriter(@filepath, true))
        {
            file.WriteLine(Convert.ToString(x) + "," + Convert.ToString(y));
        }
    }




}
