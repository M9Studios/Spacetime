using UnityEngine;
using M9Debug;

[AddComponentMenu("Spacetime/Lighting2D/Block2D")]

public class Block2D : MonoBehaviour {

	private const int MAX_LIGHTS = Lighting2D.MAX_DISPLAY_LIGHTS;
	private const float UPDATE_TIME = Lighting2D.LIGHTING_UPDATE_TIME;

	public Light2D[] lights = new Light2D[MAX_LIGHTS];
	public float[] lightValues = new float[MAX_LIGHTS];
	public float hv = 0;

	private void Start ()
	{
		InvokeRepeating("RunLightingUpdate", UPDATE_TIME, UPDATE_TIME);
	}

	public void RunLightingUpdate ()
	{
		ApplyHighestLightValue();
	}

	public void SetLightValue (int i1, Light2D light)
	{
		for (int i = 0; i < lights.Length; i++)
		{
			if (lights[i].lightID == light.lightID)
			{
				lights[i].lightValue = 1-(float)i1/(float)lights[i].range;
				lightValues[i] = lights[i].lightValue;
			}
		}
	}

	private void ApplyHighestLightValue ()
	{
		hv = 0;

		for (int i = 0; i < lightValues.Length; i++)
		{
			if (lightValues[i] > hv)
			{
				hv = lightValues[i];
			}
		}

		renderer.material.SetColor("_Color", new Color(hv, hv, hv));
	}

	public void AddLight (Light2D light)
	{
		for (int i = 0; i < lights.Length; i++)
		{
			if (lights[i].lightID == 0)
			{
				lights[i] = light;
				return;
			}
		}
	}

	public void ChangeLight (int id, Light2D light)
	{
		for (int i = 0; i < lights.Length; i++)
		{
			if (lights[i].lightID == id)
			{
				lights[i].range = light.range;
			}
		}
	}

	public void RemoveLight (Light2D light)
	{
		for (int i = 0; i < lights.Length; i++)
		{
			if (lights[i].lightID == light.lightID)
			{
				lights[i].lightID = 0;
				lights[i].range = 0;
				lights[i].lightValue = 0;
				lightValues[i] = 0;
				hv = 0;
				return;
			}
		}

		ApplyHighestLightValue();
	}

	public bool LightExists (Light2D light)
	{
		for (int i = 0; i < lights.Length; i++)
		{
			if (lights[i].lightID == light.lightID)
			{
				return true;
			}
		}

		return false;
	}

	public float GetLightValue (Light2D light)
	{
		for (int i = 0; i < lights.Length; i++)
		{
			if (lights[i].lightID == light.lightID)
			{
				return lights[i].lightValue;
			}
		}

		return -1;
	}

}