/// <summary>
/// 
/// Assigned an appearance and a name (and a language).
/// 
/// Faction is assigned a stage (on doc).
/// 
/// Stage will affect the starting points of the faction.
/// 
/// Traits will be assigned. 2-3 max.
/// These traits will affect the starting points.
/// 
/// Factions will start at 0 relationship unless xeno perks added.
/// 
/// End points will define the faction.
/// 
/// Every so often in game a point of tech will be added every 2 points will increase other points by 1, aside from starting colonies.
/// 
/// Every point in military industry and expzansion will affect the number of ships spawned.
/// 
/// Starting colonies every point in starting colonies adds one pre-coloniesed planet when the faction is created.
/// 
/// </summary>

public class Faction
{
	private string _name;
	private byte _military;
	private byte _industry;
	private byte _defence;
	private byte _expansion;
	private byte _technology;
	private byte _startingColonies;

	public Faction (string s1, byte b1, byte b2, byte b3, byte b4, byte b5, byte b6)
	{
		name = s1;
		military = b1;
		industry = b2;
		defence = b3;
		expansion = b4;
		technology = b5;
		startingColonies = b6;
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
	
	public byte military
	{
		get
		{
			return _military;
		}
		set
		{
			_military = value;
		}
	}

	public byte industry
	{
		get
		{
			return _industry;
		}
		set
		{
			_industry = value;
		}
	}
	
	public byte defence
	{
		get
		{
			return _defence;
		}
		set
		{
			_defence = value;
		}
	}
	
	public byte expansion
	{
		get
		{
			return _expansion;
		}
		set
		{
			_expansion = value;
		}
	}
	
	public byte technology
	{
		get
		{
			return _technology;
		}
		set
		{
			_technology = value;
		}
	}
	
	public byte startingColonies
	{
		get
		{
			return _startingColonies;
		}
		set
		{
			_startingColonies = value;
		}
	}
}