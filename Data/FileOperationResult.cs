using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirShare
{
    public class FileOperationResult
    {
        public FileOperationCode Code { get; set; }
        public string DisplayMsg { get; set; }
        public bool Success { get; set; }
    }

    public enum FileOperationCode
    {
        Error,
        Success,
        AllReadyExists,
        UnauthorizedAccessException,
        ArgumentException,
        PathTooLongException,
        DirectoryNotFoundException,
        NotSupportedException
    }
}
