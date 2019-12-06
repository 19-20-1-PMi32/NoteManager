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
using System.Diagnostics;


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
        List<File> deleted;
        public AdditionPicture()
        {
            InitializeComponent();
            files = new List<File>();
            deleted = new List<File>();
            FileList.ItemsSource = files;
        }
        
        private void AddFileToList(File video)
        {
            if (!files.Contains(video, new FileComparer()))
            {
                FileList.BeginInit();
                files.Add(video);
                FileList.EndInit();
                // Push notification that item was added
            }
            else
            {
                // Push notification that item was not added(for some reasons)
            }
        }
        private void AddFile(object sender, MouseEventArgs e)
        {
            // File extension must be loaded from any config file ar anywhere else
            FileUploader uploader = new FileUploader(fileExtensions);
            string filePath = uploader.Upload();
            if (String.Empty != filePath)
            {
                File file = new File(filePath, (int)FileType.Video, (int)FileState.OnlyUploaded);
                AddFileToList(file);
                Debug.WriteLine(files.Count);
                Debug.WriteLine(deleted.Count);
            }
            else
            {
                //Push notification that file was not added(for some reasons)
            }
        }
        private File SelectedFile()
        {
            return (File)FileList.SelectedItem;
        }
        private void DeleteFronList(File file)
        {
            file.State = FileState.MustBeDeleted;
            FileList.BeginInit();
            files.Remove(file);
            FileList.EndInit();


        }
        private void DeleteFile(object sender, MouseEventArgs e)
        {
            var file = SelectedFile();
            if (file != null)
            {
                DeleteFronList(file);
                deleted.Add(file);

            }
            Debug.WriteLine(files.Count);
            Debug.WriteLine(deleted.Count);
        }
        private void SaveFiles(object sender, MouseEventArgs e)
        {
            //logic for save file to database
        }

        private void FileList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var sel = SelectedFile();
            if (sel != null)
                PictureFrame.Source = new BitmapImage(new Uri(sel.FilePath));
        }
    }
}
