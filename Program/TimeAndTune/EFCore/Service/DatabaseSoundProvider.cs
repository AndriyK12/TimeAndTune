using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.Service
{
    internal class DatabaseSoundProvider : ISoundProvider
    {
        public string getAudioFilePath(Sound sound)
        {
            return sound.Audiofilepath;
        }

        public string getPhotoFilePath(Sound sound)
        {
            return sound.Photofilepath;
        }

        public int getSoundId(Sound sound)
        {
            return sound.Soundid;
        }

        public string getSoundName(Sound sound)
        {
            return sound.Soundname;
        }
    }
}
