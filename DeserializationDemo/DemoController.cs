﻿using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DeserializationDemo;

[ApiController]
[Route("demo")]
public class DemoController
{
    private readonly ThirdPartyCode _thirdPartyCode = new();

    [HttpPost]
    [Route("test1")]
    public void DoTest([FromBody] DemoContract1 contract1)
    {
        // attach breakpoint here and see
        // contract1.Settings["Foo"] is of type Newtonsoft.Json.Linq.JObect and not TestObject with TypeNameHandling.None
        // contract1.Settings["Foo"] is of type TestObject with TypeNameHandling.All
        Console.WriteLine("test1");
        _thirdPartyCode.DoStuff(contract1.Feature, contract1.Settings);
    }
    
    [HttpPost]
    [Route("test2")]
    public void DoTest2([FromBody] DemoContract2 contract2)
    {
        Console.WriteLine("test2");
        _thirdPartyCode.DoStuff(contract2.Feature, contract2.Settings as IDictionary<string, object>);
    }
}