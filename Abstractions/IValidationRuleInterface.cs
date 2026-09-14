namespace Tymchuk_Petro_IPZ_32_Duplocate_Detector.Abstractions
{
    public interface IValidationRuleInterface<T>
    {
        bool Validate(T item);
        string ErrorMessage { get; }
    }
}
