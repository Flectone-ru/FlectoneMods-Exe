using Microsoft.VisualBasic;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using Path = System.IO.Path;

namespace FlectoneModInstaller
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            textbox.Text = $@"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}\AppData\Roaming\.minecraft\mods";
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }
        public void Method2()
        {
            try
            {
                string MyPath = textbox.Text;
                bool checkboxcheckedfabric = (bool)CheckBoxDownloadFabric.IsChecked;
                bool maxfps = (bool)MaxFps.IsChecked;
                bool clearfolder = (bool)ClearFolder.IsChecked;
                string strExeFilePath = System.Reflection.Assembly.GetExecutingAssembly().Location.Replace($"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.exe", "") + @"mods\";
                string chosen = ComboBox2.SelectedItem.ToString();
                strExeFilePath += $@"{chosen}\" + ComboBox1.SelectedItem.ToString() + @"\" + @"main";
                if (clearfolder == true)
                {
                    string[] mymods = Directory.GetFiles($@"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}\AppData\Roaming\.minecraft\mods");
                    foreach (var Mods in mymods)
                    {
                        File.Delete(Mods);
                    }
                }
                string[] filePaths = Directory.GetFiles(strExeFilePath);
                foreach (var StingPath in filePaths)
                {
                    string file = StingPath.ToString();
                    string str = $@"{MyPath}\" + file.ToString().Replace($@"{strExeFilePath}\", " ");
                    if (!File.Exists(str))
                    {
                        File.Copy(file, str);
                    }
                }
                if (maxfps)
                {
                    var a = strExeFilePath.Replace("main", "extension");
                    string[] extension = Directory.GetFiles(a);
                    foreach (var ext in extension)
                    {
                        string file = ext.ToString();
                        string str = $@"{MyPath}\" + file.ToString().Replace($@"{a}\", " ");
                        if (!File.Exists(str))
                        {
                            File.Copy(file, str);
                        }
                    }
                }
                if (checkboxcheckedfabric == true)
                {
                    Process.Start(System.Reflection.Assembly.GetExecutingAssembly().Location.Replace($"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.exe", "") + @"Fabric.jar");
                }
                System.Windows.MessageBox.Show("Моды успешно установлены!");
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Если вы увидели эту ошибку! Возможно у вас отсутствует папка mods или приложению не удалось определить путь к папке. Пожалуйста обратитесь к разработчику приложения ( Discord - LilGreen_#8298 ) или к ( Discord - Фасэр#2895 ) \n\n" + ex.ToString());
            }
        }
        public void GetTypeName()
        {
            string strExeFilePath = System.Reflection.Assembly.GetExecutingAssembly().Location.Replace($"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.exe", "") + @"mods\";
            string[] ArrayOfTypes = Directory.GetDirectories(strExeFilePath);
            Array.Reverse(ArrayOfTypes);
            for (int i = 0; i < ArrayOfTypes.Length; i++)
            {
                string cutArray = ArrayOfTypes[i].Replace(strExeFilePath, "");
                cutArray.ToString();
                ComboBox2.Items.Add(cutArray);
            }
        }
        private void Combobox1Isloaded(object sender, RoutedEventArgs e)
        {
        }
        private void ComboBox2Loaded(object sender, RoutedEventArgs e)
        {
            GetTypeName();
            ComboBox2.SelectedIndex = 0;
            ComboBox2.Items.Add("[+] Добавить свою сборку");
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var dialog = new FolderBrowserDialog();
            dialog.ShowDialog();
            dialog.ShowNewFolderButton = true;
            textbox.Text = dialog.SelectedPath;
        }
        private void ComboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }
        public void VersionModpack()
        {
            string strExeFilePath = System.Reflection.Assembly.GetExecutingAssembly().Location.Replace($"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.exe", "") + @"mods\";
            string chosen = ComboBox2.SelectedItem.ToString();
            strExeFilePath += $@"{chosen}\";
            string[] array = Directory.GetDirectories(strExeFilePath);
            Array.Reverse(array);
            for (int i = 0; i < array.Length; i++)
            {
                string cutArray = array[i].Replace(strExeFilePath, "");
                cutArray.ToString();
                ComboBox1.Items.Add(cutArray);
            }
        }
        public string temp;
        private void ComboBox2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!ComboBox2.SelectedItem.Equals("[+] Добавить свою сборку"))
            {
                ComboBox1.Items.Clear();
                VersionModpack();
                ComboBox1.SelectedIndex = 0;
            }

            if (ComboBox2.SelectedItem.Equals("[+] Добавить свою сборку"))
            {
                var dialog = new FolderBrowserDialog();
                dialog.ShowDialog();
                dialog.ShowNewFolderButton = true;
                string path = dialog.SelectedPath;
                string directoryPath = Path.GetFileName(path);
                //System.Windows.MessageBox.Show(directoryPath);

                if (Directory.Exists(path))
                {
                    ComboBox2.SelectedIndex = 0;
                    string version = Interaction.InputBox("Укажите версию вашей сборки или её название", "Помощник", "");
                    if (version == string.Empty)
                    {
                        version = "Личная";
                    }
                    string strExeFilePath = System.Reflection.Assembly.GetExecutingAssembly().Location.Replace($"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.exe", "") + @"mods\";

                    Directory.CreateDirectory(strExeFilePath + directoryPath + @"\" + version + @"\" + "main");
                    Directory.CreateDirectory(strExeFilePath + directoryPath + @"\" + version + @"\" + "extension");
                    string[] filePaths = Directory.GetFiles(path);
                    strExeFilePath += directoryPath + @"\" + version + @"\" + "main";
                    foreach (var StingPath in filePaths)
                    {
                        string file = StingPath.ToString();
                        string str = $@"{strExeFilePath}\" + file.ToString().Replace($@"{path}\", " ");
                        if (!File.Exists(str))
                        {
                            File.Copy(file, str);
                        }
                    }
                    System.Windows.MessageBox.Show("Ваша сборка успешно добавлена пожалуйста перезапустите приложение!");
                }
            }
        }
        private void textbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (textbox.Text.Length == 0)
                textbox.Text = $@"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}\AppData\Roaming\.minecraft\mods";
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Method2();
        }
    }
}
