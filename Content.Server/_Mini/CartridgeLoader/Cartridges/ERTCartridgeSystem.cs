// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Server.CartridgeLoader;
using Content.Shared.CartridgeLoader;
using Content.Shared.CartridgeLoader.Cartridges;
// using Content.Client.Mini.ERT.UI;
using Content.Server.Mini.Cartridges;

namespace Content.Server.Mini.ERT.Cartridges;

public sealed class ERTCartridgeSystem : EntitySystem
{
    [Dependency] private readonly CartridgeLoaderSystem _cartridge = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<ERTCallCartridgeComponent, CartridgeUiReadyEvent>(OnCartridgeUiReady);
    }

    private void OnCartridgeUiReady(Entity<ERTCallCartridgeComponent> ent, ref CartridgeUiReadyEvent args)
    {
        // Просто открываем UI ERT
        OpenErtUi(ent, args.Loader);
    }

    private void OpenErtUi(Entity<ERTCallCartridgeComponent> ent, EntityUid loaderUid)
    {
        // Открываем стандартный UI ERT через bound user interface
        // Предполагается, что ERT система уже имеет свой UI

        // Создаем состояние для открытия UI ERT
        var state = new ERTCartridgeUiState();
        _cartridge.UpdateCartridgeUiState(loaderUid, state);
    }
}
