using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomPicSpam
{
    public class DataSettings
    {
        public bool Allow_SaveFolderPath { get; set; }
        public bool Allow_SaveFilePath { get; set; }
        public bool Allow_CopyFiles { get; set; }
        public bool Allow_Spammer { get; set; }
        public int HowManyFilesSpamming { get; set; }

        public bool Allow_txt { get; set; }
        public bool Allow_html { get; set; }
        public bool Allow_css { get; set; }
        public bool Allow_sql { get; set; }
        public bool Allow_docx { get; set; }
        public bool Allow_ppts { get; set; }
        public bool Allow_xlsx { get; set; }
        public bool Allow_png { get; set; }
        public bool Allow_jpg { get; set; }
        public bool Allow_gif { get; set; }
        public bool Allow_exe { get; set; }
        public bool Allow_mp3 { get; set; }
        public bool Allow_mp4 { get; set; }

        public bool ShowFolderPathInConsole { get; set; }
        public bool ShowFilePathInConsole { get; set; }
    }
}
