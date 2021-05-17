using System.Runtime.Serialization;

namespace Common.Enums
{
    public enum Day
    {
        None,
        [EnumMember(Value="Monday")]
        Monday, 
        [EnumMember(Value="Tuesday")]
        Tuesday,
        [EnumMember(Value="Wednesday")]
        Wednesday,
        [EnumMember(Value="Thursday")]
        Thursday,
        [EnumMember(Value="Friday")]
        Friday,
        [EnumMember(Value="Saturday")]
        Saturday,
        [EnumMember(Value="Sunday")]
        Sunday
    }
}