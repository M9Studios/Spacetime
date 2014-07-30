using UnityEngine;
using M9Debug;

[AddComponentMenu("Spacetime/Lighting2D/Block2D")]

public class Block2D : MonoBehaviour {

	private const int MAX_LIGHTS = 32;
	private float increment = 0.0F;

	public void SetLightValue (int i1, int i2) // dist, range
	{
		increment = 1/(i2-1);

		for (int i = 0; i < i2; i++)
		{
			if (i1 == i) renderer.material.SetColor("_Color", new Color(increment*(i1-i), increment*(i1-i), increment*(i1-i)));
		}

		/*
		increment = 1/(i2-1);

		if (i1 == 1 || i1 == 0)
		{
			renderer.material.SetColor("_Color", new Color(1.00F, 1.00F, 1.00F));
			return;
		}
		if (i1 == i2)
		{
			renderer.material.SetColor("_Color", new Color(0.00F, 0.00F, 0.00F));
			return;
		}

		for (int i = 1; i < i2; i++)
		{
			if (i1 == i) renderer.material.SetColor("_Color", new Color(increment*(i2-i), increment*(i2-i), increment*(i2-i)));
		}
		*/
	}

}