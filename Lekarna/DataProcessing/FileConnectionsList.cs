using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy
{
    public class FileConnectionsList
    {
        private List<FilesIOLogic> _connections;

        public List<FilesIOLogic> Connections { get { return _connections; } }

        public FileConnectionsList()
        {
            _connections = new List<FilesIOLogic>();
        }

        public void Add(FilesIOLogic filesConnection)
        {
            _connections.Add(filesConnection);
        }

        public bool IsEmpty()
        {
            return !_connections.Any();
        }
    }
}
