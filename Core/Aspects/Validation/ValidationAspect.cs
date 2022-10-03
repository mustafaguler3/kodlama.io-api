
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Validation;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Aspects.Validation
{
    public class ValidationAspect : MethodInterception
    {
        private Type _validator;

        public ValidationAspect(Type validator)
        {
            if (!typeof(IValidator).IsAssignableFrom(validator))
            {
                throw new Exception("Message.WrongValidationType");
            }
            _validator = validator;
        }

        protected override void OnBefore(IInvocation invocation)
        {
            var validator = (IValidator)Activator.CreateInstance(_validator);

            var entityType = _validator.BaseType.GetGenericArguments()[0];

            var entities = invocation.Arguments.Where(type => type.GetType() == entityType);

            foreach (var entity in entities)
            {
                ValidationTool.Validate(validator, entity);
            }

            base.OnBefore(invocation);
        }
    }
}
