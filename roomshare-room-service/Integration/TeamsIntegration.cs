using Newtonsoft.Json;
using roomshare_room_service.Model;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using static Confluent.Kafka.ConfigPropertyNames;

namespace roomshare_room_service_command.Integration
{
    public static class TeamsIntegration
    {
        private static readonly HttpClient _client = new HttpClient();

        public static async Task SendMessageTeams(string message)
        {
            var messageTeams = new MessageTeams()
            {
                channel = "88AOJ-RoomShare",
                text = message,
                icon_emoji = ":mailbox_with_mail:",
                username = "app_kafka_producer"
            };

            string? webhook = Environment.GetEnvironmentVariable("WEBHOOK");

            using StringContent jsonContent = new(JsonConvert.SerializeObject(messageTeams), Encoding.UTF8, "application/json");

            var result = await _client.PostAsync(
                    webhook,
                    jsonContent
            );

            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine("SUCCEDED: Sent webhook");
            } else
            {
                Console.WriteLine("FAILED: Sent webhook");
            }
        }
    }

    public class MessageTeams
    {
        public string? channel { get; set; }
        public string? username { get; set; }
        public string? text { get; set; }
        public string? icon_emoji { get; set; }
    }
}
