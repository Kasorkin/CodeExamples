namespace StrategicManagement
{
    public interface IStrategicCommand
    {
        public void Start();

        public void Stop();

        public bool IsEnd();

        //public bool IsStarted();
    }
}