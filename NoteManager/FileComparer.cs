using System.Collections.Generic;

namespace NoteManager
{
    class FileComparer : IEqualityComparer<File>
    {
        public bool Equals(File x, File y)
        {
            if(x.FilePath == y.FilePath)
            {
                return true;
            }
            return false;
        }

        public int GetHashCode(File obj)
        {
            return obj.GetHashCode();
        }
    }
}
