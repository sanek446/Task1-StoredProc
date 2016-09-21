using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
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
using Excel = Microsoft.Office.Interop.Excel;


namespace Task1WithUC
{
    /// <summary>
    /// Interaction logic for UC.xaml
    /// </summary>
    public partial class UC : UserControl
    {

        string serverConn = "";
        SqlConnection connection;        
        string tableName = "";        
        List<string> headerNames = new List<string>();
        List<string> headerTypes = new List<string>();
        int i, j;
        int fontSize = 11;
        string font = "Segoe UI";
        DataTable table = new DataTable("Table");        
        DataRow addedRow;
                
        private ObservableCollection<CheckedListItem<string>> customerFilters = new ObservableCollection<CheckedListItem<string>>();
        private CollectionViewSource viewSource = new CollectionViewSource();        
        private ObservableCollection<CheckedListItem<string>>[] filterArray = new ObservableCollection<CheckedListItem<string>>[50];

        public UC()
        {
            InitializeComponent();

            List<int> data = new List<int>();
            data.Add(9);
            data.Add(10);
            data.Add(11);
            data.Add(12);
            data.Add(14);
            data.Add(16);
            data.Add(18);
            data.Add(20);
            data.Add(22);
            data.Add(24);
            data.Add(36);
            data.Add(48);

            comboBox.ItemsSource = data;
            comboBox.SelectedIndex = 2;                       

        }

        private void comboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            font = comboBox1.SelectedValue.ToString();
            dataGrid.FontFamily = new FontFamily(font);
        }
                
        public void getData(string conString, string storedProcedure)
        {
            serverConn = conString;
            if (serverConn == "")
            {
                MessageBox.Show("Connection string is empty");
                return;
            }

            clearOldData();

            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(serverConn);
            using (connection = new SqlConnection(builder.ConnectionString))
            {
               try
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand();
                    SqlDataReader reader;

                    //how to get table/tables name???
                    tableName = getTableName("select * from table");
                    // if (tableName == "")
                    //    return;
                    tableName = "tableName";


                    cmd.CommandText = storedProcedure;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = connection;

                    reader = cmd.ExecuteReader();

                    var tableSchema = reader.GetSchemaTable();

                    int n = 0;
                    // Each row in the table schema describes a column
                    foreach (DataRow row in tableSchema.Rows)
                    {
                        headerNames.Add(row["ColumnName"].ToString());
                        headerTypes.Add(row["DataType"].ToString());

                        //dinamically create columns in Table
                        DataColumn fNameColumn = new DataColumn();
                        //fNameColumn.DataType = System.Type.GetType("System.String");
                        //fNameColumn.DataType = System.Type.GetType(row["DataType"].ToString());
                        fNameColumn.ColumnName = row["ColumnName"].ToString();
                        fNameColumn.DefaultValue = row["ColumnName"].ToString();
                        table.Columns.Add(fNameColumn);

                        //also add column in dataGrid and filter button                        
                        DataGridTextColumn col = new DataGridTextColumn();
                        col.Binding = new Binding(row["ColumnName"].ToString());
                        var spHeader = new StackPanel() { Orientation = Orientation.Horizontal };
                        spHeader.Children.Add(new TextBlock(new Run(row["ColumnName"].ToString() + "  ")));
                        var button = new Button();
                        button.Click += Button_Filter_Click;
                        button.Height = 12;
                        button.Width = 12;                        
                        button.Name = "F" + n;
                        n++;
                        button.Content = new Image
                        {
                            Source = new BitmapImage(new Uri("/Images/filter.png", UriKind.Relative)),
                            Stretch = Stretch.Fill,
                            VerticalAlignment = VerticalAlignment.Center
                        };

                        spHeader.Children.Add(button);
                        col.Header = spHeader;
                        dataGrid.Columns.Add(col);
                    }

                    while (reader.Read())
                    {
                        addedRow = table.NewRow();
                        for (i = 0; i < reader.FieldCount; i++)
                            addedRow[headerNames[i]] = reader[i];

                    table.Rows.Add(addedRow);
                    }
                    reader.Close();

                    dataGrid.AutoGenerateColumns = false;
                    dataGrid.ItemsSource = table.DefaultView;
                    dataGrid.CanUserDeleteRows = false;
                    dataGrid.CanUserAddRows = false;
                    dataGrid.FontSize = fontSize;
                    dataGrid.FontFamily = new FontFamily(font);

                    refillFilters();
                    blockColumn(0);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }

        }

        public void returnData(string someString)
        {            
            Console.WriteLine(someString);
        }

        private void Button_Filter_Click(object sender, RoutedEventArgs e)
        {
            Button myButton = (Button) sender;
            //Console.WriteLine(myButton.Name);

            string mystring = myButton.Name.Substring(myButton.Name.Length - 1);
            lstCountries.ItemsSource = filterArray[Convert.ToInt32(mystring)];
            popCountry.IsOpen = true;
        }
             
        private string getTableName(string strCommand, string word = "FROM")
        {
            //get list of all tables from DB
            DataTable tbls = connection.GetSchema("Tables");

            if (word == "UPDATE")
                word = word + " ";
            else
                word = " " + word + " ";

            string parsedTableName = strCommand.ToLower().Substring(strCommand.ToLower().IndexOf(word.ToLower())).Split(new char[0], StringSplitOptions.RemoveEmptyEntries)[1];

            foreach (DataRow row in tbls.Rows)
            {
                //take the table name                        
                tableName = row["TABLE_NAME"].ToString();
                if (tableName.ToLower() == parsedTableName.ToLower())
                    return tableName;
            }

            Console.WriteLine("There is no such table");
            return "";
        }

        private void buttonChoose_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".xlsx, .xls";
            dlg.Filter = "(*.xlsx)|*.xlsx|(*.xls)|*.xls";

            
            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                string fileExcel = dlg.FileName;

                if (this.dataGrid.Columns.Count == 0)
                {
                    MessageBox.Show("Table is empty", "Error");
                    return;
                }

                string conditionStr, key, value;
                Excel.Application xlApp;
                Excel.Workbook xlWorkBook;
                Excel.Worksheet xlWorkSheet;
                object misValue = System.Reflection.Missing.Value;
                xlApp = new Excel.Application();
                xlWorkBook = xlApp.Workbooks.Open(fileExcel, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

                var lastCell = xlWorkSheet.Cells.SpecialCells(Microsoft.Office.Interop.Excel.XlCellType.xlCellTypeLastCell);
                string[,] list = new string[lastCell.Column, lastCell.Row];

                for (int i = 0; i < (int)lastCell.Row; i++)
                {
                    addedRow = table.NewRow();
                    conditionStr = "";
                    for (int j = 0; j < (int)lastCell.Column; j++)
                    {
                        key = headerNames[j];
                        value = xlWorkSheet.Cells[i + 1, j + 1].Text.ToString();

                        addedRow[key] = value;

                        //if value is Double then we must change , on .
                        if (headerTypes[j] == "System.Double")
                            value = value.Replace(',', '.');

                        //for DateTime fields convert 24.08.2016 13:35:37 => 2016-08-24 13:35:37
                        if (headerTypes[j] == "System.DateTime")
                            value = DateTime.ParseExact(value, "dd.MM.yyyy HH:mm:ss", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                        
                        conditionStr = conditionStr + "'" + value + "', ";

                    }
                    table.Rows.Add(addedRow);

                    //delete last comma
                    conditionStr = conditionStr.Substring(0, conditionStr.Length - 2);
                                        
                    returnData("INSERT INTO " + tableName + " VALUES (" + conditionStr + ")");                   
                }

              // dataGrid.ItemsSource = table.DefaultView;
                            
                
                xlWorkBook.Close(true, misValue, misValue);
                xlApp.Quit();
                GC.Collect();
            }

            //refill filters with new values
            refillFilters();
        }

        private void buttonFind_Click(object sender, RoutedEventArgs e)
        {
           for (int i = 0; i < dataGrid.Items.Count; i++)
            {
                DataGridRow row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(i);                
              (dataGrid.ItemContainerGenerator.ContainerFromIndex(i) as DataGridRow).Background = Brushes.White;
            }      


            for (int i = 0; i < dataGrid.Items.Count; i++)
            {
                DataGridRow row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(i);
                if (row != null)
                {
                    for (int j = 0; j < dataGrid.Columns.Count; j++)
                    {

                        TextBlock cellContent = dataGrid.Columns[j].GetCellContent(row) as TextBlock;

                        if (cellContent.Text.ToLower().Contains(textBox.Text.ToLower()))
                            (dataGrid.ItemContainerGenerator.ContainerFromIndex(i) as DataGridRow).Background = Brushes.Green;

                    }
                }
            }

        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (textBox.Text == "")
                for (int i = 0; i < dataGrid.Items.Count; i++)
                {
                    DataGridRow row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(i);                    
                        (dataGrid.ItemContainerGenerator.ContainerFromIndex(i) as DataGridRow).Background = Brushes.White;
                }

        }

        private void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            deleteSelectedRows();
        }

        private void deleteSelectedRows()
        {
            string sMessageBoxText = "Do you want to delete selected rows?";
            string sCaption = "";

            MessageBoxButton btnMessageBox = MessageBoxButton.YesNo;
            MessageBoxImage icnMessageBox = MessageBoxImage.Question;

            MessageBoxResult rsltMessageBox = MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);

            switch (rsltMessageBox)
            {
                case MessageBoxResult.Yes:

                    while (dataGrid.SelectedItems.Count > 0)
                    {
                        var row = table.Rows[dataGrid.SelectedIndex];

                        //delete from DB                
                        returnData("DELETE FROM " + tableName + " WHERE " + table.Columns[0].ToString() + " = " + row[0].ToString());

                        //delete from dataGrid
                        table.Rows.RemoveAt(dataGrid.SelectedIndex);
                    }

                    break;

                case MessageBoxResult.No:
                    break;
            }
        }
               
        private void dataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
                        
            var row = table.Rows[dataGrid.SelectedIndex];
            string conditionStr = "";
            string key = "";
            string value = "";
            
            var newValue = e.EditingElement as TextBox;

            key = e.Column.SortMemberPath.ToString();
            ///key = e.Column.Header.ToString();            
            value = newValue.Text;

            //if value is Double then we must change , on .
            if (headerTypes[e.Column.DisplayIndex].ToString() == "System.Double")
                value = value.Replace(',', '.');

            //for DateTime fields convert 24.08.2016 13:35:37 => 2016-08-24 13:35:37
            if (headerTypes[e.Column.DisplayIndex].ToString() == "System.DateTime")
            {
                try
                {
                    value = DateTime.ParseExact(value, "dd.MM.yyyy HH:mm:ss", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                }
                catch
                {
                    value = DateTime.ParseExact(value, "dd.MM.yyyy H:mm:ss", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                }
            }
            
            conditionStr = key + "='" + value + "' WHERE " + headerNames[0] + "='" + row[0].ToString() + "'";
            returnData("UPDATE " + tableName + " SET " + conditionStr);            
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            fontSize = Int32.Parse(comboBox.SelectedValue.ToString());
            dataGrid.FontSize = fontSize;
        }

        private void popCountry_Closed(object sender, EventArgs e)
        {
            string str = "(1=1) ";

            for (i = 0; i < filterArray.Count(); i++)
            {
                if (filterArray[i] == null)
                    break;

                if (isFilterAllChecked(filterArray[i]))
                    continue;

                str = str + " AND (";
                foreach (var data in filterArray[i])
                {
                    if (data.IsChecked)
                        str = str + headerNames[i] + " = '" + data.Item + "' OR ";
                }

                str = str.Substring(0, str.Length - 4);
                str = str + ")";
            }

            //Console.WriteLine(str);
            var dv = table.DefaultView;
            dv.RowFilter = str;
        }

        public void blockColumn(int columnNumber)
        {
            //here we forbid to edit some columns            
            dataGrid.Columns[columnNumber].IsReadOnly = true;            
        }
        
        private bool isFilterAllChecked<T>(ObservableCollection<T> customerFilters) where T : CheckedListItem<string>

{
int n = 0;

foreach (var data in customerFilters)
{
    if (data.IsChecked)
        n++;
}

if (n == customerFilters.Count)
    return true;
else
    return false;
}

        private void refillFilters()
        {

            for (i = 0; i < filterArray.Count(); i++)
                filterArray[i] = null;

            //fill popup checked lists                    
            for (i = 0; i < table.Columns.Count; i++)
            {
                filterArray[i] = new ObservableCollection<CheckedListItem<string>>();
                for (j = 0; j < table.Rows.Count; j++)
                {
                    var row = table.Rows[j];
                    if (!filterArray[i].Contains(new CheckedListItem<string> { Item = row[i].ToString(), IsChecked = true }))
                        filterArray[i].Add(new CheckedListItem<string> { Item = row[i].ToString(), IsChecked = true });
                }
            }
        }

        private void clearOldData()
        {
            //clear old data
            table.Clear();
            table.Columns.Clear();
            headerNames.Clear();
            headerTypes.Clear();
            dataGrid.ItemsSource = null;
            dataGrid.Columns.Clear();

            for (i = 0; i < filterArray.Count(); i++)
                filterArray[i] = null;

            var dv = table.DefaultView;
            dv.RowFilter = "";
        }
    }



internal class CheckedListItem<T> : INotifyPropertyChanged
{
public event PropertyChangedEventHandler PropertyChanged;

private bool isChecked;
private T item;

public CheckedListItem()
{ }

public CheckedListItem(T item, bool isChecked = false)
{
this.item = item;
this.isChecked = isChecked;
}

public T Item
{
get { return item; }
set
{
    item = value;
    if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Item"));
}
}

public override bool Equals(object obj)
{
if (obj == null || !(obj is CheckedListItem<T>))
    return false;

return (obj as CheckedListItem<T>).item.Equals(item);
}

public override int GetHashCode()
{
return item.GetHashCode();
}

public bool IsChecked
{
get { return isChecked; }
set
{
    isChecked = value;
    if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("IsChecked"));
}
}
}

}
