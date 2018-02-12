using System.Windows.Forms;

namespace Tc.Psg.CloudFtpBridge.UI.DependencyInjection
{
    public interface IFormFactory
    {
        T CreateForm<T>() where T : Form;
        T CreateForm<T>(Form owner) where T : Form;
    }
}
