using System;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Examples.gRPC.ProtoContracts;
using ProtoBuf.Grpc.Client;

namespace Examples.gRPC.Client
{
    internal class Program
    {
        private static readonly GetRandomIntegerInRangeRequest VALID_RANGE_MESSAGE = new GetRandomIntegerInRangeRequest(1, 100);
        private static readonly GetRandomIntegerInRangeRequest INVALID_RANGE_MESSAGE = new GetRandomIntegerInRangeRequest(100, 1);

        private static async Task Main(string[] args)
        {
            using (var channel = Grpc.Net.Client.GrpcChannel.ForAddress("https://localhost:7222/"))
            {
                var serviceProxy = channel.CreateGrpcService<IAmAProtoServiceContract>();

                var validRangeResponse = await serviceProxy.GetRandomIntegerInRange(VALID_RANGE_MESSAGE);
                Console.WriteLine($"Response to valid Random Integer range: {validRangeResponse.RandomValue!.Value}");

                var invalidRangeResponse = await serviceProxy.GetRandomIntegerInRange(INVALID_RANGE_MESSAGE);
                Console.WriteLine($"Response to invalid Random Integer range is null: {invalidRangeResponse.RandomValue == null}");

                var someObject = await serviceProxy.GetAnObject();
                Console.WriteLine($"Response to getting an object: {JsonSerializer.Serialize(someObject)}");
                Console.WriteLine($"Serialized Object from response to getting an object: {JsonSerializer.Serialize(JsonSerializer.Deserialize<JsonObject>(someObject.SomeSerializedObject))}");
            }

            Console.WriteLine("Done. Press any key to exit...");
            _ = Console.ReadKey(true);
        }
    }
}
