namespace TransportationProject.Panels
{
    partial class PanelVehicles
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
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.dgvVehicles = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSearchVehicle = new System.Windows.Forms.Button();
            this.txtVehiclesSearchBox = new System.Windows.Forms.TextBox();
            this.btnModifyVehicle = new System.Windows.Forms.Button();
            this.btnDeleteVehicle = new System.Windows.Forms.Button();
            this.btnAddVehicle = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVehicles)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.label1, 3);
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(628, 44);
            this.label1.TabIndex = 1;
            this.label1.Text = "Vehicles";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnModifyVehicle, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnDeleteVehicle, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnAddVehicle, 2, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(634, 444);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel1.SetColumnSpan(this.tableLayoutPanel2, 3);
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.Controls.Add(this.dgvVehicles, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnSearchVehicle, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.txtVehiclesSearchBox, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 47);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(628, 349);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // dgvVehicles
            // 
            this.dgvVehicles.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvVehicles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tableLayoutPanel2.SetColumnSpan(this.dgvVehicles, 3);
            this.dgvVehicles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvVehicles.Location = new System.Drawing.Point(3, 37);
            this.dgvVehicles.Name = "dgvVehicles";
            this.dgvVehicles.Size = new System.Drawing.Size(622, 309);
            this.dgvVehicles.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(151, 34);
            this.label2.TabIndex = 1;
            this.label2.Text = "Search Vehicles";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnSearchVehicle
            // 
            this.btnSearchVehicle.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnSearchVehicle.BackColor = System.Drawing.Color.Blue;
            this.btnSearchVehicle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearchVehicle.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnSearchVehicle.Location = new System.Drawing.Point(474, 3);
            this.btnSearchVehicle.Name = "btnSearchVehicle";
            this.btnSearchVehicle.Size = new System.Drawing.Size(151, 28);
            this.btnSearchVehicle.TabIndex = 2;
            this.btnSearchVehicle.Text = "Search";
            this.btnSearchVehicle.UseVisualStyleBackColor = false;
            this.btnSearchVehicle.Click += new System.EventHandler(this.btnSearchVehicle_Click);
            // 
            // txtVehiclesSearchBox
            // 
            this.txtVehiclesSearchBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtVehiclesSearchBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVehiclesSearchBox.Location = new System.Drawing.Point(160, 3);
            this.txtVehiclesSearchBox.Multiline = true;
            this.txtVehiclesSearchBox.Name = "txtVehiclesSearchBox";
            this.txtVehiclesSearchBox.Size = new System.Drawing.Size(308, 28);
            this.txtVehiclesSearchBox.TabIndex = 3;
            // 
            // btnModifyVehicle
            // 
            this.btnModifyVehicle.BackColor = System.Drawing.Color.Silver;
            this.btnModifyVehicle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnModifyVehicle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnModifyVehicle.Location = new System.Drawing.Point(3, 402);
            this.btnModifyVehicle.Name = "btnModifyVehicle";
            this.btnModifyVehicle.Size = new System.Drawing.Size(205, 39);
            this.btnModifyVehicle.TabIndex = 3;
            this.btnModifyVehicle.Text = "Modify";
            this.btnModifyVehicle.UseVisualStyleBackColor = false;
            this.btnModifyVehicle.Click += new System.EventHandler(this.btnModifyVehicle_Click);
            // 
            // btnDeleteVehicle
            // 
            this.btnDeleteVehicle.BackColor = System.Drawing.Color.Red;
            this.btnDeleteVehicle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDeleteVehicle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteVehicle.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnDeleteVehicle.Location = new System.Drawing.Point(214, 402);
            this.btnDeleteVehicle.Name = "btnDeleteVehicle";
            this.btnDeleteVehicle.Size = new System.Drawing.Size(205, 39);
            this.btnDeleteVehicle.TabIndex = 4;
            this.btnDeleteVehicle.Text = "Delete";
            this.btnDeleteVehicle.UseVisualStyleBackColor = false;
            this.btnDeleteVehicle.Click += new System.EventHandler(this.btnDeleteVehicle_Click);
            // 
            // btnAddVehicle
            // 
            this.btnAddVehicle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnAddVehicle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAddVehicle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddVehicle.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnAddVehicle.Location = new System.Drawing.Point(425, 402);
            this.btnAddVehicle.Name = "btnAddVehicle";
            this.btnAddVehicle.Size = new System.Drawing.Size(206, 39);
            this.btnAddVehicle.TabIndex = 5;
            this.btnAddVehicle.Text = "Add";
            this.btnAddVehicle.UseVisualStyleBackColor = false;
            this.btnAddVehicle.Click += new System.EventHandler(this.btnAddVehicle_Click);
            // 
            // PanelVehicles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 444);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "PanelVehicles";
            this.Text = "PanelVehicles";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVehicles)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.DataGridView dgvVehicles;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSearchVehicle;
        private System.Windows.Forms.TextBox txtVehiclesSearchBox;
        private System.Windows.Forms.Button btnModifyVehicle;
        private System.Windows.Forms.Button btnDeleteVehicle;
        private System.Windows.Forms.Button btnAddVehicle;
    }
}