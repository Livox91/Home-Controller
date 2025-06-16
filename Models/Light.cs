namespace HomeController.Models
{
    public class Light : IDevice
    {
        public int Brightness { get; set; } // Brightness level (e.g., 0-100)

        public override void TurnOn() { }

        public override void TurnOff() { }

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
