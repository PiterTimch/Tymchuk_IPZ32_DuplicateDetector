using Tymchuk_Petro_IPZ_32_Duplocate_Detector.Abstractions;

namespace Tymchuk_Petro_IPZ_32_Duplocate_Detector.Validators.Rules
{
    public class DirectoryNotNullOrEmptyRule : IValidationRuleInterface<string>
    {
        public string ErrorMessage => "Path cannot be null or empty";

        public bool Validate(string item)
        {
            if (item is null)
            {
                return false;
            }

            if (item is "")
            {
                return false;
            }

            return item.Trim().Length > 0;
        }
    }
}
