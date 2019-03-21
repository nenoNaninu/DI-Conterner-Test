using System;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace DiTest
{
    public interface ISome1
    {
        void SaySome1();
    }

    public interface ISome2
    {
        void SaySome2();
    }

    public interface ISome3
    {
        void SaySome3();
    }

    public class Some1 : ISome1
    {
        private ISome2 _some2;
        public Some1(ISome2 some2)
        {
            _some2 = some2;
        }
        public void SaySome1()
        {
            Console.WriteLine("Some1");
            _some2.SaySome2();
        }
    }

    public class Some2 : ISome2
    {
        private ISome3 _some3;
        public Some2(ISome3 some3)
        {
            _some3 = some3;
        }


        public void SaySome2()
        {
            Console.WriteLine("Some2");
            _some3.SaySome3();
        }
    }
    
    public class Some3 : ISome3
    {
        public void SaySome3()
        {
            Console.WriteLine("Some3");
        }
    }

    public class Some4
    {
        public void SayHai()
        {
            Console.WriteLine("Some4");
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var collection = new ServiceCollection();
            collection.AddTransient<ISome1, Some1>();
            collection.AddTransient<ISome2, Some2>();
            collection.AddTransient<ISome3, Some3>();
            collection.AddTransient<Some4>();
            
            ServiceProvider provider = collection.BuildServiceProvider();
            var some1 = provider.GetService<ISome1>();
            some1.SaySome1();
            var some4 = provider.GetService<Some4>();
            some4.SayHai();

        }
    }
}