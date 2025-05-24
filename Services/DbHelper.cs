using MongoDB.Bson;
using MongoDB.Driver;

namespace HomeController.Services
{
    public class DbHelper
    {
        static IMongoDatabase database = new MongoClient("mongodb://localhost:27017").GetDatabase("HomeController");

        static IMongoCollection<BsonDocument> collection = database.GetCollection<BsonDocument>("devices");

        public void InsertDevice(string DeviceID, string DeviceName, string DeviceType, string DeviceIp, string RoomName, string DeviceStatus)
        {
            // Create a new BsonDocument
            var device = new BsonDocument
            {
                { "DeviceID", DeviceID },
                { "DeviceName", DeviceName },
                { "DeviceType", DeviceType },
                { "DeviceIp", DeviceIp },
                { "RoomName", RoomName },
                { "DeviceStatus", DeviceStatus }
            };

            // Insert the document into the collection
            collection.InsertOne(device);
        }

        public List<BsonDocument> GetDevices()
        {

            var devices = collection.Find(new BsonDocument()).ToList();
            return devices;
        }

        public List<string> GetRooms()
        {
            var rooms = collection.Distinct<string>("RoomName", new BsonDocument()).ToList();
            return rooms;
        }

        public List<string> getIps()
        {
            var ips = collection.Distinct<string>("DeviceIp", new BsonDocument()).ToList();
            return ips;
        }



    }
}
