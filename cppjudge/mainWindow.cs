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


namespace cppjudge
{
    public partial class mainWindowForm : Form
    {
        bool isOk = true; // флаг ошибки
        string CurrentFile;   // Текущий открытый файл
        string testResult;   // Выход теста      
        int globalGrade = 0;  // Оценка
        int testsCount = 0;   // Количество тестов
        int currTest = 0; // Номер текущего теста
        int timeLimit = 150;
        int memoryLimit = 268435456; // Ограничение памяти
        string timeLim = ""; // Поле времени
        string memLimit = ""; // Поле памяти
        string compilerPath = ""; // Поле пути к компилятору
        string directoryPath = ""; // Путь к папке с тестами       
        string[] testGrades = new string[1000]; // Оценки за тесты
        public mainWindowForm()
        {
            InitializeComponent();
            //Скомпилировать программу
        }

        // Обработки кнопки компиляции
        private void compileFile_Click(object sender, EventArgs e)
        {
            compileCode();
            statWindow.Clear();
        }


        /* Скомпилировать тестируемую программу
         */
        public void compileCode()
        {
            loadConfig();
            preprocessing(CurrentFile);
            string exeName = Path.GetFileNameWithoutExtension(CurrentFile);
            Process proc = new Process();
            proc.StartInfo.FileName = @compilerPath;
            proc.StartInfo.Arguments = CurrentFile + " -o" + exeName;
            proc.StartInfo.CreateNoWindow = false;
            proc.StartInfo.RedirectStandardError = true;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            proc.EnableRaisingEvents = true;

            var errors = new StringBuilder();
            var output = new StringBuilder();
            var hadErrors = false;

            // Получить вывод
            proc.OutputDataReceived += (s, d) =>
            {
                output.Append(d.Data);
            };

            // Получить ошибки
            proc.ErrorDataReceived += (s, d) =>
            {
                if (!hadErrors)
                {
                    hadErrors = !String.IsNullOrEmpty(d.Data);
                }
                errors.Append(d.Data);
            };

            proc.Start();
            // Слушать поток
            proc.BeginErrorReadLine();
            proc.BeginOutputReadLine();

            proc.WaitForExit();
            string stdout = output.ToString();
            string stderr = errors.ToString();

            if (proc.ExitCode != 0 || hadErrors)
            {
                MessageBox.Show("error:" + stderr);
            }
            else
            {
                MessageBox.Show("Compilation successful");
            }
        }

        // Запуск одного теста
        public void runTest(string fileName, string expectedResult)
        {
            isOk = true;
            bool timeout = true;
            string result = "";
            // Загрузить конфигурацию
            loadConfig();
            // Прочитать файл теста
            string[] readText = File.ReadAllLines(fileName);

            string exeName = Path.GetFileNameWithoutExtension(CurrentFile);
            Process proc = new Process();
            proc.StartInfo.FileName = exeName;
            //proc.StartInfo.Arguments = CurrentFile + " -o" + exeName;
            proc.StartInfo.RedirectStandardInput = true;
            proc.StartInfo.CreateNoWindow = false;
            proc.StartInfo.RedirectStandardError = true;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            proc.EnableRaisingEvents = true;
            // Считать ограничение по времени
            if (timeLim.Length != 0)
            {
                Int32.TryParse(timeLim, out timeLimit);
            }
            // Считать ограничение по памяти
            if (memLimit.Length != 0)
            {
                Int32.TryParse(memLimit, out memoryLimit);
            }

            var errors = new StringBuilder();
            var output = new StringBuilder();
            var hadErrors = false;

            // Получить вывод
            proc.OutputDataReceived += (s, d) =>
            {
                output.Append(d.Data);
            };

            // Получить ошибки
            proc.ErrorDataReceived += (s, d) =>
            {
                if (!hadErrors)
                {
                    hadErrors = !String.IsNullOrEmpty(d.Data);
                }
                errors.Append(d.Data);
            };
            Stopwatch sw = Stopwatch.StartNew();
            proc.Start();
            // Слушать поток
            proc.BeginErrorReadLine();
            proc.BeginOutputReadLine();
            long memoryUsed = proc.PrivateMemorySize64; //считать память            
            // Убить процесс если память превышена
            if (memoryUsed > memoryLimit)
            {
                proc.Kill();
            }
            foreach (string s in readText)
            {
                proc.StandardInput.WriteLine(s);
            }
            timeout = proc.WaitForExit(timeLimit * 1000);
            //Убить процесс если время превышено
            if (!timeout)
            {
                proc.Kill();
            }
            // Вывести ошибку памяти
            if (memoryUsed > memoryLimit)
            {
                result = "[FAIL] Out of memory " + (memoryUsed / 1024).ToString() + "KB used";
                statWindow.Text += result + " in test №" + currTest.ToString() + "\n";
                isOk = false;
            }
            TimeSpan timeElapsed = sw.Elapsed; //считать время выполнения
            // Вывести ошибку времени выполнения         
            if (timeElapsed.TotalSeconds > timeLimit)
            {
                result = "[FAIL] Timeout " + timeElapsed.TotalSeconds.ToString() + " seconds";
                statWindow.Text += result + " in test №" + currTest.ToString() + "\n";
                isOk = false;
            }
            // Вывод
            string stdout = output.ToString();
            string stderr = errors.ToString();
            if (proc.ExitCode != 0 || hadErrors)
            {
                MessageBox.Show("error:" + stderr);
            }

            if (isOk)
                testResult = stdout;
            else
                testResult = result;

            int grade = compareResult(expectedResult, testResult) * Int32.Parse(testGrades[currTest]);
            globalGrade += grade;
            currTest++;
        }

        // Обработчик кнопки открытия файла
        private void openFile_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                CurrentFile = openFileDialog1.FileName;
            }
        }


        // Проверка результатов тестирования
        public int compareResult(string fileName, string result)
        {
            int grade = 0;
            // Прочитать файл теста
            string[] readText = File.ReadAllLines(fileName);
            foreach (string s in readText)
            {
                if (result == s)
                {
                    grade++;
                }
            }
            return grade;
        }

        // Обработчик кнопки тестирования
        private void testBtn_Click(object sender, EventArgs e)
        {

            if (CurrentFile != null && testFolders.SelectedNode != null)
            {
                statWindow.Clear();
                directoryPath = Environment.CurrentDirectory.Replace(Path.GetFileName(Environment.CurrentDirectory), "") + testFolders.SelectedNode.FullPath;
                testAll(directoryPath);
                int maxGrade = 0;
                if (testsCount % 2 == 0)
                {
                    for (int i = 0; i < testsCount / 2; i++)
                    {
                        maxGrade += Int32.Parse(testGrades[i]);
                    }
                    double grade = ((double)globalGrade / (double)maxGrade) * 100;
                    statWindow.Text += "\n Overall grade: " + grade.ToString() + "\n";
                } else
                {
                    statWindow.Text += "\n Testing failed";
                }
            }else
            {
                if(CurrentFile == null)
                    MessageBox.Show("error: Source code is not open");
                else
                    MessageBox.Show("error: No folder chosen");
                statWindow.Text += "\n Testing failed";
            }
        }

        // Тестировать все тесты
        private void testAll(string targetDirectory)
        {
            int procCount = Environment.ProcessorCount; // Получить количество ядер процессора
            System.Threading.ThreadPool.SetMaxThreads(procCount, procCount); // Задать максимум активных потоков
            currTest = 0;
            globalGrade = 0;
            // Тестировать
            string[] fileEntries = Directory.GetFiles(targetDirectory);
            testsCount = fileEntries.Count();
            if (testsCount < 2)
            {
                MessageBox.Show("error: There is not enough files in this folder");
            }
            else if (testsCount % 2 != 0)
            {
                MessageBox.Show("error: Not all of the tests have answer files");
            }
            else
            {
                string prevFile = null;
                foreach (string fileName in fileEntries)
                {
                    if (prevFile != null && !prevFile.EndsWith(".a"))
                    {
                        System.Threading.ThreadStart starter = () => runTest(prevFile, fileName);
                        System.Threading.Thread thread = new System.Threading.Thread(starter);
                        thread.Start();
                        thread.Join();
                    }
                    prevFile = fileName;
                }
            }
        }

        // Обработчик кнопки настроек
        private void btnConfig_Click(object sender, EventArgs e)
        {
            Config configForm = new Config(this);
            configForm.Show();
        }

        // Загрузка конфигурации из файла
        private void loadConfig()
        {
            string startupPath = Environment.CurrentDirectory;
            string[] lines = System.IO.File.ReadAllLines(@startupPath + "//Config.txt");
            memLimit = lines[0];
            timeLim = lines[1];
            compilerPath = lines[2];
 
            int i = 0;
            foreach (string line in lines.Skip(3))
            {
                testGrades[i] = line.Substring(1);
                i++;
            }
        }

        private void mainWindowForm_Load(object sender, EventArgs e)
        {
            loadConfig();
            PopulateTreeView();
        }
        
        // Загрузка интерфейса
        DirectoryInfo info = new DirectoryInfo(@Environment.CurrentDirectory);
        private void PopulateTreeView()
        {
            TreeNode rootNode;
            if (info.Exists)
            {
                rootNode = new TreeNode(info.Name);
                rootNode.Tag = info;
                GetDirectories(info.GetDirectories(), rootNode);
                testFolders.Nodes.Add(rootNode);
            }
        }

        // Получение папок
        private void GetDirectories(DirectoryInfo[] subDirs, TreeNode nodeToAddTo)
        {
            TreeNode aNode;
            DirectoryInfo[] subSubDirs;
            foreach (DirectoryInfo subDir in subDirs)
            {
                aNode = new TreeNode(subDir.Name, 0, 0);
                aNode.Tag = subDir;
                aNode.ImageKey = "folder";
                subSubDirs = subDir.GetDirectories();
                if (subSubDirs.Length != 0)
                {
                    GetDirectories(subSubDirs, aNode);
                }
                nodeToAddTo.Nodes.Add(aNode);
            }
        }

        // Обработка виджета, отображающего содержимое папки
        void testFolders_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode newSelected = e.Node;
            listView1.Items.Clear();
            DirectoryInfo nodeDirInfo = (DirectoryInfo)newSelected.Tag;
            ListViewItem.ListViewSubItem[] subItems;
            ListViewItem item = null;

            foreach (DirectoryInfo dir in nodeDirInfo.GetDirectories())
            {
                item = new ListViewItem(dir.Name, 0);
                subItems = new ListViewItem.ListViewSubItem[]
                          {new ListViewItem.ListViewSubItem(item, "Directory"),
                   new ListViewItem.ListViewSubItem(item,
                dir.LastAccessTime.ToShortDateString())};
                item.SubItems.AddRange(subItems);
                listView1.Items.Add(item);
            }
            foreach (FileInfo file in nodeDirInfo.GetFiles())
            {
                item = new ListViewItem(file.Name, 1);
                subItems = new ListViewItem.ListViewSubItem[]
                          { new ListViewItem.ListViewSubItem(item, "File"),
                   new ListViewItem.ListViewSubItem(item,
                file.LastAccessTime.ToShortDateString())};

                item.SubItems.AddRange(subItems);
                listView1.Items.Add(item);
            }

            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        // Предварительная обработка файла с кодом
        void preprocessing(string codeFile)
        {
            int id = 0;
            string[] code = System.IO.File.ReadAllLines(codeFile);
            foreach (string line in code)
            {
                if (line.Contains("system(\"pause\")") || line.Contains("system (\"pause\")"))
                {
                    code[id]=code[id].Replace("system(\"pause\")", "");
                    code[id] = code[id].Replace("system (\"pause\")", "");
                }
                if (line.Contains("_tmain"))
                {
                    code[id] = code[id].Replace("_tmain", "main");
                }
                if (line.Contains("_tmain"))
                {
                    code[id] = code[id].Replace("_tmain", "main");
                }
                if (line.Contains("_TCHAR"))
                {
                    code[id] = code[id].Replace("_TCHAR", "char");
                }
                if (code.Length >= id + 2)
                {
                    if (code[id + 1].Contains("return"))
                    {
                        if (code[id].Contains("_getch"))
                        {
                            code[id] = code[id].Replace("_getch()", "");
                        }
                    }
                }
                id++;
            }
            System.IO.File.WriteAllLines(codeFile,code);
        }

    }
}
