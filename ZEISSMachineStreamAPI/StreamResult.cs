namespace ZEISSMachineStreamAPI
{
    public class Payload
    {
        public string machine_id { get; set; }
        public string id { get; set; }
        public DateTime timestamp { get; set; }
        public string status { get; set; }
    }

    public class StreamResult
    {
        public string topic { get; set; }
        public object @ref { get; set; }
        public Payload payload { get; set; }
        public string @event { get; set; }
    }


}
