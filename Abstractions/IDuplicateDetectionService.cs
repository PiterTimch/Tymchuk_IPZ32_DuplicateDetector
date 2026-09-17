using System.Collections.Generic;
using System.Threading.Tasks;
using Tymchuk_Petro_IPZ_32_Duplocate_Detector.Models;

namespace Tymchuk_Petro_IPZ_32_Duplocate_Detector.Abstractions
{
    public interface IDuplicateDetectionService
    {
        Task<List<DuplicateGroupResult>> FindDuplicatesAsync(IEnumerable<FileInfoModel> files);
    }
}
