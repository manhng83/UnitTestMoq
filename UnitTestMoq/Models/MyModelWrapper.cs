using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnitTestMoq.Models
{
    public interface IJsonAttribute
    {
        object TryConvert(string modelValue, Type targertType, out bool success);
    }

    public class MyModelWrapper
    {
        public IList<IFormFile> Files { get; set; }

        [FromJson]
        public MyModel Model { get; set; } // <-- JSON will be deserialized to this object
    }

    public class MyModel
    {
        public string Name { get; set; }
        public string Address { get; set; }

        public MyModel()
        {
        }

        public MyModel(string name, string address)
        {
            Name = name;
            Address = address;
        }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class FromJsonAttribute : Attribute, IJsonAttribute
    {
        public object TryConvert(string modelValue, Type targetType, out bool success)
        {
            var value = JsonConvert.DeserializeObject(modelValue, targetType);
            success = value != null;
            return value;
        }
    }

    public class JsonModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            if (context.Metadata.IsComplexType)
            {
                var propName = context.Metadata.PropertyName;
                var propInfo = context.Metadata.ContainerType?.GetProperty(propName);
                if (propName == null || propInfo == null)
                    return null;
                // Look for FromJson attributes
                var attribute = propInfo.GetCustomAttributes(typeof(FromJsonAttribute), false).FirstOrDefault();
                if (attribute != null)
                    return new JsonModelBinder(context.Metadata.ModelType, attribute as IJsonAttribute);
            }
            return null;
        }
    }

    public class JsonModelBinder : IModelBinder
    {
        private IJsonAttribute _attribute;
        private Type _targetType;

        public JsonModelBinder(Type type, IJsonAttribute attribute)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            _attribute = attribute as IJsonAttribute;
            _targetType = type;
        }

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null) throw new ArgumentNullException(nameof(bindingContext));
            // Check the value sent in
            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (valueProviderResult != ValueProviderResult.None)
            {
                bindingContext.ModelState.SetModelValue(bindingContext.ModelName, valueProviderResult);
                // Attempt to convert the input value
                var valueAsString = valueProviderResult.FirstValue;
                bool success;
                var result = _attribute.TryConvert(valueAsString, _targetType, out success);
                if (success)
                {
                    bindingContext.Result = ModelBindingResult.Success(result);
                    return Task.CompletedTask;
                }
            }
            return Task.CompletedTask;
        }
    }
}