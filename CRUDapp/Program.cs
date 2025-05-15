using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDapp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*var c = new Category();
            Console.WriteLine(c.CategoryName);
            Console.ReadLine();*/

            var list = Category.GetAll();
            foreach (var item in list) {
                Console.WriteLine(item.CategoryName);
            
            }
            Console.ReadLine();
        }
    }
}
