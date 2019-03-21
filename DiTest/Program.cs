using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace DiTest
{
    public interface ISome1
    {
        void SaySome1();
        Guid Id { get; set; }

    }

    public interface ISome2
    {
        void SaySome2();
        Guid Id { get; set; }
    }

    public interface ISome3
    {
        void SaySome3();
        Guid Id { get; set; }
    }

    public interface ISome4
    {
        void SaySome4();
        Guid Id { get; set; }
    }

    public class Some1 : ISome1
    {
        public Guid Id { get; set; }

        private ISome2 _some2;
        private ISome3 _some3;
        public Some1(ISome2 some2)
        {
            Console.WriteLine("construct 1");
            _some2 = some2;
            Id = Guid.NewGuid();
        }
        
        public Some1(ISome2 some2, ISome3 some3)
        {
            Console.WriteLine("construct 2");
            _some2 = some2;
            _some3 = some3;
            Id = Guid.NewGuid();

        }
        
        public void SaySome1()
        {
            Console.WriteLine("Some1");
            _some2?.SaySome2();
            _some3?.SaySome3();
        }
    }

    public class Some2 : ISome2
    {
        public Guid Id { get; set; }

        private ISome3 _some3;
        public Some2(ISome3 some3)
        {
            _some3 = some3;
            Id = Guid.NewGuid();
            Console.WriteLine($"Some2:{Id}");
        }


        public void SaySome2()
        {
            Console.WriteLine("Some2");
            _some3.SaySome3();
        }
    }
    
    public class Some3 : ISome3
    {
        public Guid Id { get; set; }

        public Some3()
        {
            Id = Guid.NewGuid();
            Console.WriteLine($"Some3:{Id}");

        }

        public void SaySome3()
        {
            Console.WriteLine("Some3");
        }
    }

    public class Some4:ISome4
    {
        public Guid Id { get; set; }

        public Some4()
        {
           Id = Guid.NewGuid();
        }

        public void SayHai()
        {
            Console.WriteLine("Some4Hai");
        }

        public void SaySome4()
        {
            Console.WriteLine("Some4");
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            var collection = new ServiceCollection();
            collection.AddTransient<ISome1, Some1>();
            collection.AddTransient<ISome2, Some2>();
//            collection.AddScoped<ISome3,Some3>();
            collection.AddTransient<ISome3,Some3>();

            collection.AddTransient<Some4>();
            
            ServiceProvider provider = collection.BuildServiceProvider();
            var some1 = provider.GetService<ISome1>();
            some1.SaySome1();
            var some2 = provider.GetService<ISome1>();
            some2.SaySome1();
//            var some4 = provider.GetService<Some4>();
//            some4.SayHai();
            
//            Dictionary<Type, object> dictionary = new Dictionary<Type, object>();
//            dictionary[typeof(Some1)] = some1;
//            var hoge = some1.GetType();
//            Some1 some1_1 = (Some1)dictionary[typeof(Some1)];
//            some1_1.SaySome1();
            
        }
    }
}