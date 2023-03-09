using System;
using System.Threading;
using Confluent.Kafka;
using Newtonsoft.Json;

namespace KafkaConsumerFirst
{ 
    class Program
    {
        [Obsolete("Obsolete")]
        static void Main(string[] args)
        {
            var topicName = "topic_0";
            var kafkaBootStrapServers = "pkc-lzvrd.us-west4.gcp.confluent.cloud:9092";
            var username = "B2GUYKSUAYO56XE2";
            var password = "dqBOlwAkb+/nkzLOGDkk/hTQsERKWCNbFrtP7P/hgBYFmvhzg7WjCnmFZvga5wPm";

            var config = new ConsumerConfig
            {
                BootstrapServers = kafkaBootStrapServers,
                SecurityProtocol = SecurityProtocol.SaslSsl,
                SaslMechanism = SaslMechanism.Plain,
                SaslUsername = username,
                SaslPassword = password,
                GroupId = "consumer1", 
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            using var consumer = new ConsumerBuilder<string, string>(config).Build();
            consumer.Subscribe(topicName);

            while (true)
            {
                var consumeResult = consumer.Consume(CancellationToken.None);
                var product = JsonConvert.DeserializeObject<CreateDeveloperDto>(consumeResult.Value);
                Console.WriteLine($"{product.Name + product.Surname + product.Department}");

            }
        }
    }
}