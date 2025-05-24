namespace HomeController.Services
{

    class RoomService
    {
        List<string> rooms = new List<string>();

        private DbHelper dbHelper = new DbHelper();

        public RoomService()
        {
            Initialize();
        }

        private void Initialize()
        {
            rooms = dbHelper.GetRooms();
            if (rooms.Count == 0)
            {
                rooms.Add("Room-1");
            }
        }

        public List<string> GetRooms()
        {
            return rooms;
        }
    }
}