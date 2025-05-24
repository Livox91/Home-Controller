namespace HomeController.Models
{
    public class Television : IDevice
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int ScreenSize { get; set; }
        public bool IsSmartTv { get; set; }
        public string IpAddress { get; set; }
        public bool IsConnectedToNetwork { get; set; }
        public string FirmwareVersion { get; set; }
        public bool IsOn { get; set; }
        public int CurrentChannel { get; set; }
        public string Ipaddr { get; set; }
        public Television()
        {
        }

        public Television(int id, string brand, string model, int screenSize, bool isSmartTv, string ipAddress, bool isConnectedToNetwork, string firmwareVersion, bool isOn, int currentChannel)
        {
            Id = id;
            Brand = brand;
            Model = model;
            ScreenSize = screenSize;
            IsSmartTv = isSmartTv;
            IpAddress = ipAddress;
            IsConnectedToNetwork = isConnectedToNetwork;
            FirmwareVersion = firmwareVersion;
            IsOn = isOn;
            CurrentChannel = currentChannel;
        }

        public void ConnectToNetwork(string ipAddress)
        {
            IpAddress = ipAddress;
            IsConnectedToNetwork = true;
        }

        public void DisconnectFromNetwork()
        {
            IpAddress = null;
            IsConnectedToNetwork = false;
        }

        public void UpdateFirmware(string newVersion)
        {
            FirmwareVersion = newVersion;
        }

        override public void TurnOn()
        {
            IsOn = true;
        }

        override public void TurnOff()
        {
            IsOn = false;
        }

        public void ChangeChannel(int channel)
        {
            if (IsOn)
            {
                CurrentChannel = channel;
            }
        }

        public override string ToString()
        {
            return $"{Brand} {Model} - {ScreenSize}\" {(IsSmartTv ? "Smart TV" : "Non-Smart TV")} - {(IsConnectedToNetwork ? $"Connected (IP: {IpAddress})" : "Not Connected")} - {(IsOn ? $"On (Channel: {CurrentChannel})" : "Off")}";
        }
    }
}