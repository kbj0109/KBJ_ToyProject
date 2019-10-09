namespace KBJ_GetFilesByDate
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
            this.dateTimePickerStart = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerEnd = new System.Windows.Forms.DateTimePicker();
            this.buttonStart = new System.Windows.Forms.Button();
            this.chkLog = new System.Windows.Forms.CheckBox();
            this.chkResult = new System.Windows.Forms.CheckBox();
            this.txtLogPath = new System.Windows.Forms.TextBox();
            this.txtResultPath = new System.Windows.Forms.TextBox();
            this.buttonResultPath = new System.Windows.Forms.Button();
            this.buttonLogPath = new System.Windows.Forms.Button();
            this.labelLogPath = new System.Windows.Forms.Label();
            this.labelResultPath = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // dateTimePickerStart
            // 
            this.dateTimePickerStart.Font = new System.Drawing.Font("Gulim", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.dateTimePickerStart.Location = new System.Drawing.Point(31, 96);
            this.dateTimePickerStart.Name = "dateTimePickerStart";
            this.dateTimePickerStart.Size = new System.Drawing.Size(354, 29);
            this.dateTimePickerStart.TabIndex = 0;
            this.dateTimePickerStart.ValueChanged += new System.EventHandler(this.dateTimePickerStart_ValueChanged);
            // 
            // dateTimePickerEnd
            // 
            this.dateTimePickerEnd.Font = new System.Drawing.Font("Gulim", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.dateTimePickerEnd.Location = new System.Drawing.Point(427, 96);
            this.dateTimePickerEnd.Name = "dateTimePickerEnd";
            this.dateTimePickerEnd.Size = new System.Drawing.Size(354, 29);
            this.dateTimePickerEnd.TabIndex = 1;
            this.dateTimePickerEnd.ValueChanged += new System.EventHandler(this.dateTimePickerEnd_ValueChanged);
            // 
            // buttonStart
            // 
            this.buttonStart.Font = new System.Drawing.Font("Gulim", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.buttonStart.Location = new System.Drawing.Point(326, 144);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(455, 35);
            this.buttonStart.TabIndex = 2;
            this.buttonStart.Text = "Get All Files In Time";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // chkLog
            // 
            this.chkLog.AutoSize = true;
            this.chkLog.Checked = true;
            this.chkLog.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkLog.Font = new System.Drawing.Font("Gulim", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chkLog.Location = new System.Drawing.Point(149, 151);
            this.chkLog.Name = "chkLog";
            this.chkLog.Size = new System.Drawing.Size(58, 23);
            this.chkLog.TabIndex = 3;
            this.chkLog.Text = "Log";
            this.chkLog.UseVisualStyleBackColor = true;
            // 
            // chkResult
            // 
            this.chkResult.AutoSize = true;
            this.chkResult.Checked = true;
            this.chkResult.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkResult.Font = new System.Drawing.Font("Gulim", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chkResult.Location = new System.Drawing.Point(234, 151);
            this.chkResult.Name = "chkResult";
            this.chkResult.Size = new System.Drawing.Size(77, 23);
            this.chkResult.TabIndex = 4;
            this.chkResult.Text = "Result";
            this.chkResult.UseVisualStyleBackColor = true;
            // 
            // txtLogPath
            // 
            this.txtLogPath.BackColor = System.Drawing.Color.White;
            this.txtLogPath.Font = new System.Drawing.Font("Gulim", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtLogPath.Location = new System.Drawing.Point(154, 12);
            this.txtLogPath.Name = "txtLogPath";
            this.txtLogPath.Size = new System.Drawing.Size(595, 29);
            this.txtLogPath.TabIndex = 5;
            this.txtLogPath.TextChanged += new System.EventHandler(this.txtLogPath_TextChanged);
            // 
            // txtResultPath
            // 
            this.txtResultPath.BackColor = System.Drawing.Color.White;
            this.txtResultPath.Font = new System.Drawing.Font("Gulim", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtResultPath.Location = new System.Drawing.Point(154, 47);
            this.txtResultPath.Name = "txtResultPath";
            this.txtResultPath.Size = new System.Drawing.Size(595, 29);
            this.txtResultPath.TabIndex = 6;
            this.txtResultPath.TextChanged += new System.EventHandler(this.txtResultPath_TextChanged);
            // 
            // buttonResultPath
            // 
            this.buttonResultPath.Font = new System.Drawing.Font("Gulim", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.buttonResultPath.Location = new System.Drawing.Point(31, 47);
            this.buttonResultPath.Name = "buttonResultPath";
            this.buttonResultPath.Size = new System.Drawing.Size(117, 29);
            this.buttonResultPath.TabIndex = 8;
            this.buttonResultPath.Text = "Result Path";
            this.buttonResultPath.UseVisualStyleBackColor = true;
            this.buttonResultPath.Click += new System.EventHandler(this.buttonResultPath_Click);
            // 
            // buttonLogPath
            // 
            this.buttonLogPath.Font = new System.Drawing.Font("Gulim", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.buttonLogPath.Location = new System.Drawing.Point(31, 12);
            this.buttonLogPath.Name = "buttonLogPath";
            this.buttonLogPath.Size = new System.Drawing.Size(117, 29);
            this.buttonLogPath.TabIndex = 9;
            this.buttonLogPath.Text = "Log Path";
            this.buttonLogPath.UseVisualStyleBackColor = true;
            this.buttonLogPath.Click += new System.EventHandler(this.buttonLogPath_Click);
            // 
            // labelLogPath
            // 
            this.labelLogPath.AutoSize = true;
            this.labelLogPath.Font = new System.Drawing.Font("Gulim", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labelLogPath.ForeColor = System.Drawing.Color.Red;
            this.labelLogPath.Location = new System.Drawing.Point(755, 12);
            this.labelLogPath.Name = "labelLogPath";
            this.labelLogPath.Size = new System.Drawing.Size(26, 24);
            this.labelLogPath.TabIndex = 10;
            this.labelLogPath.Text = "X";
            // 
            // labelResultPath
            // 
            this.labelResultPath.AutoSize = true;
            this.labelResultPath.Font = new System.Drawing.Font("Gulim", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labelResultPath.ForeColor = System.Drawing.Color.Red;
            this.labelResultPath.Location = new System.Drawing.Point(755, 50);
            this.labelResultPath.Name = "labelResultPath";
            this.labelResultPath.Size = new System.Drawing.Size(26, 24);
            this.labelResultPath.TabIndex = 11;
            this.labelResultPath.Text = "X";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Gulim", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(27, 152);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 19);
            this.label1.TabIndex = 12;
            this.label1.Text = "Copy From";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Gulim", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.Location = new System.Drawing.Point(394, 103);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(19, 19);
            this.label2.TabIndex = 13;
            this.label2.Text = "-";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(809, 193);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelResultPath);
            this.Controls.Add(this.labelLogPath);
            this.Controls.Add(this.buttonLogPath);
            this.Controls.Add(this.buttonResultPath);
            this.Controls.Add(this.txtResultPath);
            this.Controls.Add(this.txtLogPath);
            this.Controls.Add(this.chkResult);
            this.Controls.Add(this.chkLog);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.dateTimePickerEnd);
            this.Controls.Add(this.dateTimePickerStart);
            this.Name = "Main";
            this.Text = "Get All Data By Date";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dateTimePickerStart;
        private System.Windows.Forms.DateTimePicker dateTimePickerEnd;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.CheckBox chkLog;
        private System.Windows.Forms.CheckBox chkResult;
        private System.Windows.Forms.TextBox txtLogPath;
        private System.Windows.Forms.TextBox txtResultPath;
        private System.Windows.Forms.Button buttonResultPath;
        private System.Windows.Forms.Button buttonLogPath;
        private System.Windows.Forms.Label labelLogPath;
        private System.Windows.Forms.Label labelResultPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

