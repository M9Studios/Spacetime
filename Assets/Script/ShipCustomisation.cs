using UnityEngine;
using System.Collections;

public class ShipCustomisation : MonoBehaviour
{
	public Sprite[] cockpitTextures = new Sprite[10];

	public Texture spacerBlack;
	public Texture spacerGray;

	public Vector2 targetResolution = new Vector2(1280, 720);
	public Vector2 currentResolution = new Vector2(0, 0);

	public float xScale = 0.0F;
	public float yScale = 0.0F;

	public float aspectRatio = 0.0F;

	private const float UPDATE_TIME = 0.1F;

	private float boxWidth = 67.8F;
	private float boxHeight = 67.8F;

	public class ShipPart
	{
		public ShipPart ()
		{

		}
	}

	private ShipPart cockpit;
	private ShipPart hull;
	private ShipPart fin;
	private ShipPart engine;
	private ShipPart tail;

	private void Start ()
	{
		cockpitTextures = Resources.LoadAll("Art/UI/Spaceship Customisation") as Sprite[];

		currentResolution.x = Screen.width;
		currentResolution.y = Screen.height;

		xScale = currentResolution.x / targetResolution.x;
		yScale = currentResolution.y / targetResolution.y;

		aspectRatio = currentResolution.x / currentResolution.y;

		InvokeRepeating("CheckScreenChange", UPDATE_TIME, UPDATE_TIME);
	}
	
	private void CheckScreenChange ()
	{
		if (Screen.width != currentResolution.x)
		{
			currentResolution.x = Screen.width;
			xScale = currentResolution.x / targetResolution.x;
		}
		if (Screen.height != currentResolution.y)
		{
			currentResolution.y = Screen.height;
			yScale = currentResolution.y / targetResolution.y;
		}
	}

	private void OnGUI ()
	{
		#region Borders
		{
			// horizontal top.
			GUI.DrawTexture(new Rect( 0, 0, Screen.width, 3*yScale )								, spacerBlack, ScaleMode.StretchToFill);

			// horizontal bottom.
			GUI.DrawTexture(new Rect( 0, Screen.height-3*yScale , Screen.width, 3*yScale )		, spacerBlack, ScaleMode.StretchToFill);

			// vertical left.
			GUI.DrawTexture(new Rect( 0, 0, 3*xScale, Screen.height )							, spacerBlack, ScaleMode.StretchToFill);

			// vertical right.
			GUI.DrawTexture(new Rect( Screen.width - 3*xScale, 0, 3*xScale, Screen.height )		, spacerBlack, ScaleMode.StretchToFill);

			// 62.5% vertical.
			GUI.DrawTexture(new Rect( Screen.width - 480*xScale, 0, 3*xScale, Screen.height )	, spacerBlack, ScaleMode.StretchToFill);

			// 89.6875% vertical.
			GUI.DrawTexture(new Rect( Screen.width - 132*xScale, 0, 3*xScale, Screen.height )	, spacerBlack, ScaleMode.StretchToFill);

			// 25% horizontal.
			GUI.DrawTexture(new Rect( Screen.width - 480*xScale, 180*yScale, Screen.width, 3*yScale ), spacerBlack, ScaleMode.StretchToFill);

			// 50% horizontal.
			GUI.DrawTexture(new Rect( Screen.width - 480*xScale, 359*yScale, Screen.width, 3*yScale ), spacerBlack, ScaleMode.StretchToFill);

			// 75% horizontal.
			GUI.DrawTexture(new Rect( Screen.width - 480*xScale, 538*yScale, Screen.width, 3*yScale ), spacerBlack, ScaleMode.StretchToFill);
		}
		#endregion

		#region Buttons
		{
			GUILayout.BeginArea(new Rect
			(
				Screen.width - 480*xScale + 3*xScale,
				0+3*yScale,
				Screen.width - 132*xScale - 3*xScale,
				180*yScale - 3*yScale
			));
			GUI.DrawTexture(new Rect( 1*xScale, 1*yScale, boxWidth*xScale, boxHeight*yScale ), spacerGray, ScaleMode.StretchToFill);
			GUI.DrawTexture(new Rect( boxWidth*xScale+2*xScale, 1*yScale, boxWidth*xScale, boxHeight*yScale ), spacerGray, ScaleMode.StretchToFill);
			GUI.DrawTexture(new Rect( 2*boxWidth*xScale+3*xScale, 1*yScale, boxWidth*xScale, boxHeight*yScale ), spacerGray, ScaleMode.StretchToFill);
			GUI.DrawTexture(new Rect( 3*boxWidth*xScale+4*xScale, 1*yScale, boxWidth*xScale, boxHeight*yScale ), spacerGray, ScaleMode.StretchToFill);
			GUI.DrawTexture(new Rect( 4*boxWidth*xScale+5*xScale, 1*yScale, boxWidth*xScale, boxHeight*yScale ), spacerGray, ScaleMode.StretchToFill);

			GUI.DrawTexture(new Rect( 1*xScale, boxHeight*yScale+2*yScale, boxWidth*xScale, boxHeight*yScale ), spacerGray, ScaleMode.StretchToFill);
			GUI.DrawTexture(new Rect( boxWidth*xScale+2*xScale, boxHeight*yScale+2*yScale, boxWidth*xScale, boxHeight*yScale ), spacerGray, ScaleMode.StretchToFill);
			GUI.DrawTexture(new Rect( 2*boxWidth*xScale+3*xScale, boxHeight*yScale+2*yScale, boxWidth*xScale, boxHeight*yScale ), spacerGray, ScaleMode.StretchToFill);
			GUI.DrawTexture(new Rect( 3*boxWidth*xScale+4*xScale, boxHeight*yScale+2*yScale, boxWidth*xScale, boxHeight*yScale ), spacerGray, ScaleMode.StretchToFill);
			GUI.DrawTexture(new Rect( 4*boxWidth*xScale+5*xScale, boxHeight*yScale+2*yScale, boxWidth*xScale, boxHeight*yScale ), spacerGray, ScaleMode.StretchToFill);

			GUI.DrawTexture(new Rect( 0, (boxHeight*yScale)*2+3*yScale, (boxWidth*xScale)*5+6*xScale, 3*yScale), spacerBlack, ScaleMode.StretchToFill);

			GUILayout.EndArea();

			GUILayout.BeginArea(new Rect
			                    (
				Screen.width - 480*xScale + 3*xScale,
				180*yScale+3*yScale,
				Screen.width - 132*xScale - 3*xScale,
				180*yScale - 3*yScale
				));
			
			GUI.DrawTexture(new Rect( 1*xScale, 1*yScale, boxWidth*xScale, boxHeight*yScale ), spacerGray, ScaleMode.StretchToFill);
			GUI.DrawTexture(new Rect( boxWidth*xScale+2*xScale, 1*yScale, boxWidth*xScale, boxHeight*yScale ), spacerGray, ScaleMode.StretchToFill);
			GUI.DrawTexture(new Rect( 2*boxWidth*xScale+3*xScale, 1*yScale, boxWidth*xScale, boxHeight*yScale ), spacerGray, ScaleMode.StretchToFill);
			GUI.DrawTexture(new Rect( 3*boxWidth*xScale+4*xScale, 1*yScale, boxWidth*xScale, boxHeight*yScale ), spacerGray, ScaleMode.StretchToFill);
			GUI.DrawTexture(new Rect( 4*boxWidth*xScale+5*xScale, 1*yScale, boxWidth*xScale, boxHeight*yScale ), spacerGray, ScaleMode.StretchToFill);
			
			GUI.DrawTexture(new Rect( 1*xScale, boxHeight*yScale+2*yScale, boxWidth*xScale, boxHeight*yScale ), spacerGray, ScaleMode.StretchToFill);
			GUI.DrawTexture(new Rect( boxWidth*xScale+2*xScale, boxHeight*yScale+2*yScale, boxWidth*xScale, boxHeight*yScale ), spacerGray, ScaleMode.StretchToFill);
			GUI.DrawTexture(new Rect( 2*boxWidth*xScale+3*xScale, boxHeight*yScale+2*yScale, boxWidth*xScale, boxHeight*yScale ), spacerGray, ScaleMode.StretchToFill);
			GUI.DrawTexture(new Rect( 3*boxWidth*xScale+4*xScale, boxHeight*yScale+2*yScale, boxWidth*xScale, boxHeight*yScale ), spacerGray, ScaleMode.StretchToFill);
			GUI.DrawTexture(new Rect( 4*boxWidth*xScale+5*xScale, boxHeight*yScale+2*yScale, boxWidth*xScale, boxHeight*yScale ), spacerGray, ScaleMode.StretchToFill);
			
			GUI.DrawTexture(new Rect( 0, (boxHeight*yScale)*2+3*yScale, (boxWidth*xScale)*5+6*xScale, 3*yScale), spacerBlack, ScaleMode.StretchToFill);
			
			GUILayout.EndArea();
			
			GUILayout.BeginArea(new Rect
			                    (
				Screen.width - 480*xScale + 3*xScale,
				359*yScale+3*yScale,
				Screen.width - 132*xScale - 3*xScale,
				180*yScale - 3*yScale
				));
			
			GUI.DrawTexture(new Rect( 1*xScale, 1*yScale, boxWidth*xScale, boxHeight*yScale ), spacerGray, ScaleMode.StretchToFill);
			GUI.DrawTexture(new Rect( boxWidth*xScale+2*xScale, 1*yScale, boxWidth*xScale, boxHeight*yScale ), spacerGray, ScaleMode.StretchToFill);
			GUI.DrawTexture(new Rect( 2*boxWidth*xScale+3*xScale, 1*yScale, boxWidth*xScale, boxHeight*yScale ), spacerGray, ScaleMode.StretchToFill);
			GUI.DrawTexture(new Rect( 3*boxWidth*xScale+4*xScale, 1*yScale, boxWidth*xScale, boxHeight*yScale ), spacerGray, ScaleMode.StretchToFill);
			GUI.DrawTexture(new Rect( 4*boxWidth*xScale+5*xScale, 1*yScale, boxWidth*xScale, boxHeight*yScale ), spacerGray, ScaleMode.StretchToFill);
			
			GUI.DrawTexture(new Rect( 1*xScale, boxHeight*yScale+2*yScale, boxWidth*xScale, boxHeight*yScale ), spacerGray, ScaleMode.StretchToFill);
			GUI.DrawTexture(new Rect( boxWidth*xScale+2*xScale, boxHeight*yScale+2*yScale, boxWidth*xScale, boxHeight*yScale ), spacerGray, ScaleMode.StretchToFill);
			GUI.DrawTexture(new Rect( 2*boxWidth*xScale+3*xScale, boxHeight*yScale+2*yScale, boxWidth*xScale, boxHeight*yScale ), spacerGray, ScaleMode.StretchToFill);
			GUI.DrawTexture(new Rect( 3*boxWidth*xScale+4*xScale, boxHeight*yScale+2*yScale, boxWidth*xScale, boxHeight*yScale ), spacerGray, ScaleMode.StretchToFill);
			GUI.DrawTexture(new Rect( 4*boxWidth*xScale+5*xScale, boxHeight*yScale+2*yScale, boxWidth*xScale, boxHeight*yScale ), spacerGray, ScaleMode.StretchToFill);
			
			GUI.DrawTexture(new Rect( 0, (boxHeight*yScale)*2+3*yScale, (boxWidth*xScale)*5+6*xScale, 3*yScale), spacerBlack, ScaleMode.StretchToFill);
			
			GUILayout.EndArea();
			
			GUILayout.BeginArea(new Rect
			                    (
				Screen.width - 480*xScale + 3*xScale,
				538*yScale+3*yScale,
				Screen.width - 132*xScale - 3*xScale,
				180*yScale - 3*yScale
				));
			
			GUI.DrawTexture(new Rect( 1*xScale, 1*yScale, boxWidth*xScale, boxHeight*yScale ), spacerGray, ScaleMode.StretchToFill);
			GUI.DrawTexture(new Rect( boxWidth*xScale+2*xScale, 1*yScale, boxWidth*xScale, boxHeight*yScale ), spacerGray, ScaleMode.StretchToFill);
			GUI.DrawTexture(new Rect( 2*boxWidth*xScale+3*xScale, 1*yScale, boxWidth*xScale, boxHeight*yScale ), spacerGray, ScaleMode.StretchToFill);
			GUI.DrawTexture(new Rect( 3*boxWidth*xScale+4*xScale, 1*yScale, boxWidth*xScale, boxHeight*yScale ), spacerGray, ScaleMode.StretchToFill);
			GUI.DrawTexture(new Rect( 4*boxWidth*xScale+5*xScale, 1*yScale, boxWidth*xScale, boxHeight*yScale ), spacerGray, ScaleMode.StretchToFill);
			
			GUI.DrawTexture(new Rect( 1*xScale, boxHeight*yScale+2*yScale, boxWidth*xScale, boxHeight*yScale ), spacerGray, ScaleMode.StretchToFill);
			GUI.DrawTexture(new Rect( boxWidth*xScale+2*xScale, boxHeight*yScale+2*yScale, boxWidth*xScale, boxHeight*yScale ), spacerGray, ScaleMode.StretchToFill);
			GUI.DrawTexture(new Rect( 2*boxWidth*xScale+3*xScale, boxHeight*yScale+2*yScale, boxWidth*xScale, boxHeight*yScale ), spacerGray, ScaleMode.StretchToFill);
			GUI.DrawTexture(new Rect( 3*boxWidth*xScale+4*xScale, boxHeight*yScale+2*yScale, boxWidth*xScale, boxHeight*yScale ), spacerGray, ScaleMode.StretchToFill);
			GUI.DrawTexture(new Rect( 4*boxWidth*xScale+5*xScale, boxHeight*yScale+2*yScale, boxWidth*xScale, boxHeight*yScale ), spacerGray, ScaleMode.StretchToFill);
			
			GUI.DrawTexture(new Rect( 0, (boxHeight*yScale)*2+3*yScale, (boxWidth*xScale)*5+6*xScale, 3*yScale), spacerBlack, ScaleMode.StretchToFill);
			
			GUILayout.EndArea();
		}
		#endregion

	}

}