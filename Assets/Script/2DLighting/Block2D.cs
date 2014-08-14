using UnityEngine;
using M9Debug;
using System.Collections.Generic;

[AddComponentMenu("Lighting2D/Block2D")]
public class Block2D : MonoBehaviour {

	public bool is2D = false;

	private float updateTime = Lighting2D.LIGHTING_UPDATE_TIME;

	private List<GameObject> trackedLights = new List<GameObject>();
	private List<Light2D._Light2D> lights = new List<Light2D._Light2D>();
	private List<float> newValues = new List<float>();

	private float newValue = 0.0F;
	private float oldValue = 0.0F;

	private float[] randomRotations = new float[] { 000.0F, 090.0F, 180.0F, 270.0F };

	public void Start ()
	{
		transform.Rotate( new Vector3(0, 0, randomRotations[Random.Range(0, randomRotations.Length)]) );
			
		InvokeRepeating("UpdateLighting", updateTime, updateTime);
	}

	public void OnMouseDown ()
	{
		Camera.main.GetComponent<WorldBuilder>().ChangeBlock(transform.position.x, transform.position.y);
		Destroy(gameObject);
	}

	private void UpdateLighting ()
	{
		ApplyAdditiveLightValue();
		CheckTrackedLights();

		if (oldValue != newValue)
		{
			if (is2D)
			{
				renderer.material.SetColor("_Color", new Color(newValue, newValue, newValue));
			}
			else
			{
				renderer.material.color = new Color(newValue, newValue, newValue, 1);
			}

			oldValue = newValue;
		}
	}

	private void ApplyAdditiveLightValue ()
	{
		newValue = 0.0F;

		for (int i = 0; i < newValues.Count; i++)
		{
			if (newValues[i] > 0)
			{
				newValue += newValues[i];
			}
		}
	}

	private void ApplyHighestLightValue () // Simple highest value = light value method.
	{
		newValue = 0.0F;

		foreach(float f in newValues)
		{
			if (f > newValue)
			{
				newValue = f;
			}
		}
	}

	private void CheckTrackedLights ()
	{
		for (int i = 0; i < trackedLights.Count; i++)
		{
			if (trackedLights[i] != null)
			{
				if (lights[i] != null)
				{
					float x, y, a, b, r;

					r = lights[i]._range;
					a = transform.position.x;
					b = transform.position.y;
					x = trackedLights[i].transform.position.x-1.01F;
					y = trackedLights[i].transform.position.y-1.01F;

					if (((x-a)*(x-a))+((y-b)*(y-b)) > ((r*r)))
					{
						RemoveLight(lights[i]._lightID);
					}
				}
			}
		}
	}

	public void SetLightValue (int i1, int i2)
	{
		int i = 0;

		foreach(Light2D._Light2D l in lights)
		{
			if (l._lightID == i1)
			{
				newValues[i] = 1 - (float)i2/(float)lights[i]._range;
			}
			i++;
		}
	}

	public void AddLight (Light2D._Light2D light)
	{
		lights.Add(light);
		trackedLights.Add(null);
		newValues.Add(0.0F);
	}

	public void AddLight (Light2D._Light2D light, GameObject go)
	{
		lights.Add(light);
		trackedLights.Add(go);
		newValues.Add(0.0F);
	}

	public void RemoveLight (int i1)
	{
		if (lights.Count > 0)
		{
			for (int i = 0; i < lights.Count; i++)
			{
				if (lights[i]._lightID == i1)
				{
					lights.RemoveAt(i);
	
					if (trackedLights[i] != null)
					{
						trackedLights.RemoveAt(i);
					}

					newValues.RemoveAt(i);
					return;
				}
			}
		}
		else
		{
			//M9Debugger.LogWarning("No lights to remove.");
			return;
		}

		M9Debugger.LogWarning("Attempt to remove light " + i1 + " failed.");
	}

	public bool LightExists (int i1)
	{
		foreach (Light2D._Light2D l in lights)
		{
			if (l._lightID == i1) return true;
		}

		return false;
	}

}