using UnityEngine;
using System.Collections;

[AddComponentMenu("Spacetime/Utilities/MoveCamera")]

public class MoveCamera : MonoBehaviour {

	private void Update ()
	{
		if (Input.GetKey(KeyCode.W)) transform.Translate(new Vector2(0, 5F * Time.deltaTime));
		if (Input.GetKey(KeyCode.S)) transform.Translate(new Vector2(0, -5F * Time.deltaTime));
		if (Input.GetKey(KeyCode.A)) transform.Translate(new Vector2(-5F * Time.deltaTime, 0));
		if (Input.GetKey(KeyCode.D)) transform.Translate(new Vector2(5F * Time.deltaTime, 0));
	}

}