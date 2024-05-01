using PlcBase.Shared.Utilities;

namespace PlcBase.Base.DomainModel;

public class ReqUser
{
    public int Id { get; set; }

    public string Email { get; set; }

    public override string ToString()
    {
        return JsonUtility.Stringify(this);
    }
}
