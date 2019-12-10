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

using NoteManager.DBClasses;

namespace NoteManager.PagesForResourses
{
    /// <summary>
    /// Interaction logic for AdditionPicture.xaml
    /// </summary>
    public partial class AdditionPicture : Page
    {
        string fileExtensions = "jpg,png,bmp";
        List<Picture> pictures;
        public AdditionPicture()
        {
            InitializeComponent();
            pictures = TemporaryNote.Pictures;
            FileList.ItemsSource = pictures;
        }
        
        private void AddFileToList(Picture picture)
        {
            if (!pictures.Contains(picture, new FileComparer()))
            {
                FileList.BeginInit();
                pictures.Add(picture);
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
                Picture picture = new Picture() { FilePath = filePath, CreationTime = DateTime.Now };
                AddFileToList(picture);
            }
            else
            {
                Notification.ShowMessage("Picture was not loaded");
            }
        }
        private Picture SelectedFile()
        {
            return (Picture)FileList.SelectedItem;
        }
        private void DeleteFronList(Picture picture)
        {
            FileList.BeginInit();
            pictures.Remove(picture);
            FileList.EndInit();
        }
        private void DeleteFile(object sender, MouseEventArgs e)
        {
            var picture = SelectedFile();
            if (picture != null)
            {
                DeleteFronList(picture);
            }
        }
        private void Save(Picture picture)
        {

        }
        private void Delete(Picture picture)
        {

        }

        private void FileList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var sel = SelectedFile();
            if (sel != null)
                PictureFrame.Source = new BitmapImage(new Uri(sel.FilePath));
        }
        private void FilePlay(object sender, MouseEventArgs e)
        {
            if (SelectedFile() != null)
            {
                Process.Start(SelectedFile().FilePath);
            }
        }
    }
}
