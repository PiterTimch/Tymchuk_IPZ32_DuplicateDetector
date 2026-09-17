using System;
using System.IO;
using Tymchuk_Petro_IPZ_32_Duplocate_Detector.Abstractions;

namespace Tymchuk_Petro_IPZ_32_Duplocate_Detector.Validators.Rules
{
    public class DirectoryAccessPermissionRule : IValidationRuleInterface<string>
    {
        public string ErrorMessage => "Access permission denied";

        public bool Validate(string item)
        {
            if (!Directory.Exists(item))
            {
                return false;
            }

            try
            {
                string[] filesList = Directory.GetFiles(item);
                return filesList is not null;
            }
            catch (UnauthorizedAccessException)
            {
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
