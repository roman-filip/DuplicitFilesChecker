using System;

namespace RFI.DuplicitFilesChecker.Services.Entities
{
    public class FileInfo
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public string Hash { get; set; }
        public DateTime Date { get; set; }
    }
}
