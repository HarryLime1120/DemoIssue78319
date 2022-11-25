using System.IO.Ports;
using System.Reflection;
using System.Runtime.InteropServices;

namespace TestProjectNet7
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {

            //Arrange
            // print some version infos
            Console.Write($"{RuntimeInformation.FrameworkDescription.ToString()}, ");
            //Console.WriteLine(RuntimeInformation.OSArchitecture.ToString());
            Console.Write($"{RuntimeInformation.OSDescription.ToString()}, ");
            // Console.WriteLine(RuntimeInformation.ProcessArchitecture.ToString());
            Console.WriteLine(RuntimeInformation.RuntimeIdentifier.ToString());

            AssemblyName[] names = Assembly.GetExecutingAssembly().GetReferencedAssemblies();
            foreach (AssemblyName name in names) { Console.WriteLine(name.ToString()); }

            // list available serial ports and use the first one
            Console.Write("\nAvailable Ports: ");
            string[] AvailablePorts = SerialPort.GetPortNames();

            if (AvailablePorts.Length == 0)
            {
                Console.WriteLine("none => exiting");
                return;
            }

            foreach (string s in AvailablePorts) { Console.Write("{0} ", s); }

            string Port = AvailablePorts[0];
            Console.WriteLine($"\nTesting with {Port}");
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