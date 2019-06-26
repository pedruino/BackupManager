using CommonServiceLocator;
using Ninject;
using System;
using System.Collections.Generic;

namespace BackupManager.App.IoC
{
    public class NinjectServiceLocator : IServiceLocator
    {
        private readonly IKernel _kernel;

        public NinjectServiceLocator(IKernel kernel)
        {
            _kernel = kernel;
        }

        public IEnumerable<object> GetAllInstances(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }

        public IEnumerable<TService> GetAllInstances<TService>()
        {
            return _kernel.GetAll<TService>();
        }

        public object GetInstance(Type serviceType)
        {
            return _kernel.Get(serviceType);
        }

        public object GetInstance(Type serviceType, string key)
        {
            return !string.IsNullOrEmpty(key)
                ? _kernel.Get(serviceType, key)
                : _kernel.Get(serviceType, (string)null);
        }

        public TService GetInstance<TService>()
        {
            return _kernel.Get<TService>();
        }

        public TService GetInstance<TService>(string key)
        {
            return _kernel.Get<TService>(key);
        }

        public object GetService(Type serviceType)
        {
            return _kernel.GetService(serviceType);
        }
    }
}