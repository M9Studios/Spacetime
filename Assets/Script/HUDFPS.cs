using UnityEngine;
using System.Collections;

public class HUDFPS : MonoBehaviour 
{
	public float updateInterval = 0.5F;
	
	private float accum = 0;
	private int frames = 0;
	private float timeleft;
	
	void Start()
	{
		if (!guiText)
		{
			Debug.Log("Needs a GUIText component!");
			enabled = false;
			return;
		}

		timeleft = updateInterval;  
	}
	
	void Update()
	{
		timeleft -= Time.deltaTime;
		accum += Time.timeScale/Time.deltaTime;
		++frames;
		
		if (timeleft <= 0.0)
		{
			float fps = accum/frames;
			string format;

			if (!Application.isEditor)
				format = System.String.Format("{0:F2} FPS", fps);
			else
				format = System.String.Format("{0:F2} FPS", fps*3);

			guiText.text = format;
			
			if(fps < 60)
				guiText.material.color = Color.yellow;
			else if(fps < 30)
				guiText.material.color = Color.red;
			else
				guiText.material.color = Color.green;

			timeleft = updateInterval;
			accum = 0.0F;
			frames = 0;
		}
	}
}