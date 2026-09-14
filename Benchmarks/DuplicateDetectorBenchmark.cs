using System.Collections.Generic;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using Tymchuk_Petro_IPZ_32_Duplocate_Detector.Abstractions;
using Tymchuk_Petro_IPZ_32_Duplocate_Detector.Models;
using Tymchuk_Petro_IPZ_32_Duplocate_Detector.Services;

namespace Tymchuk_Petro_IPZ_32_Duplocate_Detector.Benchmarks
{
    [MemoryDiagnoser]
    [Config(typeof(CustomBenchmarkConfig))]
    public class DuplicateDetectorBenchmark
    {
        public static string TargetFolderPathText { get; set; } = string.Empty;

        private IDirectoryScannerInterface directoryScannerItem = new DirectoryScannerService();
        private IDuplicateDetectionService duplicateDetectionItem = new DuplicateDetectionService();

        [Benchmark]
        public async Task<List<DuplicateGroupResult>> RunScanAndDetectionBenchmark()
        {
            IEnumerable<FileInfoModel> filesList = directoryScannerItem.ScanDirectory(TargetFolderPathText);
            return await duplicateDetectionItem.FindDuplicatesAsync(filesList);
        }

        private class CustomBenchmarkConfig : ManualConfig
        {
            public CustomBenchmarkConfig()
            {
                Options = ConfigOptions.DisableOptimizationsValidator;
            }
        }
    }
}
