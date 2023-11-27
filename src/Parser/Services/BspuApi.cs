using Parser.Abstractions;
using Parser.Models;

namespace Parser.Services
{
    public class BspuApi : IBspuApi
    {
        const string BaseUrl = "https://asu.bspu.ru";

        private readonly ILogger _logger;

        private HttpClient _httpClient;

        public BspuApi(ILogger<BspuApi> logger)
        {
            _logger = logger;
            _httpClient = new HttpClient();
        }

        public async Task<IEnumerable<Professor>> GetProfessorsAsync(CancellationToken cancellationToken)
        {
            var url = $"{BaseUrl}/api/raspTeacherlist";
            var professorsResponse = await _httpClient.GetFromJsonAsync<Response<IEnumerable<Professor>>>(url, cancellationToken);
            return professorsResponse.Data;
        }

        public async Task<IEnumerable<Group>> GetGroupsAsync(CancellationToken cancellationToken)
        {
            var url = $"{BaseUrl}/api/raspGrouplist";
            var groupsResponse = await _httpClient.GetFromJsonAsync<Response<IEnumerable<Group>>>(url, cancellationToken);
            return groupsResponse.Data;
        }

        public async Task<ScheduleData> GetScheduleAsync(long groupId, CancellationToken cancellationToken)
        {
            var url = $"{BaseUrl}/api/Rasp?idGroup={groupId}";
            var scheduleResponse = await _httpClient.GetFromJsonAsync<Response<ScheduleData>>(url, cancellationToken);
            return scheduleResponse.Data;
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}
