using Bluong4_Project2V4.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bluong4_Project2V4.Data
{
    public interface ISetWallPaper
    {
        Wallpaper SetWallPaper();
        object RestoreWallPaper(byte[] input);
        string GetIdentifier();
    }
}
