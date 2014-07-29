using UnityEngine;
using System.Collections;

public class WorldBuilder : MonoBehaviour {
	
	public GameObject[] go_blocks = new GameObject[byte.MaxValue];
	public GameObject go_worldObject;

	private GameObject[,] go_objectBlock = new GameObject[WorldInformation.MAP_WIDTH, WorldInformation.MAP_HEIGHT];
	private byte xAdj = 40;
	private byte yAdj = 25;
	private int xx = 0, yy = 0, lx = 0, ly = 0;
	private byte[,] LocalBlock = new byte[WorldInformation.MAP_WIDTH, WorldInformation.MAP_HEIGHT];
	private bool worldInit = false;
	private bool localInit = false;
	
	void Start ()
	{
		lx = xx = Mathf.CeilToInt(transform.position.x);
		ly = yy = Mathf.CeilToInt(transform.position.y);
		InvokeRepeating("MapSpawner", 0.1F, 0.024F);
	}
	
	void Update ()
	{
		xx = Mathf.CeilToInt(transform.position.x);
		yy = Mathf.CeilToInt(transform.position.y);
		if (worldInit && !localInit) SpawnInitArea();
	}
	
	/// Spawns the initial area.
	private void SpawnInitArea ()
	{
		for (int x = xx-xAdj; x < xx+xAdj; x++)
		{
			for (int y = yy-yAdj; y < yy+yAdj; y++)
			{
				if (x > 0 && y > 0)
				{
					if (LocalBlock[x, y] != (byte)BlockID.Air)
					{
						go_objectBlock[x, y] = Instantiate(go_blocks[LocalBlock[x, y]], new Vector2(x, y), go_blocks[LocalBlock[x, y]].transform.rotation) as GameObject;
						go_objectBlock[x, y].transform.parent = go_worldObject.transform;
					}
				}
			}
		}
		localInit = true;
	}
	
	/// Tests the position of the player against previous positions and starts the SpawnBlocks coroutine.
	private void MapSpawner ()
	{
		if (lx != xx || ly != yy)
		{
			if (lx > xx) // x pos decrease.
				StartCoroutine(SpawnBlocks(xx-xAdj, lx-xAdj, yy-yAdj, yy+yAdj));
			if (lx < xx) // x pos increase.
				StartCoroutine(SpawnBlocks(lx+xAdj, xx+xAdj, yy-yAdj, yy+yAdj));
			if (ly > yy) // y pos decrease.
				StartCoroutine(SpawnBlocks(xx-xAdj, xx+xAdj, yy-yAdj, ly-yAdj));
			if (ly < yy) // y pos increase.
				StartCoroutine(SpawnBlocks(xx-xAdj, xx+xAdj, ly+yAdj, yy+yAdj));
			
			lx = xx;
			ly = yy;
		}
	}
	
	/// Instantiates blocks between the bounds of i1 & i2 on the x-axis and i3 & i4 on the y-axis.
	IEnumerator SpawnBlocks (int i1, int i2, int i3, int i4)
	{
		for (int x = i1; x < i2; x++)
		{
			for (int y = i3; y < i4; y++)
			{
				if (x > 0 && y > 0 && x < WorldInformation.MAP_WIDTH && y < WorldInformation.MAP_HEIGHT)
				{
					if (LocalBlock[x, y] != (byte)BlockID.Air && go_objectBlock[x, y] == null)
					{
						go_objectBlock[x, y] = Instantiate(go_blocks[LocalBlock[x, y]], new Vector2(x, y), go_blocks[LocalBlock[x, y]].transform.rotation) as GameObject;
						go_objectBlock[x, y].transform.parent = go_worldObject.transform;
						yield return new WaitForSeconds(float.Epsilon);
					}
				}
			}
		}
	}
	
	public void ChangeBlock (float x, float y, byte b)
	{
		LocalBlock[(int)x, (int)y] = b;
	}
	
	public bool Initialised
	{
		get
		{
			return worldInit;
		}
		set
		{
			worldInit = value;
		}
	}
	
	public byte[,] SetWorld
	{
		get
		{
			return LocalBlock;
		}
		set
		{
			LocalBlock = value;
		}
	}
}