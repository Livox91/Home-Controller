namespace HomeController.Models
{
    public class Light : IDevice
    {


        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsOn { get; set; }
        public int Brightness { get; set; } // Brightness level (e.g., 0-100)
        public string Ipaddr { get; set; }

        public Light()
        {
            Id = 0;
            Name = "Light";
            IsOn = false;
            Brightness = 0;
        }
        override public void TurnOn()
        {
            IsOn = true;
        }

        override public void TurnOff()
        {
            IsOn = false;
        }

        public void SetBrightness(int level)
        {
            if (level < 0)
                Brightness = 0;
            else if (level > 100)
                Brightness = 100;
            else
                Brightness = level;
        }
    }
}

