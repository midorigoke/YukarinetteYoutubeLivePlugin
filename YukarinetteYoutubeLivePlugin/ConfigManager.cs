using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;
using Yukarinette;

namespace YukarinetteYoutubeLivePlugin
{
	public class ConfigManager
	{
		public ConfigData Data
		{
			get;
			protected set;
		}

		private string SettingPath
		{
			get;
			set;
		}

		public ConfigManager()
		{
			Data = null;

			var fileName = Path.GetFileName(Assembly.GetExecutingAssembly().Location);
			var path = Path.Combine(YukarinetteCommon.AppSettingFolder, "plugins");

			SettingPath = Path.Combine(path, fileName + ".config");
		}

		public void Load(string pluginName)
		{
			if (!File.Exists(SettingPath))
			{
				YukarinetteLogger.Instance.Info("setting file not found. SettingPath=" + SettingPath);

				CreateNewSetting();

				return;
			}

			try
			{
				using (var fileStream = new FileStream(SettingPath, FileMode.Open))
				{
					using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
					{
						var xmlSerializer = new XmlSerializer(typeof(ConfigData));

						Data = (ConfigData)xmlSerializer.Deserialize(streamReader);
					}
				}

				YukarinetteLogger.Instance.Info("setting load ok. SettingPath=" + SettingPath);
			}
			catch(Exception message)
			{
				YukarinetteLogger.Instance.Error(message);

				CreateNewSetting();

				YukarinetteConsoleMessage.Instance.WriteMessage(pluginName + " の設定ファイルが読み取れませんでした。初期値で動作します。");
			}
		}

		public void Save(string pluginName)
		{
			if(Data == null)
			{
				YukarinetteLogger.Instance.Info("save data is null.");

				return;
			}

			try
			{
				if (!Directory.Exists(Path.GetDirectoryName(SettingPath)))
				{
					Directory.CreateDirectory(Path.GetDirectoryName(SettingPath));
				}

				using (var fileStream = new FileStream(SettingPath, FileMode.Create))
				{
					using (var StreamWriter = new StreamWriter(fileStream, Encoding.UTF8))
					{
						new XmlSerializer(typeof(ConfigData)).Serialize(StreamWriter, Data);
					}
				}

				YukarinetteLogger.Instance.Info("setting save ok. SettingPath=" + this.SettingPath);
			}
			catch(Exception message)
			{
				YukarinetteLogger.Instance.Error(message);
				YukarinetteConsoleMessage.Instance.WriteMessage(pluginName + " の設定ファイルの保存に失敗しました。");
			}
		}

		public void CreateNewSetting()
		{
			Data = new ConfigData();
		}
	}
}