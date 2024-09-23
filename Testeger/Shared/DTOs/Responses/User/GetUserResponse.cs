﻿namespace Testeger.Shared.DTOs.Responses.User;

public class GetUserResponse
{
    public required string Id {  get; set; }
    public required string UserName { get; set; }
    public required string Email { get; set; }
    public required string PhoneNumber { get; set; }
}
