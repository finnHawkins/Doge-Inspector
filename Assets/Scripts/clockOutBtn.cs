using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clockOutBtn : MonoBehaviour
{

    public gameManager gm;

    private bool btnPressed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseOver() {
        if(Input.GetMouseButton(0)) {

            if(!btnPressed) {

                btnPressed = true;

                gm.toggleMenu();

            }
        
        } else {
            btnPressed = false;
        }
    }
}
