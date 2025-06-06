using CRUDapp.Models;
using Spectre.Console;
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
            #region

            /*int option;

            do
            {
                Console.Clear();
                Console.WriteLine("***************");
                Console.WriteLine("* 1. Products *");
                Console.WriteLine("* 2. Sales    *");
                Console.WriteLine("* 3. Suppliers*");
                Console.WriteLine("* 4. Customers*");
                Console.WriteLine("* 0. Exit     *");
                Console.WriteLine("***************");
                Console.Write("Enter an option: ");

                string input = Console.ReadLine();
                bool isValid = int.TryParse(input, out option);

                if (!isValid)
                {
                    Console.WriteLine("Invalid input. Press any key to try again...");
                    Console.ReadKey();
                    continue;
                }

                switch (option)
                {
                    case 1:
                        Console.WriteLine("You selected Products.");
                        break;
                    case 2:
                        Console.WriteLine("You selected Sales.");
                        break;
                    case 3:
                        Console.WriteLine("You selected Suppliers.");
                        break;
                    case 4:
                        Console.WriteLine("You selected Customers.");
                        break;
                    case 0:
                        Console.WriteLine("Exiting...");
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please select a number between 0 and 4.");
                        break;
                }

                if (option != 0)
                {
                    Console.WriteLine("Press any key to return to the menu...");
                    Console.ReadKey();
                }

            } while (option != 0);


            */



            /*var layout = new Layout("Root")
            .SplitRows(
                new Layout("Top"),
                new Layout("Bottom"));


            layout["Top"].Update(
                new Panel(
                new Grid()
                .AddColumn()
                .AddColumn()
                .AddColumn()
                .AddColumn()
                .AddRow("[blue]Customer ID:[/]", "VNET", "[blue]Customer name:[/]", "Arnulfo Leon")
                .AddRow("[blue]Ship Via:[/]", "3", "[blue]Shipper:[/]", "Aliexpress")
                )
                .Header("Data").Padding(2,2)
                );



            layout["Bottom"].Update(
                new Panel(
                new Table()
                .AddColumn("ID")
                .AddColumn("Product")
                .AddColumn("Unit Price")
                .AddColumn("Quantity")
                .AddColumn("Discount")
                .AddColumn("Total")

                .AddRow("1", "Coca cola", "15", "10", "10%", "100")

                )
                .Header("Products").Padding(2, 2)
                )
                ;

            AnsiConsole.Write(layout);*/
            #endregion



            int orderId = 10248; // Por ejemplo

            // 1. Obtener la orden
            Order order = new Order(orderId);

            // 2. Obtener detalles de productos de la orden
            var orderDetails = OrderDetail.GetAllFromOrder(orderId);


            // 3. Crear el layout
            var layout = new Layout("Root")
                .SplitRows(
                    new Layout("Top").Ratio(3),
                    new Layout("Bottom").Ratio(7)
                );

            // 4. Panel superior con información general
            layout["Top"].Update(
                new Panel(
                    new Grid()
                    .AddColumn()
                    .AddColumn()
                    .AddColumn()
                    .AddColumn()
                    .AddRow("[blue]Customer ID:[/]", order.Customer?.CustomerID ?? "N/A", "[blue]Customer name:[/]", order.Customer?.CompanyName ?? "N/A")
                    .AddRow("[blue]Ship Via:[/]", order.ShipVia?.ShipperId.ToString() ?? "N/A", "[blue]Shipper:[/]", order.ShipVia?.CompanyName ?? "N/A")
                ).Header("Data").Padding(1, 1)
            );

            // 5. Tabla inferior con productos
            var table = new Table()
                .AddColumn("ID")
                .AddColumn("Product")
                .AddColumn("Unit Price")
                .AddColumn("Quantity")
                .AddColumn("Discount")
                .AddColumn("Total");
            decimal TotalInOrder = 0;
            foreach (var detail in orderDetails)
            {
                var product = detail.Product;
                var unitPrice = detail.UnitPrice;
                var quantity = detail.Quantity;
                var discount = detail.Discount;
                var total = unitPrice * quantity * (1 - (decimal)discount);
                TotalInOrder += total;

                table.AddRow(
                    product.ProductId.ToString(),
                    product.ProductName,
                    unitPrice.ToString("C"),
                    quantity.ToString(),
                    (discount * 100).ToString("0") + "%",
                    total.ToString("C")
                );



            }

            table.Caption($"[yellow]Total: {TotalInOrder}[/]").RightAligned();

            layout["Bottom"].Update(
                new Panel(table).Header("Products").Padding(1, 1)
            );

            AnsiConsole.Write(layout);


        }


        /*private static Table CreateTable()
        {
            var simple = new Table()
                .Border(TableBorder.Square)
                .BorderColor(Color.Red)
                .AddColumn(new TableColumn("[u]CDE[/]").Footer("EDC").Centered())
                .AddColumn(new TableColumn("[u]FED[/]").Footer("DEF"))
                .AddColumn(new TableColumn("[u]IHG[/]").Footer("GHI"))
                .AddRow("Hello", "[red]World![/]", "")
                .AddRow("[blue]Bonjour[/]", "[white]le[/]", "[red]monde![/]")
                .AddRow("[blue]Hej[/]", "[yellow]Världen![/]", "");

            var second = new Table()
                .Border(TableBorder.Rounded)
                .BorderColor(Color.Green)
                .AddColumn(new TableColumn("[u]Foo[/]"))
                .AddColumn(new TableColumn("[u]Bar[/]"))
                .AddColumn(new TableColumn("[u]Baz[/]"))
                .AddRow("Hello", "[red]World![/]", "")
                .AddRow(simple, new Text("Whaaat"), new Text("Lolz"))
                .AddRow("[blue]Hej[/]", "[yellow]Världen![/]", "");

            return new Table()
                .Centered()
                .Border(TableBorder.DoubleEdge)
                .Title("TABLE [yellow]TITLE[/]")
                .Caption("TABLE [yellow]CAPTION[/]")
                .AddColumn(new TableColumn(new Panel("[u]ABC[/]").BorderColor(Color.Red)).Footer("[u]FOOTER 1[/]"))
                .AddColumn(new TableColumn(new Panel("[u]DEF[/]").BorderColor(Color.Green)).Footer("[u]FOOTER 2[/]"))
                .AddColumn(new TableColumn(new Panel("[u]GHI[/]").BorderColor(Color.Blue)).Footer("[u]FOOTER 3[/]"))
                .AddRow(new Text("Hello").Centered(), new Markup("[red]World![/]"), Text.Empty)
                .AddRow(second, new Text("Whaaat"), new Text("Lol"))
                .AddRow(new Markup("[blue]Hej[/]").Centered(), new Markup("[yellow]Världen![/]"), Text.Empty);
        }*/


    }
}
