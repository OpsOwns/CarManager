using CarManager.Domain.Core;

namespace CarManager.Domain.Enumeration;

public sealed class Role : Enumeration<Role>
{
    public static readonly Role Worker = new(1, "Worker");
    public static readonly Role Moderator = new(2, "Moderator");

    private Role(int value, string name)
        : base(value, name)
    {
    }

    private Role(int value)
        : base(value, GetValueByKey(value).Value)
    {
    }
}