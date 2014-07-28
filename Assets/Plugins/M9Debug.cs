namespace M9Debug
{
	using UnityEngine;
	using System;

	public class M9Debugger : MonoBehaviour
	{
		public static bool DEBUG = true;
		public static string FILE_LOCATION = "";
		
		public static void Log (object o)
		{
			if (DEBUG && o != null)
			{
				Debug.Log(o);

				if (!Application.isEditor)
				{
					FILE_LOCATION = Application.persistentDataPath + "/debug.log";
					System.IO.StreamWriter sw = new System.IO.StreamWriter(@FILE_LOCATION, true);
					sw.WriteLine(o.ToString());
					sw.Close();
				}
			}
		}

		public static void LogError (object o)
		{
			if (DEBUG && o != null)
			{
				Debug.LogError(o);

				if (!Application.isEditor)
				{
					FILE_LOCATION = Application.persistentDataPath + "/debug.log";
					System.IO.StreamWriter sw = new System.IO.StreamWriter(@FILE_LOCATION, true);
					sw.WriteLine(o.ToString());
					sw.Close();
				}
			}
		}
		
		public static void LogWarning (object o)
		{
			if (DEBUG && o != null)
			{
				Debug.LogWarning(o);

				if (!Application.isEditor)
				{
					FILE_LOCATION = Application.persistentDataPath + "/debug.log";
					System.IO.StreamWriter sw = new System.IO.StreamWriter(@FILE_LOCATION, true);
					sw.WriteLine(o.ToString());
					sw.Close();
				}
			}
		}

		public static void LogException (System.Exception e)
		{
			if (DEBUG && e != null)
			{
				Debug.LogException(e);

				if (!Application.isEditor)
				{
					FILE_LOCATION = Application.persistentDataPath + "/debug.log";
					System.IO.StreamWriter sw = new System.IO.StreamWriter(@FILE_LOCATION, true);
					sw.WriteLine(e);
					sw.Close();
				}
			}
		}
	}
}