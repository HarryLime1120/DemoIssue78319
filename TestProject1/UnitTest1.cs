using System.IO.Ports;

namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {

            //Arrange
            Console.WriteLine("Available Ports:");
            string[] AvailablePorts = SerialPort.GetPortNames();

            if (AvailablePorts.Length == 0)
            {
                Console.WriteLine("none => exiting");
                return;
            }

            foreach (string s in AvailablePorts)
            {
                Console.WriteLine("   {0}", s);
            }

            StringComparer stringComparer = StringComparer.OrdinalIgnoreCase;

            string Port = AvailablePorts[0];
            Console.WriteLine($"Testing with {Port}");
            // Create a new SerialPort object with default settings.
            SerialPort _serialPort = new SerialPort(Port, 115200, Parity.None, 8, StopBits.One);
            _serialPort.Handshake = Handshake.None;

            // Set the read/write timeouts
            _serialPort.ReadTimeout = 500;
            _serialPort.WriteTimeout = 500;

            _serialPort.Open();

            //Act
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            Action act = () => { _serialPort.ReadLine(); Thread.Sleep(1000); _serialPort.Close(); };
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            // Assert
            Assert.ThrowsException<TimeoutException>(act);
        }
    }
}