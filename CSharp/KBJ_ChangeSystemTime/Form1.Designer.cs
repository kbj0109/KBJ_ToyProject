namespace KBJ_ChangeSystemTime
{
    partial class Form1
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
            this.changeDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.buttonChange = new System.Windows.Forms.Button();
            this.buttonBacToActualDate = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.chkCloseWithOriginalTime = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.labelResult = new System.Windows.Forms.Label();
            this.labelActualTime = new System.Windows.Forms.Label();
            this.labelTimeChange = new System.Windows.Forms.Label();
            this.labelErrorMessage = new System.Windows.Forms.Label();
            this.labelSystemTime = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // changeDateTimePicker
            // 
            this.changeDateTimePicker.Location = new System.Drawing.Point(90, 16);
            this.changeDateTimePicker.Name = "changeDateTimePicker";
            this.changeDateTimePicker.Size = new System.Drawing.Size(142, 21);
            this.changeDateTimePicker.TabIndex = 2;
            this.changeDateTimePicker.Value = new System.DateTime(2018, 7, 10, 0, 0, 0, 0);
            // 
            // buttonChange
            // 
            this.buttonChange.Location = new System.Drawing.Point(249, 14);
            this.buttonChange.Name = "buttonChange";
            this.buttonChange.Size = new System.Drawing.Size(75, 23);
            this.buttonChange.TabIndex = 0;
            this.buttonChange.Text = "Change";
            this.buttonChange.UseVisualStyleBackColor = true;
            this.buttonChange.Click += new System.EventHandler(this.buttonChange_Click);
            // 
            // buttonBacToActualDate
            // 
            this.buttonBacToActualDate.Location = new System.Drawing.Point(330, 14);
            this.buttonBacToActualDate.Name = "buttonBacToActualDate";
            this.buttonBacToActualDate.Size = new System.Drawing.Size(145, 23);
            this.buttonBacToActualDate.TabIndex = 1;
            this.buttonBacToActualDate.Text = "Back To Actual Date";
            this.buttonBacToActualDate.UseVisualStyleBackColor = true;
            this.buttonBacToActualDate.Click += new System.EventHandler(this.buttonBacToActualDate_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 12);
            this.label2.TabIndex = 5;
            // 
            // chkCloseWithOriginalTime
            // 
            this.chkCloseWithOriginalTime.AutoSize = true;
            this.chkCloseWithOriginalTime.Checked = true;
            this.chkCloseWithOriginalTime.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCloseWithOriginalTime.Location = new System.Drawing.Point(249, 52);
            this.chkCloseWithOriginalTime.Name = "chkCloseWithOriginalTime";
            this.chkCloseWithOriginalTime.Size = new System.Drawing.Size(160, 16);
            this.chkCloseWithOriginalTime.TabIndex = 6;
            this.chkCloseWithOriginalTime.Text = "Close with Original Date";
            this.chkCloseWithOriginalTime.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "Change To";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 53);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "Actual Date";
            // 
            // labelResult
            // 
            this.labelResult.AutoSize = true;
            this.labelResult.Font = new System.Drawing.Font("Gulim", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labelResult.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.labelResult.Location = new System.Drawing.Point(245, 71);
            this.labelResult.Name = "labelResult";
            this.labelResult.Size = new System.Drawing.Size(78, 19);
            this.labelResult.TabIndex = 12;
            this.labelResult.Text = "RESULT";
            // 
            // labelActualTime
            // 
            this.labelActualTime.AutoSize = true;
            this.labelActualTime.Font = new System.Drawing.Font("Gulim", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labelActualTime.Location = new System.Drawing.Point(86, 52);
            this.labelActualTime.Name = "labelActualTime";
            this.labelActualTime.Size = new System.Drawing.Size(88, 13);
            this.labelActualTime.TabIndex = 13;
            this.labelActualTime.Text = "Actual Date";
            // 
            // labelTimeChange
            // 
            this.labelTimeChange.AutoSize = true;
            this.labelTimeChange.Font = new System.Drawing.Font("Gulim", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labelTimeChange.Location = new System.Drawing.Point(327, 75);
            this.labelTimeChange.Name = "labelTimeChange";
            this.labelTimeChange.Size = new System.Drawing.Size(90, 13);
            this.labelTimeChange.TabIndex = 14;
            this.labelTimeChange.Text = "Time Change";
            // 
            // labelErrorMessage
            // 
            this.labelErrorMessage.AutoSize = true;
            this.labelErrorMessage.Font = new System.Drawing.Font("Gulim", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labelErrorMessage.ForeColor = System.Drawing.Color.Red;
            this.labelErrorMessage.Location = new System.Drawing.Point(87, 100);
            this.labelErrorMessage.Name = "labelErrorMessage";
            this.labelErrorMessage.Size = new System.Drawing.Size(102, 15);
            this.labelErrorMessage.TabIndex = 15;
            this.labelErrorMessage.Text = "Error Message";
            // 
            // labelSystemTime
            // 
            this.labelSystemTime.AutoSize = true;
            this.labelSystemTime.Font = new System.Drawing.Font("Gulim", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labelSystemTime.Location = new System.Drawing.Point(86, 75);
            this.labelSystemTime.Name = "labelSystemTime";
            this.labelSystemTime.Size = new System.Drawing.Size(96, 13);
            this.labelSystemTime.TabIndex = 17;
            this.labelSystemTime.Text = "System Date";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 76);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 12);
            this.label5.TabIndex = 16;
            this.label5.Text = "System Date";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(560, 142);
            this.Controls.Add(this.labelSystemTime);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.labelErrorMessage);
            this.Controls.Add(this.labelTimeChange);
            this.Controls.Add(this.labelActualTime);
            this.Controls.Add(this.labelResult);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.chkCloseWithOriginalTime);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonBacToActualDate);
            this.Controls.Add(this.buttonChange);
            this.Controls.Add(this.changeDateTimePicker);
            this.Name = "Form1";
            this.Text = "Change Your System Time";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker changeDateTimePicker;
        private System.Windows.Forms.Button buttonChange;
        private System.Windows.Forms.Button buttonBacToActualDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkCloseWithOriginalTime;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelResult;
        private System.Windows.Forms.Label labelActualTime;
        private System.Windows.Forms.Label labelTimeChange;
        private System.Windows.Forms.Label labelErrorMessage;
        private System.Windows.Forms.Label labelSystemTime;
        private System.Windows.Forms.Label label5;
    }
}

