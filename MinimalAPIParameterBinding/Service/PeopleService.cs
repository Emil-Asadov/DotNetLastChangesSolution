using MinimalAPIParameterBinding.Entity;

namespace MinimalAPIParameterBinding.Service
{
    public sealed class PeopleService
    {
        private readonly List<Person> _people = new()
        {
            new Person("Asadov Kamran"),
            new Person("Asadov Qismat"),
            new Person("Asadov Kamil"),
            new Person("Asadov Kenan"),
            new Person("Asadov Elmir")
        };

        public IEnumerable<Person> Serach(string serachTerm)
        {
            var res = _people.Where(x => x.FullName.Contains(serachTerm, StringComparison.OrdinalIgnoreCase));

            return res;
        }

        public IEnumerable<Person> Add(Person person)
        {
            _people.Add(person);

            return _people;
        }
    }
}
