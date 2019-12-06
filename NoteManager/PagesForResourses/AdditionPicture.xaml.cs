using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NoteManager.PagesForResourses
{
    /// <summary>
    /// Interaction logic for AdditionPicture.xaml
    /// </summary>
    public partial class AdditionPicture : Page
    {
        string fileExtensions = "jpg,png,bmp";
        FileType type = FileType.Picture;
        List<File> files;
        public AdditionPicture()
        {
            InitializeComponent();
            files = new List<File>();

            FileList.ItemsSource = files;
        }

        private void DoubleClickOnNmeOfPicture1(object sender, RoutedEventArgs e)
        {
            MyImage.Source = new BitmapImage(new Uri("pack://application:,,,/NoteManager;component/Resources/Pictures/BackroundBlackWorld.jpg"));
        }

        private void DoubleClickOnNmeOfPicture2(object sender, RoutedEventArgs e)
        {
            MyImage.Source = new BitmapImage(new Uri("pack://application:,,,/NoteManager;component/Resources/Pictures/Start1.jpg"));
        }
        private void AddFileToList(File video)
        {
            if (!files.Contains(video, new FileComparer()))
            {
                FileList.BeginInit();
                files.Add(video);
                FileList.EndInit();
                // Push notification that file was added
            }
            else
            {
                // Push notification that file was not added(for some reasons)
            }
        }
        private void AddFile(object sender, MouseEventArgs e)
        {
            // File extension must be loaded from any config file ar anywhere else
            FileUploader uploader = new FileUploader(fileExtensions);
            string filePath = uploader.Upload();
            if (String.Empty != filePath)
            {
                File file = new File(filePath, (int)type, (int)FileState.OnlyUploaded);
                AddFileToList(file);
            }
            else
            {
                //Push notification that file was not added(for some reasons)
            }
        }
        private void DeleteFile(object sender, MouseEventArgs e)
        {
            var file = (File)FileList.SelectedItem;
            if (file != null)
            {
                FileList.BeginInit();
                files.Remove(file);
                FileList.EndInit();
            }
        }
        private void SaveFiles(object sender, MouseEventArgs e)
        {
            // logic for save fiels to database
        }
    }
}
