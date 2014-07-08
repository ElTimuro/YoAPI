using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Xml;

namespace YoClient
{
    public class ConfigLoader
    {
        /// <summary>
        /// Gets a setting from the local config file.
        /// </summary>
        /// <param name="settingKey">The name of the Setting</param>
        /// <returns>The setting with the key settingkey</returns>
        public static string GetSetting(string settingKey)
        {
            var valueToGet = string.Empty;
            var reader = XmlReader.Create("App.Config");
            reader.MoveToContent();

            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element && reader.Name == "add")
                {
                    if (reader.HasAttributes)
                    {
                        valueToGet = reader.GetAttribute("key");
                        if (!string.IsNullOrEmpty(valueToGet) && valueToGet == settingKey)
                        {
                            valueToGet = reader.GetAttribute("value");
                            return valueToGet;
                        }
                    }
                }
            }

            return valueToGet;
        }
    }
}
