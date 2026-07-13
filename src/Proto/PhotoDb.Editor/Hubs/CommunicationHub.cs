// <copyright file="CommunicationHub.cs" company="Dmitry Kolchev">
// Copyright (c) 2026 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Microsoft.AspNetCore.SignalR;

namespace PhotoDb.Editor.Hubs;

public class CommunicationHub: Hub
{
    public async Task SendMessage(string username, string message)
    {
        await Clients.All.SendAsync("messageReceived", username, message);
    }
}
