using InsuranceAPI.DTOs.Requests;
using InsuranceAPI.Models;
using InsuranceAPI.Models.Insurances;

namespace InsuranceAPI.Services.Factories
{
    public static class InsuranceFactory
    {
        public static Insurance Create(InsuranceRequest request)
        {
            if (request is null)
                throw new InvalidOperationException("");

            Insurance insurance = request.InsuranceType switch 
            {
                InsuranceType.Accident => new AccidentInsurance(request),
                InsuranceType.Car => new CarInsurance(request),
                InsuranceType.House => new HouseInsurance(request),
                InsuranceType.Pet => new PetInsurance(request),
                InsuranceType.Spacecraft => new SpacecraftInsurance(request),
                InsuranceType.Travel => new TravelInsurance(request),

                _ => throw new ArgumentException($"Unsupported insurance type: {request.InsuranceType}")
            };

            return insurance;
        }   
    }
}
