namespace HomeController.Models
{
    public abstract class IDevice
    {

        public String containderId { get; set; }

        public String deviceType { get; set; }

        public String deviceIp { get; set; }

        public String deviceName { get; set; }

        public String deviceStatus { get; set; }

        public String roomName { get; set; }
        public abstract void TurnOn();
        public abstract void TurnOff();
    }
}