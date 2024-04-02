namespace DeserializationDemo;

public class VpnSetting : BaseSetting
{
    public int Type { get; set; } // 0 = IKEv2, 1 = L2TP, ...
    public string Address { get; set; }
}