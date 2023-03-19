namespace Generator_Logic
{
    public class TapItem : GeneratorItem
    {
        protected override bool CanFix => Game.inst.player.pawsAreWashed;
    }
}