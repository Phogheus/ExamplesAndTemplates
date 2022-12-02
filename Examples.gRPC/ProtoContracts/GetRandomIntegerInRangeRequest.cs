using ProtoBuf;

namespace Examples.gRPC.ProtoContracts
{
    [ProtoContract]
    public class GetRandomIntegerInRangeRequest
    {
        [ProtoMember(1)]
        public int InclusiveMinimum { get; set; }

        [ProtoMember(2)]
        public int InclusiveMaximum { get; set; }

        public GetRandomIntegerInRangeRequest()
        {
        }

        public GetRandomIntegerInRangeRequest(int inclusiveMinimum, int inclusiveMaximum)
        {
            InclusiveMinimum = inclusiveMinimum;
            InclusiveMaximum = inclusiveMaximum;
        }
    }
}
