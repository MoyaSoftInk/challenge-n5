namespace ChallengeN5.Query.API.Architecture.Model;

using System.Net;

/// <summary>
/// Base response
/// </summary>
public class BaseResponse
{
    /// <summary>
    /// Constructor
    /// </summary>
    public BaseResponse()
    {
        HttpCode = (int)HttpStatusCode.OK;
        HttpMessage = HttpStatusCode.OK.ToString();
        UserFriendlyError = "";
        MoreInformation = "";
        InternalId = "-";
    }

    /// <summary>
    /// http code
    /// </summary>
    public int HttpCode { get; set; }

    /// <summary>
    /// Message http response
    /// </summary>
    public string HttpMessage { get; set; }

    /// <summary>
    /// unique Id 
    /// </summary>
    public string InternalId { get; set; }

    /// <summary>
    /// Message to show to the user
    /// </summary>
    public string UserFriendlyError { get; set; }

    /// <summary>
    /// More information about the error
    /// </summary>
    public string MoreInformation { get; set; }
}
