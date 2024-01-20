using Newtonsoft.Json;

public static class ObjectUtils
{
    public static TResult ParseJson<TResult>(string data)
    {
        return JsonConvert.DeserializeObject<TResult>(data);
    }
}
