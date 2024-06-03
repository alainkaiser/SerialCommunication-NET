using SerialCommunication;

var serialService1 = new SerialCommunicationService("/dev/ttys002", 9600);
var serialService2 = new SerialCommunicationService("/dev/ttys003", 9600);

serialService1.Start();
serialService2.Start();

serialService1.SendData("Hello from ttys002");

await Task.Delay(1000);

serialService1.Stop();
serialService2.Stop();