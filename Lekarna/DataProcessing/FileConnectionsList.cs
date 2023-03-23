using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy
{
    public static class FileConnectionsList
    {
        private static List<FilesIOLogic> _connections;

        public static List<FilesIOLogic> Connections { get { return _connections; } }

        static FileConnectionsList()
        {
            _connections = new List<FilesIOLogic>();
        }

        public static void Add(FilesIOLogic filesConnection)
        {
            _connections.Add(filesConnection);
        }

        public static bool IsEmpty()
        {
            return !_connections.Any();
        }
        public static int Count()
        {
            return _connections.Count;
        }
    }
}
