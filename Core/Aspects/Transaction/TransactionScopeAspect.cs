using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Core.Aspects.Transaction
{
    public class TransactionScopeAspect : MethodInterception
    {
        public override void Intercept(IInvocation invocation)
        {
            using (TransactionScope scope = new TransactionScope())//using bloğu açmamızın nedeni dispose pattern uyguladığı için
            {
                try
                {
                    invocation.Proceed();//metodu çalıştır başarılı olursa alttaki kodu yazdık işlemi kabul et
                    scope.Complete();
                }
                catch (Exception)
                {
                    scope.Dispose(); //yapılan işlemi geri al
                    throw;
                }
            }
            base.Intercept(invocation);
        }
    }
}
