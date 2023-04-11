namespace PlcBase.Shared.Helpers;

public class PermissionContent
{
    public int Key { get; set; }

    public string Description { get; set; }

    public PermissionContent() { }

    public PermissionContent(int Key, string Description)
    {
        this.Key = Key;
        this.Description = Description;
    }
}
