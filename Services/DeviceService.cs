using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using HomeController.Models;
using Microsoft.AspNetCore.Components;

namespace HomeController.Services
{
    public class DeviceService
    {
        private readonly IConfiguration _config;
        private readonly IPAssigner IPAddressService;
        private readonly HttpClient httpClient;

        public DeviceService(IPAssigner IpaddService, HttpClient httpClient, IConfiguration _config)
        {
            this.httpClient = httpClient ?? new HttpClient();
            this._config = _config;
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
                device.deviceStatus = "Running";
                device.deviceIp = IPAddressService.RequestNewIP();

                Console.WriteLine($"Device IP: {device.deviceIp}");

                string serverIp = $"http://{_config["BackendIP"]}:3000/device/add/";
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
                var content = new StringContent(
                    json,
                    System.Text.Encoding.UTF8,
                    "application/json"
                );

                var response = await httpClient.PostAsync(serverIp, content);
                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();

                    var doc = JsonDocument.Parse(responseBody);

                    var deviceId = doc.RootElement.GetProperty("deviceId").GetString();
                    var deviceIp = doc.RootElement.GetProperty("deviceip").GetString();
                    var deviceStatus = "Running";
                    var DbHelper = new DbHelper();

                    DbHelper.InsertDevice(
                        deviceId,
                        deviceName,
                        deviceType,
                        deviceIp,
                        roomName,
                        deviceStatus
                    );
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
            var deviceList = new List<IDevice>();
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

        public async Task<bool> DeleteDevice(string containderID)
        {
            try
            {
                string serverIp = $"http://{_config["BackendIP"]}:3000/device/delete/";
                var body = new { containerIds = containderID };
                var json = JsonSerializer.Serialize(body);
                var content = new StringContent(
                    json,
                    System.Text.Encoding.UTF8,
                    "application/json"
                );
                var response = await httpClient.PostAsync(serverIp, content);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Device with ID {containderID} deleted successfully.");
                }

                var dbHelper = new DbHelper();
                dbHelper.DeleteDevice(containderID);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting device: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> StopDevice(string containderID)
        {
            try
            {
                string serverIp = $"http://{_config["BackendIP"]}:3000/device/stop/";
                var body = new { containerIds = containderID };
                var json = JsonSerializer.Serialize(body);
                var content = new StringContent(
                    json,
                    System.Text.Encoding.UTF8,
                    "application/json"
                );
                var response = await httpClient.PostAsync(serverIp, content);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Device with ID {containderID} Stopped successfully.");
                }

                var dbHelper = new DbHelper();

                dbHelper.StopDevice(containderID);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Stopping device: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> StartDevice(string containderID)
        {
            try
            {
                string serverIp = $"http://{_config["BackendIP"]}:3000/device/start/";
                var body = new { containerIds = containderID };
                var json = JsonSerializer.Serialize(body);
                var content = new StringContent(
                    json,
                    System.Text.Encoding.UTF8,
                    "application/json"
                );
                var response = await httpClient.PostAsync(serverIp, content);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Device with ID {containderID} Started successfully.");
                }

                var dbHelper = new DbHelper();
                dbHelper.StartDevice(containderID);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Starting device: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateDevice(string deviceIP, string json)
        {
            try
            {
                using TcpClient client = new TcpClient();
                await client.ConnectAsync(deviceIP, 3000);

                using NetworkStream stream = client.GetStream();
                if (!json.EndsWith("\n"))
                    json += "\n";

                byte[] requestBytes = Encoding.UTF8.GetBytes(json);
                await stream.WriteAsync(requestBytes, 0, requestBytes.Length);

                byte[] buffer = new byte[1024];
                int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                Console.WriteLine($"Response from {deviceIP}: {response}");

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating device {deviceIP}: {ex.Message}");
                return false;
            }
        }

        public async Task<string> GetDeviceStatus(string deviceIP)
        {
            try
            {
                using TcpClient client = new TcpClient();
                await client.ConnectAsync(deviceIP, 3000);

                using NetworkStream stream = client.GetStream();
                string request = "{\"action\":\"get\"}\n";
                byte[] requestBytes = Encoding.UTF8.GetBytes(request);
                await stream.WriteAsync(requestBytes, 0, requestBytes.Length);

                // Read response
                byte[] buffer = new byte[1024];
                int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting device status: {ex.Message}");
                return "Error";
            }
        }

        public async Task DeleteDevicesByRoom(string roomName)
        {
            try
            {
                var devices = GetDevices();
                foreach (var device in devices)
                {
                    if (device.roomName == roomName)
                    {
                        await DeleteDevice(device.containderId);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting devices in room {roomName}: {ex.Message}");
            }
        }
    }
}
