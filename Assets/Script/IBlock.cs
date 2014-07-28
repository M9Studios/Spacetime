public interface IBlock {
	
	byte blockID
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
	
	float destroyTime
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