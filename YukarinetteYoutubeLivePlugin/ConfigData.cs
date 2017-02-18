namespace YukarinetteYoutubeLivePlugin
{
	public class ConfigData
	{
		public string YoutubeChannelId
		{
			get;
			set;
		}

		public string MessagePrefix
		{
			get;
			set;
		}

		public string MessageSuffix
		{
			get;
			set;
		}

		public int TxDelay
		{
			get;
			set;
		}

		public ConfigData()
		{
			YoutubeChannelId = "";
			MessagePrefix = "(音声認識) ";
			MessageSuffix = "";
			TxDelay = 7;
		}
	}
}