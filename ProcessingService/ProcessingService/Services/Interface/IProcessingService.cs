using ProcessingService.Models.RequestModels;

namespace ProcessingService.Services.Interface
{
    public interface IProcessingService
    {
        void ProcessMessage(InputRequestModel input);
    }
}
