namespace api
{
    public interface IProcessHost
    {
        string GetProcesses();
        bool IsServerRunning();
        void StartServer();
        void StopServer();
        string GetServerStatus();
    }
}