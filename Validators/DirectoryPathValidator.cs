using System.Collections.Generic;
using Tymchuk_Petro_IPZ_32_Duplocate_Detector.Abstractions;
using Tymchuk_Petro_IPZ_32_Duplocate_Detector.Models;

namespace Tymchuk_Petro_IPZ_32_Duplocate_Detector.Validators
{
    public class DirectoryPathValidator : IValidatorInterface<string>
    {
        private readonly List<IValidationRuleInterface<string>> rulesList = new List<IValidationRuleInterface<string>>();

        public IValidatorInterface<string> AddRule(IValidationRuleInterface<string> ruleItem)
        {
            rulesList.Add(ruleItem);
            return this;
        }

        public ValidationResultModel Validate(string valueItem)
        {
            foreach (IValidationRuleInterface<string> ruleItem in rulesList)
            {
                bool isSuccess = ruleItem.Validate(valueItem);
                if (!isSuccess)
                {
                    return new ValidationResultModel
                    {
                        IsValid = false,
                        ErrorMessage = ruleItem.ErrorMessage
                    };
                }
            }

            return new ValidationResultModel
            {
                IsValid = true,
                ErrorMessage = string.Empty
            };
        }
    }
}
