using UnityEngine;
using System.Collections;

public class CharacterScript : MonoBehaviour {

	public bool noclip = true;

	void Update ()
	{
		if (noclip)
		{
			rigidbody2D.isKinematic = true;
			if (Input.GetKey(KeyCode.W))
			{
				transform.Translate(new Vector2(0, 5F * Time.deltaTime));
			}
			if (Input.GetKey(KeyCode.S))
			{
				transform.Translate(new Vector2(0, -5F * Time.deltaTime));
			}
			if (Input.GetKey(KeyCode.A))
			{
				transform.localScale = new Vector3(1.0F, 1.0F, 1.0F);
				transform.Translate(new Vector2(-5F * Time.deltaTime, 0));
			}
			if (Input.GetKey(KeyCode.D))
			{
				transform.localScale = new Vector3(-1.0F, 1.0F, 1.0F);
				transform.Translate(new Vector2(5F * Time.deltaTime, 0));
			}
		}
		else
		{
			rigidbody2D.isKinematic = false;
			if (Input.GetKey(KeyCode.A))
			{
				transform.localScale = new Vector3(1.0F, 1.0F, 1.0F);
				transform.Translate(new Vector2(-5F * Time.deltaTime, 0));
			}
			if (Input.GetKey(KeyCode.D))
			{
				transform.localScale = new Vector3(-1.0F, 1.0F, 1.0F);
				transform.Translate(new Vector2(5F * Time.deltaTime, 0));
			}
			if (Input.GetKeyDown(KeyCode.Space))
			{
				rigidbody2D.AddForce(Vector2.up*45250.0F);
			}
		}
	}
}
