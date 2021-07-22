using Nightmare;
using NSubstitute;
using NUnit.Framework;
using Zenject;

public class PlayerTests : ZenjectUnitTestFixture
{
#region Setup/Teardown Methods

    [SetUp]
    public void Setup()
    {
        SignalBusInstaller.Install(Container);
        Container.Bind<IDomainEventBus>().FromSubstitute().AsSingle();
    }

#endregion

#region Test Methods

    [Test]
    public void CreatePlayer()
    {
        var startingHealth = 100;
        var player         = Container.Instantiate<Player>(new object[] { startingHealth });
        Assert.AreEqual(startingHealth , player.CurrentHealth);
        Assert.AreEqual(false ,          player.IsDead);
    }

    [Test]
    public void Should_Player_Created_Exist_And_Published_Event_When_CreatePlayer()
    {
        var player       = Container.Instantiate<Player>(new object[] { 0 });
        var domainEvents = player.GetDomainEvents();
        Assert.AreEqual(1 , domainEvents.Count);
        var playerCreated = (PlayerCreated)domainEvents[0];
        Assert.NotNull(playerCreated);
        var domainEventBus = Container.Resolve<IDomainEventBus>();
        domainEventBus.ReceivedWithAnyArgs(1).PostAll(null);
    }

    [Test]
    public void TakeDamage()
    {
        var startingHealth = 100;
        var damage         = 87;
        var player         = Container.Instantiate<Player>(new object[] { startingHealth });
        player.TakeDamage(damage);
        Assert.AreEqual(13 , player.CurrentHealth);
    }

    [Test]
    public void Should_PlayerTookDamage_Exist_When_TakeDamage()
    {
        // event 1
        var player = Container.Instantiate<Player>(new object[] { 100 });
        var damage = 87;
        // event 2
        player.TakeDamage(damage);
        var domainEvents = player.GetDomainEvents();
        Assert.AreEqual(2 , domainEvents.Count);
        var playerTookDamage = (PlayerTookDamage)domainEvents[1];
        Assert.NotNull(playerTookDamage);
        Assert.AreEqual(100 , playerTookDamage.StartingHealth);
        Assert.AreEqual(87 ,  playerTookDamage.Amount);
        Assert.AreEqual(13 ,  playerTookDamage.CurrentHealth);
    }

    [Test]
    public void TakeDamage_Player_Will_Die()
    {
        var startingHealth = 100;
        var damage         = 101;
        var player         = Container.Instantiate<Player>(new object[] { startingHealth });
        player.TakeDamage(damage);
        Assert.AreEqual(true , player.IsDead);
    }

    [Test]
    public void MakePlayerDie()
    {
        var player = Container.Instantiate<Player>(new object[] { 100 });
        player.MakeDie();
        Assert.AreEqual(true , player.IsDead);
    }

#endregion
}