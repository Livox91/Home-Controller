using System.Text.Json;
using HomeController.Models;
using Microsoft.AspNetCore.Components;


namespace HomeController.Services
{
    public class DeviceService
    {

        private readonly IPAssigner IPAddressService;
        private readonly HttpClient httpClient;
        List<IDevice> deviceList = new List<IDevice>();

        public DeviceService(IPAssigner IpaddService, HttpClient httpClient)
        {
            this.httpClient = httpClient ?? new HttpClient();
            this.httpClient.BaseAddress = new Uri("http://192.168.1.16:3000/device/add/");
            this.IPAddressService = IpaddService;

        }
        public async Task<bool> CreateDevice(string deviceType, string deviceName, string roomName)
        {

            try
            {
                IPAddressService.ShowAssignedIPs();
                IDevice device = DeviceFactory.CreateDevice(deviceType);

                device.deviceType = deviceType;
                device.deviceName = deviceName;
                device.roomName = roomName;
                device.deviceStatus = "Online";
                device.deviceIp = IPAddressService.RequestNewIP();

                Console.WriteLine($"Device IP: {device.deviceIp}");

                string serverIp = $"http://192.168.1.16:3000/device/add/";
                var body = new
                {
                    deviceName = deviceName,
                    image = deviceType,
                    ip = device.deviceIp,
                };
                if (httpClient == null)
                {
                    Console.WriteLine("httpClient is NULL!");
                }
                var json = JsonSerializer.Serialize(body);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync(serverIp, content);
                if (response.IsSuccessStatusCode)
                {

                    var responseBody = await response.Content.ReadAsStringAsync();


                    var doc = JsonDocument.Parse(responseBody);

                    var deviceId = doc.RootElement.GetProperty("deviceId").GetString();
                    var deviceIp = doc.RootElement.GetProperty("deviceip").GetString();
                    var deviceStatus = "Online";
                    var DbHelper = new DbHelper();

                    DbHelper.InsertDevice(deviceId, deviceName, deviceType, deviceIp, roomName, deviceStatus);
                    Console.WriteLine($"Device created with ID: {deviceId}");


                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating device: {ex.Message}");
                return false;
            }
        }

        public List<IDevice> GetDevices()
        {
            deviceList.Clear();
            var dbHelper = new DbHelper();
            var devices = dbHelper.GetDevices();

            foreach (var device in devices)
            {

                IDevice dev = DeviceFactory.CreateDevice(device["DeviceType"].ToString());
                dev.deviceIp = device["DeviceIp"].ToString() ?? "";
                dev.deviceName = device["DeviceName"].ToString() ?? "";
                dev.deviceStatus = device["DeviceStatus"].ToString() ?? "";
                dev.roomName = device["RoomName"].ToString() ?? "";
                dev.containderId = device["DeviceID"].ToString() ?? "";

                deviceList.Add(dev);
            }

            return deviceList;
        }

        public async Task startDevices()
        {
            DbHelper db = new DbHelper();

            List<string> ContainerIds = new List<string>();



        }

    }
}
