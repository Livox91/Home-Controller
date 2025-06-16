namespace HomeController.Models
{
    public class Fan : IDevice
    {
        public int FanSpeed { get; set; }

        public override void TurnOn() { }

        public override void TurnOff() { }

        public void SetFanSpeed(int speed)
        {
            if (speed >= 0)
            {
                FanSpeed = speed;
            }
        }
    }
}
