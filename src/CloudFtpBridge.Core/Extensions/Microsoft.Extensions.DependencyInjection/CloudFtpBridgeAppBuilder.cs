using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Extensions.DependencyInjection
{
    public class CloudFtpBridgeAppBuilder : IServiceCollection
    {
        private readonly IServiceCollection _services;

        public CloudFtpBridgeAppBuilder(IServiceCollection services)
        {
            _services = services;
        }

        public ServiceDescriptor this[int index] { get => ((IList<ServiceDescriptor>)_services)[index]; set => ((IList<ServiceDescriptor>)_services)[index] = value; }

        public int Count => ((ICollection<ServiceDescriptor>)_services).Count;

        public bool IsReadOnly => ((ICollection<ServiceDescriptor>)_services).IsReadOnly;

        public void Add(ServiceDescriptor item)
        {
            ((ICollection<ServiceDescriptor>)_services).Add(item);
        }

        public void Clear()
        {
            ((ICollection<ServiceDescriptor>)_services).Clear();
        }

        public bool Contains(ServiceDescriptor item)
        {
            return ((ICollection<ServiceDescriptor>)_services).Contains(item);
        }

        public void CopyTo(ServiceDescriptor[] array, int arrayIndex)
        {
            ((ICollection<ServiceDescriptor>)_services).CopyTo(array, arrayIndex);
        }

        public IEnumerator<ServiceDescriptor> GetEnumerator()
        {
            return ((IEnumerable<ServiceDescriptor>)_services).GetEnumerator();
        }

        public int IndexOf(ServiceDescriptor item)
        {
            return ((IList<ServiceDescriptor>)_services).IndexOf(item);
        }

        public void Insert(int index, ServiceDescriptor item)
        {
            ((IList<ServiceDescriptor>)_services).Insert(index, item);
        }

        public bool Remove(ServiceDescriptor item)
        {
            return ((ICollection<ServiceDescriptor>)_services).Remove(item);
        }

        public void RemoveAt(int index)
        {
            ((IList<ServiceDescriptor>)_services).RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_services).GetEnumerator();
        }
    }
}
