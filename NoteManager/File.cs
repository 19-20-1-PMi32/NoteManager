using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteManager
{
    /// <summary>
    /// Enum for representing current file state
    /// </summary>
    enum FileState
    {
        Saved,
        OnlyUploaded,
        MustBeDeleted
    }
    /// <summary>
    /// Enum for representing file type
    /// </summary>
    enum FileType
    {
        Video,
        Audio,
        Picture
    }
    /// <summary>
    /// Class represents file
    /// </summary>
    class File
    {
        /// <summary>
        /// Represents path to file
        /// </summary>
        public string FilePath { get; }
        /// <summary>
        /// Represents current file state
        /// </summary>
        public FileState State { get; }
        /// <summary>
        /// Represents current file state
        /// </summary>
        public FileType Type { get; set; }
        /// <summary>
        /// Represents name of file
        /// </summary>
        public string FileName
        {
            get
            {
                int slashind = FilePath.LastIndexOf('\\');
                return FilePath.Substring(slashind + 1, FilePath.Length - slashind- 1);
            }
        }
        /// <summary>
        /// Constructor for creating file
        /// </summary>
        /// <param name="filepath">Path to file</param>
        /// <param name="filetype">Path to file</param>
        /// <param name="filestate">Path to file</param>
        public File(string filepath, int filetype, int filestate)
        {
            FilePath = filepath;
            State = (FileState)filestate;
            Type = (FileType)filetype;
        }

    }
}
