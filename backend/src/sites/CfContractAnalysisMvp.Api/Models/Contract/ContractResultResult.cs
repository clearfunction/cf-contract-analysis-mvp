namespace CfContractAnalysisMvp.Api.Models.Contract;

public class ContractFieldResult
{
    public string Key { get; set; } = string.Empty;
    
    public string? Content { get; set; }
    
    public List<ContractFieldResult>? SubResults { get; set; }
}