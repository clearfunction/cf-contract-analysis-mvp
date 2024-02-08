using Azure;
using Azure.AI.FormRecognizer.DocumentAnalysis;
using CfContractAnalysisMvp.Api.Interfaces;
using CfContractAnalysisMvp.Api.Models.Contract;
using CfContractAnalysisMvp.Api.Models.Document;
using DocumentKeyValuePair = Azure.AI.FormRecognizer.DocumentAnalysis.DocumentKeyValuePair;

namespace CfContractAnalysisMvp.Api.Services;

public class AzureDocumentAiAnalysisService : IAzureDocumentAiAnalysisService
{
    private readonly IConfiguration _configuration;
    private readonly IKeyTermService _keyTermService;
    
    public AzureDocumentAiAnalysisService(IConfiguration configuration, IKeyTermService keyTermService)
    {
        _configuration = configuration;
        _keyTermService = keyTermService;
    }

    public async Task<AnalyzeResult?> AnalyzeDocument(IFormFile file, string model)
    {
        try
        {
            var serviceEndpoint = _configuration.GetValue<string>("CognitiveService:Endpoint");
            var serviceCredential = _configuration.GetValue<string>("CognitiveService:Key");
            var client = new DocumentAnalysisClient(new Uri(serviceEndpoint), new AzureKeyCredential(serviceCredential));
            AnalyzeDocumentOperation operation = await client.AnalyzeDocumentAsync(WaitUntil.Completed, model, file.OpenReadStream());
            return operation.Value;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public DocumentAnalysisResult FormatDocumentAnalysis(AnalyzeResult? analyzeResult)
    {
        ArgumentNullException.ThrowIfNull(analyzeResult);
        
        var analysisResult = new DocumentAnalysisResult();
        foreach (DocumentKeyValuePair kvp in analyzeResult.KeyValuePairs)
        {
            analysisResult.KeyValuePairsList.Add(new Models.Document.DocumentKeyValuePair()
            {
                Key = kvp.Key.Content,
                Value = kvp.Value?.Content ?? "No Value"
            });
        }
        
        _keyTermService.FindKeyTerms(analysisResult);

        return analysisResult;
    }

    public ContractAnalysisResult FormatContractAnalysis(AnalyzeResult? analyzeResult)
    {
        ArgumentNullException.ThrowIfNull(analyzeResult);

        var formattedResult = new ContractAnalysisResult();
        foreach (var doc in analyzeResult.Documents)
        {
            foreach (var fieldKeyValuePair in doc.Fields)
            {
                ContractFieldResult x = CreateFieldResults(fieldKeyValuePair.Key, fieldKeyValuePair.Value);
                formattedResult.Results.Add(x);
            }
        }
        
        return formattedResult;
    }

    private ContractFieldResult CreateFieldResults(string documentKey, DocumentField documentField)
    {
        switch (documentField.FieldType)
        {
            case DocumentFieldType.List:
                var resultList = new ContractFieldResult() { Key = documentKey, SubResults = new List<ContractFieldResult>() };
                foreach (var fieldItem in documentField.Value.AsList())
                {
                    var result = CreateFieldResults(documentKey, fieldItem);
                    resultList.SubResults.Add(result);
                }

                return resultList;
            
            case DocumentFieldType.Dictionary:
                var resultDictionary = new ContractFieldResult() { Key = documentKey, SubResults = new List<ContractFieldResult>() };
                foreach (var fieldValue in documentField.Value.AsDictionary())
                {
                    var result = CreateFieldResults(fieldValue.Key, fieldValue.Value);
                    resultDictionary.SubResults.Add(result);
                }

                return resultDictionary;
            
            default:
                return new ContractFieldResult()
                {
                    Key = documentKey,
                    Content = documentField.Content
                };
        }
    }
}