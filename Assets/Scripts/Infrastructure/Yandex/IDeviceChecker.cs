namespace Infrastructure.Yandex
{
    public interface IDeviceChecker
    {
        public bool IsPC { get; set; }
        bool Check();
    }
}