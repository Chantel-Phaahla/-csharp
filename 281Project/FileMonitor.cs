using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SportsEventScheduling
{
    public class FileMonitor
    {
        private readonly string _watchPath;
        private readonly string _backupPath;
        private FileSystemWatcher _watcher; 

        public FileMonitor(string watchPath, string backupPath)
        {
            if (string.IsNullOrWhiteSpace(watchPath))
                throw new ArgumentException("Watch path must be specified.");
            if (string.IsNullOrWhiteSpace(backupPath))
                throw new ArgumentException("Backup path must be specified.");

            _watchPath = watchPath;
            _backupPath = backupPath;

            if (!Directory.Exists(_watchPath))
                Directory.CreateDirectory(_watchPath);

            if (!Directory.Exists(_backupPath))
                Directory.CreateDirectory(_backupPath);
        }

        public void Start()
        {
            _watcher = new FileSystemWatcher(_watchPath, "*.txt");
            _watcher.Created += OnFileCreated;
            _watcher.EnableRaisingEvents = true;
            Console.WriteLine($"Monitoring started on: {_watchPath}");
        }

        public void Stop()
        {
            if (_watcher != null)
            {
                _watcher.EnableRaisingEvents = false;
                _watcher.Dispose();
            }
        }

        private void OnFileCreated(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine($"New registration file detected: {e.Name}");
            try
            {
                string destFile = Path.Combine(_backupPath, e.Name);
                File.Copy(e.FullPath, destFile, true);
                Console.WriteLine($"File backed up to: {destFile}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing file {e.Name}: {ex.Message}");
            }
        }
    }
}