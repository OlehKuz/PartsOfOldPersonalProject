using System.Runtime.Serialization;

namespace Common.Enums
{
    public enum Role
    {
        None,
        [EnumMember(Value="CustomerRole")]
        CustomerRole,
        [EnumMember(Value="MarketologRole")]
        MarketologRole,
        [EnumMember(Value="AdminRole")]
        AdministratorRole,
        [EnumMember(Value="BeautyMasterRole")]
        BeautyMasterRole
    }
}