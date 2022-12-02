using System.ServiceModel;
using Examples.gRPC.ProtoContracts;

namespace Examples.gRPC
{
    [ServiceContract]
    public interface IAmAProtoServiceContract
    {
        [OperationContract]
        ValueTask<GetRandomIntegerInRangeResponse> GetRandomIntegerInRange(GetRandomIntegerInRangeRequest request);

        [OperationContract]
        ValueTask<MyObjectProto> GetAnObject();
    }
}
