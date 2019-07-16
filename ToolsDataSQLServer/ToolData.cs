using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ToolsDataSQLServer
{
	public static class clsToolsData
	{
	public struct ToolData
	{
		public int ID; public string type; public int diametr;
			public int length; public int cut_length; public float corner_radius;
			public int quantity_receiving; public string date_receiving;//public int quantity_issuing; , date_issuing;
		//public ToolData(string type, int diametr, int length, int cut_length, float corner_radius, int quantity_receiving/* int quantity_issuing, DateTime date_receiving, DateTime date_issuing, int id*/)
		//{
		//	 this.type = type; this.diametr = diametr;
		//	this.length = length; this.cut_length = cut_length; this.corner_radius = corner_radius; this.quantity_receiving = quantity_receiving;
		//		// this.quantity_issuing = quantity_issuing; this.date_receiving = date_receiving; this.date_issuing = date_issuing;  

		//}
	} // struct
		public static ToolData toolData = new ToolData();
		public static List<ToolData> lst_ToolsDataBase  = new List<ToolData> { }; //internal int i = 0;
		public static WriteReadXML XML = new WriteReadXML();
		public static void AddTool( TextBox  type, TextBox diametr, TextBox length, TextBox cut_length, TextBox corner_radius, TextBox quantity_receiving, string date_receiving )
		{
			toolData.type = type.Text; 
			toolData.diametr = Int32.Parse(diametr.Text);  toolData.length = Int32.Parse(length.Text); toolData.cut_length = Int32.Parse(cut_length.Text);
			toolData.corner_radius = float.Parse(corner_radius.Text); 
			toolData.quantity_receiving = Int32.Parse(quantity_receiving.Text); toolData.date_receiving = date_receiving;
			
			toolData.ID ++;
			//lst_ToolsDataBase.Add(toolData);  //txtTooID.Text = toolData.ID.ToString();
		}
	} // class ToolData
}
