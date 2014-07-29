using UnityEngine;
using System.Collections;
using M9Debug;

public class WorldGenerator : MonoBehaviour {

	public GameObject player;

	private short mapWidth = WorldInformation.MAP_WIDTH;
	private short mapHeight = WorldInformation.MAP_HEIGHT;
	private byte trueLimit = 6, falseLimit = 4; // 6, 4
	private float initialChance = 0.5275F; // 0.5275F
	private bool newWorld = false;
	private byte[,] WorldBlock = new byte[WorldInformation.MAP_WIDTH, WorldInformation.MAP_HEIGHT];

	void Start ()
	{
		mapWidth = WorldInformation.MAP_WIDTH;
		mapHeight = WorldInformation.MAP_HEIGHT;
	}

	void Update ()
	{
		if (newWorld == true)
		{
			float t1;
			
			t1 = Time.realtimeSinceStartup;
			SpawnRandom();
			M9Debugger.Log("Stone spawn time: " + (Time.realtimeSinceStartup - t1));
			
			for (int i = 0; i < 20; i++)
			{
				t1 = Time.realtimeSinceStartup;
				SmoothWorld();
				M9Debugger.Log("Iteration " + i + " smooth time: " + (Time.realtimeSinceStartup - t1));
			}
			
			t1 = Time.realtimeSinceStartup;
			SpawnOre();
			M9Debugger.Log("Ore spawn time: " + (Time.realtimeSinceStartup - t1));
			
			t1 = Time.realtimeSinceStartup;
			SpawnDirt();
			M9Debugger.Log("Dirt spawn time: " + (Time.realtimeSinceStartup - t1));

			newWorld = false;

			if (player.GetComponent<WorldBuilder>())
			{
				player.GetComponent<WorldBuilder>().Initialised = true;
				player.GetComponent<WorldBuilder>().SetWorld = WorldBlock;
			}
		}
	}
	
	void OnGUI ()
	{
		if (GUI.Button(new Rect(10, Screen.height-50, 100, 40), "New World"))
		{
			newWorld = true;
		}
	}
	
	/// Spawns dirt around all stone in a world. Done after iterative smoothing.
	private void SpawnDirt ()
	{
		for (int x = 1; x < mapWidth-2; x++)
		{
			for (int y = 1; y < mapHeight-2; y++)
			{
				if (WorldBlock[x, y] == (byte)BlockID.Air)
				{
					if (CheckStone(x, y))
					{
						WorldBlock[x, y] = (byte)BlockID.Dirt;
					}
				}
			}
		}
	}
	
	/// Spawns the ore.
	private void SpawnOre()
	{
		GenerateOre((byte)BlockID.BauxiteOre, 0.00067F);
		GenerateOre((byte)BlockID.MagnetiteOre, 0.00061F);
		GenerateOre((byte)BlockID.CupriteOre, 0.00053F);
		GenerateOre((byte)BlockID.RutileOre, 0.00045F);
		GenerateOre((byte)BlockID.GalenaOre, 0.00039F);
		GenerateOre((byte)BlockID.SilverOre, 0.00030F);
		GenerateOre((byte)BlockID.GoldOre, 0.00020F);
	}
	
	/// Generates a single block of ore at a x,y coordinate.
	private void GenerateOre (byte b, float f)
	{
		//Dan was fighting a Dark Souls boss here. He screamed and thanked God for the drop not killing him.
		for (int x = 1; x < mapWidth-2; x++)
		{
			for (int y = 1; y < mapHeight-2; y++)
			{
				if (WorldBlock[x, y] == (byte)BlockID.Stone)
				{
					if (Random.value < f)
					{
						WorldBlock[x, y] = b;
						SpawnOreVein(b, x, y);
					}
				}
			}
		}
	}
	
	/// Spawns an ore vein at a given block position.
	private void SpawnOreVein (byte b, int i1, int i2)
	{
		byte pad = 2;
		
		if (i1 > pad && i2 > pad && i1 < mapWidth && i2 < mapHeight)
		{
			for (int x = -pad; x < pad; x++)
			{
				for (int y = -pad; y < pad; y++)
				{
					if (Random.value < 0.5F)
					{
						WorldBlock[i1+x, i2+y] = b;
					}
				}
			}
		}
	}
	
	/// Checks if there is a block of stone adjacent to the current block at a given x,y coordinate.
	private bool CheckStone (int x, int y)
	{
		if
		(
			WorldBlock[x-1, y+1] == (byte)BlockID.Stone || WorldBlock[x, y+1] == (byte)BlockID.Stone ||
			WorldBlock[x+1, y+1] == (byte)BlockID.Stone || WorldBlock[x-1, y] == (byte)BlockID.Stone ||
			WorldBlock[x+1, y] == (byte)BlockID.Stone || WorldBlock[x-1, y-1] == (byte)BlockID.Stone ||
			WorldBlock[x, y-1] == (byte)BlockID.Stone || WorldBlock[x+1, y-1] == (byte)BlockID.Stone
		)
			return true;
		else
			return false;
		
	}
		
	/// Run a single iteraion of smoothing on the world.
	private void SmoothWorld ()
	{
		byte[,] returnCave = new byte[mapWidth, mapHeight];
		byte nc;
		
		for (int x = 1; x < mapWidth-2; x++)
		{
			for (int y = 1; y < mapHeight-2; y++)
			{
				nc = CheckNeighbours_(x, y); // Neighbour count.
				
				if (WorldBlock[x, y] == (byte)BlockID.Air)
				{
					if (nc > falseLimit)
					{
						returnCave[x, y] = (byte)BlockID.Air;
					}
					else
					{
						returnCave[x, y] = (byte)BlockID.Stone;
					}
				}
				else
				{
					if (nc < trueLimit)
					{
						returnCave[x, y] = (byte)BlockID.Stone;
					}
					else
					{
						returnCave[x, y] = (byte)BlockID.Air;
					}
				}
			}
		}
		
		WorldBlock = returnCave;
	}
	
	/// Spawn random stone blocks for smoothing.
	private void SpawnRandom ()
	{
		for (int x = 0; x < mapWidth; x++)
		{
			for (int y = 0; y < mapHeight; y++)
			{
				if (Random.value < initialChance)
				{
					WorldBlock[x, y] = (byte)BlockID.Stone;
				}
			}
		}
	}
	
	/// Checks the neighbours of the block at a given location.
	private byte CheckNeighbours_ (int x, int y)
	{
		byte nc = 0;
		
		if (WorldBlock[x+1, y+1] == (byte)BlockID.Stone)
			nc++;
		if (WorldBlock[x, y+1] == (byte)BlockID.Stone)
			nc++;
		if (WorldBlock[x-1, y+1] == (byte)BlockID.Stone)
			nc++;
		if (WorldBlock[x+1, y] == (byte)BlockID.Stone)
			nc++;
		if (WorldBlock[x, y] == (byte)BlockID.Stone)
			nc++;
		if (WorldBlock[x-1, y] == (byte)BlockID.Stone)
			nc++;
		if (WorldBlock[x+1, y-1] == (byte)BlockID.Stone)
			nc++;
		if (WorldBlock[x, y-1] == (byte)BlockID.Stone)
			nc++;
		if (WorldBlock[x-1, y-1] == (byte)BlockID.Stone)
			nc++;
		
		return nc;
	}

	//[Obsolete("CheckNeighbours is deprecated, please use CheckNeighbours_ instead.")]
	private byte CheckNeighbours (byte[,] b, int x, int y)
	{
		byte nc = 0; // Neighbour count.
		int nx, ny;
		
		for (int i = -1; i < 2; i++)
		{
			for (int j = -1; j < 2; j++)
			{
				nx = x+i; // Neighbour x.
				ny = y+j; // Neighbour y.
				
				if (nx < 0 || ny < 0 || nx > mapWidth || ny > mapHeight)
				{
					nc++;
				}

				if (nx < mapWidth && nx > 0 && ny < mapHeight && ny > 0)
				{
					if (b[nx, ny] != (byte)BlockID.Air)
					{
						nc++;
					}
				}
			}
		}
		
		return nc;
	}
	
}