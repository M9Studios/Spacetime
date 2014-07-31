using UnityEngine;
using M9Debug;

public class PointLight2D : MonoBehaviour {
	
	private Light2D light;

	private Collider2D[] c2d;
	private float xd = 0.0F;
	private float yd = 0.0F;
	private int d = 0;
	private bool changeLight = false;

	public int _range = 7;
	
	private void Start ()
	{
		light.range = _range;
		light.lightID = GetInstanceID();

		InvokeRepeating("UpdateLighting", 0.1F, 0.1F);
	}

	private void UpdateLighting()
	{
		if (light.range != _range)
		{
			light.range = _range;
			changeLight = true;
		}

		// Get collider of every GameObject within radius of _range.
		c2d = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), light.range);
		
		// For every collider calculate the distance between the light and the block.
		for (int i = 0; i < c2d.Length; i++)
		{
			if (c2d[i].GetComponent<Block2D>() != null)
			{
				if (changeLight)
				{
					c2d[i].GetComponent<Block2D>().ChangeLight(light, light.lightID);
					changeLight = false;
				}

				xd = c2d[i].transform.position.x - transform.position.x;
				yd = c2d[i].transform.position.y - transform.position.y;

				d = Mathf.RoundToInt(Mathf.Sqrt((xd*xd)+(yd*yd)));

				if (!c2d[i].GetComponent<Block2D>().LightExists(light)) c2d[i].GetComponent<Block2D>().AddLight(light);

				c2d[i].GetComponent<Block2D>().SetLightValue(light, d);
			}
		}
	}

	private void OnDisable()
	{
		c2d = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), light.range);
		
		for (int i = 0; i < c2d.Length; i++)
		{
			if (c2d[i].GetComponent<Block2D>() != null)
			{
				c2d[i].GetComponent<Block2D>().RemoveLight(light);
			}
		}

		M9Debugger.Log("Light " + light.lightID + " has been disabled.");
	}

}