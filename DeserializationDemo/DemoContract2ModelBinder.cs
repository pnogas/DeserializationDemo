using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DeserializationDemo;

public class DemoContract2ModelBinder : IModelBinder
{
    private readonly Dictionary<Type, (ModelMetadata, IModelBinder)> _binders;

    public DemoContract2ModelBinder(Dictionary<Type, (ModelMetadata, IModelBinder)> binders)
    {
        _binders = binders;
    }

    public async Task BindModelAsync(ModelBindingContext bindingContext)
    {
        using var sr = new StreamReader(bindingContext.HttpContext.Request.Body);
        var json = await sr.ReadToEndAsync();
        var requestJObject = JObject.Parse(json);
        var feature = requestJObject["Feature"]?.ToObject<string>();
        var settings = (JObject)requestJObject["Settings"];

        BaseSetting setting = feature switch
        {
            "Wifi" => settings?.ToObject<WifiSetting>(),
            "Vpn" => settings?.ToObject<VpnSetting>(),
            _ => throw new Exception("Unsupported feature")
        };

        DemoContract2 result = new()
        {
            Feature = feature,
            Settings = setting
        };

        bindingContext.Result = ModelBindingResult.Success(result);
    }
}