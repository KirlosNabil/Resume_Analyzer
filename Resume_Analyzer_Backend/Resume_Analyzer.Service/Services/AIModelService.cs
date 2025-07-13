using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Resume_Analyzer.Service.Configurations;
using Resume_Analyzer.Service.DTOs;
using Resume_Analyzer.Service.IServices;

namespace Resume_Analyzer.Service.Services
{
    public class AIModelService : IAIModelService
    {
        private readonly IOptions<ResumeAIOptions> _resumeAIOptions;

        public AIModelService(IOptions<ResumeAIOptions> resumeAIOptions)
        {
            _resumeAIOptions = resumeAIOptions;
        }
        public async Task<ResumeAIResultDTO> MatchResumeJob(string resumeText)
        {
            var apiUrl = $"{_resumeAIOptions.Value.BaseUrl}/match";

            using var httpClient = new HttpClient();

            var payload = new ResumeAIRequestDTO
            {
                Resume = resumeText
            };

            var content = new StringContent(
                JsonSerializer.Serialize(payload),
                Encoding.UTF8,
                "application/json");

            var response = await httpClient.PostAsync(apiUrl, content);

            if (!response.IsSuccessStatusCode)
                throw new Exception("Python API call failed");

            var responseBody = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<ResumeAIResultDTO>(
                responseBody,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );

            return result;
        }
    }
}
