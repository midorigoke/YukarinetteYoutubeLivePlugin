using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Yukarinette;

namespace YukarinetteYoutubeLivePlugin
{
	public class YoutubeLiveManager
	{
		private UserCredential Credential;

		private YouTubeService Service;

		private string YoutubeVideoId;

		private string YoutubeLiveChatId;

		internal void Create(string youtubeChannelId)
		{
			if(string.IsNullOrEmpty(youtubeChannelId))
			{
				throw new YukarinetteException("YoutubeLive コメント投稿 の設定が行われていません。プラグイン設定を確認してください。", new object[0]);
			}

			var CredentialTask = GetCredentialAsync();

			YoutubeVideoId = GetYoutubeVideoId(youtubeChannelId);

			CredentialTask.Wait();

			Credential = CredentialTask.Result;

			Service = new YouTubeService(new BaseClientService.Initializer()
			{
				HttpClientInitializer = Credential,
				ApplicationName = "Yukarinette YoutubeLive Plugin"
			});

			YoutubeLiveChatId = GetYoutubeLiveChatId();

			if (string.IsNullOrEmpty(YoutubeLiveChatId))
			{
				throw new YukarinetteException("LiveChatIdが取得できませんでした。");
			}
		}

		internal void Dispose()
		{
			Credential = null;
			YoutubeVideoId = "";
			YoutubeLiveChatId = "";
		}

		internal void Speech(string text, int youtubeTxDelay)
		{
			Thread.Sleep(youtubeTxDelay * 1000);

			SendYoutubeLiveMessage(text);

		}

		private async Task<UserCredential> GetCredentialAsync()
		{
			return await GoogleWebAuthorizationBroker.AuthorizeAsync(
				new ClientSecrets
				{
					ClientId = "630708463403-86in21eag6felde98uf1m045gaabmjgk.apps.googleusercontent.com",
					ClientSecret = "j0Wqw0brW2BMcGP3-d0lNM42"
				},
				new[] { YouTubeService.Scope.Youtube },
				"user",
				CancellationToken.None
			);
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

		private string GetYoutubeLiveChatId()
		{
			var videoListRequest = Service.Videos.List("liveStreamingDetails");

			videoListRequest.Id = YoutubeVideoId;

			var videoListResponse = videoListRequest.Execute();

			return videoListResponse.Items[0].LiveStreamingDetails.ActiveLiveChatId;
		}

		private void SendYoutubeLiveMessage(string messageText)
		{
			var liveChatMessagesRequest = Service.LiveChatMessages.Insert(
				new LiveChatMessage()
				{
					Snippet = new LiveChatMessageSnippet()
					{
						LiveChatId = YoutubeLiveChatId,
						Type = "textMessageEvent",
						TextMessageDetails = new LiveChatTextMessageDetails()
						{
							MessageText = messageText
						}
					}
				},
				"snippet"
			);

			liveChatMessagesRequest.Execute();
		}
	}
}