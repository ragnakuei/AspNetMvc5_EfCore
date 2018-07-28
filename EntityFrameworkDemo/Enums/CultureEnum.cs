using System.ComponentModel;

namespace EntityFrameworkDemo.Enums
{
    public enum CultureEnum
    {
        [Description("zh-TW")] zh_TW = 0,
        [Description("zh-CN")] zh_CN = 1,
        [Description("en-US")] en_US = 2
    }
}