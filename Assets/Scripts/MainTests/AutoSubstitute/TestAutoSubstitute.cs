using NUnit.Framework;
using Assert = ModestTree.Assert;

namespace Zenject.Tests.AutoSubstitute
{
    [TestFixture]
    public class TestAutoSubstitute
    {
    #region Private Variables

        private DiContainer _container;

    #endregion

    #region Setup/Teardown Methods

        [SetUp]
        public void Setup()
        {
            _container = new DiContainer();
        }

    #endregion

    #region Test Methods

        [Test]
        public void Test1()
        {
            _container.Bind<IFoo>().FromSubstitute();

            _container.Bind<Bar>().AsSingle();

            _container.Resolve<Bar>().Run();
        }

        [Test]
        public void TestFactories()
        {
            _container.BindFactory<IFoo , FooFactory>().FromSubstitute();

            _container.Bind<Bar2>().AsSingle();

            _container.Resolve<Bar2>().Run();
        }

    #endregion

    #region Nested Types

        public class Bar
        {
        #region Private Variables

            private readonly IFoo _foo;

        #endregion

        #region Constructor

            public Bar(IFoo foo)
            {
                _foo = foo;
            }

        #endregion

        #region Public Methods

            public void Run()
            {
                _foo.DoSomething();

                var result = _foo.GetTest();

                Assert.IsEmpty(result);
            }

        #endregion
        }

        public class Bar2
        {
        #region Private Variables

            private readonly FooFactory _fooFactory;

        #endregion

        #region Constructor

            public Bar2(FooFactory fooFactory)
            {
                _fooFactory = fooFactory;
            }

        #endregion

        #region Public Methods

            public void Run()
            {
                var foo = _fooFactory.Create();

                foo.DoSomething();

                var result = foo.GetTest();

                Assert.IsEmpty(result);
            }

        #endregion
        }

        public class FooFactory : PlaceholderFactory<IFoo> { }

        public interface IFoo
        {
        #region Public Methods

            void   DoSomething();
            string GetTest();

        #endregion
        }

    #endregion
    }
}