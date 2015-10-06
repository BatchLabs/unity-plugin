namespace Batch
{
	public enum IOSNotificationType
	{
		None = 0x0,
		Badge = 0x1,
		Sound = 0x2,
		Alert = 0x4
	}

	public enum AndroidNotificationType
	{
		None = 0x0,
		Sound = 0x1,
		Vibrate = 0x2,
		Lights = 0x4,
		Alert = 0x8
	}
}