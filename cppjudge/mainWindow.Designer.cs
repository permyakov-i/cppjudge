namespace tinycpp
{
    partial class mainWindowForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.compileFile = new System.Windows.Forms.Button();
            this.openTests = new System.Windows.Forms.Button();
            this.openFile = new System.Windows.Forms.Button();
            this.statWindow = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.folderItems = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.memLimit = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.timeLim = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.compilerPath = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // compileFile
            // 
            this.compileFile.Location = new System.Drawing.Point(139, 303);
            this.compileFile.Name = "compileFile";
            this.compileFile.Size = new System.Drawing.Size(126, 23);
            this.compileFile.TabIndex = 0;
            this.compileFile.Text = "Компилировать";
            this.compileFile.UseVisualStyleBackColor = true;
            this.compileFile.Click += new System.EventHandler(this.compileFile_Click);
            // 
            // openTests
            // 
            this.openTests.Location = new System.Drawing.Point(12, 333);
            this.openTests.Name = "openTests";
            this.openTests.Size = new System.Drawing.Size(121, 23);
            this.openTests.TabIndex = 1;
            this.openTests.Text = "Выбрать тесты";
            this.openTests.UseVisualStyleBackColor = true;
            this.openTests.Click += new System.EventHandler(this.openTests_Click);
            // 
            // openFile
            // 
            this.openFile.Location = new System.Drawing.Point(12, 303);
            this.openFile.Name = "openFile";
            this.openFile.Size = new System.Drawing.Size(121, 23);
            this.openFile.TabIndex = 2;
            this.openFile.Text = "Выбрать файл";
            this.openFile.UseVisualStyleBackColor = true;
            this.openFile.Click += new System.EventHandler(this.openFile_Click);
            // 
            // statWindow
            // 
            this.statWindow.Location = new System.Drawing.Point(271, 14);
            this.statWindow.Multiline = true;
            this.statWindow.Name = "statWindow";
            this.statWindow.Size = new System.Drawing.Size(230, 342);
            this.statWindow.TabIndex = 3;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // folderItems
            // 
            this.folderItems.FormattingEnabled = true;
            this.folderItems.Location = new System.Drawing.Point(12, 14);
            this.folderItems.Name = "folderItems";
            this.folderItems.Size = new System.Drawing.Size(237, 21);
            this.folderItems.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(467, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Лимит памяти";
            // 
            // memLimit
            // 
            this.memLimit.Location = new System.Drawing.Point(12, 68);
            this.memLimit.Name = "memLimit";
            this.memLimit.Size = new System.Drawing.Size(236, 20);
            this.memLimit.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 92);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(152, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Лимит времени выполнения";
            // 
            // timeLim
            // 
            this.timeLim.Location = new System.Drawing.Point(12, 108);
            this.timeLim.Name = "timeLim";
            this.timeLim.Size = new System.Drawing.Size(237, 20);
            this.timeLim.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 135);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(109, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Путь к компилятору";
            // 
            // compilerPath
            // 
            this.compilerPath.Location = new System.Drawing.Point(12, 152);
            this.compilerPath.Multiline = true;
            this.compilerPath.Name = "compilerPath";
            this.compilerPath.Size = new System.Drawing.Size(236, 35);
            this.compilerPath.TabIndex = 12;
            this.compilerPath.Text = "C:/Program Files/mingw-w64/x86_64-7.3.0-posix-seh-rt_v5-rev0/mingw64/bin/g++";
            // 
            // mainWindowForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(513, 374);
            this.Controls.Add(this.compilerPath);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.timeLim);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.memLimit);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.folderItems);
            this.Controls.Add(this.statWindow);
            this.Controls.Add(this.openFile);
            this.Controls.Add(this.openTests);
            this.Controls.Add(this.compileFile);
            this.Name = "mainWindowForm";
            this.Text = "Тестер олимпиад";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button compileFile;
        private System.Windows.Forms.Button openTests;
        private System.Windows.Forms.Button openFile;
        private System.Windows.Forms.TextBox statWindow;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.ComboBox folderItems;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox memLimit;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox timeLim;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox compilerPath;
    }
}

