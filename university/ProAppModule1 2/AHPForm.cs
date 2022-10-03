using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ArcGIS.Desktop.Core.Geoprocessing;
using System.IO;
using ArcGIS.Core.Data;
using ArcGIS.Desktop.Core;
using ArcGIS.Desktop.Framework.Threading.Tasks;


namespace ProAppModule1
{
    public partial class AHPForm : Form
    {
        List<string> nameList = new List<string>();
        double[] rowSum;
        string[] x;
        public AHPForm()
        {
            InitializeComponent();
            this.AutoSize = true;
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
    }

        private void AHPForm_Load(object sender, EventArgs e)
        {

        }
        private void AvailableRasters_Click(object sender, EventArgs e)
        {

        }
        private void List_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        public void AddToRight_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Please select an item before starting!!");

            //TreeNode node = new TreeNode(List.SelectedItem.ToString());

            //Goal.Nodes.Add(node);
            if (List.SelectedItem == null)
            {
                return;
            }
            TreeNode node1 = Goal.TopNode.Nodes.Add(List.SelectedItem.ToString());
            node1.Tag = List.SelectedItem;
            nameList.Add(List.SelectedItem.ToString());


            Goal.ExpandAll();
            List.Items.Remove(List.SelectedItem);
            int MyNodeCount = Goal.TopNode.GetNodeCount(true);
            if (MyNodeCount >= 3)
            {
                Enter.Enabled = true;

            }
            else
                Enter.Enabled = false;



        }

        private void AddToLeft_Click(object sender, EventArgs e)
        {

            var str = Goal.SelectedNode;
            List.Items.Add(str);
            str.Remove();
            int MyNodeCount = Goal.TopNode.GetNodeCount(true);
            if (MyNodeCount >= 3)
            {
                Enter.Enabled = true;

            }
            else
                Enter.Enabled = false;

        }

        private void AddMulti_Click(object sender, EventArgs e)
        {

            //TreeNode node = new TreeNode(List.SelectedItems.ToString());

            if (List.SelectedItems == null)
            {
                return;
            }

            foreach (string Item in List.SelectedItems)
            {

                TreeNode node = Goal.TopNode.Nodes.Add(Item.ToString());
                node.Tag = Item;
                //List.Items.Remove(Item);
            }

            System.Diagnostics.Debug.WriteLine("Rohit hello" + List.SelectedItems.Count);
            int x = List.SelectedItems.Count;
            while (List.SelectedItems.Count != 0)
            {
                List.Items.Remove(List.SelectedItems[0]);

            }
            //Goal.Nodes.Add(node);
            // Goal.TopNode.Nodes.Add(node);

            Goal.ExpandAll();

            int MyNodeCount = Goal.TopNode.GetNodeCount(true);
            if (MyNodeCount >= 3)
            {
                Enter.Enabled = true; ;

            }
            else
                Enter.Enabled = false;
        }

        private void Enter_Click(object sender, EventArgs e)
        {

            //Selecting Criterion for SA
            for (int c = 0; c < Goal.TopNode.GetNodeCount(true); c++)
            {
                combo1.Items.Add(Goal.TopNode.Nodes[c].Text.ToString());
                System.Diagnostics.Debug.WriteLine(Goal.TopNode.Nodes[c].Text);
            }

            //Selecting no. of simulations
            combo2.Items.Add("2");
            combo2.Items.Add("4");
            combo2.Items.Add("6");
            combo2.Items.Add("8");
            combo2.Items.Add("10");


            dataGridView.ColumnCount = Goal.TopNode.GetNodeCount(true);
            dataGridView.RowCount = Goal.TopNode.GetNodeCount(true);

            for (int k = 0; k < Goal.TopNode.GetNodeCount(true); k++)
            {
                this.dataGridView.Columns[k].HeaderText = Goal.TopNode.Nodes[k].Text;

                this.dataGridView.Rows[k].HeaderCell.Value = Goal.TopNode.Nodes[k].Text;

                this.dataGridView.Columns[k].SortMode = DataGridViewColumnSortMode.NotSortable;
            }


            for (int j = 0; j < Goal.TopNode.GetNodeCount(true); j++)
            {
                this.dataGridView.Rows[j].Cells[j].Value = "1";

            }


        }

        private void dataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

            var ref_list = new List<string> { "1", "2", "3", "4", "5", "6", "7", "8", "9", "1/2", "1/3", "1/4", "1/5", "1/6", "1/7", "1/8", "1/9",
                "0.5", "0.3", "0.33", "0.25", "0.2", "0.16", "0.17", "0.14", "0.125", "0.12", "0.11", "0.1"};
            int r = e.RowIndex;
            int c = e.ColumnIndex;
            if (dataGridView[c, r].Value == null || !ref_list.Contains(dataGridView[c, r].Value.ToString()))
            {
                MessageBox.Show("Please enter a value from 1,2,3,4,5,6,7,8,9,1/2,1/3,1/4,1/5,1/6,1/7,1/8,1/9 before proceeding");
            }
            else
            {
                DataTable dt = new DataTable();
                dataGridView[c, r].Value = dt.Compute(dataGridView[c, r].Value.ToString(), "");
                double val = Convert.ToDouble(dataGridView[c, r].Value.ToString());
                dataGridView[r, c].Value = ((double)1 / val).ToString("0.00");
            }


        }

        private void dataGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            foreach (DataGridViewColumn dgvc in dataGridView.Columns)
            {
                dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void ratingMatrix_Click(object sender, EventArgs e)
        {

        }


        public void Cal_Click_1(object sender, EventArgs e)
        {
            string installPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string toolboxPath = Path.Combine(installPath, "Calculator.pyt\\Tool");

            //IEnumerable<string> myStrings = new List<string> { "first item", "second item", "bybyb"};
            //IGPResult ret = (IGPResult)result;
            string column_names = "";
            for (var i = 0; i < dataGridView.ColumnCount; i++)
            {
                column_names += Convert.ToString(dataGridView.Columns[i].HeaderText) + ",";
            }
            column_names = column_names.Remove(column_names.Length - 1);

            string text_result = "";
            for (var i = 0; i < dataGridView.RowCount; i++)
            {
                for (var j = 0; j < dataGridView.ColumnCount; j++)
                {
                    text_result += Convert.ToString(dataGridView[j, i].Value) + ",";
                }
                text_result = text_result.Remove(text_result.Length - 1);
                text_result += "\r\n";
            }
            text_result.Remove(text_result.Length - 4);
            string table_path = this.ComboBoxInput.Text; // Convert.ToString(ComboBoxInput.Text);
            string table_path2 = this.ComboBoxInput2.Text;
            //C:\\Users\\Administrator\\Documents\\ArcGIS\\Projects\\MyProject\\MyProject.gdb\\test_calc_from_csv -- test_calc_from_csv
            //C:\Users\Administrator\Desktop\BRIC Index data_Charf\BRIC Index data_Charf.shp -- coord_table
            IEnumerable<string> arguments = new List<string> { column_names, table_path, table_path2, text_result };
            var gpResult = Geoprocessing.ExecuteToolAsync(toolboxPath, arguments);

            IGPResult ProcessingResult = gpResult.Result;

            string Message_ = "";
            List<double> ParseCoord(string coordRaw)
            {
                List<double> coord = new List<double>();
                string coordClean = coordRaw.Substring(1, coordRaw.Length - 2);
                foreach (string oneCoord in coordClean.Split(','))
                {
                    coord.Add(Convert.ToDouble(oneCoord));
                    //System.Windows.MessageBox.Show(oneCoord);
                }
                return coord;
            }

            string Prettify(string table)
            {
                string text = "";
                foreach (string row in table.Split(new string[] { "\n" }, StringSplitOptions.None))
                {
                    if (String.IsNullOrEmpty(row))
                    {
                        continue;
                    }
                    var wordArray = row.Split(' ');
                    string newrow = String.Format("{0,-10} {1,-10:0.00} {2,-10:0.00} \n", wordArray[0], wordArray[1], wordArray[2]);
                    text += newrow;
                }
                return text;
            }

            foreach (IGPMessage gpMessage in ProcessingResult.Messages)
            {
                //System.Windows.MessageBox.Show("all_message" + gpMessage.Text);
                if (gpMessage.Text.Contains("cci"))
                {
                    Message_ += gpMessage.Text //Prettify(gpMessage.Text);
                    System.Windows.MessageBox.Show(Message_);
                }
                else if (gpMessage.Text.Trim().StartsWith("x:"))
                {
                    List<double> xCoord = ParseCoord(gpMessage.Text.Split(':')[1].Trim());
                    //System.Windows.MessageBox.Show(Convert.ToString(xCoord));
                }
                else if (gpMessage.Text.Trim().StartsWith("y:"))
                {
                    List<double> yCoord = ParseCoord(gpMessage.Text.Split(':')[1].Trim());
                    //System.Windows.MessageBox.Show(Convert.ToString(yCoord));
                }

            }

            // System.Windows.MessageBox.Show("last message");

            /*if (ProcessingResult.IsFailed)
            {
                string errorMessage = "";
                foreach (IGPMessage gpMessage in ProcessingResult.Messages)
                {
                    errorMessage += $"{{Error Code: {gpMessage.ErrorCode}, Text :  {gpMessage.Text} }}";
                }
                System.Windows.MessageBox.Show(errorMessage);
            }*/
            textBox1.Text = Message_;
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void ComboBoxInput_TextChanged(object sender, EventArgs e)
        {

        }

        private void ComboBoxInput_2_TextChanged(object sender, EventArgs e)
        {

        }

        private string[] ComboboxInputRange()
        {
            string installPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string toolboxPath = Path.Combine(installPath, "ListTables.pyt\\Tool");
            var gpResult = Geoprocessing.ExecuteToolAsync(toolboxPath, null);

            IGPResult ProcessingResult1 = gpResult.Result;
            List<string> TableList = new List<string>();
            //this.ComboBoxInput.Items.Add("oneTable");
            //this.ComboBoxInput2.Items.Add("oneTable");
            foreach (IGPMessage gpMessage in ProcessingResult1.Messages)
            {
                if (gpMessage.Text.Trim().Contains("tables_019A37:"))
                {

                    string TableListStr = gpMessage.Text.Split(':')[1].Trim(' ');
                    TableListStr = TableListStr.Substring(1, TableListStr.Length -2);
                    foreach (string oneTable in TableListStr.Split(','))
                    {
                        //TableList.Add(oneTable);
                        this.ComboBoxInput.Items.Add(oneTable.Trim(' ').Substring(1, oneTable.Length - 2).Replace("'", ""));
                        this.ComboBoxInput2.Items.Add(oneTable.Trim(' ').Substring(1, oneTable.Length - 2).Replace("'", "") );
                    }
                }
            }
            string[] TableArray = TableList.ToArray();
            return TableArray;

        }


    }
}
