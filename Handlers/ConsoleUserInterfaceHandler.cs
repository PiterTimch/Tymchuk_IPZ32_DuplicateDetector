using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Tymchuk_Petro_IPZ_32_Duplocate_Detector.Abstractions;
using Tymchuk_Petro_IPZ_32_Duplocate_Detector.Models;

namespace Tymchuk_Petro_IPZ_32_Duplocate_Detector.Handlers
{
    public class ConsoleUserInterfaceHandler : IConsoleUserInterfaceHandler
    {
        private readonly IValidatorInterface<string> pathValidatorItem;
        private readonly IDirectoryScannerInterface directoryScannerItem;
        private readonly IDuplicateDetectionService duplicateDetectionItem;

        public ConsoleUserInterfaceHandler(
            IValidatorInterface<string> pathValidatorItem,
            IDirectoryScannerInterface directoryScannerItem,
            IDuplicateDetectionService duplicateDetectionItem)
        {
            this.pathValidatorItem = pathValidatorItem;
            this.directoryScannerItem = directoryScannerItem;
            this.duplicateDetectionItem = duplicateDetectionItem;
        }

        public async Task RunAsync()
        {
            Console.WriteLine("Duplicate Detector Console Application");

            string folderPathText = ReadValidFolderPath();

            Console.WriteLine("Scanning directory for files...");
            Stopwatch timerItem = Stopwatch.StartNew();

            IEnumerable<FileInfoModel> filesList = directoryScannerItem.ScanDirectory(folderPathText);
            List<DuplicateGroupResult> duplicatesList = await duplicateDetectionItem.FindDuplicatesAsync(filesList);

            timerItem.Stop();

            DisplayResults(duplicatesList, timerItem.ElapsedMilliseconds);
        }

        private string ReadValidFolderPath()
        {
            while (true)
            {
                Console.Write("Enter folder path: ");
                string inputPathText = Console.ReadLine() ?? string.Empty;

                ValidationResultModel resultItem = pathValidatorItem.Validate(inputPathText);
                if (resultItem.IsValid)
                {
                    return inputPathText;
                }

                Console.WriteLine("Validation Error: " + resultItem.ErrorMessage);
                Console.WriteLine("Please try again.");
            }
        }

        private void DisplayResults(List<DuplicateGroupResult> duplicatesList, long elapsedMilliseconds)
        {
            Console.WriteLine();
            Console.WriteLine("Scan Completed in " + elapsedMilliseconds + " ms.");
            Console.WriteLine("Found duplicate groups: " + duplicatesList.Count);
            Console.WriteLine();

            if (duplicatesList.Count == 0)
            {
                Console.WriteLine("No duplicate files found.");
                return;
            }

            int groupIndex = 1;
            foreach (DuplicateGroupResult groupItem in duplicatesList)
            {
                Console.WriteLine("Group " + groupIndex + " | File Size: " + groupItem.FileSize + " bytes");
                foreach (string filePathText in groupItem.FilePathsList)
                {
                    Console.WriteLine("  - " + filePathText);
                }
                Console.WriteLine();
                groupIndex++;
            }
        }
    }
}
