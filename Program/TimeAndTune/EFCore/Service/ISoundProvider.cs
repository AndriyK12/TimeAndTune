using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.Service
{
    internal interface ISoundProvider
    {
        int getSoundId(Sound sound);

        string getSoundName(Sound sound);

        string getAudioFilePath(Sound sound);

        string getPhotoFilePath(Sound sound);
    }
}
