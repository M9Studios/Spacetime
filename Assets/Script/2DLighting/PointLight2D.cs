using UnityEngine;
using M9Debug;

public class PointLight2D : MonoBehaviour {
	
	public int _range = 7;
	
	private Collider2D[] c2d;
	private float xd = 0.0F;
	private float yd = 0.0F;
	private int d = 0;
	
	public PointLight2D (int i1)
	{
		this.range = i1;
	}
	
	private void Start ()
	{
		InvokeRepeating("UpdateLighting", 0.025F, 0.025F);
	}
	
	private void UpdateLighting()
	{
		#region Block lighting.
		// Get collider of every GameObject within radius of _range.
		c2d = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), _range);
		
		// For every collider calculate the distance between the light and the block.
		for (int i = 0; i < c2d.Length; i++)
		{
			if (c2d[i].GetComponent<Block2D>() != null)
			{
				xd = c2d[i].transform.position.x - transform.position.x;
				yd = c2d[i].transform.position.y - transform.position.y;
				d = Mathf.RoundToInt(Mathf.Sqrt((xd*xd)+(yd*yd)));
				c2d[i].GetComponent<Block2D>().SetLightValue(d, _range);
			}
		}
		#endregion
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
	
}