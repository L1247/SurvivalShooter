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
    }

#endregion
}