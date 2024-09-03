using System;
using System.Text.Json.Serialization;

namespace ECommerceAPII.Application.DTOs.Facebook;

public class FacebookUserAccessTokenValidation
{
    [JsonPropertyName("data.is-valid")]
    public string IsValid { get; set; }

    [JsonPropertyName("data.user-id")]
    public string UserId { get; set; }
}

