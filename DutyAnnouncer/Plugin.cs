using System;
using Dalamud.Plugin;
using Dalamud.Plugin.Services;

namespace DutyAnnouncer;

internal class Plugin : IDalamudPlugin
{
    private readonly Discovery _discovery;

    public Plugin(IClientState clientState, IDataManager dataManager, IChatGui chatGui
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