using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using System.Windows.Forms;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.IO;
using System.Collections;


namespace tinycpp
{
    public partial class Form1 : Form
    {
        string CurrentFile;
        public Form1()
        {
            InitializeComponent();        
            //Скомпилировать программу
            this.testFile.Click += new System.EventHandler(this.testFile_Click);
        }

        private void testFile_Click(object sender, EventArgs e)
        {
            string fileName = Path.GetFileName(CurrentFile);
            string exeName = Path.GetFileNameWithoutExtension(CurrentFile) + ".exe";
            string workingDir = Directory.GetParent(CurrentFile) + "";
            string compile = Directory.GetParent(Application.ExecutablePath) + "\\compile.cmd";

            File.Delete(Path.Combine(workingDir, exeName));

            StartProcess(true, "cmd", "/c", "\"" + compile + "\"", workingDir, fileName, exeName);
            //Microsoft.CSharp.CSharpCodeProvider codeProvider = new Microsoft.CSharp.CSharpCodeProvider;
            //Microsoft.VisualC.CppCodeProvider codeProvider = new Microsoft.VisualC.CppCodeProvider();  
            //ICodeCompiler icc = codeProvider.CreateCompiler();
            /*string Output = "Out.exe";
            Button ButtonObject = (Button)sender;
            CodeDomProvider codeProvider = CodeDomProvider.CreateProvider("C++");
            textBox2.Text = "";
            System.CodeDom.Compiler.CompilerParameters parameters = new CompilerParameters();
            //Сгенерировать EXE
            parameters.GenerateExecutable = true;
            parameters.OutputAssembly = Output;
            CompilerResults results = codeProvider.CompileAssemblyFromSource(parameters, textBox1.Text);

            if (results.Errors.Count > 0)
            {
                textBox2.ForeColor = Color.Red;
                foreach (CompilerError CompErr in results.Errors)
                {
                    textBox2.Text = textBox2.Text +
                                "Compilation failed, err string " + CompErr.Line +
                                ", err code: " + CompErr.ErrorNumber +
                                ", '" + CompErr.ErrorText + ";" +
                                Environment.NewLine + Environment.NewLine;
                }
            }
            else
            {
                //Успешная компиляция
                textBox2.ForeColor = Color.Blue;
                textBox2.Text = "Success!";
            }*/
        }

        private void openFile_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                CurrentFile = openFileDialog1.FileName;
                System.IO.StreamReader sr = new
                   System.IO.StreamReader(openFileDialog1.FileName);
                textBox1.Text=sr.ReadToEnd();
                sr.Close();
            }
        }

        private void openTests_Click(object sender, EventArgs e)
        {
            string path = "";
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                path = folderBrowserDialog1.SelectedPath;
            }
            ProcessDirectory(path);
        }

        public void ProcessDirectory(string targetDirectory)
        {
            // Process the list of files found in the directory.
            string[] fileEntries = Directory.GetFiles(targetDirectory);
            foreach (string fileName in fileEntries)
                ProcessFile(fileName);
        }

        public void ProcessFile(string path)
        {
            comboBox1.Items.Clear();
            comboBox1.Items.Add(path);
        }

        void StartProcess(bool hidden, string command, params string[] args)
        {
            ProcessStartInfo pStartInfo = new ProcessStartInfo();

            pStartInfo.FileName = command;
            pStartInfo.Arguments = string.Join(" ", args);
            pStartInfo.UseShellExecute = false;

            if (hidden)
            {
                pStartInfo.RedirectStandardError = true;
                pStartInfo.RedirectStandardOutput = true;
                pStartInfo.CreateNoWindow = true;
            }

            Process proc = new Process();
            proc.StartInfo = pStartInfo;

            proc.Start();

            textBox2.Clear();

            if (hidden)
            {
                while (!proc.StandardError.EndOfStream)
                {
                    textBox2.AppendText("  Error: " + proc.StandardError.ReadLine() + Environment.NewLine);
                }
            }
        }
    }
}
