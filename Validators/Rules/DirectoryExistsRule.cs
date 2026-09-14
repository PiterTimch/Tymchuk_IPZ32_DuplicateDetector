using System.IO;
using Tymchuk_Petro_IPZ_32_Duplocate_Detector.Abstractions;

namespace Tymchuk_Petro_IPZ_32_Duplocate_Detector.Validators.Rules
{
    public class DirectoryExistsRule : IValidationRuleInterface<string>
    {
        public string ErrorMessage => "Directory does not exist";

        public bool Validate(string item)
        {
            if (item is not string pathText)
            {
                return false;
            }

            return Directory.Exists(pathText);
        }
    }
}
