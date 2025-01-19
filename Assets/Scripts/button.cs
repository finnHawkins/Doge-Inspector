using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class statusBtn : MonoBehaviour
{

    public Animator animator;

    public GameObject dog;

    public status status;

    // Start is called before the first frame update
    void Start()
    {
    animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseOver() {

        if(Input.GetMouseButton(0)) {

            animator.Play("Pressed");
            dog.GetComponent<doge>().reviewInspection(status);

        }
    }

    public void changeDoge(GameObject doge) {

        dog = doge;

	}
}
