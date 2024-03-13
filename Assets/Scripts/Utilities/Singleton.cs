public class Singleton<T> where T : class, new()
{
    protected static volatile T m_instance;
    private static readonly object syncRoot = new object();

    public static T Instance
    {
        get
        {
            if (m_instance == null)
            {
                lock (syncRoot)
                {
                    if (m_instance == null)
                    {
                        m_instance = new T();
                    }
                }
            }

            return m_instance;
        }
    }

}
