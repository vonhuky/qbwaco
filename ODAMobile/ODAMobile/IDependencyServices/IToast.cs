namespace ODAMobile.IDependencyServices
{
    public interface IToast
    {
        void LongAlert(string message);

        void ShortAlert(string message);
    }
}
