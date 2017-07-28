using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollMovement : MonoBehaviour
{

  public GameObject Body;
  public GameObject Head;

  void Start()
  {
    var rigidbody2d = Body.GetComponent<Rigidbody2D>();
    rigidbody2d.centerOfMass = new Vector2(rigidbody2d.centerOfMass.x, rigidbody2d.centerOfMass.y - 0.6f);
  }
	
	// Update is called once per frame
	void Update ()
	{
    //if (Input.GetKeyDown(KeyCode.UpArrow))
    //  Body.transform.Translate(new Vector3(0, Time.deltaTime * 20));
    //else if (Input.GetKey(KeyCode.LeftArrow))
    //  Body.transform.Translate(new Vector3(-Time.deltaTime * 5, 0));
    //else if (Input.GetKey(KeyCode.RightArrow))
    //  Body.transform.Translate(new Vector3(Time.deltaTime * 5, 0));
	  if (Input.GetKeyDown(KeyCode.UpArrow))
	  {
      var headRigidBody = Head.GetComponent<Rigidbody2D>();
      Head.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 20), ForceMode2D.Impulse);
	  }
	  else if (Input.GetKey(KeyCode.LeftArrow))
	    Body.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1, 0), ForceMode2D.Impulse);
	    //Body.GetComponent<Rigidbody2D>().AddForce(new Vector2(transform.right.x * 2, 0),ForceMode2D.Impulse);
	  else if (Input.GetKey(KeyCode.RightArrow))
	  {
	    Body.GetComponent<Rigidbody2D>().AddForce(new Vector2(1, 0), ForceMode2D.Impulse);
	    //Body.GetComponent<Rigidbody2D>().AddForce(new Vector2(-transform.right.x*2, 0), ForceMode2D.Impulse);
	  }
	}
}
