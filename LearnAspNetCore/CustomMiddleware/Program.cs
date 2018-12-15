using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomMiddleware
{
    class Program
    {
        private static List<Func<RequestDelegate, RequestDelegate>>
            _list = new List<Func<RequestDelegate, RequestDelegate>>();

        static void Main(string[] args)
        {
            // List<int> ints = new List<int>();
            // ints.Add(1);
            // ints.Add(2);
            // ints.Add(3);
            // foreach (var item in ints)
            // {
            //     System.Console.WriteLine(item);
            // }
            // // 1
            // // 2
            // // 3

            // return;

            Use(next =>
            {
                return context =>
                {
                    Console.WriteLine("1");
                    return next.Invoke(context);
                };
            });

            Use(next =>
            {
                return context =>
                {
                    Console.WriteLine("2");
                    return next.Invoke(context);
                };
            });

            RequestDelegate end = context =>
            {
                System.Console.WriteLine("end...");
                return Task.CompletedTask;
            };


            // _list.Reverse();
            foreach (var middleware in _list)
            {
                end = middleware.Invoke(end);
            }

            end.Invoke(new Context());

            // Console.ReadLine();
        }


        static void Use(Func<RequestDelegate, RequestDelegate> middleware)
        {
            _list.Add(middleware);
        }
    }
}
