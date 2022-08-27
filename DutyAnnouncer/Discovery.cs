using System.Linq;
using Dalamud.Data;
using Dalamud.Game.ClientState;
using Dalamud.Game.Gui;
using Lumina.Excel;
using Lumina.Excel.GeneratedSheets;

namespace DutyAnnouncer;

public sealed class Discovery
{
    private readonly ClientState _clientState;
    private readonly DataManager _dataManager;
    private readonly ChatGui _chatGui;
    private ExcelSheet<ContentFinderCondition> _contentFinderConditionsSheet;

    private bool _dutyRoulette;

    public Discovery(ClientState clientState, DataManager dataManager, ChatGui chatGui)
    {
        _clientState = clientState;
        _dataManager = dataManager;
        _chatGui = chatGui;

        Initialize();
    }

    private void Initialize()
    {
        _clientState.CfPop += CfPop;
        _clientState.TerritoryChanged += OnTerritoryChanged;
        _contentFinderConditionsSheet = _dataManager.GameData.GetExcelSheet<ContentFinderCondition>();
    }

    private void OnTerritoryChanged(object sender, ushort e)
    {
        var content = _contentFinderConditionsSheet?.FirstOrDefault(t => t.TerritoryType.Row == _clientState.TerritoryType);
        if (content != null && _dutyRoulette)
        {
            _chatGui.Print($"Entering: {content.Name}");
        }
    }

    private void CfPop(object sender, ContentFinderCondition e)
    {
        _dutyRoulette = false;

        if (_contentFinderConditionsSheet == null) return;
        var content = _contentFinderConditionsSheet.FirstOrDefault(t => t.TerritoryType.Row == e.TerritoryType.Row);
    
        _dutyRoulette = content != null && string.IsNullOrEmpty(content.Name);
    }

    public void Dispose()
    {
        _clientState.TerritoryChanged -= OnTerritoryChanged;
        _clientState.CfPop -= CfPop;
    }
}