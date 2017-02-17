using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using Yukarinette;

namespace YukarinetteYoutubeLivePlugin
{
	public partial class OptionWindow : Window, IComponentConnector
	{
		public static void Show(ConfigManager manager, string pluginName)
		{
			var optionWindow = new OptionWindow(manager);

			YukarinetteLogger.Instance.Info("dialog open.");

			if (optionWindow.ShowDialog().Value)
			{
				YukarinetteLogger.Instance.Info("dialog ok.");
				optionWindow.Save(manager, pluginName);
			}
		}

		private OptionWindow(ConfigManager manager)
		{
			InitializeComponent();

			Owner = Application.Current.MainWindow;

			YoutubeChannelIdTextBox.Text = manager.Data.YoutubeChannelId;
			MessagePrefixTextBox.Text = manager.Data.MessagePrefix;
			MessageSuffixTextBox.Text = manager.Data.MessageSuffix;
			TxDelayTextBox.Text = manager.Data.TxDelay.ToString();
		}

		private void Save(ConfigManager manager, string pluginName)
		{
			manager.Data.YoutubeChannelId = YoutubeChannelIdTextBox.Text;
			manager.Data.MessagePrefix = MessagePrefixTextBox.Text;
			manager.Data.MessageSuffix = MessageSuffixTextBox.Text;

			try
			{
				manager.Data.TxDelay = int.Parse(TxDelayTextBox.Text);
			}
			catch
			{
				manager.Data.TxDelay = 7;
			}

			manager.Save(pluginName);
		}

		private void DeleteTokenCacheButton_Click(object sender, RoutedEventArgs e)
		{
			File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Google.Apis.Auth\Google.Apis.Auth.OAuth2.Responses.TokenResponse-user");

			MessageBox.Show("トークンキャッシュを消去しました。");
		}


		private void OkButton_Click(object sender, RoutedEventArgs e)
		{
			DialogResult = new bool?(true);
		}
	}
}
