using MongoDB.Bson;
using MongoDB.Driver;

namespace HomeController.Services
{
    public class DbHelper
    {
        static IMongoDatabase database = new MongoClient("mongodb://localhost:27017").GetDatabase(
            "HomeController"
        );

        static IMongoCollection<BsonDocument> collection = database.GetCollection<BsonDocument>(
            "devices"
        );
        static IMongoCollection<BsonDocument> Roomcollection = database.GetCollection<BsonDocument>(
            "rooms"
        );

        public void InsertDevice(
            string DeviceID,
            string DeviceName,
            string DeviceType,
            string DeviceIp,
            string RoomName,
            string DeviceStatus
        )
        {
            // Create a new BsonDocument
            var device = new BsonDocument
            {
                { "DeviceID", DeviceID },
                { "DeviceName", DeviceName },
                { "DeviceType", DeviceType },
                { "DeviceIp", DeviceIp },
                { "RoomName", RoomName },
                { "DeviceStatus", DeviceStatus },
            };

            collection.InsertOne(device);
        }

        public void InsertRoom(string roomName, string image)
        {
            var room = new BsonDocument { { "RoomName", roomName }, { "Image", image } };
            Roomcollection.InsertOne(room);
        }

        public void DeleteRoom(string roomName)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("RoomName", roomName);
            Roomcollection.DeleteOne(filter);
        }

        public void UpdateRoom(string oldRoomName, string newRoomName)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("RoomName", oldRoomName);
            var update = Builders<BsonDocument>.Update.Set("RoomName", newRoomName);
            Roomcollection.UpdateOne(filter, update);
            // Update all devices in the old room to the new room
            var deviceFilter = Builders<BsonDocument>.Filter.Eq("RoomName", oldRoomName);
            var deviceUpdate = Builders<BsonDocument>.Update.Set("RoomName", newRoomName);
            collection.UpdateMany(deviceFilter, deviceUpdate);
        }

        public string getRoomImage(string roomName)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("RoomName", roomName);
            var room = Roomcollection.Find(filter).FirstOrDefault();
            if (room != null && room.Contains("Image"))
            {
                return room["Image"].AsString;
            }
            return string.Empty; // or return a default image path
        }

        public List<BsonDocument> GetDevices()
        {
            var devices = collection.Find(new BsonDocument()).ToList();
            return devices;
        }

        public List<string> GetRooms()
        {
            var rooms = Roomcollection.Distinct<string>("RoomName", new BsonDocument()).ToList();
            return rooms;
        }

        public List<string> getIps()
        {
            var ips = collection.Distinct<string>("DeviceIp", new BsonDocument()).ToList();
            return ips;
        }

        public void DeleteDevice(string containderId)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("DeviceID", containderId);
            collection.DeleteOne(filter);
        }

        public void StopDevice(string containderId)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("DeviceID", containderId);
            var update = Builders<BsonDocument>.Update.Set("DeviceStatus", "Stopped");
            collection.UpdateOne(filter, update);
        }

        public void StartDevice(string containderId)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("DeviceID", containderId);
            var update = Builders<BsonDocument>.Update.Set("DeviceStatus", "Running");
            collection.UpdateOne(filter, update);
        }
    }
}
