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
        }

        public List<string> GetRooms()
        {
            Initialize();
            return rooms;
        }

        public void AddRoom(string roomName, string image)
        {
            if (!rooms.Contains(roomName))
            {
                dbHelper.InsertRoom(roomName, image);
                rooms.Add(roomName);
            }
        }

        public void DeleteRoom(string roomName)
        {
            if (rooms.Contains(roomName))
            {
                dbHelper.DeleteRoom(roomName);
                rooms.Remove(roomName);
            }
        }

        public void UpdateRoom(string oldRoomName, string newRoomName)
        {
            if (rooms.Contains(oldRoomName) && !rooms.Contains(newRoomName))
            {
                dbHelper.UpdateRoom(oldRoomName, newRoomName);
                int index = rooms.IndexOf(oldRoomName);
                if (index != -1)
                {
                    rooms[index] = newRoomName;
                }
            }
        }

        public string GetRoomImage(string roomName)
        {
            if (rooms.Contains(roomName))
            {
                return dbHelper.getRoomImage(roomName);
            }
            return null;
        }
    }
}
