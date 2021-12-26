namespace Banks.Clients
{
    public abstract class ClientBuilder
    {
        public Person Person { get; private set; }

        public Person CreateNewClient(string name, string surname)
        {
            Person = new Person(name, surname);
            return Person;
        }

        public abstract void SetAddress(string address);

        public abstract void SetPassport(long passport);
    }
}