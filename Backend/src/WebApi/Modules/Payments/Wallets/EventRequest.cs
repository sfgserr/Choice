namespace WebApi.Modules.Payments.Wallets
{
    public class EventRequest
    {
        public EventRequest(Object o)
        {
            Object = o;
        }

        public Object Object { get; }
    }

    public class Object
    {
        public Object(string id)
        {
            Id = id;
        }

        public string Id { get; }
    }
}