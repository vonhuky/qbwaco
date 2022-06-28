using System.IO;

namespace ODAMobile.IDependencyServices
{
    public interface IQRCodeService
    {
        Stream GenerateQR(string text, int width = 300, int height = 300);
    }
}
