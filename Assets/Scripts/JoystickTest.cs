using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickTest : MonoBehaviour {

    public bool Read;
   
    // Use this for initialization
    void Start() {
        Read = false;
        
    }

    

    // Update is called once per frame
    void FixedUpdate() {

        //La variabile partendo falsa, permette di entrare nel ciclo if.
        if (Read == false)
        {
            if (Input.GetAxisRaw("HorizontalArrow") == 1)
            {
                //una volta entrata la prima volta la variabile read ritorna vera ed esce dal primo if.
                Read = true;
                Debug.Log("Destra");
            }
            else if (Input.GetAxisRaw("HorizontalArrow") == -1)
            {
                Read = true;
                Debug.Log("Sinistra");
            }
            if (Input.GetAxisRaw("VerticalArrow") == 1)
            {
                Read = true;
                Debug.Log("Su");
            }
            else if (Input.GetAxisRaw("VerticalArrow") == -1)
            {
                Read = true;
                Debug.Log("Giu");
            }
        }
        if (Input.GetAxisRaw("HorizontalArrow") == 0 && Input.GetAxisRaw("VerticalArrow") == 0)
            Read = false;

        }


}
