using System;
using System.Windows.Forms;

using Microsoft.Extensions.DependencyInjection;

namespace Tc.Psg.CloudFtpBridge.UI.DependencyInjection
{
    public class ControlFactory : IControlFactory
    {
        private IServiceProvider _serviceProvider;

        public ControlFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public T CreateControl<T>()
            where T : Control
        {
            return _serviceProvider.GetService<T>();
        }

        public T CreateDockedControl<T>()
            where T : Control
        {
            Control control = CreateControl<T>();
            control.Dock = DockStyle.Fill;

            return (T)control;
        }
    }
}
