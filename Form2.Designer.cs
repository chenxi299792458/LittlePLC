namespace MyPLC
{
    partial class Form2
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
            this.radioButtonStopBit2 = new System.Windows.Forms.RadioButton();
            this.groupBoxSlaveParameters = new System.Windows.Forms.GroupBox();
            this.radioButtonOff = new System.Windows.Forms.RadioButton();
            this.radioButtonOn = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxSlaveDelay = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxSlaveAddress = new System.Windows.Forms.TextBox();
            this.groupBoxParity = new System.Windows.Forms.GroupBox();
            this.radioButtonParityOdd = new System.Windows.Forms.RadioButton();
            this.radioButtonParityEven = new System.Windows.Forms.RadioButton();
            this.radioButtonParityNone = new System.Windows.Forms.RadioButton();
            this.radioButtonStopBit1 = new System.Windows.Forms.RadioButton();
            this.groupBoxStopBits = new System.Windows.Forms.GroupBox();
            this.radioButtonDataBits8 = new System.Windows.Forms.RadioButton();
            this._serialPort = new System.IO.Ports.SerialPort(this.components);
            this.radioButtonDataBits7 = new System.Windows.Forms.RadioButton();
            this.listBoxCommLog = new System.Windows.Forms.ListBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBoxDataBits = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxBaud = new System.Windows.Forms.TextBox();
            this.groupBoxConnParameters = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxSerialPorts = new System.Windows.Forms.ComboBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.groupBoxSlaveParameters.SuspendLayout();
            this.groupBoxParity.SuspendLayout();
            this.groupBoxStopBits.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBoxDataBits.SuspendLayout();
            this.groupBoxConnParameters.SuspendLayout();
            this.SuspendLayout();
            // 
            // radioButtonStopBit2
            // 
            this.radioButtonStopBit2.AutoSize = true;
            this.radioButtonStopBit2.Location = new System.Drawing.Point(15, 56);
            this.radioButtonStopBit2.Name = "radioButtonStopBit2";
            this.radioButtonStopBit2.Size = new System.Drawing.Size(59, 16);
            this.radioButtonStopBit2.TabIndex = 1;
            this.radioButtonStopBit2.Text = "2 bits";
            this.radioButtonStopBit2.UseVisualStyleBackColor = true;
            // 
            // groupBoxSlaveParameters
            // 
            this.groupBoxSlaveParameters.Controls.Add(this.radioButtonOff);
            this.groupBoxSlaveParameters.Controls.Add(this.radioButtonOn);
            this.groupBoxSlaveParameters.Controls.Add(this.label2);
            this.groupBoxSlaveParameters.Controls.Add(this.textBoxSlaveDelay);
            this.groupBoxSlaveParameters.Controls.Add(this.label1);
            this.groupBoxSlaveParameters.Controls.Add(this.textBoxSlaveAddress);
            this.groupBoxSlaveParameters.Location = new System.Drawing.Point(12, 5);
            this.groupBoxSlaveParameters.Name = "groupBoxSlaveParameters";
            this.groupBoxSlaveParameters.Size = new System.Drawing.Size(422, 92);
            this.groupBoxSlaveParameters.TabIndex = 12;
            this.groupBoxSlaveParameters.TabStop = false;
            this.groupBoxSlaveParameters.Text = "Slave Parameters";
            // 
            // radioButtonOff
            // 
            this.radioButtonOff.AutoSize = true;
            this.radioButtonOff.Checked = true;
            this.radioButtonOff.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonOff.ForeColor = System.Drawing.Color.Red;
            this.radioButtonOff.Location = new System.Drawing.Point(27, 53);
            this.radioButtonOff.Name = "radioButtonOff";
            this.radioButtonOff.Size = new System.Drawing.Size(52, 24);
            this.radioButtonOff.TabIndex = 13;
            this.radioButtonOff.TabStop = true;
            this.radioButtonOff.Text = "Off";
            this.radioButtonOff.UseVisualStyleBackColor = true;
            // 
            // radioButtonOn
            // 
            this.radioButtonOn.AutoSize = true;
            this.radioButtonOn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonOn.ForeColor = System.Drawing.Color.DarkGreen;
            this.radioButtonOn.Location = new System.Drawing.Point(27, 25);
            this.radioButtonOn.Name = "radioButtonOn";
            this.radioButtonOn.Size = new System.Drawing.Size(50, 24);
            this.radioButtonOn.TabIndex = 12;
            this.radioButtonOn.Text = "On";
            this.radioButtonOn.UseVisualStyleBackColor = true;
            this.radioButtonOn.CheckedChanged += new System.EventHandler(this.radioButtonOn_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(146, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 12);
            this.label2.TabIndex = 11;
            this.label2.Text = "Slave Delay (ms) =";
            // 
            // textBoxSlaveDelay
            // 
            this.textBoxSlaveDelay.Location = new System.Drawing.Point(263, 52);
            this.textBoxSlaveDelay.Name = "textBoxSlaveDelay";
            this.textBoxSlaveDelay.Size = new System.Drawing.Size(50, 21);
            this.textBoxSlaveDelay.TabIndex = 10;
            this.textBoxSlaveDelay.Text = "0";
            this.textBoxSlaveDelay.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxSlaveDelay_Validating);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(157, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 12);
            this.label1.TabIndex = 9;
            this.label1.Text = "Slave Address =";
            // 
            // textBoxSlaveAddress
            // 
            this.textBoxSlaveAddress.Location = new System.Drawing.Point(263, 29);
            this.textBoxSlaveAddress.Name = "textBoxSlaveAddress";
            this.textBoxSlaveAddress.Size = new System.Drawing.Size(50, 21);
            this.textBoxSlaveAddress.TabIndex = 8;
            this.textBoxSlaveAddress.Text = "1";
            this.textBoxSlaveAddress.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxSlaveAddress_Validating);
            // 
            // groupBoxParity
            // 
            this.groupBoxParity.Controls.Add(this.radioButtonParityOdd);
            this.groupBoxParity.Controls.Add(this.radioButtonParityEven);
            this.groupBoxParity.Controls.Add(this.radioButtonParityNone);
            this.groupBoxParity.Location = new System.Drawing.Point(158, 18);
            this.groupBoxParity.Name = "groupBoxParity";
            this.groupBoxParity.Size = new System.Drawing.Size(80, 95);
            this.groupBoxParity.TabIndex = 20;
            this.groupBoxParity.TabStop = false;
            this.groupBoxParity.Text = "Parity";
            // 
            // radioButtonParityOdd
            // 
            this.radioButtonParityOdd.AutoSize = true;
            this.radioButtonParityOdd.Location = new System.Drawing.Point(15, 43);
            this.radioButtonParityOdd.Name = "radioButtonParityOdd";
            this.radioButtonParityOdd.Size = new System.Drawing.Size(41, 16);
            this.radioButtonParityOdd.TabIndex = 2;
            this.radioButtonParityOdd.Text = "Odd";
            this.radioButtonParityOdd.UseVisualStyleBackColor = true;
            // 
            // radioButtonParityEven
            // 
            this.radioButtonParityEven.AutoSize = true;
            this.radioButtonParityEven.Location = new System.Drawing.Point(15, 64);
            this.radioButtonParityEven.Name = "radioButtonParityEven";
            this.radioButtonParityEven.Size = new System.Drawing.Size(47, 16);
            this.radioButtonParityEven.TabIndex = 1;
            this.radioButtonParityEven.Text = "Even";
            this.radioButtonParityEven.UseVisualStyleBackColor = true;
            // 
            // radioButtonParityNone
            // 
            this.radioButtonParityNone.AutoSize = true;
            this.radioButtonParityNone.Checked = true;
            this.radioButtonParityNone.Location = new System.Drawing.Point(15, 22);
            this.radioButtonParityNone.Name = "radioButtonParityNone";
            this.radioButtonParityNone.Size = new System.Drawing.Size(47, 16);
            this.radioButtonParityNone.TabIndex = 0;
            this.radioButtonParityNone.TabStop = true;
            this.radioButtonParityNone.Text = "None";
            this.radioButtonParityNone.UseVisualStyleBackColor = true;
            // 
            // radioButtonStopBit1
            // 
            this.radioButtonStopBit1.AutoSize = true;
            this.radioButtonStopBit1.Checked = true;
            this.radioButtonStopBit1.Location = new System.Drawing.Point(15, 27);
            this.radioButtonStopBit1.Name = "radioButtonStopBit1";
            this.radioButtonStopBit1.Size = new System.Drawing.Size(53, 16);
            this.radioButtonStopBit1.TabIndex = 0;
            this.radioButtonStopBit1.TabStop = true;
            this.radioButtonStopBit1.Text = "1 bit";
            this.radioButtonStopBit1.UseVisualStyleBackColor = true;
            // 
            // groupBoxStopBits
            // 
            this.groupBoxStopBits.Controls.Add(this.radioButtonStopBit2);
            this.groupBoxStopBits.Controls.Add(this.radioButtonStopBit1);
            this.groupBoxStopBits.Location = new System.Drawing.Point(330, 18);
            this.groupBoxStopBits.Name = "groupBoxStopBits";
            this.groupBoxStopBits.Size = new System.Drawing.Size(80, 95);
            this.groupBoxStopBits.TabIndex = 19;
            this.groupBoxStopBits.TabStop = false;
            this.groupBoxStopBits.Text = "Stop Bits";
            // 
            // radioButtonDataBits8
            // 
            this.radioButtonDataBits8.AutoSize = true;
            this.radioButtonDataBits8.Checked = true;
            this.radioButtonDataBits8.Location = new System.Drawing.Point(15, 56);
            this.radioButtonDataBits8.Name = "radioButtonDataBits8";
            this.radioButtonDataBits8.Size = new System.Drawing.Size(59, 16);
            this.radioButtonDataBits8.TabIndex = 1;
            this.radioButtonDataBits8.TabStop = true;
            this.radioButtonDataBits8.Text = "8 bits";
            this.radioButtonDataBits8.UseVisualStyleBackColor = true;
            // 
            // _serialPort
            // 
            this._serialPort.PortName = "COM6";
            // 
            // radioButtonDataBits7
            // 
            this.radioButtonDataBits7.AutoSize = true;
            this.radioButtonDataBits7.Location = new System.Drawing.Point(15, 27);
            this.radioButtonDataBits7.Name = "radioButtonDataBits7";
            this.radioButtonDataBits7.Size = new System.Drawing.Size(59, 16);
            this.radioButtonDataBits7.TabIndex = 0;
            this.radioButtonDataBits7.Text = "7 bits";
            this.radioButtonDataBits7.UseVisualStyleBackColor = true;
            // 
            // listBoxCommLog
            // 
            this.listBoxCommLog.BackColor = System.Drawing.Color.Black;
            this.listBoxCommLog.ForeColor = System.Drawing.Color.LimeGreen;
            this.listBoxCommLog.FormattingEnabled = true;
            this.listBoxCommLog.ItemHeight = 12;
            this.listBoxCommLog.Location = new System.Drawing.Point(14, 18);
            this.listBoxCommLog.Name = "listBoxCommLog";
            this.listBoxCommLog.Size = new System.Drawing.Size(396, 124);
            this.listBoxCommLog.TabIndex = 3;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.listBoxCommLog);
            this.groupBox4.Location = new System.Drawing.Point(12, 241);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(422, 154);
            this.groupBox4.TabIndex = 14;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Communication Log";
            // 
            // groupBoxDataBits
            // 
            this.groupBoxDataBits.Controls.Add(this.radioButtonDataBits8);
            this.groupBoxDataBits.Controls.Add(this.radioButtonDataBits7);
            this.groupBoxDataBits.Location = new System.Drawing.Point(244, 18);
            this.groupBoxDataBits.Name = "groupBoxDataBits";
            this.groupBoxDataBits.Size = new System.Drawing.Size(80, 95);
            this.groupBoxDataBits.TabIndex = 18;
            this.groupBoxDataBits.TabStop = false;
            this.groupBoxDataBits.Text = "Data Bits";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(36, 78);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 12;
            this.label5.Text = "Baud =";
            // 
            // textBoxBaud
            // 
            this.textBoxBaud.Location = new System.Drawing.Point(88, 75);
            this.textBoxBaud.Name = "textBoxBaud";
            this.textBoxBaud.Size = new System.Drawing.Size(58, 21);
            this.textBoxBaud.TabIndex = 11;
            this.textBoxBaud.Text = "9600";
            // 
            // groupBoxConnParameters
            // 
            this.groupBoxConnParameters.Controls.Add(this.groupBoxParity);
            this.groupBoxConnParameters.Controls.Add(this.groupBoxStopBits);
            this.groupBoxConnParameters.Controls.Add(this.groupBoxDataBits);
            this.groupBoxConnParameters.Controls.Add(this.label5);
            this.groupBoxConnParameters.Controls.Add(this.textBoxBaud);
            this.groupBoxConnParameters.Controls.Add(this.label4);
            this.groupBoxConnParameters.Controls.Add(this.comboBoxSerialPorts);
            this.groupBoxConnParameters.Location = new System.Drawing.Point(12, 102);
            this.groupBoxConnParameters.Name = "groupBoxConnParameters";
            this.groupBoxConnParameters.Size = new System.Drawing.Size(422, 133);
            this.groupBoxConnParameters.TabIndex = 13;
            this.groupBoxConnParameters.TabStop = false;
            this.groupBoxConnParameters.Text = "Connection Parameters";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 47);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "Port Name =";
            // 
            // comboBoxSerialPorts
            // 
            this.comboBoxSerialPorts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSerialPorts.FormattingEnabled = true;
            this.comboBoxSerialPorts.Location = new System.Drawing.Point(86, 44);
            this.comboBoxSerialPorts.Name = "comboBoxSerialPorts";
            this.comboBoxSerialPorts.Size = new System.Drawing.Size(58, 20);
            this.comboBoxSerialPorts.TabIndex = 0;
            this.comboBoxSerialPorts.SelectedIndexChanged += new System.EventHandler(this.radioButtonOn_CheckedChanged);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(447, 403);
            this.Controls.Add(this.groupBoxSlaveParameters);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBoxConnParameters);
            this.Name = "Form2";
            this.Text = "Modbus";
            this.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxSlaveAddress_Validating);
            this.groupBoxSlaveParameters.ResumeLayout(false);
            this.groupBoxSlaveParameters.PerformLayout();
            this.groupBoxParity.ResumeLayout(false);
            this.groupBoxParity.PerformLayout();
            this.groupBoxStopBits.ResumeLayout(false);
            this.groupBoxStopBits.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBoxDataBits.ResumeLayout(false);
            this.groupBoxDataBits.PerformLayout();
            this.groupBoxConnParameters.ResumeLayout(false);
            this.groupBoxConnParameters.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton radioButtonStopBit2;
        private System.Windows.Forms.GroupBox groupBoxSlaveParameters;
        private System.Windows.Forms.RadioButton radioButtonOff;
        private System.Windows.Forms.RadioButton radioButtonOn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxSlaveDelay;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxSlaveAddress;
        private System.Windows.Forms.GroupBox groupBoxParity;
        private System.Windows.Forms.RadioButton radioButtonParityOdd;
        private System.Windows.Forms.RadioButton radioButtonParityEven;
        private System.Windows.Forms.RadioButton radioButtonParityNone;
        private System.Windows.Forms.RadioButton radioButtonStopBit1;
        private System.Windows.Forms.GroupBox groupBoxStopBits;
        private System.Windows.Forms.RadioButton radioButtonDataBits8;
        private System.IO.Ports.SerialPort _serialPort;
        private System.Windows.Forms.RadioButton radioButtonDataBits7;
        private System.Windows.Forms.ListBox listBoxCommLog;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBoxDataBits;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxBaud;
        private System.Windows.Forms.GroupBox groupBoxConnParameters;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBoxSerialPorts;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}