using Newtonsoft.Json;

namespace PlcBase.Base.DomainModel;

public class ReqUser
{
    public int Id { get; set; }

    public string Email { get; set; }

    public ReqUser() { }

    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}