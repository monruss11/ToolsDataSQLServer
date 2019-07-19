using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace ToolsDataSQLServer
{
	public static class clsSQLServerToDataSet
	{
		    private static string ConnectionString ="Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MillToolsDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";  
			private static SqlDataReader reader = null;   
			internal static SqlConnection conn =   new SqlConnection(ConnectionString);   
			internal static SqlCommand cmd = null;  
			internal static string  sql = null;
			internal static DataSet  dataSet = new DataSet();
			internal static SqlDataAdapter sqldataAdapter = new SqlDataAdapter("select * from  Tools_Table", conn);
#region SQL Command     
		    static int sqlMaxID()
			{	
				if (conn.State == ConnectionState.Open)  conn.Close();  
				conn.Open();
				string sqlMaxID = "select max(ID) from Tools_Table  "; 	cmd = new SqlCommand(sqlMaxID, conn);
				return  (int)cmd.ExecuteScalar();	
			}
			static int sqlCountRows()
			{
				string sqlCountRows = "select count(*) from dbo.Tools_Table"; cmd = new SqlCommand(sqlCountRows, conn);
				int rowcount =(int) cmd.ExecuteScalar();
				return rowcount;
			}
#endregion SQL Command
		    private static void UpdateDB()
			{
				SqlCommandBuilder comandbuilder = new SqlCommandBuilder(sqldataAdapter);
				sqldataAdapter.Update(dataSet.Tables[0]);
			}
			internal static void AddItemToDataSet (TextBox type,TextBox diametr,TextBox length,TextBox cutlength,TextBox cornerradius,TextBox quantity,DatePicker date)
			{
			//conn.Open();
				int i = dataSet.Tables.Count;
			if (i > 0)
			{
				DataTable toolstable = dataSet.Tables[0];
				DataRow newtoolrow = toolstable.NewRow();

				newtoolrow["ID"] = dataSet.Tables[0].Rows.Count + 1;
				newtoolrow["Type"] = type.Text; newtoolrow["Diametr"] = Int32.Parse(diametr.Text); newtoolrow["Length"] = Int32.Parse(length.Text);
				newtoolrow["Cut_Length"] = Int32.Parse(cutlength.Text); newtoolrow["Corner_Radius"] = Math.Round(float.Parse(cornerradius.Text), 2);
				newtoolrow["Amount"] = Int32.Parse(quantity.Text);
				string date_added = date.SelectedDate.Value.Day.ToString() + "/" + date.SelectedDate.Value.Month.ToString() + "/" + date.SelectedDate.Value.Year.ToString();
				newtoolrow["Date_Added"] = date_added;
				toolstable.Rows.Add(newtoolrow);
				dataSet.AcceptChanges();
				sqldataAdapter.Update(dataSet, "Tools_Table");
			}
			else { MessageBox.Show("Data is Empty \n\r Connect to Data Base");  }
		}
			internal static void AddItemToSqlDataBase (TextBox type,TextBox diametr,TextBox length,TextBox cutlength,TextBox cornerradius,TextBox quantity,DatePicker date)
			{
				if (conn.State == ConnectionState.Open)  conn.Close();  
				conn.Open();
				int index;   
				index = dataSet.Tables["Tools_Table"].Rows.Count;            
   
				try
				{
					if (index ==0) index = 1; else index=sqlCountRows()+1;
					sql = "INSERT INTO dbo.Tools_Table( ID,Type, Diametr, Length, Cut_Length, Corner_Radius, Amount,Date_Added) " +
						"VALUES (@id,@type, @diametr,@length,@cutlength,@cornerradius,@quantity,@date) ";
					cmd = new SqlCommand(sql, conn);
				// Adding records the table  
					
					cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int)); 	cmd.Parameters["@id"].Value = index;
					cmd.Parameters.Add(new SqlParameter("@type", SqlDbType.Text)); 	cmd.Parameters["@type"].Value = type.Text;
					cmd.Parameters.Add(new SqlParameter("@diametr", SqlDbType.Int)); 	cmd.Parameters["@diametr"].Value = Int32.Parse(diametr.Text);
					cmd.Parameters.Add(new SqlParameter("@length", SqlDbType.Int)); 	cmd.Parameters["@length"].Value = Int32.Parse(length.Text);
					cmd.Parameters.Add(new SqlParameter("@cutlength", SqlDbType.Int)); 	cmd.Parameters["@cutlength"].Value = Int32.Parse(cutlength.Text);
					cmd.Parameters.Add(new SqlParameter("@cornerradius", SqlDbType.Float)); 	cmd.Parameters["@cornerradius"].Value = float.Parse(cornerradius.Text);
					cmd.Parameters.Add(new SqlParameter("@quantity", SqlDbType.Int)); 	cmd.Parameters["@quantity"].Value = Int32.Parse(quantity.Text);
					string dateadded = date.SelectedDate.Value.Day.ToString()+"/"+date.SelectedDate.Value.Month.ToString()+
						"/"+date.SelectedDate.Value.Year.ToString();
					cmd.Parameters.Add(new SqlParameter("@date", SqlDbType.Text)); 	cmd.Parameters["@date"].Value =dateadded;
					 	cmd.ExecuteNonQuery();
				}
				catch (SqlException ae) { MessageBox.Show(ae.Message.ToString()); }
			conn.Close();
		    } // AddItemToSqlDataBase
			internal static void AddCollectionItemsToSqlDataBase (/*TextBox type,TextBox diametr,TextBox length,TextBox cutlength,TextBox cornerradius,TextBox quantity,DatePicker date*/)
			{
				if (conn.State == ConnectionState.Open)  conn.Close();  
				conn.Open();
				int index;   index = sqlCountRows();         
				sql = "INSERT INTO dbo.Tools_Table( ID,Type, Diametr, Length, Cut_Length, Corner_Radius, Amount,Date_Added) " +
						"VALUES (@id,@type, @diametr,@length,@cutlength,@cornerradius,@quantity,@date) ";
				try
				{
					if (index ==0) index = 1; else index=sqlCountRows();
				for (int i = index; i < dataSet.Tables[0].Rows.Count; i++)
				{
					
					cmd = new SqlCommand(sql, conn);
					/// Adding records the table  

					cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int)); cmd.Parameters["@id"].Value = i+1;
					cmd.Parameters.Add(new SqlParameter("@type", SqlDbType.Text));
					cmd.Parameters["@type"].Value = dataSet.Tables[0].Rows[i].Field<string>(1) ;
					cmd.Parameters.Add(new SqlParameter("@diametr", SqlDbType.Int)); 
					cmd.Parameters["@diametr"].Value = dataSet.Tables[0].Rows[i].Field<int>(2) ;
					cmd.Parameters.Add(new SqlParameter("@length", SqlDbType.Int)); 
					cmd.Parameters["@length"].Value = dataSet.Tables[0].Rows[i].Field<int>(3) ;
					cmd.Parameters.Add(new SqlParameter("@cutlength", SqlDbType.Int)); 
					cmd.Parameters["@cutlength"].Value = dataSet.Tables[0].Rows[i].Field<int>(4) ;
					cmd.Parameters.Add(new SqlParameter("@cornerradius", SqlDbType.Float)); 
					cmd.Parameters["@cornerradius"].Value = dataSet.Tables[0].Rows[i][5] ;
					cmd.Parameters.Add(new SqlParameter("@quantity", SqlDbType.Int)); 
					cmd.Parameters["@quantity"].Value = dataSet.Tables[0].Rows[i].Field<int>(6) ;
					cmd.Parameters.Add(new SqlParameter("@date", SqlDbType.Text)); 
					cmd.Parameters["@date"].Value = dataSet.Tables[0].Rows[i].Field<string>(7) ;
					cmd.ExecuteNonQuery();
				}
				} 
				catch (SqlException ae) { MessageBox.Show(ae.Message.ToString()); }
			conn.Close();
		    } // AddCollectionItemsToSqlDataBase
			internal static void ConnectDataBase ()
			{
				if (conn.State == ConnectionState.Open)  conn.Close();  
				conn.Open();
				MessageBox.Show("Connection Open  !");
				if ( dataSet.Tables.Count!=0) dataSet.Tables[0].Clear();
				sqldataAdapter.Fill(dataSet, "Tools_Table"); 
				conn.Close();
			}
			
	} // class
}// namespace 
