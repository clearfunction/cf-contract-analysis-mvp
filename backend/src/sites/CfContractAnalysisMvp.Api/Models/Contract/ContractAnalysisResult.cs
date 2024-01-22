namespace CfContractAnalysisMvp.Api.Models.Contract;

public class ContractAnalysisResult
{
    public ContractAnalysisResult()
    {
        Results = new List<ContractFieldResult>();
    }

    public List<ContractFieldResult> Results { get; set; }
}