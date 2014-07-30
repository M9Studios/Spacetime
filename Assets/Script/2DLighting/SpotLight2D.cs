using UnityEngine;
using System.Collections;
using M9Debug;

public class SpotLight2D : MonoBehaviour {

	public int _range;
	public int _angle;
	public float _direction;
	public float c1, c2;
	
	private float xd = 0.0F;
	private float yd = 0.0F;
	private int d = 0;

	private Collider2D[] c2d;

	private void Start ()
	{
		InvokeRepeating("UpdateLighting", 0.025F, 0.025F);
	}

	private void Update ()
	{
		if (_direction != transform.rotation.z)
		{
			if (transform.rotation.eulerAngles.z < 0.0F)
			{
				_direction = 360-transform.rotation.eulerAngles.z;
			}
			else
			{
				_direction = transform.rotation.eulerAngles.z;
			}
		}
	}

	private void UpdateLighting ()
	{
		c2d = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), _range);

		for (int i = 0; i < c2d.Length; i++)
		{
			if (c2d[i].GetComponent<Block2D>() != null)
			{
				c1 = _direction + _angle;
				if (c1 > 360) c1 = c1-360;
				c2 = _direction - _angle;

				float a = Vector2.Angle(new Vector2(c2d[i].transform.position.x, c2d[i].transform.position.y), new Vector2(transform.position.x, transform.position.y));

				if (a > c2 || a < c1)
				{
					xd = c2d[i].transform.position.x - transform.position.x;
					yd = c2d[i].transform.position.y - transform.position.y;
					d = Mathf.RoundToInt(Mathf.Sqrt((xd*xd)+(yd*yd)));
					c2d[i].GetComponent<Block2D>().SetLightValue(d, _range);
				}
			}
		}
	}

	public int range
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
	
	public int angle
	{
		get
		{
			return _angle;
		}
		set
		{
			_angle = value;
		}
	}

	public float direction
	{
		get
		{
			return _direction;
		}
		set
		{
			_direction = value;
		}
	}

}