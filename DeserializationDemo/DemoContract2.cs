using Microsoft.AspNetCore.Mvc;

namespace DeserializationDemo;

public class DemoContract2
{
    public string Feature { get; set; }
    
    // [ModelBinder(BinderType = typeof(BaseSetting))]
    public BaseSetting Settings { get; set; }
}