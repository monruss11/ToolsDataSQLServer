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
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;
using ServertoDS = ToolsDataSQLServer.clsSQLServerToDataSet;

namespace ToolsDataSQLServer
{
	/// <summary>
	/// Interaction logic for QueryFromSQLDataBase.xaml
	/// </summary>
	public partial class QueryFromSQLDataBase : Window
	{
		DataSet queryDataSet = new DataSet(); internal string selectedcondition = "";
		public QueryFromSQLDataBase()
		{
			InitializeComponent();
			Double width  = SystemParameters.FullPrimaryScreenWidth;
            Double height = SystemParameters.FullPrimaryScreenHeight;
			this.Top = (height - this.Height) / 2+50;
            this.Left = (width - this.Width) / 2+50;
			
		}
		private void Search_MenuItem_Click(object sender, RoutedEventArgs ex)
		{
			if (ServertoDS.conn.State == ConnectionState.Open)  ServertoDS.conn.Close();  
				ServertoDS.conn.Open();

			if (cmb_Condition.SelectedItem!=null) 
				{ ComboBoxItem selecteditem = (ComboBoxItem)cmb_Condition.SelectedItem; selectedcondition = selecteditem.Content.ToString(); }
			else 	{  MessageBox.Show("Choise Condition" ); goto exit; }
			string selectedvalue = ""; string selectedposition="";
			if (txt_DiametrQuery.Text != "") { selectedvalue = txt_DiametrQuery.Text.ToString(); selectedposition = "diametr"; }
			else { selectedvalue = txt_LengthQuery.Text.ToString(); selectedposition = "length"; }
						
			SearchRegardingToolProperties( selectedposition, selectedcondition,selectedvalue);
			
			SqlDataAdapter queryDataAdpter = new SqlDataAdapter(ServertoDS.cmd);
			queryDataSet.Clear();
			queryDataAdpter.Fill(queryDataSet);
			dgQuerySearchResult.ItemsSource = queryDataSet.Tables[0].DefaultView;
			exit:;
			} // Search MenuItem
		void SearchRegardingToolProperties(string position,string condition,string value)
			{
				ServertoDS.sql = "select * from Tools_Table where"+" "+ position +" "+ condition + "@"+position;
				ServertoDS.cmd = new SqlCommand(ServertoDS.sql, ServertoDS.conn);
				ServertoDS.cmd.Parameters.Add(new SqlParameter("@"+position, SqlDbType.Int));
				ServertoDS.cmd.Parameters["@" + position].Value = Int32.Parse(value);
				ServertoDS.cmd.ExecuteScalar();
			
			}
		private void Txt_DiametrQuery_PreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			short value;
			if (!Int16.TryParse(e.Text, out value)) e.Handled = true;
		}
		

	} // class
} // namespace
