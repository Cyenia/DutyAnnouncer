using System;
using Dalamud.Game.Gui;
using Dalamud.IoC;
using Dalamud.Plugin;
using Dalamud.Data;
using Dalamud.Game.ClientState;

namespace DutyAnnouncer;

internal class Plugin : IDalamudPlugin
{
    public string Name => "Duty Announcer";

    private readonly Discovery _discovery;

    public Plugin(
        [RequiredVersion("1.0")] ClientState client,
        [RequiredVersion("1.0")] DataManager data,
        [RequiredVersion("1.0")] ChatGui chat
    )
    {
        _discovery = new Discovery(client, data, chat);
    }

    public void Dispose()
    {
        _discovery.Dispose();
        GC.SuppressFinalize(this);
    }
}