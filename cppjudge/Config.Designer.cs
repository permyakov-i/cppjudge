namespace cppjudge
{
    partial class Config
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
            this.compilerPath = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.timeLim = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.memLimit = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.saveConfig = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // compilerPath
            // 
            this.compilerPath.Location = new System.Drawing.Point(12, 111);
            this.compilerPath.Multiline = true;
            this.compilerPath.Name = "compilerPath";
            this.compilerPath.Size = new System.Drawing.Size(403, 35);
            this.compilerPath.TabIndex = 18;
            this.compilerPath.Text = "C:/Program Files/mingw-w64/x86_64-7.3.0-posix-seh-rt_v5-rev0/mingw64/bin/g++";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 94);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(109, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "Путь к компилятору";
            // 
            // timeLim
            // 
            this.timeLim.Location = new System.Drawing.Point(12, 67);
            this.timeLim.Name = "timeLim";
            this.timeLim.Size = new System.Drawing.Size(403, 20);
            this.timeLim.TabIndex = 16;
            this.timeLim.Text = "100";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(152, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Лимит времени выполнения";
            // 
            // memLimit
            // 
            this.memLimit.Location = new System.Drawing.Point(12, 27);
            this.memLimit.Name = "memLimit";
            this.memLimit.Size = new System.Drawing.Size(403, 20);
            this.memLimit.TabIndex = 14;
            this.memLimit.Text = "100000000";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Лимит памяти";
            // 
            // saveConfig
            // 
            this.saveConfig.Location = new System.Drawing.Point(12, 167);
            this.saveConfig.Name = "saveConfig";
            this.saveConfig.Size = new System.Drawing.Size(403, 23);
            this.saveConfig.TabIndex = 19;
            this.saveConfig.Text = "Сохранить";
            this.saveConfig.UseVisualStyleBackColor = true;
            this.saveConfig.Click += new System.EventHandler(this.saveConfig_Click);
            // 
            // Config
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(427, 347);
            this.Controls.Add(this.saveConfig);
            this.Controls.Add(this.compilerPath);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.timeLim);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.memLimit);
            this.Controls.Add(this.label2);
            this.Name = "Config";
            this.Text = "Config";
            this.Load += new System.EventHandler(this.Config_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox compilerPath;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox timeLim;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox memLimit;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button saveConfig;
    }
}