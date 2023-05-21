using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsLib.Models
{
    public sealed class PersonModel
    {
        public string FirstName;
        public string LastName;
        public string EmployeeId;
        private ISave _modelSave;
        private ILoad _modelLoad;


        public PersonModel(ISave save, ILoad modelLoad)
        {
            _modelSave = save;
            _modelLoad = modelLoad;
        }
        public PersonModel()//without param for XMl serialization
        {
        }
        public PersonModel Load()
        {
            return _modelLoad.Load<PersonModel>();
        }

        public void  Save()
        {
           _modelSave.Save(this);
        }
    }
}
