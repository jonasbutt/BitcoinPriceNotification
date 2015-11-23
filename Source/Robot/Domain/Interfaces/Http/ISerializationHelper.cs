namespace Jonas.BitcoinPriceNotification.Robot.Domain.Interfaces.Http
{
    public interface ISerializationHelper
    {
        T DeserializeJson<T>(string json);

        string SerializeToJson(object data);

        string SerializeToQueryString(object data);
    }
}