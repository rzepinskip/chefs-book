namespace ChefsBook.Auth.Google
{
    public sealed class GoogleUser
    {
        public string Id { get; }
        public string Email { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Photo { get; }

        public GoogleUser(
            string id, string email,
            string firstName, string lastName, string photo)
        {
            Id = id;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            Photo = photo;
        }
    }
}