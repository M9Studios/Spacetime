using UnityEngine;
using M9Debug;

public class Light2D : MonoBehaviour {

	private Light2D light2d;

	/*
	public float _r = 0.0F;
	public float _g = 0.0F;
	public float _b = 0.0F;
	*/
	public float _range = 5.0F;
	private const float updateTime = 0.0024F;
	private Collider2D[] c2d;

	public Light2D (float f1)
	{
		range = f1;
	}

	/*
	public Light2D (float f1, float f2, float f3, float f4)
	{
		r = f1;
		g = f2;
		b = f3;
		range = f4;
	}
	*/

	private void Start ()
	{
		this.light2d = new Light2D(range);
		// HEy now brown cow.
		InvokeRepeating("UpdateLighting", updateTime, updateTime);
	}

	private void UpdateLighting ()
	{
		c2d = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), _range);

		for (int i = 0; i < c2d.Length; i++)
		{
			float xd = c2d[i].transform.position.x - transform.position.x;
			float yd = c2d[i].transform.position.y - transform.position.y;
			int d = Mathf.RoundToInt(Mathf.Sqrt(xd*xd+yd*yd));
			c2d[i].GetComponent<Block2D>().SetLight(light2d, d);
		}
	}

	/*
	public float r
	{
		get
		{
			return _r;
		}
		set
		{
			_r = value;
		}
	}
	
	public float g
	{
		get
		{
			return _g;
		}
		set
		{
			_g = value;
		}
	}
	
	public float b
	{
		get
		{
			return _b;
		}
		set
		{
			_b = value;
		}
	}
	*/

	public float range
	{
		get
		{
			return _range;
		}
		set
		{
			_range = value;
		}
	}

}