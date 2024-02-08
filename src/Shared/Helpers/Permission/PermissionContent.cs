namespace PlcBase.Shared.Helpers;

public class PermissionContent
{
    public string Key { get; set; }

    public string Description { get; set; }

    public PermissionContent(string key, string description)
    {
        Key = key;
        Description = description;
    }
}
