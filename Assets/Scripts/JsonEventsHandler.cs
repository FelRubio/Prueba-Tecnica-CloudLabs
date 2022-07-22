using System.IO;
using UnityEngine;

public class JsonEventsHandler : MonoBehaviour
{
    void Start()
    {
        var watcher = new FileSystemWatcher(Application.streamingAssetsPath);

        watcher.NotifyFilter = NotifyFilters.LastAccess;

        watcher.Changed += OnJsonChanged;
        watcher.Created += OnJsonCreated;
        watcher.Deleted += OnJsonDeleted;
        watcher.Error += OnError;

        watcher.Filter = "data.json";
        watcher.IncludeSubdirectories = true;
        watcher.EnableRaisingEvents = true;
    }

    private void OnJsonChanged(object sender, FileSystemEventArgs e)
    {
        if (e.ChangeType != WatcherChangeTypes.Changed)
            return;

        Debug.Log($"Json has been changed!\nPath: {e.FullPath}");
    }

    private void OnJsonCreated(object sender, FileSystemEventArgs e)
    {
        print(sender.ToString());
        print(e.Name);
        Debug.Log($"A new json has been created!\nPath: {e.FullPath}");
    }

    private void OnJsonDeleted(object sender, FileSystemEventArgs e)
    {
        Debug.LogWarning($"Json has been deleted!");
    }

    private void OnError(object sender, ErrorEventArgs e)
    {
        var ex = e.GetException();
        Debug.LogError($"Message: {e.GetException().Message}\nStacktrace:{ex.StackTrace}\n{ex.InnerException}");
    }
}
