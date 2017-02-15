using Codeplex.Data;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using Yukarinette;

namespace YukarinetteYoutubeLivePlugin
{
	public class YoutubeLiveManager
	{
		private string YoutubeVideoId;

		private string YoutubeLiveChatId;

		internal void Create(string youtubeChannelId, string youtubeApiKey)
		{
			if(string.IsNullOrEmpty(youtubeChannelId) || string.IsNullOrEmpty(youtubeApiKey))
			{
				throw new YukarinetteException("YoutubeLive コメント投稿 の設定が行われていません。プラグイン設定を確認してください。", new object[0]);
			}

			YoutubeVideoId = GetYoutubeVideoId(youtubeChannelId);

			YoutubeLiveChatId = GetYoutubeLiveChatId(YoutubeVideoId, youtubeApiKey);
		}

		internal void Dispose()
		{
			throw new NotImplementedException();
		}

		internal void Speech(string text, int youtubeTxDelay)
		{
			throw new NotImplementedException();
		}

		private string GetYoutubeVideoId(string youtubeChannelId)
		{
			var videoIdRequest = WebRequest.Create("https://www.youtube.com/channel/" + youtubeChannelId + "/videos?flow=list&live_view=501&view=2");

			try
			{
				using (var videoIdResponse = videoIdRequest.GetResponse())
				{
					using (var videoIdStream = new StreamReader(videoIdResponse.GetResponseStream(), Encoding.UTF8))
					{
						var videoIdRegex = new Regex("href=\"\\/watch\\?v=(.+?)\"", RegexOptions.IgnoreCase | RegexOptions.Singleline);

						var videoIdMatch = videoIdRegex.Match(videoIdStream.ReadToEnd());

						if (!videoIdMatch.Success)
						{
							throw new YukarinetteException("YoutubeLiveの放送が見つかりませんでした。", new object[0]);
						}

						var index1 = videoIdMatch.Value.LastIndexOf('=') + 1;
						var index2 = videoIdMatch.Value.LastIndexOf('"');

						return videoIdMatch.Value.Substring(index1, index2 - index1);
					}
				}
			}
			catch (Exception message)
			{
				YukarinetteLogger.Instance.Error(message);
				throw new YukarinetteException("YoutubeLiveの放送情報の取得に失敗しました。");
			}
		}

		private string GetYoutubeLiveChatId(string youtubeVideoId, string youtubeApiKey)
		{
			string liveChatId = "";

			var liveChatIdRequest = WebRequest.Create("https://www.googleapis.com/youtube/v3/videos?part=liveStreamingDetails&id=" + youtubeVideoId + "&key=" + youtubeApiKey);

			try
			{
				using (var liveChatIdResponse = liveChatIdRequest.GetResponse())
				{
					using (var liveChatIdStream = new StreamReader(liveChatIdResponse.GetResponseStream(), Encoding.UTF8))
					{
						var liveChatIdObject = DynamicJson.Parse(liveChatIdStream.ReadToEnd());

						liveChatId = liveChatIdObject.items[0].liveStreamingDetails.activeLiveChatId;

						if (string.IsNullOrEmpty(liveChatId))
						{
							throw new YukarinetteException("LiveChatが見つかりませんでした。");
						}
					}
				}
			}
			catch
			{
				throw new YukarinetteException("LiveChatIDの取得に失敗しました。");
			}

			return liveChatId;
		}
	}
}