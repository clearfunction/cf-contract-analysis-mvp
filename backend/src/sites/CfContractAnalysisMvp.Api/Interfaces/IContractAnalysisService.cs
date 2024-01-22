using Azure.AI.FormRecognizer.DocumentAnalysis;
using CfContractAnalysisMvp.Api.Models.Contract;
using CfContractAnalysisMvp.Api.Models.Document;

namespace CfContractAnalysisMvp.Api.Interfaces;

public interface IContractAnalysisService
{
    public Task<AnalyzeResult?> AnalyzeDocument(IFormFile input, string model);

    public DocumentAnalysisResult FormatDocumentAnalysis(AnalyzeResult? analyzeResult);

    public ContractAnalysisResult FormatContractAnalysis(AnalyzeResult? analyzeResult);
}