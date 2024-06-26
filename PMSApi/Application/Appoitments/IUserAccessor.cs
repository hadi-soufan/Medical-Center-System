﻿
using System.Diagnostics;

public interface IUserAccessor
{
    /// <summary>
    /// Gets the username of the current user.
    /// </summary>
    /// <returns>The username of the current user.</returns>
    string GetUsername();
}
