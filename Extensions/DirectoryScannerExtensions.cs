using System.Collections.Generic;
using Tymchuk_Petro_IPZ_32_Duplocate_Detector.Abstractions;
using Tymchuk_Petro_IPZ_32_Duplocate_Detector.Models;

namespace Tymchuk_Petro_IPZ_32_Duplocate_Detector.Extensions
{
    public static class DirectoryScannerExtensions
    {
        public static IEnumerable<FileInfoModel> AsFileEnumerable(this IDirectoryScannerInterface scannerItem, string pathText)
        {
            return scannerItem.ScanDirectory(pathText);
        }
    }
}
