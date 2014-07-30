using UnityEngine;
using M9Debug;

[AddComponentMenu("Spacetime/Lighting2D/Block2D")]

public class Block2D : MonoBehaviour {

	private const int MAX_LIGHTS = Lighting2D.MAX_DISPLAY_LIGHTS;

	public Light2D[] lights = new Light2D[MAX_LIGHTS];
	public float[] lightValues = new float[MAX_LIGHTS];
	public float nv = 0;

	public void RunLightingUpdate (int i1, Light2D light)
	{
		SetLightValue(i1, light);
		ApplyHighestLightValue();
	}

	public void SetLightValue (int i1, Light2D light)
	{
		for (int i = 0; i < lights.Length; i++)
		{
			lights[i].lightValue = 1-(float)i1/(float)lights[i].range;
			lightValues[i] = lights[i].lightValue;
		}
	}

	private void ApplyHighestLightValue ()
	{
		for (int i = 0; i < lightValues.Length; i++)
		{
			if (lightValues[i] < nv)
			{
				nv = lightValues[i];
			}
		}

		renderer.material.SetColor("_Color", new Color(nv, nv, nv));
	}

	public void AddLight (Light2D light)
	{
		for (int i = 0; i < lights.Length; i++)
		{
			if (lights[i].lightID == 0)
			{
				lights[i] = light;
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
			}
		}
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