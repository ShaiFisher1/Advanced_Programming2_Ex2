namespace WebApplication1.Services
{
    public interface IContactService
    {
        public List<Contact> GetAll();
        public Contact Get(string id);
        public void Edit(Contact contact);
        public bool Delete(string id);
        public void Add(string id, string nickName, string service);

    }
}
