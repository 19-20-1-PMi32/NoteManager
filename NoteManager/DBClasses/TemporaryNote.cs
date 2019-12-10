﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteManager.DBClasses
{
    static class TemporaryNote
    {
        public static bool IsUsed { get; set; } = false;
        public static string Text { get; set; }
        public static DateTime CreationTime { get; set; }
        public static List<Video> Videos { get; set; }
        public static List<Picture> Pictures { get; set; }
        public static List<Music> Musics { get; set; }
        public static List<Record> Records { get; set; }

        public static void AnnulOfNote()
        {
            IsUsed = false;
            Text = null;
            Videos = null;
            Pictures = null;
            Musics = null;
            Records = null;
        }
    }
}
