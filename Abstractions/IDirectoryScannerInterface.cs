using System.Collections.Generic;
using Tymchuk_Petro_IPZ_32_Duplocate_Detector.Models;

namespace Tymchuk_Petro_IPZ_32_Duplocate_Detector.Abstractions
{
    public interface IDirectoryScannerInterface
    {
        IEnumerable<FileInfoModel> ScanDirectory(string path);
    }
}
