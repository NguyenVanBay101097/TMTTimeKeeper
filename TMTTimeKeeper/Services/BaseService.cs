using System;

namespace TMTTimeKeeper.Services
{
    public class BaseService
    {
        private IServiceProvider _provider;
        public BaseService(IServiceProvider provider)
        {
            _provider = provider;
        }

        protected T GetService<T>()
        {
            return (T)_provider.GetService(typeof(T));
        }
    }
}
