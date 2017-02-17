namespace YukarinetteYoutubeLivePlugin
{
	public class ConfigData
	{
		public string YoutubeChannelId
		{
			get;
			set;
		}

		public int YoutubeTxDelay
		{
			get;
			set;
		}

		public ConfigData()
		{
			YoutubeChannelId = "";
			YoutubeTxDelay = 0;
		}
	}
}