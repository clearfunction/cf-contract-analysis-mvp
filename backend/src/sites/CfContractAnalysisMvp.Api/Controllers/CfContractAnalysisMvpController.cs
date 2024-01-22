using System.Text.Json;
using CfContractAnalysisMvp.Api.Interfaces;
using CfContractAnalysisMvp.Api.Models.Contract;
using CfContractAnalysisMvp.Api.Models.Document;
using Microsoft.AspNetCore.Mvc;

namespace CfContractAnalysisMvp.Api.Controllers;

[ApiController]
[Route("api")]
public class CfContractAnalysisMvpController : ControllerBase
{
    private readonly ILogger<CfContractAnalysisMvpController> _logger;
    private readonly IContractAnalysisService _contractAnalysisService;

    public CfContractAnalysisMvpController(ILogger<CfContractAnalysisMvpController> logger, IContractAnalysisService contractAnalysisService)
    {
        _logger = logger;
        _contractAnalysisService = contractAnalysisService;
    }

    [HttpPost]
    [Route("analyze-document")]
    public async Task<IActionResult> AnalyzeDocument([FromForm] IFormFile file)
    {
        ArgumentNullException.ThrowIfNull(file);

        var analysis = await _contractAnalysisService.AnalyzeDocument(file, "prebuilt-document");
        var result = _contractAnalysisService.FormatDocumentAnalysis(analysis);
        await SaveJsonFile(documentResult: result);
        return Ok(result);
    }
    
    [HttpPost]
    [Route("analyze-contract")]
    public async Task<IActionResult> AnalyzeContract([FromForm] IFormFile file)
    {
        ArgumentNullException.ThrowIfNull(file);

        var analysis = await _contractAnalysisService.AnalyzeDocument(file, "prebuilt-contract");
        var result = _contractAnalysisService.FormatContractAnalysis(analysis);
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
