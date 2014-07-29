using UnityEngine;
using M9Debug;

public class Block2D : MonoBehaviour {

	public void SetLight (Light2D l2d, int i1)
	{
		if (i1 == 1) renderer.material.SetColor("_Color", new Color(1.00F, 1.00F, 1.00F));
		if (i1 == 2) renderer.material.SetColor("_Color", new Color(0.75F, 0.75F, 0.75F));
		if (i1 == 3) renderer.material.SetColor("_Color", new Color(0.50F, 0.50F, 0.50F));
		if (i1 == 4) renderer.material.SetColor("_Color", new Color(0.25F, 0.25F, 0.25F));
		if (i1 == 5) renderer.material.SetColor("_Color", new Color(0.00F, 0.00F, 0.00F));
	}

}