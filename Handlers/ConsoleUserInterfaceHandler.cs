using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tymchuk_Petro_IPZ_32_Duplocate_Detector.Abstractions;
using Tymchuk_Petro_IPZ_32_Duplocate_Detector.Models;

namespace Tymchuk_Petro_IPZ_32_Duplocate_Detector.Handlers
{
    public class ConsoleUserInterfaceHandler : IConsoleUserInterfaceHandler
    {
        private readonly IValidatorInterface<string> pathValidatorItem;
        private readonly IBenchmarkRunnerService benchmarkRunnerItem;

        public ConsoleUserInterfaceHandler(
            IValidatorInterface<string> pathValidatorItem,
            IBenchmarkRunnerService benchmarkRunnerItem)
        {
            this.pathValidatorItem = pathValidatorItem;
            this.benchmarkRunnerItem = benchmarkRunnerItem;
        }

        public async Task RunAsync()
        {
            Console.WriteLine("Duplicate Detector Console Application");

            string folderPathText = ReadValidFolderPath();

            Console.WriteLine("Running BenchmarkDotNet for scanning and duplicate detection...");

            BenchmarkExecutionResultModel benchmarkResultItem = await benchmarkRunnerItem.RunBenchmarkAsync(folderPathText);

            DisplayResults(benchmarkResultItem.DuplicatesList);
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

        private void DisplayResults(List<DuplicateGroupResult> duplicatesList)
        {
            Console.WriteLine();
            Console.WriteLine("Benchmark Completed.");
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
