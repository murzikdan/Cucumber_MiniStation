using Robust.Client.UserInterface;
using Content.Client.CartridgeLoader;
using Content.Shared.Mini.ERT;
using Content.Shared.Mini.ERT.Prototypes;
using Content.Client.Mini.ERT.UI;
using Content.Server.Mini.Cartridges;

namespace Content.Client.Mini.ERT.Cartridges;

public sealed class ERTCartridgeBoundUserInterface
{
    private ErtResponceConsoleWindow? _window;

    public ERTCartridgeBoundUserInterface(EntityUid owner, Enum uiKey)
        : base(owner, uiKey)
    {
    }

    protected override void Open()
    {
        base.Open();

        // Просто открываем существующее ERT окно
        _window = new ErtResponceConsoleWindow();
        _window.OpenCentered();

        // Настраиваем обработчики событий
        _window.ResponceTeamButton.OnPressed += _ =>
        {
            SendMessage(new ErtResponceConsoleUiButtonPressedMessage(
                ErtResponceConsoleUiButton.ResponceErt,
                team: GetSelectedTeam(),
                callReason: GetCallReason()
            ));
        };

        _window.OnClose += Close;
    }

    private string? GetSelectedTeam()
    {
        var item = _window?.AvailableTeamsList.GetSelected().FirstOrDefault();
        return item?.Metadata as string;
    }

    private string? GetCallReason()
    {
        var reason = _window?.CallReasonEdit.Text;
        return string.IsNullOrWhiteSpace(reason) ? null : reason;
    }

    protected override void UpdateState(BoundUserInterfaceState state)
    {
        if (state is not ErtResponceConsoleBoundUserInterfaceState ertState)
            return;

        _window?.Populate(ertState);
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);

        if (disposing)
        {
            _window?.Close();
            _window = null;
        }
    }
}
