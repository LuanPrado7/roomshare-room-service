using Confluent.Kafka;
using System.Diagnostics;
using System.Net;

namespace roomshare_room_service_command.Service
{
    public class KafkaProducerService
    {
        public static async Task SendChangeRequest(string host, string port, string topic, string message)
        {
            ProducerConfig config = new ProducerConfig
            {
                BootstrapServers = host + ":" + port,
                ClientId = Dns.GetHostName()
            };

            try
            {
                using (var producer = new ProducerBuilder
                <Null, string>(config).Build())
                {
                    var result = await producer.ProduceAsync
                    (topic, new Message<Null, string>
                    {
                        Value = message
                    });

                    Console.WriteLine($"Delivery Timestamp:{ result.Timestamp.UtcDateTime}");                
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occured: {ex.Message}");
            }

        }
    }
}
