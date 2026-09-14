using System.Collections.Generic;

namespace Tymchuk_Petro_IPZ_32_Duplocate_Detector.Models
{
    public class BenchmarkExecutionResultModel
    {
        public List<DuplicateGroupResult> DuplicatesList { get; set; } = new List<DuplicateGroupResult>();
    }
}
