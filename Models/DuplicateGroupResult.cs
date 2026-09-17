using System.Collections.Generic;

namespace Tymchuk_Petro_IPZ_32_Duplocate_Detector.Models
{
    public class DuplicateGroupResult
    {
        public long FileSize { get; set; }
        public List<string> FilePathsList { get; set; } = new List<string>();
    }
}
