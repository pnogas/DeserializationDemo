using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DeserializationDemo;

public class DemoContract2ModelBinderProvider : IModelBinderProvider
{
    public IModelBinder GetBinder(ModelBinderProviderContext context)
    {
        if (context.Metadata.ModelType != typeof(DemoContract2))
        {
            return null;
        }

        var binders = new Dictionary<Type, (ModelMetadata, IModelBinder)>();
        return new DemoContract2ModelBinder(binders);
    }
}