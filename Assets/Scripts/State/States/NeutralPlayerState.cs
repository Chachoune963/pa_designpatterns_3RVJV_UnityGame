using UnityEngine;

public class NeutralPlayerState : PlayerState
{
    public NeutralPlayerState(PlayerController player) : base(player)
    {
    }

    public override void OnUpdate()
    {
        Vector2 movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        player.CurrentVelocity = movementInput * player.MovementSpeed;
    }

    public override void OnHit(AttackInfo attackInfo)
    {
        player.CurrentHealth.Value -= attackInfo.Strength;
        player.CurrentVelocity = attackInfo.ImpactDirection;
        player.ChangeState(new HitstunPlayerState(player, attackInfo.HitStun));
    }
}
