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
using System.Data.SqlClient;
using System.Data;

namespace ToolsDataSQLServer
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			Double width  = SystemParameters.FullPrimaryScreenWidth;
            Double height = SystemParameters.FullPrimaryScreenHeight;
			this.Top = (height - this.Height) / 2;
            this.Left = (width - this.Width) / 2;
		}

		#region Events
		clsInputValidtion validtion = new clsInputValidtion(); 
		void Txt_OnlyDigits_PreviewTextInput(object sender, TextCompositionEventArgs e)
			{
				
				validtion.OnlyDigitsPreviewTextInput(e);
			}
			void Txt_CornerRadius_PreviewTextInput(object sender, TextCompositionEventArgs e)
			{
				short value;
				if (!Int16.TryParse(e.Text, out value) & e.Text!=",") e.Handled = true;
			}
			void Txt_Type_PreviewTextInput(object sender, TextCompositionEventArgs e)
			{
				validtion.OnlyLettersPreviewTextInput(e); 
			}
			//void Grid_PreviewKeyDown(object sender, KeyEventArgs e)
			//{
			//	if (e.Key == Key.Space || e.Key == Key.RightShift || e.Key == Key.LeftShift) e.Handled = true;
			//}
#endregion Events

#region Buttons
		private void Btn_AddTool_Click(object sender, RoutedEventArgs e)
		{
			if (validtion.InputsCheck( txt_Type, txt_Diametr, txt_Length, txt_CutLength, txt_CornerRadius, txt_Quantity, datePicker)==false)
			e.Handled = true;
			else
			{
				clsSQLServerToDataSet.AddItemToDataSet(txt_Type,txt_Diametr,txt_Length,txt_CutLength,txt_CornerRadius,txt_Quantity, datePicker);
				int i = clsSQLServerToDataSet.dataSet.Tables.Count;
				if (i > 0)
				validtion.ClearInput(ref txt_Type, txt_Diametr, txt_Length, txt_CutLength, txt_CornerRadius, txt_Quantity);
			}
		}
		private void Btn_Connect_Click(object sender, RoutedEventArgs e)
		{
			clsSQLServerToDataSet.ConnectDataBase();
			dgToosData.ItemsSource = clsSQLServerToDataSet.dataSet.Tables[0].DefaultView; ColumnsProperty();
			//dgToosData.Columns[0].IsReadOnly = true;
		}
		public void ColumnsProperty()
		{
			dgToosData.Columns[0].Header = "Tool Id"; dgToosData.Columns[1].Header = "Tool Type"; dgToosData.Columns[2].Header = "Diametr";
			dgToosData.Columns[3].Header = "Length Tool"; dgToosData.Columns[4].Header = "Cutting Lengh"; 
			dgToosData.Columns[5].Header = "Corner Radius"; dgToosData.Columns[6].Header = "Quantity"; dgToosData.Columns[7].Header = "Date Received"; 
		    dgToosData.Columns[0].IsReadOnly=true;  dgToosData.Columns[7].IsReadOnly=true;
		}
		private void Btn_Update_Click(object sender, RoutedEventArgs e)
		{
			//clsSQLServerToDataSet.conn.Open();
			// if (clsSQLServerToDataSet.dataSet.Tables.Count!=0) clsSQLServerToDataSet.dataSet.Tables[0].Clear();
			dgToosData.ItemsSource = null; 
			dgToosData.Items.Refresh();
			dgToosData.ItemsSource = clsSQLServerToDataSet.dataSet.Tables[0].DefaultView;
		}
		private void Btn_AddToSQLData_Click(object sender, RoutedEventArgs e)
		{
			//if(validtion.InputsCheck( txt_Type, txt_Diametr, txt_Length, txt_CutLength, txt_CornerRadius, txt_Quantity, datePicker)==false)
			//e.Handled = true;
			//else
			{
				//clsSQLServerToDataSet.AddItemToSqlDataBase(txt_Type,txt_Diametr,txt_Length,txt_CutLength,txt_CornerRadius,txt_Quantity, datePicker);
				clsSQLServerToDataSet.AddCollectionItemsToSqlDataBase();
				int i = clsSQLServerToDataSet.dataSet.Tables.Count;
				if (i > 0)
				validtion.ClearInput(ref txt_Type, txt_Diametr, txt_Length, txt_CutLength, txt_CornerRadius, txt_Quantity);
			}
		}
#endregion Buttons
		
	}//MainWindow
} //namespace
