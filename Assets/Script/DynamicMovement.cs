using UnityEngine;
using System.Collections;
using M9Debug;

public class DynamicMovement : MonoBehaviour {

	public float changeTime = 1.0F;
	public float time = 0.0F;
	public float direction = 0.0F;
	public float speed = 3.0F;

	void Update ()
	{
		if ((direction < 22.5F) && (direction > 337.5F)) // Up
		{
			transform.Translate(new Vector2(0, Time.deltaTime*speed));
		}
		if ((direction > 22.5F) && (direction < 67.5F)) // Up Right
		{
			transform.Translate(new Vector2(Time.deltaTime*speed, Time.deltaTime*speed));
		}
		if ((direction > 67.5F) && (direction < 112.5F)) // Right
		{
			transform.Translate(new Vector2(Time.deltaTime*speed, 0));
		}
		if ((direction > 112.5F) && (direction < 157.5F)) // Down Right
		{
			transform.Translate(new Vector2(Time.deltaTime*speed, -Time.deltaTime*speed));
		}
		if ((direction > 157.5F) && (direction < 202.5F)) // Down
		{
			transform.Translate(new Vector2(0, -Time.deltaTime*speed));
		}
		if ((direction > 202.5F) && (direction < 247.5F)) // Down Left
		{
			transform.Translate(new Vector2(-Time.deltaTime*speed, -Time.deltaTime*speed));
		}
		if ((direction > 247.5F) && (direction < 292.5F)) // Left
		{
			transform.Translate(new Vector2(-Time.deltaTime*speed, 0));
		}
		if ((direction > 292.5F) && (direction < 337.5F)) // Up Left
		{
			transform.Translate(new Vector2(-Time.deltaTime*speed, Time.deltaTime*speed));
		}

		time += Time.deltaTime;

		if (time > changeTime)
		{
			if (Random.value > 0.01F)
			{
				direction = Random.Range(0, 360);
				//M9Debugger.Log("Changing direction at time: " + time);
				//M9Debugger.Log("New direction: " + direction);
				changeTime = Random.value*2;
				time = 0.0F;
			}
		}
	}

}
