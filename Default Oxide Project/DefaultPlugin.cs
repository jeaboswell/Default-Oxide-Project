/// <summary>
/// Author: S0N_0F_BISCUIT
/// Permissions:
///		
///	Chat Commands:
///		
/// </summary>
using Newtonsoft.Json;
using Oxide.Core;
using Oxide.Core.Libraries.Covalence;
using Oxide.Core.Plugins;
using Rust;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Oxide.Plugins
{
	[Info("DefaultPlugin", "S0N_0F_BISCUIT", "1.0.0")]
	[Description("Plugin template")]
	class DefaultPlugin : RustPlugin
	{
		#region Variables
		class ConfigData
		{
			[JsonProperty(PropertyName = "Default Variable")]
			public int variable = 0;
		}
		/// <summary>
		/// Data saved by the plugin
		/// </summary>
		class StoredData
		{
			public int variable = 0;
		}
		
		private ConfigData config = new ConfigData();
		private StoredData data;
		#endregion

		#region Config Handling
		/// <summary>
		/// Load default config file
		/// </summary>
		protected override void LoadDefaultConfig()
		{
			config = new ConfigData();
			Config.WriteObject(config, true, Title);
		}
		/// <summary>
		/// Load the config values to the config class
		/// </summary>
		private void LoadConfigData()
		{
			config = Config.ReadObject<ConfigData>(Title);
		}
		#endregion

		#region Data Handling
		/// <summary>
		/// Load plugin data
		/// </summary>
		private void LoadData()
		{
			try
			{
				data = Interface.Oxide.DataFileSystem.ReadObject<StoredData>(Title);
			}
			catch
			{
				data = new StoredData();
				SaveData();
			}
		}
		/// <summary>
		/// Save StoredData
		/// </summary>
		private void SaveData()
		{
			Interface.Oxide.DataFileSystem.WriteObject(Title, data);
		}
		/// <summary>
		/// Clear StoredData
		/// </summary>
		private void ClearData()
		{
			data = new StoredData();
			SaveData();
		}
		#endregion

		#region Localization
		/// <summary>
		/// Load messages relayed to player
		/// </summary>
		private new void LoadDefaultMessages()
		{
			// English
			lang.RegisterMessages(new Dictionary<string, string>
			{
				["DefaultMessage"] = "Your message here"
			}, this);
		}
		#endregion

		#region Initialization
		/// <summary>
		/// Plugin initialization
		/// </summary>
		private void Init()
		{
			// Permissions
			
			// Configuration
			try
			{
				LoadConfigData();
			}
			catch
			{
				LoadDefaultConfig();
				LoadConfigData();
			}
			// Data
			LoadData();
		}
		/// <summary>
		/// Unloading Plugin
		/// </summary>
		void Unload()
		{
			SaveData();
		}
		#endregion

		#region Helpers
		/// <summary>
		/// Get string and format from lang file
		/// </summary>
		/// <param name="key"></param>
		/// <param name="userId"></param>
		/// <param name="args"></param>
		/// <returns></returns>
		private string Lang(string key, string userId = null, params object[] args) => string.Format(lang.GetMessage(key, this, userId), args);
		#endregion
	}
}