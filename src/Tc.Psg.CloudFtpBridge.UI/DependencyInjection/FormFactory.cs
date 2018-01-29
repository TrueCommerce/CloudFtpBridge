using System;
using System.Windows.Forms;

using Microsoft.Extensions.DependencyInjection;

namespace Tc.Psg.CloudFtpBridge.UI.DependencyInjection
{
    public class FormFactory : IFormFactory
    {
        private IServiceProvider _serviceProvider;

        public FormFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public T CreateForm<T>()
            where T : Form
        {
            return _serviceProvider.GetService<T>();
        }

        public T CreateForm<T>(Form owner)
            where T : Form
        {
            Form form = CreateForm<T>();
            form.Owner = owner;

            return (T)form;
        }
    }
}
