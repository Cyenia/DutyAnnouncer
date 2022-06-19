using Dalamud.Data;
using Dalamud.Game.ClientState;
using Dalamud.Game.Gui;
using Dalamud.Logging;
using Lumina.Excel;
using Lumina.Excel.GeneratedSheets;

namespace DutyAnnouncer;
public sealed class Discovery
{
    private readonly ClientState _clientState;
    private readonly DataManager _dataManager;
    private readonly ChatGui _chatGui;
    private ExcelSheet<ContentFinderCondition>? _contentFinderConditionsSheet;

    private bool dutyRoulette = false;

    public Discovery(ClientState client, DataManager data, ChatGui chat)
    {
        _clientState = client;
        _dataManager = data;
        _chatGui = chat;

        Initialize();
    }

    public void Initialize()
    {
        _clientState.CfPop += CfPop;
        _clientState.TerritoryChanged += OnTerritoryChanged;
        _contentFinderConditionsSheet = _dataManager.GameData.GetExcelSheet<ContentFinderCondition>();
    }

    private void OnTerritoryChanged(object? sender, ushort e)
    {
        var content = _contentFinderConditionsSheet?.FirstOrDefault(t => t.TerritoryType.Row == _clientState.TerritoryType);
        if (content != null && _dutyRoulette)
        {
            _chatGui.Print($"Entering: {content.Name}");
        }
    }

    private void CfPop(object? sender, ContentFinderCondition e)
    {
        dutyRoulette = false;
    
        if (_contentFinderConditionsSheet != null)
        {
            var content = _contentFinderConditionsSheet.FirstOrDefault(t => t.TerritoryType.Row == e.TerritoryType.Row);
    
            dutyRoulette = content != null && String.IsNullOrEmpty(content.Name);
        }
    }

    public void Dispose()
    {
        _clientState.TerritoryChanged -= OnTerritoryChanged;
        _clientState.CfPop -= CfPop;
    }
}