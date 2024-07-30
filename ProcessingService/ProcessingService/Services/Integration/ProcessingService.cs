using ProcessingService.Models.RequestModels;
using ProcessingService.Services.Interface;

namespace ProcessingService.Services.Integration
{
    public class ProcessingService : IProcessingService
    {
        private readonly ILogger<ProcessingService> _logger;

        public ProcessingService(ILogger<ProcessingService> logger)
        {
            _logger = logger;
        }

        public void ProcessMessage(InputRequestModel input)
        {
            int result = input.Number1 + input.Number2;
            _logger.LogInformation($"Processed: {input.Number1} + {input.Number2} = {result}");
        }
    }
}
