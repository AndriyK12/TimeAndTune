namespace EFCore.Service
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class DatabaseSoundProvider : ISoundProvider
    {
        public Sound getSoundById(int id)
        {
            Sound sound = new Sound();
            using (var context = new TTContext())
            {
                var allSounds = context.Sounds.ToList();
                foreach (Sound s in allSounds)
                {
                    if (getSoundId(s) == id)
                    {
                        sound = s;
                    }
                }
            }
            return sound;
        }
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
