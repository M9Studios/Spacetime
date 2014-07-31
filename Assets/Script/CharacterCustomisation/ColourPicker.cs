using UnityEngine;
using System.Collections;

public class ColourPicker : MonoBehaviour {

	public GameObject colourObject = null;
	public Vector2 guiArea = new Vector2(0, 0);
	public bool GUIEnabled = true;

	private float yAdj = 20;

	private float sliderWidth = 100;
	private float sliderHeight = 15;

	private Color _RGB = new Color(1, 1, 1);

	private float fr = 1.0F;
	private float fg = 1.0F;
	private float fb = 1.0F;

	private bool changeColour = false;
	public bool randomize = false;

	private void Start ()
	{
		if (colourObject == null)
		{
			if (gameObject.renderer != null) colourObject = gameObject;
		}
	}

	private void Update ()
	{
		CreateRGB(fr, fg, fb);
	}

	private void OnGUI ()
	{
		if (GUIEnabled)
		{
			changeColour = (colourObject != null) ? true : false;

			if (changeColour)
			{
				colourObject.renderer.material.SetColor("_Color", RGB);
				changeColour = false;
			}

			if (randomize)
			{
				RandomizeRGB();
			}
		}
	}

	private void RandomizeRGB ()
	{
		fr = UnityEngine.Random.value;
		fg = UnityEngine.Random.value;
		fb = UnityEngine.Random.value;

		randomize = false;
	}

	private void CreateRGB (float r, float g, float b)
	{
		RGB = new Color(r, g, b);
	}

	public Color RGB
	{
		get
		{
			return _RGB;
		}
		private set
		{
			_RGB = value;
		}
	}
	
	public float Fr
	{
		get { return fr; }
		set { fr = value; }
	}

	public float Fg
	{
		get { return fg; }
		set { fg = value; }
	}

	public float Fb
	{
		get { return fb; }
		set { fb = value; }
	}

	public bool Randomize
	{
		get
		{
			return randomize;
		}
		set
		{
			randomize = value;
		}
	}

}