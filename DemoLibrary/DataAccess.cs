using System.Collections.Generic;
using DemoLibrary.Models;
using Utils.FileSystemWrapper;

namespace DemoLibrary
{
    public class DataAccess : IDataAccess
    {
        private static string personTextFile = "PersonText.txt";
        private readonly IFileWrapper _fileWrapper;
        private readonly IPathWrapper _pathWrapper;
        public DataAccess(IFileWrapper fileWrapper, IPathWrapper pathWrapper)
        {
            _fileWrapper = fileWrapper;
            _pathWrapper = pathWrapper;
        }

        public void AddNewPerson(PersonModel personModel)
        {
            var lines = new List<string>();
            var people = GetAllPeople();
            people.Add(personModel);

            foreach (var person in people)
            {
                lines.Add($"{person.FirstName},{person.LastName}");
            }
            _fileWrapper.WriteAllLines(personTextFile, lines);
        }

        public List<PersonModel> GetAllPeople()
        {
            var output = new List<PersonModel>();

            string[] content = new string[0];
            var fullPath = _pathWrapper.GetFullPath(personTextFile);
            if (_fileWrapper.Exists(fullPath))
            {
                content = _fileWrapper.ReadAllLines(fullPath);
            }

            foreach (var line in content)
            {
                string[] data = line.Split(',');
                output.Add(new PersonModel {FirstName = data[0], LastName = data[1]});
            }

            return output;
        }
    }
}