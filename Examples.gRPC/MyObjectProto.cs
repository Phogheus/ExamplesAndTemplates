using ProtoBuf;

namespace Examples.gRPC
{
    [ProtoContract]
    public class MyObjectProto
    {
        [ProtoMember(1)]
        public int SomeValue { get; set; }

        [ProtoMember(2)]
        public string? SomeString { get; set; }

        [ProtoMember(3, IsPacked = true, OverwriteList = true)]
        public byte[] SomeSerializedObject { get; set; } = Array.Empty<byte>();
    }
}
