﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using System.Xml.XPath;

namespace MarlinComunicationHelper
{
    /// <summary>
    /// Configuration of all storable values
    /// The Config is done in separate XML file 
    /// The file is stored in C:\ProgramData
    /// </summary>
    public class Configuration
    {

        /// <summary>
        /// Set the color used for showing 3D model
        /// </summary>
        public string STLcolor
        {
            get
            {
                var xml = new XmlDocument();
                xml.Load(GetConfigurationFile("Marlin3DprinterToolConfiguration.xml"));
                var xmlNode = (XmlElement) xml.SelectSingleNode("/configuration/STLviewer");
                if (xmlNode == null) return "Blue";
                return xmlNode.GetAttribute("color");
            }
            set
            {
                var xml = new XmlDocument();
                xml.Load(GetConfigurationFile("Marlin3DprinterToolConfiguration.xml"));
                var xmlNode = (XmlElement) xml.SelectSingleNode("/configuration/STLviewer");
                if (xmlNode == null)
                {
                    xmlNode = (XmlElement) CreateMissingXmlNode(xml, xml.DocumentElement, "STLviewer");
                }

                xmlNode?.SetAttribute("color", value);
                xml.Save(GetConfigurationFile("Marlin3DprinterToolConfiguration.xml"));

            }

        }

        private static XmlNode CreateMissingXmlNode(XmlDocument xml, XmlNode parentNode, string tag)
        {
            XmlNode xmlNode = xml.CreateElement(tag);

            parentNode.AppendChild(xmlNode);
            return xmlNode;
            
        }

        //public void SetInitMeshpoint(int x, int y, double z)
        //{
        //    var xml = new XmlDocument();
        //    xml.Load(GetConfigurationFile("Marlin3DprinterToolConfiguration.xml"));

        //    XmlNode meshpointsXmlNode = xml.SelectSingleNode("/configuration/InitMeshPoints");
        //    if (meshpointsXmlNode == null)
        //    {
        //        meshpointsXmlNode = CreateMissingXmlNode(xml, xml.DocumentElement, "InitMeshPoints");
        //    }


        //    bool meshpointFound = false;

        //    XmlNodeList meshpointXmlNodeList = xml.SelectNodes("/configuration/InitMeshPoints/MeshPoint");
            
        //    if (meshpointXmlNodeList != null)
        //    {
        //        foreach (XmlElement meshPointXmlElement in meshpointXmlNodeList)
        //        {
        //            if (
        //                Convert.ToInt16(meshPointXmlElement.Attributes["X"].Value) == x &&
        //                Convert.ToInt16(meshPointXmlElement.Attributes["Y"].Value) == y
        //                )
        //            {
        //                meshpointFound = true;
        //                meshPointXmlElement.SetAttribute("Z", z.ToString().Replace(',', '.'));
        //                break;
        //            }

        //        }

        //    }
        //    if (meshpointFound == false)
        //    {
        //        XmlElement meshpointXmlNode = (XmlElement) CreateMissingXmlNode(xml, meshpointsXmlNode, "MeshPoint");
        //        meshpointXmlNode.SetAttribute("X", x.ToString());
        //        meshpointXmlNode.SetAttribute("Y", y.ToString());
        //        meshpointXmlNode.SetAttribute("Z", z.ToString().Replace(',','.'));
        //    }
                
            
        //    xml.Save(GetConfigurationFile("Marlin3DprinterToolConfiguration.xml"));

        //}


       

        

        /// <summary>
        /// Configure the type of Bed
        /// </summary>
        public string BedType
        {
            get
            {
                var xml = new XmlDocument();
                xml.Load(GetConfigurationFile("Marlin3DprinterToolConfiguration.xml"));
                var xmlNode = (XmlElement) xml.SelectSingleNode("/configuration/bed");
                if (xmlNode == null) return "4point";

                return xmlNode.GetAttribute("bedtype");
            }

            set
            {
                var xml = new XmlDocument();
                xml.Load(GetConfigurationFile("Marlin3DprinterToolConfiguration.xml"));
                var xmlNode = (XmlElement) xml.SelectSingleNode("/configuration/bed");
                if (xmlNode == null)
                {
                    xmlNode = (XmlElement) CreateMissingXmlNode(xml, xml.DocumentElement, "bed");
                }
                // 4point, 3pointLeft, 3pointRight
                xmlNode?.SetAttribute("bedtype", value);
                xml.Save(GetConfigurationFile("Marlin3DprinterToolConfiguration.xml"));
            }
        }

        private static string GetConfigurationFile(string filename)
        {
            string programDataDirectory = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            programDataDirectory = Path.Combine(programDataDirectory, "cabbagecreek");
            programDataDirectory = Path.Combine(programDataDirectory, "Marlin3DprinterTool");
            return Path.Combine(programDataDirectory, filename);
        }


        /// <summary>
        /// Define the lower Left Corner
        /// </summary>
        public Position LowerLeftCorner
        {
            get { return GetPosition("LowerLeftCorner"); }
            set { SetPosition(value, "LowerLeftCorner"); }
        }


        /// <summary>
        /// Never clear The ShowCommands window
        /// </summary>
        public static bool NeverClear
        {
            get
            {
                var xml = new XmlDocument();
                xml.Load(GetConfigurationFile("Marlin3DprinterToolConfiguration.xml"));
                var xmlNode = (XmlElement) xml.SelectSingleNode("/configuration/NeverClear");
                if (xmlNode == null) return false;

                return xmlNode.GetAttribute("value").ToLower() == "true";
            }
            set
            {
                var xml = new XmlDocument();
                xml.Load(GetConfigurationFile("Marlin3DprinterToolConfiguration.xml"));
                var xmlNode = (XmlElement) xml.SelectSingleNode("/configuration/NeverClear");
                if (xmlNode == null)
                {
                    xmlNode = (XmlElement) CreateMissingXmlNode(xml, xml.DocumentElement, "NeverClear");
                }
                xmlNode?.SetAttribute("value", value.ToString()); // boolian.ToString = True/False
                xml.Save(GetConfigurationFile("Marlin3DprinterToolConfiguration.xml"));
            }
        }


        /// <summary>
        /// Type of adjuster for the bed
        /// </summary>
        public string Adjuster
        {
            get
            {
                var xml = new XmlDocument();
                xml.Load(GetConfigurationFile("Marlin3DprinterToolConfiguration.xml"));
                var xmlNode = (XmlElement) xml.SelectSingleNode("/configuration/Adjuster");
                if (xmlNode == null) return "M3";

                return xmlNode.GetAttribute("type");
            }
            set
            {
                var xml = new XmlDocument();
                xml.Load(GetConfigurationFile("Marlin3DprinterToolConfiguration.xml"));
                var xmlNode = (XmlElement) xml.SelectSingleNode("/configuration/Adjuster");
                if (xmlNode == null)
                {
                    xmlNode = (XmlElement) CreateMissingXmlNode(xml, xml.DocumentElement, "Adjuster");
                }
                xmlNode?.SetAttribute("type", value); //(xmlNodeAdjuster != null)
                xml.Save(GetConfigurationFile("Marlin3DprinterToolConfiguration.xml"));
            }
        }

        /// <summary>
        /// Define the max travel for the Z-axis
        /// </summary>
        public int ZmaxTravel
        {
            get
            {
                var xml = new XmlDocument();
                xml.Load(GetConfigurationFile("Marlin3DprinterToolConfiguration.xml"));
                var xmlNode = (XmlElement) xml.SelectSingleNode("/configuration/ZmaxTravel");
                if (xmlNode == null) return 300;

                return Convert.ToInt16(xmlNode.GetAttribute("zmax"));
            }
            set
            {
                var xml = new XmlDocument();
                xml.Load(GetConfigurationFile("Marlin3DprinterToolConfiguration.xml"));
                var xmlNode = (XmlElement) xml.SelectSingleNode("/configuration/ZmaxTravel");
                if (xmlNode == null)
                {
                    xmlNode = (XmlElement) CreateMissingXmlNode(xml, xml.DocumentElement, "ZmaxTravel");
                }
                xmlNode?.SetAttribute("zmax", value.ToString()); // if (xmlNodeZmax != null) Int = no need for decimal 
                xml.Save(GetConfigurationFile("Marlin3DprinterToolConfiguration.xml"));
            }
        }


        /// <summary>
        /// The latest connected COM-port
        /// </summary>
        public string ComPort
        {
            get
            {
                var xml = new XmlDocument();
                xml.Load(GetConfigurationFile("Marlin3DprinterToolConfiguration.xml"));
                var xmlNode = (XmlElement) xml.SelectSingleNode("/configuration/comport");
                if (xmlNode == null) return "";

                return xmlNode.GetAttribute("port");
            }
            set
            {
                var xml = new XmlDocument();
                xml.Load(GetConfigurationFile("Marlin3DprinterToolConfiguration.xml"));
                var xmlNode = (XmlElement) xml.SelectSingleNode("/configuration/comport");
                if (xmlNode == null)
                {
                    xmlNode = (XmlElement) CreateMissingXmlNode(xml, xml.DocumentElement, "comport");
                }
                xmlNode?.SetAttribute("port", value);
                xml.Save(GetConfigurationFile("Marlin3DprinterToolConfiguration.xml"));

            }
        }

        /// <summary>
        /// Last connected Baudrate
        /// </summary>
        public string Baudrate
        {
            get
            {
                var xml = new XmlDocument();
                xml.Load(GetConfigurationFile("Marlin3DprinterToolConfiguration.xml"));
                var xmlNode = (XmlElement) xml.SelectSingleNode("/configuration/comport");
                if (xmlNode == null) return "";

                return xmlNode.GetAttribute("baudrate");
            }
            set
            {
                var xml = new XmlDocument();
                xml.Load(GetConfigurationFile("Marlin3DprinterToolConfiguration.xml"));
                var xmlNode = (XmlElement) xml.SelectSingleNode("/configuration/baudrate");
                if (xmlNode == null)
                {
                    xmlNode = (XmlElement) CreateMissingXmlNode(xml, xml.DocumentElement, "baudrate");
                }
                xmlNode?.SetAttribute("baudrate", value);
                xml.Save(GetConfigurationFile("Marlin3DprinterToolConfiguration.xml"));

            }
        }

        /// <summary>
        /// Path where Arduino IDE is located
        /// </summary>
        public string ArduinoIde
        {
            get
            {
                var xml = new XmlDocument();
                xml.Load(GetConfigurationFile("Marlin3DprinterToolConfiguration.xml"));
                var xmlNode = (XmlElement) xml.SelectSingleNode("/configuration/ArduinoIDE");
                if (xmlNode == null) return "";

                return xmlNode.GetAttribute("directory");
            }
            set
            {
                var xml = new XmlDocument();
                xml.Load(GetConfigurationFile("Marlin3DprinterToolConfiguration.xml"));
                var xmlNode = (XmlElement) xml.SelectSingleNode("/configuration/ArduinoIDE");
                if (xmlNode == null)
                {
                    xmlNode = (XmlElement) CreateMissingXmlNode(xml, xml.DocumentElement, "ArduinoIDE");
                }
                xmlNode?.SetAttribute("directory", value);
                xml.Save(GetConfigurationFile("Marlin3DprinterToolConfiguration.xml"));
            }
        }


        /// <summary>
        /// Lower Right Coner
        /// </summary>
        public Position LowerRightCorner
        {
            get { return GetPosition("LowerRightCorner"); }
            set { SetPosition(value, "LowerRightCorner"); }
        }

        /// <summary>
        /// Upper Left Corner
        /// </summary>
        public Position UpperLeftCorner
        {
            get { return GetPosition("UpperLeftCorner"); }
            set { SetPosition(value, "UpperLeftCorner"); }
        }

        /// <summary>
        /// Upper Right Corner
        /// </summary>
        public Position UpperRightCorner
        {
            get { return GetPosition("UpperRightCorner"); }
            set { SetPosition(value, "UpperRightCorner"); }
        }

        /// <summary>
        /// Lower Left Adjuster
        /// </summary>
        public Position LowerLeftAdjuster
        {
            get { return GetPosition("LowerLeftAdjuster"); }
            set { SetPosition(value, "LowerLeftAdjuster"); }
        }


        /// <summary>
        /// Lower Right Adjuster
        /// </summary>
        public Position LowerRightAdjuster
        {
            get { return GetPosition("LowerRightAdjuster"); }
            set { SetPosition(value, "LowerRightAdjuster"); }
        }

        /// <summary>
        /// Upper Left Adjuster
        /// </summary>
        public Position UpperLeftAdjuster
        {
            get { return GetPosition("UpperLeftAdjuster"); }
            set { SetPosition(value, "UpperLeftAdjuster"); }
        }


        /// <summary>
        /// Upper Right Adjuster
        /// </summary>
        public Position UpperRightAdjuster
        {
            get { return GetPosition("UpperRightAdjuster"); }
            set { SetPosition(value, "UpperRightAdjuster"); }
        }

        /// <summary>
        /// Position where the probe is in a safe position
        /// and its possible to work with the adjusters of the bed
        /// </summary>
        public Position SafeHome
        {
            get { return GetPosition("SafeHome"); }
            set { SetPosition(value, "SafeHome"); }
        }


        /// <summary>
        /// Describe how a probe is engaged with GCODE
        /// </summary>
        public List<string> GcodeAssistZprobeEngage
        {
            get
            {
                var returnList = new List<string>();

                var xml = new XmlDocument();
                xml.Load(GetConfigurationFile("Marlin3DprinterToolConfiguration.xml"));

                var nav = xml.CreateNavigator();

                var exp = nav.Compile(@"/configuration/GcodeAssistZprobeEngage/Row");

                exp.AddSort("@order", XmlSortOrder.Ascending, XmlCaseOrder.None, "", XmlDataType.Text);

                foreach (XPathNavigator gcode in nav.Select(exp))
                {
                    returnList.Add(gcode.InnerXml);
                }

                return returnList;
            }
            set
            {
                var xml = new XmlDocument();
                xml.Load(GetConfigurationFile("Marlin3DprinterToolConfiguration.xml"));

                var gcodeAssistZprobeEngare = xml.SelectSingleNode("/configuration/GcodeAssistZprobeEngage");


                var gcodeAssistZprobeEngageChilds = xml.SelectNodes("/configuration/GcodeAssistZprobeEngage/Row");

                if (gcodeAssistZprobeEngageChilds != null)
                {
                    foreach (XmlNode gcode in gcodeAssistZprobeEngageChilds)
                    {
                        gcode.ParentNode?.RemoveChild(gcode);
                    }
                }

                for (var order = 0; order < value.Count; order++)
                {
                    var gcode = value[order];
                    var newRow = xml.CreateElement("Row");
                    newRow.SetAttribute("order", order.ToString()); // Int = no need for decimal
                    newRow.InnerXml = gcode;
                    gcodeAssistZprobeEngare?.AppendChild(newRow);
                }

                xml.Save(GetConfigurationFile("Marlin3DprinterToolConfiguration.xml"));
            }
        }

        /// <summary>
        /// Describe how a probe is disengaged with GCODE
        /// </summary>
        public List<string> GcodeAssistZprobeRelease
        {
            get
            {
                var returnList = new List<string>();

                var xml = new XmlDocument();
                xml.Load(GetConfigurationFile("Marlin3DprinterToolConfiguration.xml"));

                var nav = xml.CreateNavigator();

                var exp = nav.Compile(@"/configuration/GcodeAssistZprobeRelease/Row");

                exp.AddSort("@order", XmlSortOrder.Ascending, XmlCaseOrder.None, "", XmlDataType.Text);

                foreach (XPathNavigator gcode in nav.Select(exp))
                {
                    returnList.Add(gcode.InnerXml);
                }

                return returnList;
            }
            set
            {
                //List<string> returnList = new List<string>();

                var xml = new XmlDocument();
                xml.Load(GetConfigurationFile("Marlin3DprinterToolConfiguration.xml"));

                var gcodeAssistZprobeRelease = xml.SelectSingleNode("/configuration/GcodeAssistZprobeRelease");

                var gcodeAssistZprobeReleaseChilds = xml.SelectNodes("/configuration/GcodeAssistZprobeRelease/Row");

                if (gcodeAssistZprobeReleaseChilds != null)
                {
                    foreach (XmlNode gcode in gcodeAssistZprobeReleaseChilds)
                    {
                        gcode.ParentNode?.RemoveChild(gcode);
                    }
                }

                for (var order = 0; order < value.Count; order++)
                {
                    var gcode = value[order];
                    var newRow = xml.CreateElement("Row");
                    newRow.SetAttribute("order", order.ToString()); // Int = no need for decimal
                    newRow.InnerXml = gcode;
                    gcodeAssistZprobeRelease?.AppendChild(newRow);
                }

                xml.Save(GetConfigurationFile("Marlin3DprinterToolConfiguration.xml"));
            }
        }


        /// <summary>
        /// Licensekey
        /// </summary>
        public string LicenseKey
        {
            get
            {
                var xml = new XmlDocument();
                xml.Load(GetConfigurationFile("Marlin3DprinterToolConfiguration.xml"));
                var xmlNode = (XmlElement) xml.SelectSingleNode("/configuration/LicenseKey");
                if (xmlNode == null) return "";


                return xmlNode.GetAttribute("key");

            }
            set
            {
                var xml = new XmlDocument();
                xml.Load(GetConfigurationFile("Marlin3DprinterToolConfiguration.xml"));
                var xmlNode = (XmlElement) xml.SelectSingleNode("/configuration/LicenseKey");
                if (xmlNode == null)
                {
                    xmlNode = (XmlElement) CreateMissingXmlNode(xml, xml.DocumentElement, "LicenseKey");
                }
                xmlNode?.SetAttribute("key", value);
                xml.Save(GetConfigurationFile("Marlin3DprinterToolConfiguration.xml"));
            }
        }


        /// <summary>
        /// Extra distance that lifts the Z-Probe during travel
        /// If not entered a value of 50 is default.
        /// </summary>
        public string ZextraDistance
        {
            get 
            {
                var xml = new XmlDocument();
                xml.Load(GetConfigurationFile("Marlin3DprinterToolConfiguration.xml"));
                var xmlNode = (XmlElement) xml.SelectSingleNode("/configuration/Zprobe");
                if (xmlNode == null) return "50";  // Default is 50

                return xmlNode.GetAttribute("safedistance");
            }

            set 
            {
                var xml = new XmlDocument();
                xml.Load(GetConfigurationFile("Marlin3DprinterToolConfiguration.xml"));
                var xmlNode = (XmlElement) xml.SelectSingleNode("/configuration/Zprobe");
                if (xmlNode == null)
                {
                    xmlNode = (XmlElement) CreateMissingXmlNode(xml, xml.DocumentElement, "Zprobe");
                }
                if (!value.Contains(".")) value = value + ".00";
                xmlNode?.SetAttribute("safedistance", value);
                xml.Save(GetConfigurationFile("Marlin3DprinterToolConfiguration.xml"));
            }
        }


        /// <summary>
        /// Z probe X Offset Left/Right
        /// </summary>
        public string ZprobeXoffset
        {
            get
            {
                var xml = new XmlDocument();
                xml.Load(GetConfigurationFile("Marlin3DprinterToolConfiguration.xml"));
                var xmlNode = (XmlElement)xml.SelectSingleNode("/configuration/Zprobe");
                if (xmlNode == null) return "to the left of the nozzle";  // to the left of the nozzle

                return xmlNode.GetAttribute("Xoffset");
            }

            set
            {
                var xml = new XmlDocument();
                xml.Load(GetConfigurationFile("Marlin3DprinterToolConfiguration.xml"));
                var xmlNode = (XmlElement)xml.SelectSingleNode("/configuration/Zprobe");
                if (xmlNode == null)
                {
                    xmlNode = (XmlElement)CreateMissingXmlNode(xml, xml.DocumentElement, "Zprobe");
                }
                xmlNode?.SetAttribute("Xoffset", value);
                xml.Save(GetConfigurationFile("Marlin3DprinterToolConfiguration.xml"));
            }
        }


        


        /// <summary>
        /// Z probe Offset in X-axis in mm
        /// </summary>
        public string ZprobeXoffsetValue
        {
            get
            {
                var xml = new XmlDocument();
                xml.Load(GetConfigurationFile("Marlin3DprinterToolConfiguration.xml"));
                var xmlNode = (XmlElement)xml.SelectSingleNode("/configuration/Zprobe");
                if (xmlNode == null) return "0";  // Default is 0

                return xmlNode.GetAttribute("X");
            }

            set
            {
                var xml = new XmlDocument();
                xml.Load(GetConfigurationFile("Marlin3DprinterToolConfiguration.xml"));
                var xmlNode = (XmlElement)xml.SelectSingleNode("/configuration/Zprobe");
                if (xmlNode == null)
                {
                    xmlNode = (XmlElement)CreateMissingXmlNode(xml, xml.DocumentElement, "Zprobe");
                }
                xmlNode?.SetAttribute("X", value);
                xml.Save(GetConfigurationFile("Marlin3DprinterToolConfiguration.xml"));
            }
        }

        /// <summary>
        /// Z probe Offset in Z-axis in mm
        /// </summary>
        public string ZprobeZoffsetValue
        {
            get
            {
                var xml = new XmlDocument();
                xml.Load(GetConfigurationFile("Marlin3DprinterToolConfiguration.xml"));
                var xmlNode = (XmlElement)xml.SelectSingleNode("/configuration/Zprobe");
                if (xmlNode == null) return "0";  // Default is 0

                return xmlNode.GetAttribute("Z");
            }

            set
            {
                var xml = new XmlDocument();
                xml.Load(GetConfigurationFile("Marlin3DprinterToolConfiguration.xml"));
                var xmlNode = (XmlElement)xml.SelectSingleNode("/configuration/Zprobe");
                if (xmlNode == null)
                {
                    xmlNode = (XmlElement)CreateMissingXmlNode(xml, xml.DocumentElement, "Zprobe");
                }
                xmlNode?.SetAttribute("Z", value);
                xml.Save(GetConfigurationFile("Marlin3DprinterToolConfiguration.xml"));
            }
        }



        


        /// <summary>
        /// Z probe Y Offset Front/Behind
        /// </summary>
        public string ZprobeYoffset
        {
            get
            {
                var xml = new XmlDocument();
                xml.Load(GetConfigurationFile("Marlin3DprinterToolConfiguration.xml"));
                var xmlNode = (XmlElement)xml.SelectSingleNode("/configuration/Zprobe");
                if (xmlNode == null) return "behind the nozzle";  // Default is 50

                return xmlNode.GetAttribute("Yoffset");
            }

            set
            {
                var xml = new XmlDocument();
                xml.Load(GetConfigurationFile("Marlin3DprinterToolConfiguration.xml"));
                var xmlNode = (XmlElement)xml.SelectSingleNode("/configuration/Zprobe");
                if (xmlNode == null)
                {
                    xmlNode = (XmlElement)CreateMissingXmlNode(xml, xml.DocumentElement, "Zprobe");
                }
                xmlNode?.SetAttribute("Yoffset", value);
                xml.Save(GetConfigurationFile("Marlin3DprinterToolConfiguration.xml"));
            }
        }

        /// <summary>
        /// Z-probe  Y-offset in mm
        /// </summary>
        public string ZprobeYoffsetValue
        {
            get
            {
                var xml = new XmlDocument();
                xml.Load(GetConfigurationFile("Marlin3DprinterToolConfiguration.xml"));
                var xmlNode = (XmlElement)xml.SelectSingleNode("/configuration/Zprobe");
                if (xmlNode == null) return "10";  // Default is 10

                return xmlNode.GetAttribute("Y");
            }

            set
            {
                var xml = new XmlDocument();
                xml.Load(GetConfigurationFile("Marlin3DprinterToolConfiguration.xml"));
                var xmlNode = (XmlElement)xml.SelectSingleNode("/configuration/Zprobe");
                if (xmlNode == null)
                {
                    xmlNode = (XmlElement)CreateMissingXmlNode(xml, xml.DocumentElement, "Zprobe");
                }
                xmlNode?.SetAttribute("Y", value);
                xml.Save(GetConfigurationFile("Marlin3DprinterToolConfiguration.xml"));
            }
        }

        /// <summary>
        /// Decrypt a string. Usage for license key
        /// </summary>
        /// <param name="text"></param>
        /// <returns>Decrypted sting or null if nota able to Decrypt</returns>
        public static string Decrypt(string text)
        {
            try
            {
                byte[] key = new byte[8] { 1, 2, 3, 4, 5, 6, 7, 8 };
                byte[] iv = new byte[8] { 1, 2, 3, 4, 5, 6, 7, 8 };
                SymmetricAlgorithm algorithm = DES.Create();
                ICryptoTransform transform = algorithm.CreateDecryptor(key, iv);
                byte[] inputbuffer = Convert.FromBase64String(text);
                byte[] outputBuffer = transform.TransformFinalBlock(inputbuffer, 0, inputbuffer.Length);
                return Encoding.Unicode.GetString(outputBuffer);
            }
            catch (Exception)
            {

                return null;
            }
            
        }



        private Position GetPosition(string tag)
        {
            var xml = new XmlDocument();
            xml.Load(GetConfigurationFile("Marlin3DprinterToolConfiguration.xml"));
            var xmlNodePosition = (XmlElement) xml.SelectSingleNode($"/configuration/{tag}");
            if (xmlNodePosition == null) return null;
            try
            {
                var position = new Position
                {
                    X =
                        Convert.ToDouble(xmlNodePosition.Attributes["x"].Value,
                            CultureInfo.CreateSpecificCulture("en-GB")),
                    Y =
                        Convert.ToDouble(xmlNodePosition.Attributes["y"].Value,
                            CultureInfo.CreateSpecificCulture("en-GB"))
                };
                return position;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private void SetPosition(Position position, string tag)
        {
            var xml = new XmlDocument();
            xml.Load(GetConfigurationFile("Marlin3DprinterToolConfiguration.xml"));
            var xmlNodePosition = (XmlElement) xml.SelectSingleNode($"/configuration/{tag}");
            if (xmlNodePosition == null)
            {
                xmlNodePosition = (XmlElement)CreateMissingXmlNode(xml, xml.DocumentElement, tag);
            }

            if (xmlNodePosition != null)
            {
                xmlNodePosition.SetAttribute("x", position.X.ToString(CultureInfo.InvariantCulture).Replace(',','.') ); // Allways decimal point
                xmlNodePosition.SetAttribute("y", position.Y.ToString(CultureInfo.InvariantCulture).Replace(',', '.')); // Allways decimal point
            }

            xml.Save(GetConfigurationFile("Marlin3DprinterToolConfiguration.xml"));
        }

        /// <summary>
        /// Get all the MeassuredMeshpoints
        /// </summary>
        /// <returns></returns>
        public List<Position> GetMeshpoints()
        {
            List<Position> meshPoints = new List<Position>();
            var xml = new XmlDocument();
            xml.Load(GetConfigurationFile("Marlin3DprinterToolConfiguration.xml"));

            XmlNodeList meshPointXmlNodeList = xml.SelectNodes("/configuration/MeassuredMeshPoints/MeshPoint");
            foreach (XmlNode meshpoint in meshPointXmlNodeList)
            {
                double x = Convert.ToDouble(meshpoint.Attributes["X"].Value.Replace('.',','));
                double y = Convert.ToDouble(meshpoint.Attributes["Y"].Value.Replace('.', ','));
                double z = Convert.ToDouble(meshpoint.Attributes["Z"].Value.Replace('.', ','));

                meshPoints.Add(new Position { X= x, Y=y, Z=z});
            }


            return meshPoints;
        }


        /// <summary>
        /// Get the stored "Reset" Meshpoint
        /// </summary>
        /// <returns></returns>
        public List<Position> GetTrueMeshpoints()
        {
            List<Position> meshPoints = new List<Position>();
            var xml = new XmlDocument();
            xml.Load(GetConfigurationFile("Marlin3DprinterToolConfiguration.xml"));

            XmlNodeList meshPointXmlNodeList = xml.SelectNodes("/configuration/TrueMeshPoints/MeshPoint");
            foreach (XmlNode meshpoint in meshPointXmlNodeList)
            {
                double x = Convert.ToDouble(meshpoint.Attributes["X"].Value.Replace('.', ','));
                double y = Convert.ToDouble(meshpoint.Attributes["Y"].Value.Replace('.', ','));
                double z = Convert.ToDouble(meshpoint.Attributes["Z"].Value.Replace('.', ','));

                meshPoints.Add(new Position { X = x, Y = y, Z = z });
            }


            return meshPoints;
        }


        /// <summary>
        /// Set the MeshPoints
        /// Saves recalculated MeshPoints
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public void SetMeassuredMeshpoint(double x, double y, double z)
        {
            var xml = new XmlDocument();
            xml.Load(GetConfigurationFile("Marlin3DprinterToolConfiguration.xml"));

            XmlNode meshpointsXmlNode = xml.SelectSingleNode("/configuration/MeassuredMeshPoints");
            if (meshpointsXmlNode == null)
            {
                meshpointsXmlNode = CreateMissingXmlNode(xml, xml.DocumentElement, "MeassuredMeshPoints");
            }


            bool meshpointFound = false;

            XmlNodeList meshpointXmlNodeList = xml.SelectNodes("/configuration/MeassuredMeshPoints/MeshPoint");

            if (meshpointXmlNodeList != null)
            {
                foreach (XmlElement meshPointXmlElement in meshpointXmlNodeList)
                {
                    if (
                        Math.Abs(Convert.ToInt16(meshPointXmlElement.Attributes["X"].Value) - x) < 20 &&
                        Math.Abs(Convert.ToInt16(meshPointXmlElement.Attributes["Y"].Value) - y) < 20
                        )
                    {
                        meshpointFound = true;
                        meshPointXmlElement.SetAttribute("Z", z.ToString().Replace(',', '.'));
                        break;
                    }

                }

            }
            if (meshpointFound == false)
            {
                XmlElement meshpointXmlNode = (XmlElement)CreateMissingXmlNode(xml, meshpointsXmlNode, "MeshPoint");
                meshpointXmlNode.SetAttribute("X", x.ToString());
                meshpointXmlNode.SetAttribute("Y", y.ToString());
                meshpointXmlNode.SetAttribute("Z", z.ToString().Replace(',', '.'));
            }


            xml.Save(GetConfigurationFile("Marlin3DprinterToolConfiguration.xml"));
        }


        /// <summary>
        /// Delete all meassured Meshpoints
        /// </summary>
        public void DeleteMeshPoints()
        {
            var xml = new XmlDocument();
            xml.Load(GetConfigurationFile("Marlin3DprinterToolConfiguration.xml"));

            XmlNode meshpointsXmlNode = xml.SelectSingleNode("/configuration/InitMeshPoints");
            if (meshpointsXmlNode == null) 
            {
                meshpointsXmlNode = CreateMissingXmlNode(xml, xml.DocumentElement, "InitMeshPoints");
            }

            meshpointsXmlNode.RemoveAll();

            XmlNode meassuredMeshXmlNode = xml.SelectSingleNode("/configuration/MeassuredMeshPoints");
            if (meassuredMeshXmlNode == null)
            {
                meassuredMeshXmlNode = CreateMissingXmlNode(xml, xml.DocumentElement, "MeassuredMeshPoints");
            }

            meassuredMeshXmlNode.RemoveAll();





            xml.Save(GetConfigurationFile("Marlin3DprinterToolConfiguration.xml"));
        }

    }


    /// <summary>
    /// Position with X/y/Z values
    /// Conversion to string and tu Double
    /// Uses Decimal.point in all strings
    /// </summary>
    public class Position
    {
        private double _x;
        private double _y;
        private double _z;


        /// <summary>
        /// Set/get the X value as double
        /// </summary>
        public double X
        {
            get { return _x; }
            set {_x = value; }
        }

        /// <summary>
        /// Set/get the Y value as double
        /// </summary>
        public double Y
        {
            get { return _y; }
            set { _y = value;}
        }

        /// <summary>
        /// Set/get the Z value as double
        /// </summary>
        public double Z
        {
            get { return _z; }
            set { _z = value;}
        }

        /// <summary>
        ///  Set and Get X position as string
        /// </summary>
        public string Xstring
        {
            get { return _x.ToString(CultureInfo.InvariantCulture).Replace(',', '.'); } // Always decimalpoint
            set { _x = Convert.ToDouble(value.Replace('.', ',')); }
        }

        /// <summary>
        ///  Set and Get Y position as string
        /// </summary>
        public string Ystring
        {
            get { return _y.ToString(CultureInfo.InvariantCulture).Replace(',', '.'); } // Always decimalpoint
            set { _y = Convert.ToDouble(value.Replace('.', ',')); }
        }

        /// <summary>
        ///  Set and Get Z position as string
        /// </summary>
        public string Zstring
        {
            get { return _z.ToString(CultureInfo.InvariantCulture).Replace(',', '.'); } // Always decimalpoint
            set { _z = Convert.ToDouble(value.Replace('.', ',')); }
        }
    }


    

}