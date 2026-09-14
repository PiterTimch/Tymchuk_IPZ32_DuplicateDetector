using System.Threading.Tasks;
using Tymchuk_Petro_IPZ_32_Duplocate_Detector.Abstractions;
using Tymchuk_Petro_IPZ_32_Duplocate_Detector.Handlers;
using Tymchuk_Petro_IPZ_32_Duplocate_Detector.Services;
using Tymchuk_Petro_IPZ_32_Duplocate_Detector.Validators;
using Tymchuk_Petro_IPZ_32_Duplocate_Detector.Validators.Rules;

namespace Tymchuk_Petro_IPZ_32_Duplocate_Detector
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            IValidatorInterface<string> pathValidatorItem = new DirectoryPathValidator()
                .AddRule(new DirectoryNotNullOrEmptyRule())
                .AddRule(new DirectoryExistsRule())
                .AddRule(new DirectoryAccessPermissionRule());

            IDirectoryScannerInterface directoryScannerItem = new DirectoryScannerService();
            IDuplicateDetectionService duplicateDetectionItem = new DuplicateDetectionService();

            IConsoleUserInterfaceHandler userInterfaceItem = new ConsoleUserInterfaceHandler(
                pathValidatorItem,
                directoryScannerItem,
                duplicateDetectionItem
            );

            await userInterfaceItem.RunAsync();
        }
    }
}
