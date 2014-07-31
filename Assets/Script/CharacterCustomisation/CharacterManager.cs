using UnityEngine;
using System;
using System.IO;
using System.Collections;

public class CharacterManager : MonoBehaviour {

	private bool saveCharacter = false;
	private bool loadCharacter = false;
	private bool randomiseAll = true;

	public string loadedCharacterName = "";
	public string characterName = "";

	public Color[] loaderCharacterColours = new Color[5];
	public Color[] characterColours = new Color[5];

	private string dataLocation = "";

	private BinaryWriter bw;
	private BinaryReader br;

	private void Start ()
	{
		dataLocation = Application.persistentDataPath;
		Debug.Log("Saving to and loading from: " + dataLocation);
		dataLocation += "/Characters";
		Debug.Log("Saving to and loading from: " + dataLocation);
	}

	private void Update ()
	{
		if (saveCharacter)
		{
			SaveCharacter();
		}
		if (loadCharacter)
		{
			LoadCharacter();
		}
		if (randomiseAll)
		{
			RandomiseAll();
		}
	}
	
	private void OnGUI ()
	{
		/*
		if (GUI.Button(new Rect(Screen.width-110, Screen.height-30, 100, 20), "Save Character"))
		{
			saveCharacter = true;
		}
		if (GUI.Button(new Rect(Screen.width-110, Screen.height-60, 100, 20), "Load Character"))
		{
			loadCharacter = true;
		}
		*/
		if (GUI.Button(new Rect(Screen.width-110, Screen.height-90, 100, 20), "Randomise All"))
		{
			randomiseAll = true;
		}

		//loadedCharacterName = GUI.TextField(new Rect(Screen.width-370, Screen.height-60, 250, 20), loadedCharacterName, 32);
		//characterName = GUI.TextField(new Rect(Screen.width-370, Screen.height-30, 250, 20), characterName, 32);
	}

	private void SaveCharacter ()
	{
		if (characterName != "")
		{
			int counter = 0;

			foreach (Transform child in transform)
			{
				if (child.GetComponent<ColourPicker>())
				{
					characterColours[counter] = child.GetComponent<ColourPicker>().RGB;
					counter++;
				}
			}

			float[] RGBValues = new float[15];

			RGBValues[0] = characterColours[0].r; RGBValues[1] = characterColours[0].g; RGBValues[2] = characterColours[0].b;
			RGBValues[3] = characterColours[1].r; RGBValues[4] = characterColours[1].g; RGBValues[5] = characterColours[1].b;
			RGBValues[6] = characterColours[2].r; RGBValues[7] = characterColours[2].g; RGBValues[8] = characterColours[2].b;
			RGBValues[9] = characterColours[3].r; RGBValues[10] = characterColours[3].g; RGBValues[11] = characterColours[3].b;
			RGBValues[12] = characterColours[4].r; RGBValues[13] = characterColours[4].g; RGBValues[14] = characterColours[4].b;

			if (!Directory.Exists(@dataLocation))
			{
				Directory.CreateDirectory(@dataLocation);
			}
			using (StreamWriter sw = new StreamWriter(@dataLocation + "\\" + characterName))
			{
				Debug.Log("Writing character " + characterName + " to " + dataLocation + "/" + characterName);
				for (int i = 0; i < 15; i++)
				{
					sw.WriteLine(RGBValues[i].ToString());
				}
			}
		}

		saveCharacter = false;
	}

	private void LoadCharacter ()
	{
		if (loadedCharacterName != "")
		{
			using (StreamReader sr = new StreamReader(@dataLocation + "\\" + loadedCharacterName))
			{
				Debug.Log("Loading character " + loadedCharacterName + " from " + dataLocation + "/" + characterName);
				for (int i = 0; i < 5; i++)
				{
					loaderCharacterColours[i].r = float.Parse(sr.ReadLine());
					loaderCharacterColours[i].g = float.Parse(sr.ReadLine());
					loaderCharacterColours[i].b = float.Parse(sr.ReadLine());
				}
			}

			ApplyCharacter();
		}
		loadCharacter = false;
	}

	private void ApplyCharacter ()
	{
		int i = 0;

		foreach (Transform child in transform)
		{
			if (child.GetComponent<ColourPicker>())
			{
				child.GetComponent<ColourPicker>().Fr = loaderCharacterColours[i].r;
				child.GetComponent<ColourPicker>().Fg = loaderCharacterColours[i].g;
				child.GetComponent<ColourPicker>().Fb = loaderCharacterColours[i].b;
				i++;
			}
		}
	}
	
	private void RandomiseAll ()
	{
		foreach (Transform child in transform)
		{
			if (child.GetComponent<ColourPicker>())
			{
				child.GetComponent<ColourPicker>().Randomize = true;
			}
		}
		
		randomiseAll = false;
	}

}