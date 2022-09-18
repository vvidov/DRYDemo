using System;
using System.Collections.Generic;
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
        private IModelStorage _modelStorage;

        public PersonModel(IModelStorage modelStorage)
        {
            _modelStorage = modelStorage;
        }

        public PersonModel Load()
        {
            return _modelStorage.Load<PersonModel>();
        }

        public void  Save()
        {
           _modelStorage.Save(this);
        }
    }
}
