using System;
using System.Windows.Forms;

namespace NoteManager
{
    /// <summary>
    /// Class for checking and uploading file by any extensions
    /// </summary>
    class FileUploader
    {
        /// <summary>
        /// fileExtension string that contains available extensions separated by commas
        /// <code>fileExtension = "mp3,flac,aac"</code>
        /// </summary>
        public string fileExtensions;
        /// <summary>
        /// Constructor for initialization file extensions field
        /// </summary>
        /// <param name="f">fileExtensions string</param>
        public FileUploader(string f)
        {
            fileExtensions = f;
        }
        /// <summary>
        /// Main method for uploading file
        /// </summary>
        /// <returns></returns>
        public string Upload()
        {
            string result;
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var fileName = dialog.FileName;
                bool supp = checkIsSupported(fileName);
                if (supp)
                    result = fileName;
                else
                    result = "";
            }
            else
                result = "";
            return result;
        }
        /// <summary>
        /// Method checks checks is file suppported.
        /// </summary>
        /// <param name="path">Path to given file</param>
        /// <returns></returns>
        public bool checkIsSupported(string path)
        {
            int dotInd = path.LastIndexOf('.');
            string ext;
            try
            {
                ext = path.Substring(dotInd + 1, path.Length - dotInd - 1);
            }
            catch (ArgumentOutOfRangeException e)
            {
                return false;
            }
            if (fileExtensions.Contains(ext))
                return true;

            return false;

        }
    }
}
