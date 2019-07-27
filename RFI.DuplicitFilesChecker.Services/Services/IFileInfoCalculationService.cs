using System.Collections.Generic;
using RFI.DuplicitFilesChecker.Services.Entities;

namespace RFI.DuplicitFilesChecker.Services.Services
{
    public interface IFileInfoCalculationService
    {
        FileInfo CalculateFileInfo(string filePath);
        IEnumerable<FileInfo> CalculateFileInfos(string directory);
    }
}