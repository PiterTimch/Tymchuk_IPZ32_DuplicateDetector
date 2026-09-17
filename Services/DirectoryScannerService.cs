using System;
using System.Collections.Generic;
using System.IO;
using Tymchuk_Petro_IPZ_32_Duplocate_Detector.Abstractions;
using Tymchuk_Petro_IPZ_32_Duplocate_Detector.Models;

namespace Tymchuk_Petro_IPZ_32_Duplocate_Detector.Services
{
    public class DirectoryScannerService : IDirectoryScannerInterface
    {
        public IEnumerable<FileInfoModel> ScanDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                yield break;
            }

            EnumerationOptions optionsItem = new EnumerationOptions
            {
                RecurseSubdirectories = true,
                IgnoreInaccessible = true
            };

            IEnumerable<string> filesList = Directory.EnumerateFiles(path, "*", optionsItem);

            foreach (string fileText in filesList)
            {
                FileInfoModel? modelItem = GetFileInfo(fileText);
                if (modelItem is not null)
                {
                    yield return modelItem;
                }
            }
        }

        private FileInfoModel? GetFileInfo(string pathText)
        {
            try
            {
                FileInfo fileItem = new FileInfo(pathText);
                return new FileInfoModel
                {
                    FilePath = fileItem.FullName,
                    FileSize = fileItem.Length
                };
            }
            catch (UnauthorizedAccessException)
            {
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
