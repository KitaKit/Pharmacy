namespace Pharmacy
{
    public class ProviderModel
    {
        private int _id;
        private string _name;
        public ProviderModel() { }
        public int Id
        {
            get { return _id; }
            set
            {
                if (_id != 0)
                    return;
                _id = value;
            }
        }
        public string Name
        {
            get { return _name;}
            set
            {
                if (!(string.IsNullOrEmpty(value)) && value != _name)
                    _name = value;
            }
        }

        public ProviderModel(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
