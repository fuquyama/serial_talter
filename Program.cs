// See https://aka.ms/new-console-template for more information
using System.IO.Ports;
using serial_talker;

Console.WriteLine("serial talker");
Console.WriteLine("");

Config config = Config.Deserialize(File.ReadAllText("config.yaml"));
Console.WriteLine($"Port settings:");
Console.WriteLine($"  Name:     {config.Port.Name}");
Console.WriteLine($"  BaudRate: {config.Port.BaudRate}");
Console.WriteLine($"  DataBits: {config.Port.DataBits}");
Console.WriteLine($"  StopBits: {config.Port.StopBits}");
Console.WriteLine($"  Parity:   {config.Port.Parity}");
Console.WriteLine("");

SerialPort port = new(config.Port.Name, config.Port.BaudRate, config.Port.Parity, config.Port.DataBits, config.Port.StopBits)
{
    NewLine = config.Port.NewLine,
};

port.DataReceived += (sender, e) =>
{
    string str = port.ReadLine();
    Console.WriteLine(str);
    foreach (var cr in config.CallAndResponses)
    {
        if (str.Contains(cr.Call))
        {
            port.WriteLine(cr.Response);
            Console.WriteLine($"  -> {cr.Response}");
        }
    }
};

port.Open();

Console.WriteLine("Infinite loop start. `Ctrl + C` to exit.");
while (true)
{
    System.Threading.Thread.Sleep(1000);
}