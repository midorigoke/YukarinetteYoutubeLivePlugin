namespace YukarinetteYoutubeLivePlugin
{
	public class ConfigData
	{
		public string YoutubeChannelId
		{
			get;
			set;
		}

		public string YoutubeApiKey
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
			YoutubeApiKey = "";
			YoutubeTxDelay = 0;
		}
	}
}