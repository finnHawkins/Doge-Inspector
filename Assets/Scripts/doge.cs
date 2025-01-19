using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum status {
  accepted,
  declined
}

public class doge : MonoBehaviour
{

  public Rigidbody body;
  public Animator animator;
  private gameManager gm;

  private float starttime;
  private float endtime;
  private static float transitionSpeed = 2f;
  private float transitionLength;

  public Vector3 startpos;
  public Vector3 endpos;

  public bool travelling;
  public bool entering; //true if entering scene, false if exiting

  public bool isDog;

  public bool correctStatus;

  // Start is called before the first frame update
  public void Start() {

    startpos = new Vector3(-5.25f, 2f, 4f);
    endpos = new Vector3(0f, 2f, 4f);

    prepTransition(startpos, endpos);

    entering = true;

    gm = GameObject.Find("clockmachine").GetComponent<gameManager>();

  }

  // Update is called once per frame
  void Update(){

    if(gm.gs == gameScreen.gameScreen && !gm.gamePaused) {

      if(travelling) {

        float t = ((Time.time - starttime) * transitionSpeed) / transitionLength;
        t = Mathf.Clamp01(t);

        body.position = Vector3.Lerp(startpos, endpos, t);

        if(Vector3.Distance(body.position, endpos) == 0) {

          travelling = false;
          
          if(entering) {
            
            entering = false;
            startpos = body.position;

          } else {

            gm.makeNewDog();

          }
          
        }

      }
    }

  }

  void OnMouseOver() {

    if(Input.GetMouseButton(0)) {
    
      animator.Play("Clicked");

    }
  }

  public void reviewInspection(status status) {

    if(!travelling) {

      switch(status) {
        case status.accepted:

          correctStatus = isDog ? true : false;

          endpos = new Vector3(5.5f, 2f, 4f);
          break;
        case status.declined:
          
          int rand = Random.Range(1, 3);

          string animName = rand == 1 ? "Roll" : "Spin";

          animator.Play(animName);

          correctStatus = isDog ? false : true;

          endpos = new Vector3(0, 5.5f, 4f);
          break;
      }

      if(!correctStatus) {
        gm.flashLight();
      }

      prepTransition(body.position, endpos);

    }

  }

  public void prepTransition(Vector3 startpos, Vector3 endpos) {

    this.startpos = startpos;
    body.position = startpos;
    this.endpos = endpos;
    transitionLength = Vector3.Distance(startpos, endpos);

    starttime = Time.time;
    endtime = starttime + transitionSpeed;

    travelling = true;

  }

}
