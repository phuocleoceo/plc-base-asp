using PlcBase.Base.DomainModel;
using System.Net.Sockets;
using System.Net;

namespace PlcBase.Extensions.Utilities;

public static class HttpContextUtility
{
    public static ReqUser GetRequestUser(this HttpContext context)
    {
        return context.Items["reqUser"] as ReqUser;
    }

    public static string GetIpAddress(this HttpContext context)
    {
        string ipAddress = string.Empty;

        var remoteIpAddress = context.Connection.RemoteIpAddress;

        if (remoteIpAddress != null)
        {
            if (remoteIpAddress.AddressFamily == AddressFamily.InterNetworkV6)
            {
                remoteIpAddress = Dns.GetHostEntry(remoteIpAddress).AddressList
                    .FirstOrDefault(x => x.AddressFamily == AddressFamily.InterNetwork);
            }

            if (remoteIpAddress != null) ipAddress = remoteIpAddress.ToString();

            return ipAddress;
        }

        return "127.0.0.1";
    }
}