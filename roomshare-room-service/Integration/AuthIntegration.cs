using roomshare_room_service.Model;
using System.Net.Http.Headers;
using System.Text.Json;

namespace roomshare_room_service_command.Integration
{
    public static class AuthIntegration
    {
        private static readonly HttpClient _client = new HttpClient();

        public static async Task<User?> GetUser(string token)
        {
            if (token == null) return null;

            token = token.Split(" ")[1];

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            
            var result = await _client.GetAsync("http://host.docker.internal:5003/api/user");
            var user = await result.Content.ReadAsStringAsync();

            if (user == null) return null;

            var userModel = JsonSerializer.Deserialize<User>(user);

            return userModel;
        }
    }
}
