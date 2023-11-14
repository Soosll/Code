namespace Infrastructure.Yandex
{
    public class DeviceChecker : IDeviceChecker
    {
        public bool IsPC { get; set; }

        public bool Check()
        {
            return false;
        }
    }
}