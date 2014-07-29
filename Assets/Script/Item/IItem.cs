public interface IItem {
	
	short itemID
	{
		get;
		set;
	}
	
	string name
	{
		get;
		set;
	}
	
	string description
	{
		get;
		set;
	}
	
	byte maxStack
	{
		get;
		set;
	}

}