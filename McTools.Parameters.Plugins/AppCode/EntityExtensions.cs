using System;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.Xrm.Sdk;

namespace McTools.Parameters.Plugins.AppCode
{
    public static class EntityExtensions
    {
        public static bool Contains<T>(this Entity entity, Expression<Func<T>> expression)
        {
            var me = expression.Body as MemberExpression;

            if (me == null)
                throw new ArgumentException("attribute name must be specified");

            var pi = (PropertyInfo)me.Member;

            var customAttributes = pi.GetCustomAttributes(true);

            if (customAttributes.Length > 0)
            {
                var attribute = (AttributeLogicalNameAttribute)customAttributes[0];
                return entity.Contains(attribute.LogicalName);
            }

            return false;
        }

        public static void Remove<T>(this Entity entity, Expression<Func<T>> expression)
        {
            var me = expression.Body as MemberExpression;

            if (me == null)
                throw new ArgumentException("attribute name must be specified");

            var pi = (PropertyInfo)me.Member;

            var customAttributes = pi.GetCustomAttributes(true);

            if (customAttributes.Length <= 0) return;
            var attribute = (AttributeLogicalNameAttribute)customAttributes[0];

            if (entity.Contains(attribute.LogicalName))
                entity.Attributes.Remove(attribute.LogicalName);
        }
    }
}