namespace Brain_Evolution_Simulator
{
    partial class MainForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadWorldToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadEntityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveWorldToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveWorldAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveEntityAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel2D = new System.Windows.Forms.Panel();
            this.startButton = new System.Windows.Forms.Button();
            this.settingsBox = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.statusBox = new System.Windows.Forms.GroupBox();
            this.framerateBox = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.displayCheckBox = new System.Windows.Forms.CheckBox();
            this.stopButton = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.statusBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.framerateBox)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.AutoSize = false;
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(134)))), ((int)(((byte)(134)))), ((int)(((byte)(134)))));
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.optionsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(9, 9);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(108, 24);
            this.menuStrip1.Stretch = false;
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadWorldToolStripMenuItem,
            this.loadEntityToolStripMenuItem,
            this.saveWorldToolStripMenuItem,
            this.saveWorldAsToolStripMenuItem,
            this.saveEntityAsToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loadWorldToolStripMenuItem
            // 
            this.loadWorldToolStripMenuItem.Name = "loadWorldToolStripMenuItem";
            this.loadWorldToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.loadWorldToolStripMenuItem.Text = "Load World";
            // 
            // loadEntityToolStripMenuItem
            // 
            this.loadEntityToolStripMenuItem.Name = "loadEntityToolStripMenuItem";
            this.loadEntityToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.loadEntityToolStripMenuItem.Text = "Load Entity";
            // 
            // saveWorldToolStripMenuItem
            // 
            this.saveWorldToolStripMenuItem.Name = "saveWorldToolStripMenuItem";
            this.saveWorldToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.saveWorldToolStripMenuItem.Text = "Save World";
            // 
            // saveWorldAsToolStripMenuItem
            // 
            this.saveWorldAsToolStripMenuItem.Name = "saveWorldAsToolStripMenuItem";
            this.saveWorldAsToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.saveWorldAsToolStripMenuItem.Text = "Save World As...";
            // 
            // saveEntityAsToolStripMenuItem
            // 
            this.saveEntityAsToolStripMenuItem.Name = "saveEntityAsToolStripMenuItem";
            this.saveEntityAsToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.saveEntityAsToolStripMenuItem.Text = "Save Entity As...";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(134)))), ((int)(((byte)(134)))), ((int)(((byte)(134)))));
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // panel2D
            // 
            this.panel2D.BackColor = System.Drawing.Color.Black;
            this.panel2D.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2D.Location = new System.Drawing.Point(607, 12);
            this.panel2D.Name = "panel2D";
            this.panel2D.Size = new System.Drawing.Size(838, 838);
            this.panel2D.TabIndex = 1;
            this.panel2D.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2D_Paint);
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(9, 36);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(58, 23);
            this.startButton.TabIndex = 3;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.start_Click);
            // 
            // settingsBox
            // 
            this.settingsBox.ForeColor = System.Drawing.Color.White;
            this.settingsBox.Location = new System.Drawing.Point(12, 393);
            this.settingsBox.Name = "settingsBox";
            this.settingsBox.Size = new System.Drawing.Size(193, 293);
            this.settingsBox.TabIndex = 2;
            this.settingsBox.TabStop = false;
            this.settingsBox.Text = "Settings";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "label1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(6, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "label2";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(6, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "label3";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(9, 65);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(193, 23);
            this.button2.TabIndex = 7;
            this.button2.Text = "Save Entities";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // statusBox
            // 
            this.statusBox.Controls.Add(this.label1);
            this.statusBox.Controls.Add(this.label2);
            this.statusBox.Controls.Add(this.label3);
            this.statusBox.ForeColor = System.Drawing.Color.White;
            this.statusBox.Location = new System.Drawing.Point(12, 94);
            this.statusBox.Name = "statusBox";
            this.statusBox.Size = new System.Drawing.Size(193, 293);
            this.statusBox.TabIndex = 3;
            this.statusBox.TabStop = false;
            this.statusBox.Text = "Status";
            // 
            // framerateBox
            // 
            this.framerateBox.Location = new System.Drawing.Point(534, 12);
            this.framerateBox.Maximum = new decimal(new int[] {
            50000,
            0,
            0,
            0});
            this.framerateBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.framerateBox.Name = "framerateBox";
            this.framerateBox.Size = new System.Drawing.Size(67, 20);
            this.framerateBox.TabIndex = 8;
            this.framerateBox.ThousandsSeparator = true;
            this.framerateBox.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(437, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Steps Per Update";
            // 
            // displayCheckBox
            // 
            this.displayCheckBox.AutoSize = true;
            this.displayCheckBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.displayCheckBox.Checked = true;
            this.displayCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.displayCheckBox.ForeColor = System.Drawing.Color.White;
            this.displayCheckBox.Location = new System.Drawing.Point(497, 36);
            this.displayCheckBox.Name = "displayCheckBox";
            this.displayCheckBox.Size = new System.Drawing.Size(104, 17);
            this.displayCheckBox.TabIndex = 9;
            this.displayCheckBox.Text = "Update 2D View";
            this.displayCheckBox.UseVisualStyleBackColor = true;
            // 
            // stopButton
            // 
            this.stopButton.Location = new System.Drawing.Point(73, 36);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(58, 23);
            this.stopButton.TabIndex = 10;
            this.stopButton.Text = "Stop";
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(88)))), ((int)(((byte)(88)))));
            this.ClientSize = new System.Drawing.Size(1457, 862);
            this.Controls.Add(this.stopButton);
            this.Controls.Add(this.displayCheckBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.framerateBox);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.statusBox);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.settingsBox);
            this.Controls.Add(this.panel2D);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Brain Evolution Simulator, Andrew Scott";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusBox.ResumeLayout(false);
            this.statusBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.framerateBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadWorldToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadEntityToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveWorldToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveWorldAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveEntityAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.Panel panel2D;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.GroupBox settingsBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox statusBox;
        private System.Windows.Forms.NumericUpDown framerateBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox displayCheckBox;
        private System.Windows.Forms.Button stopButton;
    }
}

