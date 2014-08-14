using UnityEngine;
using System.Collections;

public class CharacterScript : MonoBehaviour {

	public bool noclip = true;

	private void Start ()
	{
		//CreateMesh();
		if (noclip)
		{
			if (rigidbody2D != null)
			{
				rigidbody2D.isKinematic = true;
			}
			if (rigidbody != null)
			{
				rigidbody.isKinematic = true;
			}
		}
		else
		{
			if (rigidbody2D != null)
			{
				rigidbody2D.isKinematic = false;
			}
			if (rigidbody != null)
			{
				rigidbody.isKinematic = false;
			}
		}
	}

	private void Update ()
	{
		if (noclip)
		{
			if (rigidbody2D != null)
			{
				rigidbody2D.isKinematic = true;
			}
			if (rigidbody != null)
			{
				rigidbody.isKinematic = true;
			}

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
			if (rigidbody2D != null)
			{
				rigidbody2D.isKinematic = false;
			}
			if (rigidbody != null)
			{
				rigidbody.isKinematic = false;
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
			if (Input.GetKeyDown(KeyCode.Space))
			{
				if (rigidbody2D != null)
				{
					rigidbody2D.AddForce(Vector2.up*45250.0F);
				}
				if (rigidbody != null)
				{
					rigidbody.AddForce(Vector3.up*45250.0F);
				}
			}
		}
	}

}