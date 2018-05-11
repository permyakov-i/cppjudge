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
        string message = "";
        string timeLim = ""; // Поле времени
        string memLimit = ""; // Поле памяти
        string compilerPath = ""; // Поле пути к компилятору
        string directoryPath = ""; // Путь к папке с тестами       
        string[] testGrades = new string[1000]; // Оценки за тесты
        public mainWindowForm()
        {
            InitializeComponent();
            //Скомпилировать программу
            //Таймер автообновления
            loadFolderFiles();
            System.Windows.Forms.Timer tm;
            tm = new System.Windows.Forms.Timer();
            tm.Tick += new EventHandler((o, e) => { refresh(null); });
            tm.Interval = 5000;
            tm.Start();
        }

        // Автообновление отображения содержимого папки
        public void refresh(object obj)
        {
            if (testFolders.SelectedNode != null)
            {
                TreeNode newSelected = testFolders.SelectedNode;
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
            loadFolderFiles();        
        }

        // Обработки кнопки компиляции
        private void compileFile_Click(object sender, EventArgs e)
        {
            
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
                statWindow.Text+="Compilation successful" + Environment.NewLine;
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
                memoryLimit = memoryLimit * 1024; // Перевести из килобайт в байты
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
                message = result + " in test №" + currTest.ToString() + "\n";
                isOk = false;
            }
            TimeSpan timeElapsed = sw.Elapsed; //считать время выполнения
            // Вывести ошибку времени выполнения         
            if (timeElapsed.TotalSeconds > timeLimit)
            {
                result = "[FAIL] Timeout " + timeElapsed.TotalSeconds.ToString() + " seconds";
                message = result + " in test №" + currTest.ToString() + "\n";
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
            message= " Test № "+ currTest + " Grade "+grade+" out of "+ testGrades[currTest]+ Environment.NewLine;
            globalGrade += grade;
            currTest++;
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
                compileCode();
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
                    statWindow.Text += "\n Overall grade: " + grade.ToString() + "%" + Environment.NewLine;
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
                        statWindow.Text += message;
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

        void loadFolderFiles()
        {
            ListViewItem item= new ListViewItem();
            if (listView2.SelectedItems.Count > 0)
            {
                item = listView2.SelectedItems[0];
            }
            DirectoryInfo nodeDirInfo2 = new DirectoryInfo(Directory.GetCurrentDirectory());
            listView2.Items.Clear();
            ListViewItem.ListViewSubItem[] subItems2;
            ListViewItem item2 = null;

            foreach (DirectoryInfo dir in nodeDirInfo2.GetDirectories())
            {
                item2 = new ListViewItem(dir.Name, 0);
                subItems2 = new ListViewItem.ListViewSubItem[]
                          {new ListViewItem.ListViewSubItem(item2, "Directory"),
                   new ListViewItem.ListViewSubItem(item2,
                dir.LastAccessTime.ToShortDateString())};
                item2.SubItems.AddRange(subItems2);
                listView2.Items.Add(item2);
            }
            foreach (FileInfo file in nodeDirInfo2.GetFiles())
            {
                item2 = new ListViewItem(file.Name, 1);
                subItems2 = new ListViewItem.ListViewSubItem[]
                          { new ListViewItem.ListViewSubItem(item2, "File"),
                   new ListViewItem.ListViewSubItem(item2,
                file.LastAccessTime.ToShortDateString())};

                item2.SubItems.AddRange(subItems2);
                listView2.Items.Add(item2);
            }

            int index=listView2.FindItemWithText(item.Text).Index;
            if (listView2.Items.Count > 0 && item.Text!="")
                listView2.Items[index].Selected = true;


            listView2.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
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
                if (line.Contains("#include <conio.h>"))
                {
                    code[id] = code[id].Replace("#include <conio.h>", "");
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

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView2.SelectedItems.Count > 0)
            {
                var item = listView2.SelectedItems[0];
                CurrentFile = item.Text;
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
