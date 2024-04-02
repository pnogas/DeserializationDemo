using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DeserializationDemo;

public class BaseSettingModelBinder : IModelBinder
{
    private readonly Dictionary<Type, (ModelMetadata, IModelBinder)> _binders;

    public BaseSettingModelBinder(Dictionary<Type, (ModelMetadata, IModelBinder)> binders)
    {
        _binders = binders;
    }

    public async Task BindModelAsync(ModelBindingContext bindingContext)
    {
        using var sr = new StreamReader(bindingContext.HttpContext.Request.Body);
        var json = await sr.ReadToEndAsync();
        var requestJObject = JObject.Parse(json);
        var feature = requestJObject["Feature"]?.ToObject<string>();

        BaseSetting result = feature switch
        {
            "Wifi" => JsonConvert.DeserializeObject<WifiSetting>(json),
            "Vpn" => JsonConvert.DeserializeObject<VpnSetting>(json),
            _ => throw new Exception("Unsupported feature")
        };

        bindingContext.Result = ModelBindingResult.Success(result);
    }
}