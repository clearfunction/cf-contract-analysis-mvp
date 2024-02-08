using System.Text.Json;
using CfContractAnalysisMvp.Api.Interfaces;
using CfContractAnalysisMvp.Api.Models.Contract;
using CfContractAnalysisMvp.Api.Models.Document;
using Microsoft.AspNetCore.Mvc;

namespace CfContractAnalysisMvp.Api.Controllers;

[ApiController]
[Route("api/azure")]
public class AzureDocumentAiController : ControllerBase
{
    private readonly ILogger<AzureDocumentAiController> _logger;
    private readonly IAzureDocumentAiAnalysisService _azureDocumentAiAnalysisService;

    public AzureDocumentAiController(ILogger<AzureDocumentAiController> logger, IAzureDocumentAiAnalysisService azureDocumentAiAnalysisService)
    {
        _logger = logger;
        _azureDocumentAiAnalysisService = azureDocumentAiAnalysisService;
    }
    
    [HttpGet]
    [Route("health-status")]
    public IActionResult HealthStatus()
    {
        return Ok();
    }

    [HttpPost]
    [Route("analyze-document")]
    public async Task<IActionResult> AnalyzeDocument([FromForm] IFormFile file)
    {
        ArgumentNullException.ThrowIfNull(file);

        var analysis = await _azureDocumentAiAnalysisService.AnalyzeDocument(file, "prebuilt-document");
        var result = _azureDocumentAiAnalysisService.FormatDocumentAnalysis(analysis);
        
        // await SaveJsonFile(documentResult: result);
        return Ok(result);
    }
    
    [HttpPost]
    [Route("analyze-contract")]
    public async Task<IActionResult> AnalyzeContract([FromForm] IFormFile file)
    {
        ArgumentNullException.ThrowIfNull(file);

        var analysis = await _azureDocumentAiAnalysisService.AnalyzeDocument(file, "prebuilt-contract");
        var result = _azureDocumentAiAnalysisService.FormatContractAnalysis(analysis);
        
        // await SaveJsonFile(contractResult: result);
        return Ok(result);
    }

    private async Task SaveJsonFile(ContractAnalysisResult? contractResult = null, DocumentAnalysisResult? documentResult = null)
    {
        await using FileStream createStream = System.IO.File.Create("sample.json");

        if (contractResult != null)
        {
            await JsonSerializer.SerializeAsync(createStream, contractResult);
        }

        if (documentResult != null)
        {
            await JsonSerializer.SerializeAsync(createStream, documentResult);
        }

        await createStream.DisposeAsync();
    }
}
