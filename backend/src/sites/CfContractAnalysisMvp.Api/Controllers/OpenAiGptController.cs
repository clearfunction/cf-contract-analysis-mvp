using CfContractAnalysisMvp.Api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CfContractAnalysisMvp.Api.Controllers;

[ApiController]
[Route("api/open-ai")]
public class OpenAiGptController : ControllerBase
{
    private readonly ILogger<OpenAiGptController> _logger;
    private readonly IOpenAiGptService _openAiGptService;

    public OpenAiGptController(ILogger<OpenAiGptController> logger, IOpenAiGptService openAiGptService)
    {
        _logger = logger;
        _openAiGptService = openAiGptService;
    }
    
    [HttpGet]
    [Route("health-status")]
    public IActionResult HealthStatus()
    {
        return Ok();
    }

    [HttpPost]
    [Route("analyze-contract")]
    public async Task<IActionResult> AnalyzeContract([FromForm] IFormFile file)
    {
        ArgumentNullException.ThrowIfNull(file);

        var result = await _openAiGptService.GetResponse(file);
        return Ok(result);
    }   
}