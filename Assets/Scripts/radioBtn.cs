using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class radioBtn : MonoBehaviour
{

    public int clickObjectID;

    public Radio radio;

    private Animator animator;

    private bool btnPressed;

    // Start is called before the first frame update
    void Start()
    {
        animator = radio.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {


    }

    void OnMouseOver() {
        if(Input.GetMouseButton(0)) {

            if(!btnPressed) {

                btnPressed = true;

                string animName = "";

                switch(clickObjectID) {
                    case 1:
                        animName = "OBP";
                        radio.toggleMusic();
                        break;
                    case 2:
                        animName = "PBP";
                        radio.playLastSong();
                        break;
                    case 3:
                        animName = "NBP";
                        radio.playNextSong();
                        break;
                }

                animator.Play(animName);

            }
        
        } else {
            btnPressed = false;
        }
    }

}
