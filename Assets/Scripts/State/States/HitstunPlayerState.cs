public class HitstunPlayerState : PlayerState
{
    private int stunFrames = 0;
    
    public HitstunPlayerState(PlayerController player, int stunFrames) : base(player)
    {
        this.stunFrames = stunFrames;
    }

    public override void OnUpdate()
    {
        if (stunFrames <= 0)
            player.ChangeState(new NeutralPlayerState(player));

        --stunFrames;
    }

    public override void OnHit(AttackInfo attackInfo)
    {
        player.CurrentHealth.Value -= attackInfo.Strength;
        stunFrames = attackInfo.HitStun;
    }
}
