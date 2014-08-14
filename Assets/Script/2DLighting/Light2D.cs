using UnityEngine;
using M9Debug;
using M9MathHelper;

[AddComponentMenu("Lighting2D/Light2D")]
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
		public LightType2D	_lightType;
		public int			_lightID;
		public int			_range;
		public float		_intensity;
		public Color		_colour;
		public float		_angle;
		public bool			_isStatic;

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


	private float		updateTime = Lighting2D.LIGHTING_UPDATE_TIME;
	private _Light2D	light2D;
	private bool		lightAdded = false;
	
	public float		aMin,
						aMax,
						a;

	public bool			is2D = false;
	public LightType2D	lightType;
	[HideInInspector]
	public int			lightID;
	public int			range;
	public float		intensity;
	public Color		colour;
	public float		angle;
	public bool			isStatic;


	#region Unity functions.
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
		if (light2D._isStatic)
		{
			RemoveStaticLight();
		}
		else
		{
			if ((light2D._lightType == LightType2D.Point) || (light2D._lightType == LightType2D.Spot))
			{
				RemoveDynamicLight();
			}
			else if (light2D._lightType == LightType2D.UNIMPLEMENTED)
			{
				M9Debugger.Log("Attempting to remove unimplemented light.");
			}
			else
			{
				M9Debugger.Log("Attempting to remove non-existent light.");
			}
		}

	}
	#endregion


	#region Unity editor functions.
	private void CheckEditorChanges ()
	{
		bool updateLight = false;

		if (light2D._lightType != lightType) updateLight = true;
		if (light2D._range != range) updateLight = true;
		if (light2D._intensity != intensity) updateLight = true;
		if (light2D._colour != colour) updateLight = true;

		if ((updateLight) && (!light2D._isStatic))
		{
			M9Debugger.Log("Reconstructing light: " + lightID + " at x: " + transform.position.x + " y: " + transform.position.y);
			if (lightType == LightType2D.Point) light2D = new _Light2D(lightType, lightID, range, intensity, colour, isStatic);
			if (lightType == LightType2D.Spot) light2D = new _Light2D(lightType, lightID, range, intensity, colour, angle, isStatic);
		}
	}
	#endregion


	#region Apply lighting.
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
	#endregion


	#region Point light.
	private void AddStaticPointLight ()
	{
		M9Debugger.Log("Adding static point light " + light2D._lightID + " at x: " + transform.position.x + " y: " + transform.position.y);

		if (is2D)
		{
			Collider2D[] c2d = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), light2D._range);
			
			if (c2d.Length > 0)
			{
				ApplyPointLight(c2d);
			}
		}
		else
		{
			Collider[] c = Physics.OverlapSphere(new Vector3(transform.position.x, transform.position.y, 0), light2D._range);
			
			if (c.Length > 0)
			{
				ApplyPointLight(c);
			}
		}

		lightAdded = true;
	}

	private void UpdateLightingPoint ()
	{
		if (is2D)
		{
			Collider2D[] c2d = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), light2D._range);
			
			if (c2d.Length > 0)
			{
				ApplyPointLight(c2d);
			}
		}
		else
		{
			Collider[] c = Physics.OverlapSphere(new Vector3(transform.position.x, transform.position.y, 0), light2D._range);
			
			if (c.Length > 0)
			{
				ApplyPointLight(c);
			}
		}
	}
	#endregion


	#region Spot light.
	private void AddStaticSpotLight ()
	{
		M9Debugger.Log("Adding static spot light " + light2D._lightID + " at x: " + transform.position.x + " y: " + transform.position.y);

		if (is2D)
		{
			Collider2D[] c2d = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), light2D._range);

			if (c2d.Length > 0)
			{
				ApplySpotLight(c2d);
			}
		}
		else
		{
			Collider[] c = Physics.OverlapSphere(new Vector3(transform.position.x, transform.position.y, 0), light2D._range);

			if (c.Length > 0)
			{
				ApplySpotLight(c);
			}
		}

		lightAdded = true;
	}

	private void UpdateLightingSpot ()
	{
		if (is2D)
		{
			Collider2D[] c2d = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), light2D._range);
			
			if (c2d.Length > 0)
			{
				ApplySpotLight(c2d);
			}
		}
		else
		{
			Collider[] c = Physics.OverlapSphere(new Vector3(transform.position.x, transform.position.y, 0), light2D._range);
			
			if (c.Length > 0)
			{
				ApplySpotLight(c);
			}
		}
	}
	#endregion


	#region Light general functions.
	private int GetDistanceToBlock (Block2D b1)
	{
		float xd = b1.transform.position.x - transform.position.x;
		float yd = b1.transform.position.y - transform.position.y;
		return Mathf.RoundToInt(Mathf.Sqrt((xd*xd)+(yd*yd)));
	}

	private void SetBlockLightValue (Block2D b1, int i1)
	{
		if (!b1.LightExists(light2D._lightID))
		{
			b1.AddLight(light2D);
		}
		
		b1.SetLightValue(light2D._lightID, i1);
	}

	private void RemoveLight (Collider2D[] c1)
	{
		for (int i = 0; i < c1.Length; i++)
		{
			if (c1[i].GetComponent<Block2D>() != null)
			{
				Block2D block = c1[i].GetComponent<Block2D>();

				if (block.LightExists(light2D._lightID))
				{
					block.SetLightValue(light2D._lightID, light2D._range);
					block.RemoveLight(light2D._lightID);
				}
			}
		}
	}
	
	private void RemoveLight (Collider[] c1)
	{
		for (int i = 0; i < c1.Length; i++)
		{
			if (c1[i].GetComponent<Block2D>() != null)
			{
				Block2D block = c1[i].GetComponent<Block2D>();

				if (block.LightExists(light2D._lightID))
				{
					c1[i].GetComponent<Block2D>().SetLightValue(light2D._lightID, light2D._range);
					c1[i].GetComponent<Block2D>().RemoveLight(light2D._lightID);
				}
			}
		}
	}

	private void RemoveStaticLight ()
	{
		M9Debugger.Log("Removing static light " + light2D._lightID + " at x: " + transform.position.x + " y: " + transform.position.y);
		
		if (is2D)
		{
			Collider2D[] c2d = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), light2D._range);
			
			if (c2d.Length > 0)
			{
				RemoveLight(c2d);
			}
		}
		else
		{
			Collider[] c = Physics.OverlapSphere(new Vector3(transform.position.x, transform.position.y, 0), light2D._range);
			
			if (c.Length > 0)
			{
				RemoveLight(c);
			}
		}
	}

	private void RemoveDynamicLight ()
	{
		M9Debugger.Log("Removing dynamic light " + light2D._lightID + " at x: " + transform.position.x + " y: " + transform.position.y);
		
		if (is2D)
		{
			Collider2D[] c2d = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), light2D._range);
			
			if (c2d.Length > 0)
			{
				RemoveLight(c2d);
			}
		}
		else
		{
			Collider[] c = Physics.OverlapSphere(new Vector3(transform.position.x, transform.position.y, 0), light2D._range);
			
			if (c.Length > 0)
			{
				RemoveLight(c);
			}
		}
	}
	#endregion

	
	#region Spot light specific functions.
	private void ApplySpotLight (Collider2D[] c1)
	{
		aMin = transform.rotation.eulerAngles.z - angle;
		aMax = transform.rotation.eulerAngles.z + angle;
		
		for (int i = 0; i < c1.Length; i++)
		{
			if (c1[i].GetComponent<Block2D>() != null)
			{
				Block2D block = c1[i].GetComponent<Block2D>();

				a = M9AngleHelper.GetAngle(new Vector2(transform.position.x, transform.position.y), new Vector2(block.transform.position.x, block.transform.position.y));
				
				if ((a > aMin) && (a < aMax))
				{
					SetBlockLightValue(block, GetDistanceToBlock(block));
				}
				else
				{
					SetBlockLightValue(block, light2D._range);
				}
			}
		}
	}

	private void ApplySpotLight (Collider[] c1)
	{
		aMin = transform.rotation.eulerAngles.z - angle;
		aMax = transform.rotation.eulerAngles.z + angle;

		for (int i = 0; i < c1.Length; i++)
		{
			if (c1[i].GetComponent<Block2D>() != null)
			{
				Block2D block = c1[i].GetComponent<Block2D>();
				
				a = M9AngleHelper.GetAngle(new Vector2(transform.position.x, transform.position.y), new Vector2(block.transform.position.x, block.transform.position.y));
				
				if ((a > aMin) && (a < aMax))
				{
					SetBlockLightValue(block, GetDistanceToBlock(block));
				}
			}
		}
	}
	#endregion


	#region Point light specific functions.
	private void ApplyPointLight (Collider2D[] c1)
	{
		for (int i = 0; i < c1.Length; i++)
		{
			if (c1[i].GetComponent<Block2D>() != null)
			{
				Block2D block = c1[i].GetComponent<Block2D>();
				SetBlockLightValue(block, GetDistanceToBlock(block));
			}
		}
	}
	
	private void ApplyPointLight (Collider[] c1)
	{
		for (int i = 0; i < c1.Length; i++)
		{
			if (c1[i].GetComponent<Block2D>() != null)
			{
				Block2D block = c1[i].GetComponent<Block2D>();
				SetBlockLightValue(block, GetDistanceToBlock(block));
			}
		}
	}
	#endregion

}