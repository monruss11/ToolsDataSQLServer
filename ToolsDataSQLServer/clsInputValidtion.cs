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
	public class clsInputValidtion
	{
		public bool InputsCheck( TextBox type,  TextBox diametr, TextBox length, TextBox cutlength, TextBox cornerradius, TextBox quantity,DatePicker date)
		{
			bool status = false;
			if (type.Text == "" || length.Text == "" || diametr.Text == "" || cutlength.Text == "" || cornerradius.Text == "" || quantity.Text == "")
			{ MessageBox.Show("One or more field is empty");
				type.Focus(); goto exit; //	
			}
			//Check block
			{ int d = Int32.Parse(diametr.Text); int cl = Int32.Parse(cutlength.Text); int l = Int32.Parse(length.Text); float j = float.Parse(cornerradius.Text);
				// Diametr must be > Corner radius *2
				if (d / 2 >= j) status = true;
				else { MessageBox.Show("Corner radius big more then half diametr"); cornerradius.Focus(); cornerradius.Text = ""; status = false; goto exit; }
				
				// Length > CutLength
				if ((l - cl) > 10) status = true;
				else { MessageBox.Show("Cut Length bigget than Length"); cutlength.Focus(); cutlength.Text = ""; status = false;  goto exit; }
				
				// if Date Choised from DatePicker
				if (date.SelectedDate != null) status = true;
				else { status = false; MessageBox.Show("Date not choised"); status = false;  goto exit; }
			} // Check Block
			exit:
			return status;
		}
		public void ClearInput(ref  TextBox type,  TextBox diametr, TextBox length, TextBox cutlength, TextBox cornerradius, TextBox quantity)
		{
			type.Text = ""; length.Text = diametr.Text = cutlength.Text = cornerradius.Text = quantity.Text = "";
		}
		#region Events
		public  void OnlyDigitsPreviewTextInput (TextCompositionEventArgs e)
		{
			short value; if (!Int16.TryParse(e.Text, out value)) e.Handled = true;
		}
		public  void OnlyLettersPreviewTextInput (TextCompositionEventArgs e)
		{
			short value; if (Int16.TryParse(e.Text, out value)) e.Handled = true;
		}
		#endregion Events
	}// class
}// namespace
