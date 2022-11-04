namespace HOA.Sessions
{

    public abstract class SessionComponent
    {
        public Session session { get; private set; }

        public SessionComponent(Session session)
        {
            this.session = session;
        }

    }
}