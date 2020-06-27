using System.Collections.Generic;
using DemoLibrary.Models;

namespace DemoLibrary
{
    public interface IDataAccess
    {
        void AddNewPerson(PersonModel personModel);
        List<PersonModel> GetAllPeople();
    }
}