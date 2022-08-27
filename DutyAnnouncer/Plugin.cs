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

    public Plugin(ClientState clientState, DataManager dataManager, ChatGui chatGui
    )
    {
        _discovery = new Discovery(clientState, dataManager, chatGui);
    }

    public void Dispose()
    {
        _discovery.Dispose();
        GC.SuppressFinalize(this);
    }
}