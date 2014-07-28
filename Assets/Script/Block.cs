using UnityEngine;

public class Block : MonoBehaviour, IBlock {
	
	private byte _blockID;
	private string _name;
	private string _description;
	private float _destroyTime;
	private byte _maxStack;
	
	public Block (byte id, string s1, string s2, float f1, byte by1)
	{
		blockID = id;
		name = s1;
		description = s2;
		destroyTime = f1;
		maxStack = by1;
	}
	
	public byte blockID
	{
		get
		{
			return _blockID;
		}
		set
		{
			_blockID = value;
		}
	}
	
	public string name
	{
		get
		{
			return _name;
		}
		set
		{
			_name = value;
		}
	}
	
	public string description
	{
		get
		{
			return _description;
		}
		set
		{
			_description = value;
		}
	}
	
	public float destroyTime
	{
		get
		{
			return _destroyTime;
		}
		set
		{
			_destroyTime = value;
		}
	}
	
	public byte maxStack
	{
		get
		{
			return _maxStack;
		}
		set
		{
			_maxStack = value;
		}
	}
	
}