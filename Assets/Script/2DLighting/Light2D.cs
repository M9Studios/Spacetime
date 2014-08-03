using UnityEngine;
using M9Debug;
using M9MathHelper;

public class Light2D : MonoBehaviour
{
	public enum LightType2D
	{
		Point,
		Spot,
		UNIMPLEMENTED
	}

	public class _Light2D
	{
		public LightType2D _lightType;
		public int _lightID;
		public int _range;
		public float _intensity;
		public Color _colour;
		public float _angle;
		public bool _isStatic;

		public _Light2D (LightType2D l1, int i1, int i2, float f1, Color c1, bool b1) // Pointlight constructor.
		{
			_lightType = l1;
			_lightID = i1;
			_range = i2;
			_intensity = f1;
			_colour = c1;
			_angle = -1.0F;
			_isStatic = b1;
		}

		public _Light2D (LightType2D l1, int i1, int i2, float f1, Color c1, float f2, bool b1) // Spotlight constructor.
		{
			_lightType = l1;
			_lightID = i1;
			_range = i2;
			_intensity = f1;
			_colour = c1;
			_angle = f2;
			_isStatic = b1;
		}
	}

	private _Light2D light2D;

	public LightType2D lightType;
	[HideInInspector]
	public int lightID;
	public int range;
	public float intensity;
	public Color colour;
	public float angle;
	public bool isStatic;

	private bool lightAdded = false;

	private float updateTime = Lighting2D.LIGHTING_UPDATE_TIME;

	public float aMin, aMax, a;

	private void Start ()
	{
		lightID = this.GetInstanceID();
		
		if (lightType == LightType2D.Point)
			light2D = new _Light2D(lightType, lightID, range, intensity, colour, isStatic);

		if (lightType == LightType2D.Spot)
			light2D = new _Light2D(lightType, lightID, range, intensity, colour, angle, isStatic);

		InvokeRepeating("UpdateLighting", updateTime, updateTime);
		InvokeRepeating("CheckEditorChanges", 0.5F, 0.5F);
	}

	private void OnEnable ()
	{
		if (light2D != null)
		{
			if (light2D._lightType == LightType2D.Point)
			{
				if (light2D._isStatic)
				{
					AddStaticPointLight();
				}
				else
				{
					// Implement me.
				}
			}
			
			if (light2D._lightType == LightType2D.Spot)
			{
				if (light2D._isStatic)
				{
					AddStaticSpotLight();
				}
				else
				{
					// Implement me.
				}
			}
		}
	}

	private void OnDisable ()
	{
		if (light2D._lightType == LightType2D.Point)
		{
			if (light2D._isStatic)
			{
				RemoveStaticPointLight();
			}
			else
			{
				// Implement me.
			}
		}

		if (light2D._lightType == LightType2D.Spot)
		{
			if (light2D._isStatic)
			{
				RemoveStaticSpotLight();
			}
			else
			{
				// Implement me.
			}
		}
	}

	private void CheckEditorChanges ()
	{
		bool updateLight = false;

		if (light2D._lightType != lightType) updateLight = true;
		if (light2D._range != range) updateLight = true;
		if (light2D._intensity != intensity) updateLight = true;
		if (light2D._colour != colour) updateLight = true;

		if ((updateLight) && (!light2D._isStatic))
		{
			M9Debugger.Log("Reconstructing light: " + lightID + " at x=" + transform.position.x + " y=" + transform.position.y);
			if (lightType == LightType2D.Point) light2D = new _Light2D(lightType, lightID, range, intensity, colour, isStatic);
			if (lightType == LightType2D.Spot) light2D = new _Light2D(lightType, lightID, range, intensity, colour, angle, isStatic);
		}
	}

	private void UpdateLighting ()
	{
		if (light2D._lightType == LightType2D.Point)
		{
			if (light2D._isStatic)
			{
				if (!lightAdded)
				{
					AddStaticPointLight();
				}
			}
			else
			{
				UpdateLightingPoint();
			}
		}
		if (light2D._lightType == LightType2D.Spot)
		{
			if (light2D._isStatic)
			{
				if (!lightAdded)
				{
					AddStaticSpotLight();
				}
			}
			else
			{
				UpdateLightingSpot();
			}
		}
		if (light2D._lightType == LightType2D.UNIMPLEMENTED)
		{
			M9Debugger.LogWarning("UNIMPLEMENTED light mode in use, please switch the LightType in the inspector.");
		}
	}

	private void AddStaticPointLight ()
	{
		Collider2D[] c2d = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), light2D._range);
		
		if (c2d.Length > 0)
		{
			for (int i = 0; i < c2d.Length; i++)
			{
				if (c2d[i].GetComponent<Block2D>() != null)
				{
					Block2D block = c2d[i].GetComponent<Block2D>();
					
					float xd = c2d[i].transform.position.x - transform.position.x;
					float yd = c2d[i].transform.position.y - transform.position.y;
					int d = Mathf.RoundToInt(Mathf.Sqrt((xd*xd)+(yd*yd)));
					
					if (!block.LightExists(light2D._lightID))
					{
						block.AddLight(light2D);
					}
					
					block.SetLightValue(light2D._lightID, d);
				}
			}
		}

		lightAdded = true;
	}

	private void RemoveStaticPointLight ()
	{
		M9Debugger.Log("Removing light " + light2D._lightID + " from scene.");

		Collider2D[] c2d = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), light2D._range);
		
		for (int i = 0; i < c2d.Length; i++)
		{
			if (c2d[i].GetComponent<Block2D>() != null)
			{
				c2d[i].GetComponent<Block2D>().SetLightValue(light2D._lightID, light2D._range);
				c2d[i].GetComponent<Block2D>().RemoveLight(light2D._lightID);
			}
		}
	}
	
	private void UpdateLightingPoint ()
	{
		Collider2D[] c2d = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), light2D._range);

		if (c2d.Length > 0)
		{
			for (int i = 0; i < c2d.Length; i++)
			{
				if (c2d[i].GetComponent<Block2D>() != null)
				{
					Block2D block = c2d[i].GetComponent<Block2D>();

					float xd = c2d[i].transform.position.x - transform.position.x;
					float yd = c2d[i].transform.position.y - transform.position.y;
					int d = Mathf.RoundToInt(Mathf.Sqrt((xd*xd)+(yd*yd)));

					if (!block.LightExists(light2D._lightID))
					{
						block.AddLight(light2D, gameObject);
					}

					block.SetLightValue(light2D._lightID, d);
				}
			}
		}
	}

	private void AddStaticSpotLight ()
	{
		Collider2D[] c2d = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), light2D._range);
		
		if (c2d.Length > 0)
		{
			aMin = transform.rotation.eulerAngles.z - angle;
			aMax = transform.rotation.eulerAngles.z + angle;
			
			for (int i = 0; i < c2d.Length; i++)
			{
				if (c2d[i].GetComponent<Block2D>() != null)
				{
					//angle between light and block
					a = M9AngleHelper.GetAngle(new Vector2(transform.position.x, transform.position.y), new Vector2(c2d[i].transform.position.x, c2d[i].transform.position.y));
					
					if ((a > aMin) && (a < aMax))
					{
						Block2D block = c2d[i].GetComponent<Block2D>();
						float xd = c2d[i].transform.position.x - transform.position.x;
						float yd = c2d[i].transform.position.y - transform.position.y;
						int d = Mathf.RoundToInt(Mathf.Sqrt((xd*xd)+(yd*yd)));
						
						if (!block.LightExists(light2D._lightID))
						{
							block.AddLight(light2D);
						}
						
						block.SetLightValue(light2D._lightID, d);
					}
				}
			}
		}

		lightAdded = true;
	}
	
	private void RemoveStaticSpotLight ()
	{
		M9Debugger.Log("Removing light " + light2D._lightID + " from scene.");

		Collider2D[] c2d = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), light2D._range);
		
		for (int i = 0; i < c2d.Length; i++)
		{
			if (c2d[i].GetComponent<Block2D>() != null)
			{
				c2d[i].GetComponent<Block2D>().SetLightValue(light2D._lightID, light2D._range);
				c2d[i].GetComponent<Block2D>().RemoveLight(light2D._lightID);
			}
		}
	}

	private void UpdateLightingSpot ()
	{
		Collider2D[] c2d = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), light2D._range);
		
		if (c2d.Length > 0)
		{
			aMin = transform.rotation.eulerAngles.z - angle;
			aMax = transform.rotation.eulerAngles.z + angle;

			for (int i = 0; i < c2d.Length; i++)
			{
				if (c2d[i].GetComponent<Block2D>() != null)
				{
					//angle between light and block
					a = M9AngleHelper.GetAngle(new Vector2(transform.position.x, transform.position.y), new Vector2(c2d[i].transform.position.x, c2d[i].transform.position.y));

					if ((a > aMin) && (a < aMax))
					{
						Block2D block = c2d[i].GetComponent<Block2D>();
						float xd = c2d[i].transform.position.x - transform.position.x;
						float yd = c2d[i].transform.position.y - transform.position.y;
						int d = Mathf.RoundToInt(Mathf.Sqrt((xd*xd)+(yd*yd)));
						
						if (!block.LightExists(light2D._lightID))
						{
							block.AddLight(light2D, gameObject);
						}
						
						block.SetLightValue(light2D._lightID, d);
					}
					else
					{
						Block2D block = c2d[i].GetComponent<Block2D>();
						
						if (!block.LightExists(light2D._lightID))
						{
							block.AddLight(light2D, gameObject);
						}
						
						block.SetLightValue(light2D._lightID, light2D._range);
					}
				}
			}
		}
	}

}