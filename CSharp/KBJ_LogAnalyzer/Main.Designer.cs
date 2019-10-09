namespace KBJ_LogAnalyzer
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonAlignLog = new System.Windows.Forms.Button();
            this.textBoxFilePath = new System.Windows.Forms.TextBox();
            this.RTB = new System.Windows.Forms.RichTextBox();
            this.chkOpenIE = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.buttonAlignLog, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.textBoxFilePath, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.RTB, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.chkOpenIE, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Font = new System.Drawing.Font("Gulim", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(800, 450);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // buttonAlignLog
            // 
            this.buttonAlignLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonAlignLog.Font = new System.Drawing.Font("Gulim", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.buttonAlignLog.Location = new System.Drawing.Point(153, 418);
            this.buttonAlignLog.Name = "buttonAlignLog";
            this.buttonAlignLog.Size = new System.Drawing.Size(644, 29);
            this.buttonAlignLog.TabIndex = 10;
            this.buttonAlignLog.Text = "Align Log";
            this.buttonAlignLog.UseVisualStyleBackColor = true;
            this.buttonAlignLog.Click += new System.EventHandler(this.buttonAlignLog_Click);
            // 
            // textBoxFilePath
            // 
            this.textBoxFilePath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxFilePath.Font = new System.Drawing.Font("Gulim", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textBoxFilePath.Location = new System.Drawing.Point(153, 3);
            this.textBoxFilePath.Name = "textBoxFilePath";
            this.textBoxFilePath.Size = new System.Drawing.Size(644, 26);
            this.textBoxFilePath.TabIndex = 2;
            // 
            // RTB
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.RTB, 2);
            this.RTB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RTB.Font = new System.Drawing.Font("Gulim", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.RTB.Location = new System.Drawing.Point(3, 38);
            this.RTB.Name = "RTB";
            this.RTB.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
            this.RTB.Size = new System.Drawing.Size(794, 374);
            this.RTB.TabIndex = 0;
            this.RTB.Text = "";
            // 
            // chkOpenIE
            // 
            this.chkOpenIE.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkOpenIE.AutoSize = true;
            this.chkOpenIE.Checked = true;
            this.chkOpenIE.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkOpenIE.Font = new System.Drawing.Font("Gulim", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chkOpenIE.Location = new System.Drawing.Point(3, 418);
            this.chkOpenIE.Name = "chkOpenIE";
            this.chkOpenIE.Size = new System.Drawing.Size(144, 29);
            this.chkOpenIE.TabIndex = 5;
            this.chkOpenIE.Text = "Open with IE";
            this.chkOpenIE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkOpenIE.UseVisualStyleBackColor = true;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Main";
            this.Text = "MES Log Analyzer";
            this.Load += new System.EventHandler(this.Main_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox textBoxFilePath;
        private System.Windows.Forms.RichTextBox RTB;
        private System.Windows.Forms.CheckBox chkOpenIE;
        private System.Windows.Forms.Button buttonAlignLog;
    }
}

