using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogForDevelopers.Repository.Tests
{
	public class BaseTest
	{
		protected static string StorageXmlPath = string.Concat(ConfigurationManager.AppSettings.Get("ServerMapPath"), ConfigurationManager.AppSettings.Get("StorageXml"));
		protected static string StorageXmlPathFacked = string.Concat(ConfigurationManager.AppSettings.Get("ServerMapPath"), "Faked\\", ConfigurationManager.AppSettings.Get("StorageXml"));

		protected static string ServerMapPath = ConfigurationManager.AppSettings.Get("ServerMapPath");
		protected static string ServerMapPathFacked = string.Concat(ConfigurationManager.AppSettings.Get("ServerMapPath"), "Faked\\");

		[TestCleanup]
		public void Cleanup()
		{
			foreach (var item in System.IO.Directory.GetFiles(BaseTest.StorageXmlPath))
			{
				System.IO.File.Delete(item);
			}
		}
	}
}
