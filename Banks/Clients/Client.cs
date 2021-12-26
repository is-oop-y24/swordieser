namespace Banks.Clients
{
    public class Client : ClientBuilder
    {
        public override void SetAddress(string address)
        {
            Person.Address = address;
            Person.CheckDoubtfulness();
        }

        public override void SetPassport(long passport)
        {
            Person.Passport = passport;
            Person.CheckDoubtfulness();
        }
    }
}