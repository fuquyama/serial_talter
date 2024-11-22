using System.IO.Ports;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace serial_talker
{
    public class Config
    {

        public required SerialConfig Port { get; set; }

        public required List<CallAndResponse> CallAndResponses { get; set; }

        public static Config Deserialize(string yaml)
        {
            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .WithNamingConvention(UnderscoredNamingConvention.Instance)
                .Build();
            return deserializer.Deserialize<Config>(yaml);
        }
    }

    public class SerialConfig
    {
        public required string Name { get; set; }
        public int BaudRate { get; set; }
        public int DataBits { get; set; }
        public Parity Parity { get; set; }
        public StopBits StopBits { get; set; }
        public required string NewLine { get; set; }
    }

    public class CallAndResponse
    {
        public required string Call { get; set; }
        public required string Response { get; set; }
    }
}