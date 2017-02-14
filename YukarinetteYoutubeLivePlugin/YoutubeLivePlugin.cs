using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
		}

		public override void Closed()
		{
		}

		public override void Setting()
		{
		}

		public override void SpeechRecognitionStart()
		{
		}

		public override void SpeechRecognitionStop()
		{
		}

		public override void Speech(string text)
		{
		}
	}
}
