public struct Light2D
{
	private int _lightID;
	private int _range;
	private float _lightValue;

	public int range
	{
		get
		{
			return _range;
		}
		set
		{
			_range = value;
		}
	}
	
	public int lightID
	{
		get
		{
			return _lightID;
		}
		set
		{
			_lightID = value;
		}
	}

	public float lightValue
	{
		get
		{
			return _lightValue;
		}
		set
		{
			_lightValue = value;
		}
	}

}