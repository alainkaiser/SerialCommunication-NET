using System.IO.Ports;

namespace SerialCommunication;

public class SerialCommunicationService
{
    private readonly SerialPort _serialPort;

    public SerialCommunicationService(string portName, int baudRate)
    {
        _serialPort = new SerialPort(portName, baudRate)
        {
            Parity = Parity.None,
            StopBits = StopBits.One,
            DataBits = 8,
            Handshake = Handshake.None,
            DtrEnable = false, // Disable DTR
            RtsEnable = false  // Disable RTS
        };
        _serialPort.DataReceived += DataReceivedHandler;
    }

    public void Start()
    {
        try
        {
            if (!_serialPort.IsOpen)
            {
                _serialPort.Open();
                Console.WriteLine("Serial port opened.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to open serial port: {ex.Message}");
        }
    }

    public void Stop()
    {
        try
        {
            if (_serialPort.IsOpen)
            {
                _serialPort.Close();
                Console.WriteLine("Serial port closed.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to close serial port: {ex.Message}");
        }
    }

    private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
    {
        try
        {
            var sp = (SerialPort)sender;
            var indata = sp.ReadExisting();
            Console.WriteLine($"Data Received by SerialPort {sp.PortName}: {indata}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in DataReceivedHandler: {ex.Message}");
        }
    }

    public void SendData(string data)
    {
        try
        {
            if (_serialPort.IsOpen)
            {
                _serialPort.Write(data);
                Console.WriteLine($"Data Sent by SerialPort {_serialPort.PortName}: {data}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to send data: {ex.Message}");
        }
    }
}