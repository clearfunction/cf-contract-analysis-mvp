using CfContractAnalysisMvp.Api.Interfaces;
using OpenAI_API;
using OpenAI_API.Models;

namespace CfContractAnalysisMvp.Api.Services;

public class OpenAiGptService : IOpenAiGptService
{
    private readonly IConfiguration _configuration;
    private readonly IAzureDocumentAiAnalysisService _azureDocumentAiAnalysisService;
    
    public OpenAiGptService(IConfiguration configuration, IAzureDocumentAiAnalysisService azureDocumentAiAnalysisService)
    {
        _configuration = configuration;
        _azureDocumentAiAnalysisService = azureDocumentAiAnalysisService;
    }

    public async Task<List<string>> GetResponse(IFormFile file)
    {
        try
        {
            var apiKey = _configuration.GetValue<string>("OpenAiSettings:GChatAPIKEY");

            var openAiClient = new OpenAIAPI(new APIAuthentication(apiKey));
            var chat = openAiClient.Chat.CreateConversation();
            chat.Model = Model.GPT4_Turbo;
            chat.RequestParameters.Temperature = 0;
            
            // use azure ai to get document paragraphs
            var document = await _azureDocumentAiAnalysisService.AnalyzeDocument(file, "prebuilt-read");
            var paragraphs = document?.Paragraphs;
            if (paragraphs is null)
            {
                return new List<string>();
            }

            // give instruction as System
            chat.AppendSystemMessage("You are a document research assistant that reads long files and summarizes them.");
            chat.AppendSystemMessage("The conversation will start by the user saying 'Upload Starting Now.' The user will then provide you with document paragraphs.");
            chat.AppendSystemMessage("You will not answer until the user has asked, 'Please Start Summary Results.'");
            chat.AppendSystemMessage("You will then provide the most accurate details about the current information given to you.");
            chat.AppendSystemMessage("You will start by listing in bullet points the following information: " +
                                            "Buyer Name (if applicable), Seller Name (if applicable), Contract Amount (if applicable)," +
                                            "Contract Key Dates, and Contract Property Address (if applicable).");
            chat.AppendSystemMessage("After providing that information, you will give a summary of all other major details in paragraph form.");

            // upload document paragraphs
            chat.AppendUserInput("Upload Starting Now.");
            foreach (var paragraph in paragraphs) chat.AppendUserInput(paragraph.Content);
            chat.AppendUserInput("Please Start Summary Results.");
            
            // get open ai response
            await chat.GetResponseFromChatbotAsync();
            return (from msg in chat.Messages where msg.Role == "assistant" select msg.TextContent).ToList();
        }
        catch (Exception ex)
        {
            return new List<string>();
        }
    }
}