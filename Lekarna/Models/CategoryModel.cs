namespace Pharmacy
{
    public class CategoryModel
    {
        private int _id;
        private string _name;

        public CategoryModel() { }

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
            get { return _name; } 
            set 
            {   if (!(string.IsNullOrEmpty(value)) && _name != value)
                    _name = value; 
            }
        }

        public CategoryModel(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
