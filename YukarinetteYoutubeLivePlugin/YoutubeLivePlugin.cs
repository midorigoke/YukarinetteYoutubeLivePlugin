using Yukarinette;

namespace YukarinetteYoutubeLivePlugin
{
	public class YoutubeLivePlugin : IYukarinetteInterface
    {
		private ConfigManager mConfigManager;

		private YoutubeLiveManager mYoutubeLiveManager;

		public override string Name
		{
			get
			{
				return "YoutubeLive コメント投稿";
			}
		}

		public override void Loaded()
		{
			mConfigManager = new ConfigManager();
			mConfigManager.Load(Name);
			mYoutubeLiveManager = new YoutubeLiveManager();
		}

		public override void Closed()
		{
			mConfigManager.Save(Name);
		}

		public override void Setting()
		{
			OptionWindow.Show(mConfigManager, Name);
		}

		public override void SpeechRecognitionStart()
		{
			mYoutubeLiveManager.Create(mConfigManager.Data.YoutubeChannelId, mConfigManager.Data.YoutubeApiKey);
		}

		public override void SpeechRecognitionStop()
		{
			mYoutubeLiveManager.Dispose();
		}

		public override void Speech(string text)
		{
			mYoutubeLiveManager.Speech(text, mConfigManager.Data.YoutubeTxDelay);
		}
	}
}
