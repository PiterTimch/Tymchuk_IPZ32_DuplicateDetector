using System.Collections.Generic;
using System.Threading.Tasks;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;
using Tymchuk_Petro_IPZ_32_Duplocate_Detector.Abstractions;
using Tymchuk_Petro_IPZ_32_Duplocate_Detector.Benchmarks;
using Tymchuk_Petro_IPZ_32_Duplocate_Detector.Models;

namespace Tymchuk_Petro_IPZ_32_Duplocate_Detector.Services
{
    public class BenchmarkRunnerService : IBenchmarkRunnerService
    {
        private readonly IDirectoryScannerInterface directoryScannerItem;
        private readonly IDuplicateDetectionService duplicateDetectionItem;

        public BenchmarkRunnerService(
            IDirectoryScannerInterface directoryScannerItem,
            IDuplicateDetectionService duplicateDetectionItem)
        {
            this.directoryScannerItem = directoryScannerItem;
            this.duplicateDetectionItem = duplicateDetectionItem;
        }

        public async Task<BenchmarkExecutionResultModel> RunBenchmarkAsync(string folderPathText)
        {
            DuplicateDetectorBenchmark.TargetFolderPathText = folderPathText;

            Summary summaryItem = BenchmarkRunner.Run<DuplicateDetectorBenchmark>();

            IEnumerable<FileInfoModel> filesList = directoryScannerItem.ScanDirectory(folderPathText);
            List<DuplicateGroupResult> duplicatesList = await duplicateDetectionItem.FindDuplicatesAsync(filesList);

            return new BenchmarkExecutionResultModel
            {
                DuplicatesList = duplicatesList
            };
        }
    }
}
