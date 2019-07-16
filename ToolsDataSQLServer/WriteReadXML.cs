using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ToolsDataSQLServer
{
	public class WriteReadXML
	{
		internal string filename;
		XmlDocument xDoc = new XmlDocument();
		clsToolsData.ToolData  stc_toolData;
		public WriteReadXML() //!!!!
		{ filename = "c:/tooldatabase.xml"; }

		public void writeToXML( clsToolsData.ToolData stc_toolData)
		{
			if (System.IO.File.Exists(filename) == false || System.IO.File.ReadAllText(filename) == "")
			{
				XmlElement clsToolsData = xDoc.CreateElement("clsToolsData");
				xDoc.CreateComment("uraaaaa");
				xDoc.AppendChild(clsToolsData);
				XmlNode xRoot = xDoc.DocumentElement;
				XmlElement toolData = xDoc.CreateElement("ToolData");
				clsToolsData.AppendChild(toolData); //!!!! need add first element to file !!!
				XmlElement toolID = xDoc.CreateElement("toolID"); toolID.InnerText = stc_toolData.ID.ToString(); toolData.AppendChild(toolID);
				XmlElement toolType = xDoc.CreateElement("type"); toolType.InnerText = stc_toolData.type; toolData.AppendChild(toolType);
				XmlElement toolDiametr = xDoc.CreateElement("diametr"); toolDiametr.InnerText = stc_toolData.diametr.ToString(); toolData.AppendChild(toolDiametr);
				XmlElement toolLength = xDoc.CreateElement("length"); toolLength.InnerText = stc_toolData.length.ToString(); toolData.AppendChild(toolLength);
				XmlElement toolCutLength = xDoc.CreateElement("cut_length"); toolCutLength.InnerText = stc_toolData.cut_length.ToString(); toolData.AppendChild(toolCutLength);
				XmlElement toolCornerRadius = xDoc.CreateElement("corner_radius"); toolCornerRadius.InnerText = stc_toolData.corner_radius.ToString();
				toolData.AppendChild(toolCornerRadius);
				XmlElement toolQuantityReceiving = xDoc.CreateElement("quantity_receiving"); toolQuantityReceiving.InnerText = stc_toolData.quantity_receiving.ToString();
				toolData.AppendChild(toolQuantityReceiving);
				XmlElement toolDateReceiving = xDoc.CreateElement("date_receiving"); toolDateReceiving.InnerText = stc_toolData.date_receiving.ToString();
				toolData.AppendChild(toolDateReceiving);
				xDoc.Save(filename);
			}
			else
			{
				xDoc.Load(filename);
				XmlNode xRoot = xDoc.DocumentElement;
				XmlElement toolData = xDoc.CreateElement("ToolData");
				xRoot.AppendChild(toolData);
				XmlElement toolID = xDoc.CreateElement("toolID"); toolID.InnerText = stc_toolData.ID.ToString(); toolData.AppendChild(toolID);
				XmlElement toolType = xDoc.CreateElement("type"); toolType.InnerText = stc_toolData.type; toolData.AppendChild(toolType);
				XmlElement toolDiametr = xDoc.CreateElement("diametr"); toolDiametr.InnerText = stc_toolData.diametr.ToString(); toolData.AppendChild(toolDiametr);
				XmlElement toolLength = xDoc.CreateElement("length"); toolLength.InnerText = stc_toolData.length.ToString(); toolData.AppendChild(toolLength);
				XmlElement toolCutLength = xDoc.CreateElement("cut_length"); toolCutLength.InnerText = stc_toolData.cut_length.ToString(); toolData.AppendChild(toolCutLength);
				XmlElement toolCornerRadius = xDoc.CreateElement("corner_radius"); toolCornerRadius.InnerText = stc_toolData.corner_radius.ToString();
				toolData.AppendChild(toolCornerRadius);
				XmlElement toolQuantityReceiving = xDoc.CreateElement("quantity_receiving"); toolQuantityReceiving.InnerText = stc_toolData.quantity_receiving.ToString();
				toolData.AppendChild(toolQuantityReceiving);
				XmlElement toolDateReceiving = xDoc.CreateElement("date_receiving"); toolDateReceiving.InnerText = stc_toolData.date_receiving.ToString();
				toolData.AppendChild(toolDateReceiving);
				xDoc.Save(filename);
			} //else
		}
		public int readIDfromXML()
		{
			XmlTextReader xmlReader = new XmlTextReader(filename);
			string nodename; int i = 0;
			while (xmlReader.Read())
			{
				nodename = xmlReader.Name;
				if (nodename == "toolID") i = Convert.ToInt32(xmlReader.ReadString());
				//{ toolList.Last t[i].value_name = xmlReader.ReadString(); }
				//{ arrDat[i].value = (float)Convert.ChangeType(xmlReader.ReadString(), typeof(float)); i++; }
			}
			return i;
		} // readIDfromXML
		public List<clsToolsData.ToolData> readToListfromXML()
		{
			List<clsToolsData.ToolData> list_tooldata = new List<clsToolsData.ToolData> { };
			XmlDocument xDoc = new XmlDocument(); xDoc.Load(filename);
			XmlNodeList node_toolID = xDoc.GetElementsByTagName("toolID");
			XmlNodeList node_type = xDoc.GetElementsByTagName("type");
			XmlNodeList node_diametr = xDoc.GetElementsByTagName("diametr");
			XmlNodeList node_length = xDoc.GetElementsByTagName("length");
			XmlNodeList node_cut_length = xDoc.GetElementsByTagName("cut_length");
			XmlNodeList node_corner_radius = xDoc.GetElementsByTagName("corner_radius");
			XmlNodeList node_quantity_receiving = xDoc.GetElementsByTagName("quantity_receiving");
			XmlNodeList node_date_receiving = xDoc.GetElementsByTagName("date_receiving");
			for (int i = 0; i < node_toolID.Count; i++)
			{
				clsToolsData.toolData.ID = Int32.Parse(node_toolID[i].InnerText);
				clsToolsData.toolData.type = node_type[i].InnerText;
				clsToolsData.toolData.diametr = Int32.Parse(node_diametr[i].InnerText);
				clsToolsData.toolData.length = Int32.Parse(node_length[i].InnerText);
				clsToolsData.toolData.cut_length = Int32.Parse(node_cut_length[i].InnerText);
				clsToolsData.toolData.corner_radius = float.Parse(node_corner_radius[i].InnerText);
				clsToolsData.toolData.quantity_receiving = Int32.Parse(node_quantity_receiving[i].InnerText);
				clsToolsData.toolData.date_receiving = node_date_receiving[i].InnerText;
				list_tooldata.Add(clsToolsData.toolData);
			}
			return list_tooldata;
		} // readToListfromXML
	} // class WriteRadXML 
	
}
