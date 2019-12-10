using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteManager
{
    /// <summary>
    /// Class represents file
    /// </summary>
    class File
    {
        /// <summary>
        /// Represents path to file
        /// </summary>
        public string FilePath { get; set; }
        /// <summary>
        /// Represents name of file
        /// </summary>
        public string Name
        {
            get
            {
                int slashind = FilePath.LastIndexOf('\\');
                return FilePath.Substring(slashind + 1, FilePath.Length - slashind - 1);
            }
        }
        /// <summary>
        /// Constructor for creating file
        /// </summary>
        /// <param name="filepath">Path to file</param>
        public File(string filepath)
        {
            FilePath = filepath;
        }
        public File() { }

    }
}
