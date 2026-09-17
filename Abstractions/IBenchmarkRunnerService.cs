using System.Threading.Tasks;
using Tymchuk_Petro_IPZ_32_Duplocate_Detector.Models;

namespace Tymchuk_Petro_IPZ_32_Duplocate_Detector.Abstractions
{
    public interface IBenchmarkRunnerService
    {
        Task<BenchmarkExecutionResultModel> RunBenchmarkAsync(string folderPathText);
    }
}
