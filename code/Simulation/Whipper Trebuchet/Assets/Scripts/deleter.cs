using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deleter : MonoBehaviour
{
    private Transform weight;

    public bool actual;
    // Start is called before the first frame update
    void Start()
    {
        weight = GameObject.FindWithTag("weight").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (weight.position.x<-0.15 & actual)
        {
            Destroy(gameObject);
        }
    }
}
