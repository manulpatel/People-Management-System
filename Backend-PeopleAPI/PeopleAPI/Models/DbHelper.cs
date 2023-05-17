using PeopleAPI.EfCore;

namespace PeopleAPI.Models
{
    public class DbHelper
    {
        private EF_DataContext _context;

        public DbHelper(EF_DataContext context)
        {
            _context = context;
        }

        //GET

        //GET all people
        public List<PersonModel> GetPeople()
        {
            List<PersonModel> response = new List<PersonModel>();
            var dataList = _context.People.ToList();
            dataList.ForEach(row => response.Add(new PersonModel()
            {
                Id = row.Id,
                FirstName = row.FirstName,
                LastName = row.LastName,
                Age = row.Age
            }));
            return response;
        }

        //GET by id

        public PersonModel GetPersonById (Guid Id)
        {
            PersonModel response = new PersonModel();
            var row = _context.People.Where(d => d.Id.Equals(Id)).FirstOrDefault();
            return new PersonModel()
            {
                Id = row.Id,
                FirstName = row.FirstName,
                LastName = row.LastName,
                Age = row.Age
            };
        }



        //Creating new record (POST)

        public void SavePerson(PersonModel personModel)
        {
            Person dbTable = new Person();

            dbTable.Id = Guid.NewGuid();
            dbTable.FirstName = personModel.FirstName;
            dbTable.LastName = personModel.LastName;
            dbTable.Age = personModel.Age;
            _context.People.Add(dbTable);
            _context.SaveChanges();
        }

        //Updating a record (PUT)
        public void UpdatePerson(PersonModel updateperson, Guid Id)
        {
            
            var person = _context.People.Where(d => d.Id.Equals(Id)).FirstOrDefault();
            if (person != null)
            {
                person.FirstName = updateperson.FirstName;
                person.LastName = updateperson.LastName;
                person.Age = updateperson.Age;
                _context.People.Update(person);
                _context.SaveChanges();
            }
        }

        //Deleting a record (DELETE)

        public void DeletePerson(Guid Id)
        {
            var person = _context.People.Where(d => d.Id.Equals(Id)).FirstOrDefault();
            if (person != null)
            {
                _context.People.Remove(person);
                _context.SaveChanges();
            }
        }

    }
}
