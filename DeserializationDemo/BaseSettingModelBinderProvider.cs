using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DeserializationDemo;

public class BaseSettingModelBinderProvider : IModelBinderProvider
{
    public IModelBinder GetBinder(ModelBinderProviderContext context)
    {
        if (context.Metadata.ModelType != typeof(BaseSetting))
        {
            return null;
        }

        var subclasses = new[]
        {
            typeof(WifiSetting),
            typeof(VpnSetting),
        };

        var binders = new Dictionary<Type, (ModelMetadata, IModelBinder)>();
        foreach (var type in subclasses)
        {
            var modelMetadata = context.MetadataProvider.GetMetadataForType(type);
            binders[type] = (modelMetadata, context.CreateBinder(modelMetadata));
        }

        return new BaseSettingModelBinder(binders);
    }
}