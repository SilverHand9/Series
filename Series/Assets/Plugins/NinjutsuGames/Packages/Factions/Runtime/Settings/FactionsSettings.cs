using GameCreator.Runtime.Common;

namespace NinjutsuGames.Runtime.Factions
{
    public class FactionsSettings : AssetRepository<FactionsRepository>
    {
        public override IIcon Icon => new IconFaction(ColorTheme.Type.TextLight);
        public override string Name => "Factions";
    }
}