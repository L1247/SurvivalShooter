using Nightmare;
using NUnit.Framework;

public class PlayerTests
{
#region Test Methods

    [Test]
    public void CreatePlayer()
    {
        var startingHealth = 100;
        var player         = new Player(startingHealth);
        Assert.AreEqual(startingHealth , player.CurrentHealth);
        Assert.AreEqual(false ,          player.IsDead);
    }

    [Test]
    public void TakeDamage()
    {
        var startingHealth = 100;
        var damage         = 87;
        var player         = new Player(startingHealth);
        player.TakeDamage(damage);
        Assert.AreEqual(13 , player.CurrentHealth);
    }

    [Test]
    public void TakeDamage_Player_Will_Die()
    {
        var startingHealth = 100;
        var damage         = 101;
        var player         = new Player(startingHealth);
        player.TakeDamage(damage);
        Assert.AreEqual(true , player.IsDead);
    }

#endregion
}