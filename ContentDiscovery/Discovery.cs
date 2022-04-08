using Dalamud.Data;
using Dalamud.Game.ClientState;
using Dalamud.Game.Gui;
using Dalamud.Logging;
using Lumina.Excel;
using Lumina.Excel.GeneratedSheets;

namespace ContentDiscovery;
public sealed class Discovery
{
    private readonly ClientState _clientState;
    private readonly DataManager _dataManager;
    private readonly ChatGui _chatGui;
    private ExcelSheet<ContentFinderCondition>? _contentFinderConditionsSheet;

    public Discovery(ClientState client, DataManager data, ChatGui chat)
    {
        _clientState = client;
        _dataManager = data;
        _chatGui = chat;

        Initialize();
    }

    public void Initialize()
    {
        _clientState.TerritoryChanged += OnTerritoryChanged;
        _contentFinderConditionsSheet = _dataManager.GameData.GetExcelSheet<ContentFinderCondition>();
    }

    private void OnTerritoryChanged(object? sender, ushort e)
    {
        if(_contentFinderConditionsSheet != null)
        {
            var content = _contentFinderConditionsSheet.FirstOrDefault(t => t.TerritoryType.Row == _clientState.TerritoryType);
            if (content != null)
            {
                _chatGui.Print($"Entering: {content.Name}");
            }
            else
            {
                PluginLog.Information($"Content is null {_clientState.TerritoryType}");
            }
        }
        else
        {
            PluginLog.Warning("Sheet is null");
        }
    }

    public void Dispose()
    {
        _clientState.TerritoryChanged -= OnTerritoryChanged;
    }
}