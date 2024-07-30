using ParameterService.Models.RequestModels;

namespace ParameterService.Services.Interface
{
    public interface IInputService
    {
        void SendMessage(InputRequestModel input);
    }
}
