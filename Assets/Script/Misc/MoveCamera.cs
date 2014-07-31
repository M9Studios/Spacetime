using UnityEngine;
using System.Collections;

[AddComponentMenu("Spacetime/Utilities/MoveCamera")]

public class MoveCamera : MonoBehaviour {

	public GameObject player;

	private void Update ()
	{
		if (player != null)
		{
			transform.position = new Vector3(player.transform.position.x, player.transform.position.y+0.5F, -10.0F);
		}
		else
		{
			if (Input.GetKey(KeyCode.W)) transform.Translate(new Vector2(0, 7.5F * Time.deltaTime));
			if (Input.GetKey(KeyCode.S)) transform.Translate(new Vector2(0, -7.5F * Time.deltaTime));
			if (Input.GetKey(KeyCode.A)) transform.Translate(new Vector2(-7.5F * Time.deltaTime, 0));
			if (Input.GetKey(KeyCode.D)) transform.Translate(new Vector2(7.5F * Time.deltaTime, 0));
		}
	}

}