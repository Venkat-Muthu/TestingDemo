using System.Collections.Generic;
using System.IO;
using DemoLibrary.Models;

namespace DemoLibrary
{
    public static class DataAccess
    {
        private static string personTextFile = "PersonText.txt";

        public static void AddNewPerson(PersonModel personModel)
        {
            var lines = new List<string>();
            var people = GetAllPeople();
            people.Add(personModel);

            foreach (var person in people)
            {
                lines.Add($"{person.FirstName},{person.LastName}");
            }
            File.WriteAllLines(personTextFile, lines);
        }

        public static List<PersonModel> GetAllPeople()
        {
            var output = new List<PersonModel>();
            var content = File.ReadAllLines(personTextFile);

            foreach (var line in content)
            {
                string[] data = line.Split(',');
                output.Add(new PersonModel {FirstName = data[0], LastName = data[1]});
            }

            return output;
        }
    }
}