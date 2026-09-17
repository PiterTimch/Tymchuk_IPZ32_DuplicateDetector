using Tymchuk_Petro_IPZ_32_Duplocate_Detector.Models;

namespace Tymchuk_Petro_IPZ_32_Duplocate_Detector.Abstractions
{
    public interface IValidatorInterface<T>
    {
        IValidatorInterface<T> AddRule(IValidationRuleInterface<T> rule);
        ValidationResultModel Validate(T value);
    }
}
