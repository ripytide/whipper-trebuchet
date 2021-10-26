using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class beamController : MonoBehaviour
{
    private Transform pivot2;
    private Transform weight;
    private Transform firingPin;
    private Transform tran;
    public bool choosePivot2;
    public bool chooseWeight;
    public bool chooseFiringPin;

    // Start is called before the first frame update
    void Start()
    {
        pivot2 = GameObject.FindWithTag("pivot2").GetComponent<Transform>();
        weight = GameObject.FindWithTag("weight").GetComponent<Transform>();
        firingPin = GameObject.FindWithTag("firingPin").GetComponent<Transform>();
        tran = gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (chooseFiringPin)
        {
            tran.position = GetMiddlePos(pivot2, firingPin);
            tran.rotation = Quaternion.Euler(0, 0, -Vector2.SignedAngle(pivot2.position, Vector2.right));
        }
        if (chooseWeight)
        {
            tran.position = GetMiddlePos(pivot2, weight);
            tran.rotation = Quaternion.Euler(0, 0, -Vector2.SignedAngle(pivot2.position-weight.position, Vector2.right));
        }
        
    }

    static Vector2 GetMiddlePos(Transform object1, Transform object2)
    {
        float x = (object1.position.x + object2.position.x) / 2;
        float y = (object1.position.y + object2.position.y) / 2;

        return new Vector2(x, y);
    }



}
