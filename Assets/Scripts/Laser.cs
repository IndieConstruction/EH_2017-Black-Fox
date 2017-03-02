using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {

	
    public Transform NE, NW, SE, SW;
    public string lastPoint = "NE";
    public float speed = 1.0f;
    float CoolDowntime = 10.0f;
    float prectime;


    // Use this for initialization

    void Start()
    {

        transform.position = NE.position;
        prectime = -CoolDowntime;
    }



    // Update is called once per frame

    void Update()
    {

        MoveLaser();

    }



    void MoveLaser()
    {

        //funzione predisposta al movimento del laser

        if (lastPoint == "NE")
        {

            transform.position = Vector3.MoveTowards(transform.position, SE.position, speed);

            if (Vector3.Distance(transform.position, SE.position) == 0)
            {
               lastPoint = "SE";
               }

        }

        else if (lastPoint == "SE")
        {

            transform.position = Vector3.MoveTowards(transform.position, SW.position, speed);

            if (Vector3.Distance(transform.position, SW.position) == 0)
            {
              lastPoint = "SW";
              }

        }

        else if (lastPoint == "SW")
        {

            transform.position = Vector3.MoveTowards(transform.position, NW.position, speed);

            if (Vector3.Distance(transform.position, NW.position) == 0)
            {
               lastPoint = "NW";
                  }
          }

        else if (lastPoint == "NW")
        {

            transform.position = Vector3.MoveTowards(transform.position, NE.position, speed);

            if (Vector3.Distance(transform.position, NE.position) == 0)
            {
               lastPoint = "NE";
                 }

        }

   }


    void TimeLaser()
    {
       
        if (Time.time >= prectime + CoolDowntime == true) 
            

        prectime = Time.time;
    }




}
