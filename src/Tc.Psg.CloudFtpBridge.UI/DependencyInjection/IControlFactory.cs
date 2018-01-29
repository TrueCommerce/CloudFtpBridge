using System.Windows.Forms;

namespace Tc.Psg.CloudFtpBridge.UI.DependencyInjection
{
    public interface IControlFactory
    {
        T CreateControl<T>() where T : Control;
        T CreateDockedControl<T>() where T : Control;
    }
}
