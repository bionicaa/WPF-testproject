using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject.DomainEntities;

namespace TestProject.DataBaseAccess
{
    public class ListPerson : IPeopleDataBase
    {

        public delegate void ReadData(object sender, PeopleAvailableEventArgs args);
        public event ReadData PeopleAvailable;

        public delegate void WriteData(object sender, PeopleRemovedEventArgs args);
        public event WriteData PeopleGone;

        public List<Person> people;


        public ICollection<Person> GetPeople()
        {
            people = new List<Person>();

            List<string> fileLines = File.ReadAllLines("nameThread.txt").ToList();

            foreach (var line in fileLines)
            {
                string[] entry = line.Split(',');

                Person newPerson = new Person
                {
                    FirstName = entry[0],
                    LastName = entry[1],
                    Age = Convert.ToInt32(entry[2]),
                    Active = Convert.ToBoolean(entry[3])
                };

                people.Add(newPerson);
                //PeopleAvailable?.Invoke(this, new PeopleAvailableEventArgs(
                //    newPerson.FirstName, newPerson.LastName, newPerson.Age, newPerson.Active));
            }

            return people;
        }

        public async void RemovePeopleAsync()
        {
            await Task.Run(() => WriteToStore());
        }

        //IPeopleDataStoreGateway _info;

        //public ListPerson(IPeopleDataStoreGateway info)
        //{
        //    _info = info;
        //}

        public void WriteToStore()
        {
            lock (this)
            {
                string path = @"C:\Users\AbdiA\source\repos\TestProject\TestProject\bin\Debug\nameThread.txt";

                List<string> list = new List<string>();
                       

                foreach (Person person in people.ToList())
                {
                    if (person.Active == false) 
                    {
                        people.Remove(person);
                    }                  
                }

                foreach (var item in people.ToList())
                {
                    list.Add($"{item.FirstName},{item.LastName}, {item.Age}, {item.Active}");
                }

                File.WriteAllLines(path, list.ToArray());
                PeopleGone?.Invoke(this, new PeopleRemovedEventArgs(people)); //publisher
            }
        }

        public async void GetPeopleAsync()
        {
            await Task.Run(() => ReadPeopleFromStore());

        }

        public void ReadPeopleFromStore()
        {
            lock (this)
            {
                people = new List<Person>();

                List<string> fileLines = File.ReadAllLines("nameThread.txt").ToList();

                for (var i = 0; i < fileLines.Count; i++)
                {
                    string line = fileLines[i];
                    string[] entry = line.Split(',');

                    Person newPerson = new Person
                    {
                        FirstName = entry[0],
                        LastName = entry[1],
                        Age = Convert.ToInt32(entry[2]),
                        Active = Convert.ToBoolean(entry[3])
                    };

                    people.Add(newPerson);
                }


                PeopleAvailable?.Invoke(this, new PeopleAvailableEventArgs(people)); //publisher
            }
        }

        public void PeopleDatabaseNotFoundException(string file)
        {
            file = @"nameThread.txt";
            try
            {
                List<string> fileLines = File.ReadAllLines(file).ToList();
            }
            catch (FileNotFoundException ex)
            {
                //SystemException exception = new SystemException($"File not found! {ex} ");
                //SystemException exception = new SystemException("File not found! " + ex.ToString());
                SystemException exception = new SystemException(string.Format("File not found! {0}", ex.ToString()));
                throw exception;
            }
            finally
            {
                SystemException exception = new SystemException(string.Format("No issue locating the file."));
                throw exception;
            }


            //if (string.IsNullOrWhiteSpace(person.FirstName))
            //{
            //    throw new ArgumentException("You passed in an invalid parameter", "FirstName");
            //}
            //if (string.IsNullOrWhiteSpace(person.LastName))
            //{
            //    throw new ArgumentException("You passed in an invalid parameter", "LastName");
            //}
            //people.Add(person);

        }
    }

}



