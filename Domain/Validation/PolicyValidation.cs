using InsuranceAPI.Models;

namespace InsuranceAPI.Domain.Validation
{
    public static class PolicyValidation
    {

        public static void Validate(Policy policy)
        {
            ValidateStructure(policy);
        }

        public static void ValidateStructure(Policy policy)
        {
            
        }

    }
}
