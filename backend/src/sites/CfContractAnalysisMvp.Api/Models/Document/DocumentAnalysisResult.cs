namespace CfContractAnalysisMvp.Api.Models.Document;

public class DocumentAnalysisResult
{
    public DocumentAnalysisResult()
    {
        BuyerName = string.Empty;
        SellerName = string.Empty;
        PropertyAddress = string.Empty;
        ContractAmount = string.Empty;
        ContractDate = string.Empty;
        KeyValuePairsList = new List<DocumentKeyValuePair>();
    }

    public string BuyerName { get; set; }

    public string SellerName { get; set; }

    public string PropertyAddress { get; set; }

    public string ContractAmount { get; set; }

    public string ContractDate { get; set; }
    
    public List<DocumentKeyValuePair> KeyValuePairsList { get; set; }
}