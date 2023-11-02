namespace SharedChatWebSite.Services;
public  class IdService 
{
    static public string GetId(string code)
    {
        return Math.Abs(code.GetHashCode()).ToString();
    }
}