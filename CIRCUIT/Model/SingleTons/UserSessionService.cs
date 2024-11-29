namespace CIRCUIT.Model.SingleTons
{
    public class UserSessionService
    {
        private static UserSessionService _instance;
        private int _userId;

        private UserSessionService() { }

        public static UserSessionService Instance => _instance ??= new UserSessionService();

        public int UserId
        {
            get => _userId;
            set => _userId = value;
        }

    }
}
