/*
 *        ┏┓   ┏┓+ +
 *       ┏┛┻━━━┛┻┓ + +
 *       ┃       ┃
 *       ┃   ━   ┃ ++ + + +
 *       ████━████ ┃+
 *       ┃       ┃ +
 *       ┃   ┻   ┃
 *       ┃       ┃ + +
 *       ┗━┓   ┏━┛
 *         ┃   ┃
 *         ┃   ┃ + + + +
 *         ┃   ┃    Code is far away from bug with the animal protecting       
 *         ┃   ┃ +     神兽保佑,代码无bug  
 *         ┃   ┃
 *         ┃   ┃  +
 *         ┃    ┗━━━┓ + +
 *         ┃        ┣┓
 *         ┃        ┏┛
 *         ┗┓┓┏━┳┓┏┛ + + + +
 *          ┃┫┫ ┃┫┫
 *          ┗┻┛ ┗┻┛+ + + +
 */

namespace ESys.Utilty.Defs
{
    using Microsoft.AspNetCore.Mvc.ApplicationModels;
    using System;

#pragma warning disable 1591
    [AttributeUsage(AttributeTargets.Parameter)]
    public class EntityWithNamesAttribute : Attribute, IParameterModelConvention
    {
        public void Apply(ParameterModel parameter)
        {
            if (parameter.BindingInfo == null)
            {
                parameter.BindingInfo = new Microsoft.AspNetCore.Mvc.ModelBinding.BindingInfo()
                {
                    BindingSource = Microsoft.AspNetCore.Mvc.ModelBinding.BindingSource.Body
                };
            }
        }
    }
    [AttributeUsage(AttributeTargets.Parameter)]
    public class EntityAttribute : Attribute, IParameterModelConvention
    {
        public void Apply(ParameterModel parameter)
        {
            if (parameter.BindingInfo == null)
            {
                parameter.BindingInfo = new Microsoft.AspNetCore.Mvc.ModelBinding.BindingInfo()
                {
                    BindingSource = Microsoft.AspNetCore.Mvc.ModelBinding.BindingSource.Body
                };
            }
        }
    }
#pragma warning restore 1591
}
