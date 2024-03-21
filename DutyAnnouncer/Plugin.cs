using System;
using Dalamud.Plugin;
using Dalamud.Plugin.Services;

namespace DutyAnnouncer;

internal class Plugin(IClientState clientState, IDataManager dataManager, IChatGui chatGui) : IDalamudPlugin
{
    private readonly Discovery _discovery = new(clientState, dataManager, chatGui);

    public void Dispose()
    {
        _discovery.Dispose();
        GC.SuppressFinalize(this);
    }
}