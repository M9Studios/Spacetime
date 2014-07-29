public class Item : IItem {
	
	private short _itemID;
	private string _name;
	private string _description;
	private byte _maxStack;
	
	public Item (short id, string s1, string s2, byte b1)
	{
		itemID = id;
		name = s1;
		description = s2;
		maxStack = b1;
	}
	
	public short itemID
	{
		get
		{
			return _itemID;
		}
		set
		{
			_itemID = value;
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