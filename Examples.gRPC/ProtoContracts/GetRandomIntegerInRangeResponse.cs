using ProtoBuf;

namespace Examples.gRPC.ProtoContracts
{
    [ProtoContract]
    public class GetRandomIntegerInRangeResponse
    {
        [ProtoMember(1)]
        public int? RandomValue { get; set; }
    }
}
