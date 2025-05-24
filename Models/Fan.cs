namespace HomeController.Models
{
    public class Fan : IDevice
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Brand { get; set; }
        public int Speed { get; set; }
        public bool IsOn { get; set; }
        public string Ipaddr { get; set; }
        override public void TurnOn()
        {
            IsOn = true;
        }

        override public void TurnOff()
        {
            IsOn = false;
        }

        public void SetSpeed(int speed)
        {
            if (speed >= 0)
            {
                Speed = speed;
            }
        }
    }
}