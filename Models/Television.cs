namespace HomeController.Models
{
    public class Television : IDevice
    {
        public int CurrentChannel { get; set; }

        public override void TurnOn() { }

        public override void TurnOff() { }

        public void SetChannel(int channel)
        {
            CurrentChannel = channel;
        }
    }
}
