namespace cppjudge
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
            this.testBtn = new System.Windows.Forms.Button();
            this.btnConfig = new System.Windows.Forms.Button();
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
            // testBtn
            // 
            this.testBtn.Location = new System.Drawing.Point(140, 333);
            this.testBtn.Name = "testBtn";
            this.testBtn.Size = new System.Drawing.Size(125, 23);
            this.testBtn.TabIndex = 13;
            this.testBtn.Text = "Тестировать";
            this.testBtn.UseVisualStyleBackColor = true;
            this.testBtn.Click += new System.EventHandler(this.testBtn_Click);
            // 
            // btnConfig
            // 
            this.btnConfig.Location = new System.Drawing.Point(12, 271);
            this.btnConfig.Name = "btnConfig";
            this.btnConfig.Size = new System.Drawing.Size(121, 23);
            this.btnConfig.TabIndex = 14;
            this.btnConfig.Text = "Настройки";
            this.btnConfig.UseVisualStyleBackColor = true;
            this.btnConfig.Click += new System.EventHandler(this.btnConfig_Click);
            // 
            // mainWindowForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(513, 374);
            this.Controls.Add(this.btnConfig);
            this.Controls.Add(this.testBtn);
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
        private System.Windows.Forms.Button testBtn;
        private System.Windows.Forms.Button btnConfig;
    }
}

