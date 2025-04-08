using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using UglyToad.PdfPig;

public class AIAnalysisService
{
    private readonly HttpClient _httpClient;
    private readonly string _azureEndpoint;
    private readonly string _azureApiKey;
    private readonly string _azureDeployment;
    private readonly string _pdfFilePath;

    public AIAnalysisService(HttpClient httpClient, IConfiguration configuration, IWebHostEnvironment env)
    {
        _httpClient = httpClient;
        _azureEndpoint = configuration["AzureOpenAI:Endpoint"];
        _azureApiKey = configuration["AzureOpenAI:ApiKey"];
        _azureDeployment = configuration["AzureOpenAI:DeploymentName"];
        _pdfFilePath = Path.Combine(env.WebRootPath, "POLICIES-CSharpTek.pdf");
    }

    public async Task<string> GetResponseAsync(string userQuery)
    {
        if (IsCompanyRelated(userQuery))
        {
            string extractedText = ExtractTextFromPdf();
            return await SearchCompanyPolicies(userQuery, extractedText);
        }
        else
        {
            return await GetGeneralResponseFromAzure(userQuery);
        }
    }

    private bool IsCompanyRelated(string query)
    {
        var keywords = new[] { "policy", "rules", "leave", "benefits", "salary", "hr", "office timing", "work hours", "holidays", "company regulations", "company","organization","office","csharptek","cloudgarner" };
        return keywords.Any(keyword => query.ToLower().Contains(keyword));
    }

    private string ExtractTextFromPdf()
    {
        if (!File.Exists(_pdfFilePath))
        {
            return "";
        }

        StringBuilder textBuilder = new StringBuilder();
        using (var document = PdfDocument.Open(_pdfFilePath))
        {
            foreach (var page in document.GetPages())
            {
                textBuilder.AppendLine(page.Text);
            }
        }
        return textBuilder.ToString();
    }

    private async Task<string> SearchCompanyPolicies(string query, string documentText)
    {
        if (string.IsNullOrWhiteSpace(documentText))
        {
            return "Company policy document is empty or not found.";
        }

        var requestData = new
        {
            messages = new[]
            {
                new { role = "system", content = "Search the following document for relevant information and return a concise answer." },
                new { role = "user", content = $"Document: {documentText}\n\nQuery: {query}" }
            },
            temperature = 0.7,
            max_tokens = 500
        };

        return await QueryAzureOpenAI(requestData);
    }

    private async Task<string> GetGeneralResponseFromAzure(string query)
    {
        var requestData = new
        {
            messages = new[]
            {
                new { role = "user", content = query }
            },
            temperature = 0.7,
            max_tokens = 500
        };

        return await QueryAzureOpenAI(requestData);
    }

    private async Task<string> QueryAzureOpenAI(object requestData)
    {
        var requestBody = JsonConvert.SerializeObject(requestData);
        var request = new HttpRequestMessage(HttpMethod.Post, $"{_azureEndpoint}/openai/deployments/{_azureDeployment}/chat/completions?api-version=2024-02-15-preview")
        {
            Content = new StringContent(requestBody, Encoding.UTF8, "application/json")
        };
        request.Headers.Add("api-key", _azureApiKey);

        var response = await _httpClient.SendAsync(request);
        if (!response.IsSuccessStatusCode)
        {
            return "Error fetching response from Azure OpenAI.";
        }

        var jsonResponse = await response.Content.ReadAsStringAsync();
        dynamic result = JsonConvert.DeserializeObject(jsonResponse);
        return result.choices[0].message.content;
    }
}
