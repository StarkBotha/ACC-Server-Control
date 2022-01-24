namespace api.Data
{
    public interface IFileManager
    {
        string GetEvent();
        string GetEventRules();
        string GetSettings();
        void SaveEvent(string text);
        void SaveEventRules(string text);
        void SaveSettings(string text);

        string GetVersion();
    }
}