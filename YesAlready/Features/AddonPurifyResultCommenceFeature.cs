using Dalamud.Game.Addon.Lifecycle.AddonArgTypes;
using Dalamud.Game.Addon.Lifecycle;
using ECommons.Automation;
using FFXIVClientStructs.FFXIV.Component.GUI;
using YesAlready.BaseFeatures;
using static ECommons.GenericHelpers;

namespace YesAlready.Features;

internal class AddonPurifyResultCommenceFeature : BaseFeature
{
    public override void Enable()
    {
        base.Enable();
        AddonLifecycle.RegisterListener(AddonEvent.PostUpdate, "PurifyResult", AddonSetup);
    }

    public override void Disable()
    {
        base.Disable();
        AddonLifecycle.UnregisterListener(AddonSetup);
    }

    protected static unsafe void AddonSetup(AddonEvent eventType, AddonArgs addonInfo)
    {
        var addon = (AtkUnitBase*)addonInfo.Addon;

        if (!P.Active || !P.Config.AetherialReductionCommence)
            return;

        if (IsAddonReady(addon->UldManager.NodeList[4]->GetAsAtkComponentNode()))
            Callback.Fire(addon, true, 0);
    }
}
