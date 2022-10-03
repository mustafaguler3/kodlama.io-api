using Castle.DynamicProxy;
using Core.CrossCuttingConserns.Caching;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Aspects.Caching
{
    public class CacheAspect : MethodInterception
    {
        private int _duration;
        private ICacheManager _cacheManager;

        public CacheAspect(int duration, ICacheManager cacheManager)
        {
            _duration = duration;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }

        public override void Intercept(IInvocation invocation)
        {
            var methodName = string.Format($"{invocation.Method.ReflectedType.FullName}.{invocation.Method.Name}");//classımızın ismi//productservice.GetAll() gibi
            var arguments = invocation.Arguments.ToList();
            var key = $"{methodName}{string.Join(",",arguments.Select(i=>i?.ToString() ?? "<Null>"))}"; //productservice.Add(Product prod)
            //productservice.GetById(1,dadads) giibi

            if (_cacheManager.IsAdd(key))//daha önce böyle key varsa o değeri dön
            {
                invocation.ReturnValue = _cacheManager.Get(key);
                return;
            }
            invocation.Proceed();//diğer türlü bunu çalıştır
            _cacheManager.Add(key, invocation.ReturnValue, _duration);

            //cache hibir zaman veri erişimde yapmaayın yada uı da hepsini business da yapı
            base.Intercept(invocation);
        }
    }
}
