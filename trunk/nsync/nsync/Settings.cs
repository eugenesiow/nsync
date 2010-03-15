﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Xml;
using System.IO;
using System.Security.AccessControl;

namespace nsync
{
    public sealed class Settings
    {
        private string settingsFile = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + nsync.Properties.Resources.settingsFilePath;
        private string NULL_STRING = nsync.Properties.Resources.nullString;

        private const int NUMBER_OF_MOST_RECENT = 5;
        private const string PATH_SETTINGS = "/nsync/SETTINGS";
        private const string PATH_MRU = "/nsync/MRU";

        private static readonly Settings instance = new Settings();

        private Settings() {}

        public static Settings Instance
        {
            get
            {
                return instance;
            }
        }

        public void SetHelperWindowStatus(bool status)
        {
            if (!File.Exists(settingsFile))
            {
                CreateNewSettingsXML();
            }

            XmlDocument doc = new XmlDocument();
            XmlNode helperWindowStatusNode = SelectNode(doc, PATH_SETTINGS+"/HelperWindowIsOn");

            helperWindowStatusNode.InnerText = "" + status;

            doc.Save(settingsFile);

        }

        public bool GetHelperWindowStatus()
        {
            if (!File.Exists(settingsFile))
            {
                CreateNewSettingsXML();
            }

            XmlDocument doc = new XmlDocument();
            XmlNode helperWindowStatusNode = SelectNode(doc, PATH_SETTINGS+"/HelperWindowIsOn");

            return Boolean.Parse(helperWindowStatusNode.InnerText);
        }

        public List<string> LoadFolderPaths()
        {
            List<string> results = new List<string>();
            if (File.Exists(settingsFile))
            {
                XmlDocument doc = new XmlDocument();
                //Load MRU Information
                XmlNode mruNode = SelectNode(doc, PATH_MRU);

                for (int i = 1; i <= 5; i++)
                {
                    results.Add(mruNode["left" + i.ToString()].InnerText);
                    results.Add(mruNode["right" + i.ToString()].InnerText);
                }
            }
            return results;
        }

        public void SaveFolderPaths(string leftPath, string rightPath)
        {
            string[] tempStorage = new string[10];

            int counter = 0;

            for (int i = 0; i < 10; i++)
            {
                tempStorage[i] = NULL_STRING;
            }

            if (!File.Exists(settingsFile))
            {
                CreateNewSettingsXML();
            }
                XmlDocument doc = new XmlDocument();
                XmlNode mruNode = SelectNode(doc, PATH_MRU);

                for (int i = 1; i <= NUMBER_OF_MOST_RECENT; i++)
                {
                    tempStorage[counter++] = mruNode["left" + i.ToString()].InnerText;
                    tempStorage[counter++] = mruNode["right" + i.ToString()].InnerText;
                }

                mruNode["left1"].InnerText = leftPath;
                mruNode["right1"].InnerText = rightPath;

                for (int i = 0; i < 10; i += 2)
                {
                    if (tempStorage[i] == leftPath && tempStorage[i + 1] == rightPath)
                    {
                        tempStorage[i] = tempStorage[i + 1] = "REPLACED";
                        break;
                    }
                }

                counter = 0;
                for (int i = 2; i <= NUMBER_OF_MOST_RECENT; i++)
                {
                    while (tempStorage[counter] == "REPLACED" && tempStorage[counter + 1] == "REPLACED")
                        counter += 2;

                    mruNode["left" + i.ToString()].InnerText = tempStorage[counter];
                    mruNode["right" + i.ToString()].InnerText = tempStorage[counter + 1];

                    counter += 2;
                }

                doc.Save(settingsFile);
        }

        private XmlNode SelectNode(XmlDocument doc, string path)
        {
            try
            {
                doc.Load(settingsFile);
            }
            catch (XmlException)
            {
                File.Delete(settingsFile);
                CreateNewSettingsXML();
                doc.Load(settingsFile);
            }

            XmlElement root = doc.DocumentElement;
            
            if (!CheckSettingsXML(doc))
            {
                File.Delete(settingsFile);
                CreateNewSettingsXML();
                doc.Load(settingsFile);
                root = doc.DocumentElement;
            }

            File.SetAttributes(settingsFile, FileAttributes.Normal);

            XmlNode node = root.SelectSingleNode(path);
            return node;
        }

        private bool CheckSettingsXML(XmlDocument doc)
        {
            if (null == doc.SelectSingleNode(PATH_SETTINGS+"/HelperWindowIsOn"))
                return false;

            for (int i = 1; i <= NUMBER_OF_MOST_RECENT; i++)
            {
                if (null == doc.SelectSingleNode(PATH_MRU+"/left" + i.ToString()))
                    return false;
                if (null == doc.SelectSingleNode(PATH_MRU+"/right" + i.ToString()))
                    return false;
            }
            
            return true;
        }

        private void CreateNewSettingsXML()
        {
            XmlTextWriter textWriter = new XmlTextWriter(settingsFile, null);
            textWriter.Formatting = Formatting.Indented;
            textWriter.WriteStartDocument();

            //Start Root
            textWriter.WriteStartElement("nsync");

            //Write last opened information
            textWriter.WriteStartElement("MRU");

            for (int i = 1; i <= 5; i++)
            {
                textWriter.WriteStartElement("left" + i.ToString());
                textWriter.WriteString(NULL_STRING);
                textWriter.WriteEndElement();

                textWriter.WriteStartElement("right" + i.ToString());
                textWriter.WriteString(NULL_STRING);
                textWriter.WriteEndElement();
            }

            //End last opened information
            textWriter.WriteEndElement();

            textWriter.WriteStartElement("SETTINGS");

            textWriter.WriteStartElement("HelperWindowIsOn");
            textWriter.WriteString("true");
            textWriter.WriteEndElement();

            textWriter.WriteEndElement();

            //End Root
            textWriter.WriteEndElement();
            textWriter.WriteEndDocument();

            textWriter.Close();
        }
    }
}
