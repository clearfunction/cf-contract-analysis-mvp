using OpenAI_API.Chat;

namespace CfContractAnalysisMvp.Api.Interfaces;

public interface IOpenAiGptService
{
    Task<List<string>> GetResponse(IFormFile input);
}