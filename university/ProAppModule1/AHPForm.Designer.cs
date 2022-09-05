
namespace ProAppModule1
{
    partial class AHPForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Goal");
            this.AvailableRasters = new System.Windows.Forms.Label();
            this.SelectedCriteria = new System.Windows.Forms.Label();
            this.TablePath1Label1 = new System.Windows.Forms.Label();
            this.TablePath1Label2 = new System.Windows.Forms.Label();
            this.About = new System.Windows.Forms.Button();
            this.AddToRight = new System.Windows.Forms.Button();
            this.Goal = new System.Windows.Forms.TreeView();
            this.Cancel = new System.Windows.Forms.Button();
            this.List = new System.Windows.Forms.ListBox();
            this.AddToLeft = new System.Windows.Forms.Button();
            this.bindingSource2 = new System.Windows.Forms.BindingSource(this.components);
            this.bindingSource3 = new System.Windows.Forms.BindingSource(this.components);
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.bindingSource4 = new System.Windows.Forms.BindingSource(this.components);
            this.bindingSource5 = new System.Windows.Forms.BindingSource(this.components);
            this.ratingMatrix = new System.Windows.Forms.Label();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.Enter = new System.Windows.Forms.Button();
            this.AddMulti = new System.Windows.Forms.Button();
            this.Cal = new System.Windows.Forms.Button();
            this.sensitivity = new System.Windows.Forms.Button();
            this.combo1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.combo2 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxInput = new System.Windows.Forms.TextBox();
            this.textBoxInput2 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // AvailableRasters
            // 
            this.AvailableRasters.AutoSize = true;
            this.AvailableRasters.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AvailableRasters.Location = new System.Drawing.Point(25, 33);
            this.AvailableRasters.Name = "AvailableRasters";
            this.AvailableRasters.Size = new System.Drawing.Size(134, 15);
            this.AvailableRasters.TabIndex = 0;
            this.AvailableRasters.Text = "Available Raster Layers";
            this.AvailableRasters.Click += new System.EventHandler(this.AvailableRasters_Click);
            // 
            // SelectedCriteria
            // 
            this.SelectedCriteria.AutoSize = true;
            this.SelectedCriteria.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SelectedCriteria.ForeColor = System.Drawing.SystemColors.ControlText;
            this.SelectedCriteria.Location = new System.Drawing.Point(394, 33);
            this.SelectedCriteria.Name = "SelectedCriteria";
            this.SelectedCriteria.Size = new System.Drawing.Size(97, 15);
            this.SelectedCriteria.TabIndex = 1;
            this.SelectedCriteria.Text = "Selected Criteria";
            // 
            // About
            // 
            this.About.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.About.Location = new System.Drawing.Point(26, 483);
            this.About.Name = "About";
            this.About.Size = new System.Drawing.Size(151, 31);
            this.About.TabIndex = 2;
            this.About.Text = "About";
            this.About.UseVisualStyleBackColor = true;
            // 
            // AddToRight
            // 
            this.AddToRight.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddToRight.Location = new System.Drawing.Point(282, 111);
            this.AddToRight.Name = "AddToRight";
            this.AddToRight.Size = new System.Drawing.Size(75, 39);
            this.AddToRight.TabIndex = 3;
            this.AddToRight.Text = ">";
            this.AddToRight.UseVisualStyleBackColor = true;
            this.AddToRight.Click += new System.EventHandler(this.AddToRight_Click);
            // 
            // Goal
            // 
            this.Goal.BackColor = System.Drawing.SystemColors.ControlLight;
            this.Goal.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Goal.Location = new System.Drawing.Point(384, 60);
            this.Goal.Name = "Goal";
            treeNode1.Name = "Goal";
            treeNode1.Text = "Goal";
            this.Goal.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
            this.Goal.Size = new System.Drawing.Size(253, 402);
            this.Goal.TabIndex = 7;
            // 
            // Cancel
            // 
            this.Cancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cancel.Location = new System.Drawing.Point(15, 395);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(184, 28);
            this.Cancel.TabIndex = 9;
            this.Cancel.Text = "Close";
            this.Cancel.UseVisualStyleBackColor = true;
            // 
            // List
            // 
            this.List.BackColor = System.Drawing.SystemColors.ControlLight;
            this.List.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.List.FormattingEnabled = true;
            this.List.ItemHeight = 12;
            this.List.Items.AddRange(new object[] {
            "Previous Land use",
            "Site size",
            "Geographic location",
            "Access",
            "Infrastructure",
            "Water risk"
            });
            this.List.Location = new System.Drawing.Point(26, 58);
            this.List.Name = "List";
            this.List.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.List.Size = new System.Drawing.Size(236, 400);
            this.List.TabIndex = 8;
            this.List.SelectedIndexChanged += new System.EventHandler(this.List_SelectedIndexChanged_1);
            // 
            // AddToLeft
            // 
            this.AddToLeft.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddToLeft.Location = new System.Drawing.Point(282, 382);
            this.AddToLeft.Name = "AddToLeft";
            this.AddToLeft.Size = new System.Drawing.Size(75, 32);
            this.AddToLeft.TabIndex = 10;
            this.AddToLeft.Text = "<";
            this.AddToLeft.UseVisualStyleBackColor = true;
            this.AddToLeft.Click += new System.EventHandler(this.AddToLeft_Click);
            // 
            // ratingMatrix
            // 
            this.ratingMatrix.AutoSize = true;
            this.ratingMatrix.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ratingMatrix.Location = new System.Drawing.Point(750, 33);
            this.ratingMatrix.Name = "ratingMatrix";
            this.ratingMatrix.Size = new System.Drawing.Size(188, 15);
            this.ratingMatrix.TabIndex = 11;
            this.ratingMatrix.Text = "Pairwise Comparison AHP Matrix";
            this.ratingMatrix.Click += new System.EventHandler(this.ratingMatrix_Click);
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(753, 131);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowTemplate.Height = 24;
            this.dataGridView.Size = new System.Drawing.Size(442, 221);
            this.dataGridView.TabIndex = 12;
            this.dataGridView.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellEndEdit);
            this.dataGridView.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView_ColumnHeaderMouseClick);
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.textBox1.Location = new System.Drawing.Point(753, 376);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(170, 100);
            this.textBox1.TabIndex = 15;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // Enter
            // 
            this.Enter.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Enter.Location = new System.Drawing.Point(650, 167);
            this.Enter.Name = "Enter";
            this.Enter.Size = new System.Drawing.Size(85, 35);
            this.Enter.TabIndex = 18;
            this.Enter.Text = "Enter";
            this.Enter.UseVisualStyleBackColor = true;
            this.Enter.Click += new System.EventHandler(this.Enter_Click);
            // 
            // AddMulti
            // 
            this.AddMulti.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddMulti.Location = new System.Drawing.Point(282, 172);
            this.AddMulti.Name = "AddMulti";
            this.AddMulti.Size = new System.Drawing.Size(75, 35);
            this.AddMulti.TabIndex = 19;
            this.AddMulti.Text = ">>>";
            this.AddMulti.UseVisualStyleBackColor = true;
            this.AddMulti.Click += new System.EventHandler(this.AddMulti_Click);
            // 
            // Cal
            // 
            this.Cal.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cal.Location = new System.Drawing.Point(976, 502);
            this.Cal.Name = "Cal";
            this.Cal.Size = new System.Drawing.Size(194, 32);
            this.Cal.TabIndex = 21;
            this.Cal.Text = "Calculate Weights";
            this.Cal.UseVisualStyleBackColor = true;
            this.Cal.Click += new System.EventHandler(this.Cal_Click_1);
            // 
            // sensitivity
            // 
            this.sensitivity.Location = new System.Drawing.Point(0, 0);
            this.sensitivity.Name = "sensitivity";
            this.sensitivity.Size = new System.Drawing.Size(75, 23);
            this.sensitivity.TabIndex = 0;
            // 
            // combo1
            // 
            this.combo1.AccessibleRole = System.Windows.Forms.AccessibleRole.Pane;
            this.combo1.FormattingEnabled = true;
            this.combo1.Location = new System.Drawing.Point(12, 69);
            this.combo1.Name = "combo1";
            this.combo1.Size = new System.Drawing.Size(187, 21);
            this.combo1.TabIndex = 23;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(750, 355);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 15);
            this.label1.TabIndex = 24;
            this.label1.Text = "Result";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 23);
            this.label2.TabIndex = 0;
            // 
            // combo2
            // 
            this.combo2.FormattingEnabled = true;
            this.combo2.Location = new System.Drawing.Point(13, 156);
            this.combo2.Name = "combo2";
            this.combo2.Size = new System.Drawing.Size(186, 21);
            this.combo2.TabIndex = 26;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 23);
            this.label3.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.SystemColors.ControlLight;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(750, 58);
            this.label4.Name = "label4";
            this.label4.Padding = new System.Windows.Forms.Padding(1);
            this.label4.Size = new System.Drawing.Size(78, 17);
            this.label4.TabIndex = 30;
            this.label4.Text = "Enter Values";
            // 
            // textBoxPath1Input
            // 
            this.textBoxInput.BackColor = System.Drawing.SystemColors.ControlLight;
            this.textBoxInput.Location = new System.Drawing.Point(976, 412);
            this.textBoxInput.Multiline = true;
            this.textBoxInput.Name = "Path1_input";
            this.textBoxInput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxInput.Size = new System.Drawing.Size(347, 25);
            this.textBoxInput.TabIndex = 23;
            this.textBoxInput.TextChanged += new System.EventHandler(this.textBoxInput_TextChanged);
            // TablePath1Label1
            // 
            this.TablePath1Label1.AutoSize = true;
            this.TablePath1Label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TablePath1Label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.TablePath1Label1.Location = new System.Drawing.Point(976, 382);
            this.TablePath1Label1.Name = "TablePath1Label1";
            this.TablePath1Label1.Size = new System.Drawing.Size(97, 15);
            this.TablePath1Label1.TabIndex = 1;
            this.TablePath1Label1.Text = "Table Criteria Path";
            // textBoxPat21Input
            // 
            this.textBoxInput2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.textBoxInput2.Location = new System.Drawing.Point(976, 472);
            this.textBoxInput2.Multiline = true;
            this.textBoxInput2.Name = "Path2_input";
            this.textBoxInput2.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxInput2.Size = new System.Drawing.Size(347, 25);
            this.textBoxInput2.TabIndex = 23;
            this.textBoxInput2.TextChanged += new System.EventHandler(this.textBoxInput_2_TextChanged);
            // TablePath1Label2
            // 
            this.TablePath1Label2.AutoSize = true;
            this.TablePath1Label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TablePath1Label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.TablePath1Label2.Location = new System.Drawing.Point(976, 442);
            this.TablePath1Label2.Name = "TablePath1Label2";
            this.TablePath1Label2.Size = new System.Drawing.Size(97, 15);
            this.TablePath1Label2.TabIndex = 1;
            this.TablePath1Label2.Text = "Table Coordinate Path";
            // 
            // AHPForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1436, 517);
            this.Controls.Add(this.textBoxInput);
            this.Controls.Add(this.textBoxInput2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Cal);
            this.Controls.Add(this.AddMulti);
            this.Controls.Add(this.Enter);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.ratingMatrix);
            this.Controls.Add(this.AddToLeft);
            this.Controls.Add(this.List);
            this.Controls.Add(this.Goal);
            this.Controls.Add(this.AddToRight);
            this.Controls.Add(this.About);
            this.Controls.Add(this.SelectedCriteria);
            this.Controls.Add(this.TablePath1Label1);
            this.Controls.Add(this.TablePath1Label2);
            this.Controls.Add(this.AvailableRasters);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "AHPForm";
            this.Text = "THE AHP ";
            this.Load += new System.EventHandler(this.AHPForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
 
        private System.Windows.Forms.Label AvailableRasters;
        private System.Windows.Forms.Label SelectedCriteria;
        private System.Windows.Forms.Label TablePath1Label1;
        private System.Windows.Forms.Label TablePath1Label2;
        private System.Windows.Forms.Button About;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.ListBox List;
        private System.Windows.Forms.Button AddToLeft;
        private System.Windows.Forms.BindingSource bindingSource2;
        private System.Windows.Forms.BindingSource bindingSource3;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.BindingSource bindingSource4;
        private System.Windows.Forms.BindingSource bindingSource5;
        public System.Windows.Forms.TreeView Goal;
        private System.Windows.Forms.Label ratingMatrix;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBoxInput;
        private System.Windows.Forms.TextBox textBoxInput2;
        private System.Windows.Forms.Button Enter;
        public System.Windows.Forms.Button AddToRight;
        private System.Windows.Forms.Button AddMulti;
        private System.Windows.Forms.Button Cal;
        private System.Windows.Forms.Button sensitivity;
        private System.Windows.Forms.ComboBox combo1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox combo2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}