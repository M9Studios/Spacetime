using UnityEngine;
using System.Collections;

public class MoveCamera : MonoBehaviour {

	void Update ()
	{
		if (Input.GetKey(KeyCode.W)) transform.Translate(new Vector2(0, 5F * Time.deltaTime));
		if (Input.GetKey(KeyCode.S)) transform.Translate(new Vector2(0, -5F * Time.deltaTime));
		if (Input.GetKey(KeyCode.A)) transform.Translate(new Vector2(-5F * Time.deltaTime, 0));
		if (Input.GetKey(KeyCode.D)) transform.Translate(new Vector2(5F * Time.deltaTime, 0));
	}

}