using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Project4
{
	class Program
	{
		static void Main(string[] args)
		{
			var suppliers = new[] {
				new {SN = 1, SName = "Smith", Status = 20, City = "London"},
				new {SN = 2, SName = "Jones", Status = 10, City = "Paris" },
				new {SN = 3, SName = "Blake", Status = 30, City = "Paris" },
				new {SN = 4, SName = "Clark", Status = 20, City = "London"},
				new {SN = 5, SName = "Adams", Status = 30, City = "Athens" },
			};
			var parts = new[] {
			 new {PN = 1, PName = "Nut", Color = "Red", Weight = 12, City = "London"},
			new {PN = 2, PName = "Bolt", Color = "Green", Weight = 17, City = "Paris"},
			new {PN = 3, PName = "Screw", Color = "Blue", Weight = 17, City = "Rome"},
			new {PN = 4, PName = "Screw", Color = "Red", Weight = 14, City = "London"},
			new {PN = 5, PName = "Cam", Color = "Blue", Weight = 12, City = "Paris"},
			new {PN = 6, PName = "Cog", Color = "Red", Weight = 19, City = "London"}
			};

			var shipment = new[] {
				new {SN = 1, PN = 1, Qty = 300},
				new {SN = 1, PN = 2, Qty = 200},
				new {SN = 1, PN = 3, Qty = 400},
				new {SN = 1, PN = 4, Qty = 200},
				new {SN = 1, PN = 5, Qty =100},
				new {SN = 1, PN = 6, Qty = 100},
				new {SN = 2, PN = 1, Qty = 300},
				new {SN = 2, PN = 2, Qty = 400},
				new {SN = 3, PN = 2, Qty =200},
				new {SN = 4, PN = 2, Qty = 200},
				new {SN = 4, PN = 4, Qty = 300},
				new {SN = 4, PN = 5, Qty =400}
			};

			DisplayArrays();
			Console.WriteLine();
			DisplayColorQuerry();
			DisplaySName();
			DisplayOrders();

			void DisplayOrders()
			{

				Console.WriteLine();
				Console.WriteLine("4. Query and display the orders for a particular supplier.\n  That is, if you type in a S# or SN,\n  your program displays a list of its corresponding PName \n  (part name) and  Qty (quantity).");
				Console.WriteLine();

				//string inputSN = Console.ReadLine();
				//int resultSN = Int32.Parse(inputSN);
				while (true)
				{

					try
					{

						Console.Write("Enter a SN to querry by: ");
						string inputSN = Console.ReadLine();
						int resultSN = Int32.Parse(inputSN);
						string tab = "	";
						Console.WriteLine();

						if (resultSN < 1 || resultSN > 5)
						{
							Console.WriteLine("Please enter a valid SN between 1 & 4\n");
							continue;
						}
						else if (resultSN == 5)
						{
							Console.WriteLine("Supplier 5 currently has 0 orders to be displayed \n");
						}

						var Pname_Qty = from ship in shipment select new { ship.PN, ship.Qty };
						//IEnumerable<int> partsQuerry = shipment.Where(PartNumber => String.Equals(PartNumber.SN, resultSN)).Select(pname => pname.PN).Concat(shipment.Select(pname => pname.Qty));
						var partsQuerry = shipment.Select(ship => new { ship.SN, ship.Qty, ship.PN }).Join(parts, ship => ship.PN, part => part.PN, (ship, part) => new { ship.SN, part.PName, ship.Qty });
						var tableJQuerry = partsQuerry.Where(PartNumber => int.Equals(PartNumber.SN, resultSN));
						Console.WriteLine("SN" + tab + "PName" + tab + "Qty" + tab);
						tableJQuerry.ToList().ForEach(s => Console.WriteLine(s.SN + tab + s.PName + tab + s.Qty));
						Console.ReadLine();
						break;

					}
					catch (FormatException)
					{
						Console.WriteLine("Error: please enter a valid integer number");
					}
					//int resultSN = Int32.Parse(inputSN);

					Console.ReadLine();
				}
			}

			void DisplaySName()
			{
				Console.WriteLine();
				Console.WriteLine("3. Query suppliers data and display only the suppliers names in ascending order.");
				Console.WriteLine();
				Console.WriteLine("Suppliers Names In ASC Order");


				IEnumerable<string> dataQuerry = suppliers.OrderBy(sup => sup.SName).Select(sup => sup.SName);
				foreach (var contents in dataQuerry)
				{
					Console.WriteLine(contents);

				}

				Console.ReadLine();
			}

			void DisplayColorQuerry()
			{
				Console.WriteLine("2. Prompt user for a Color input,\n and use that color to Query and display all cities \nthat a part with such a color is located in (show each city name once).");
				Console.WriteLine();
				Console.Write("Enter a color to querry by: ");
				String inputColor = Console.ReadLine();
				if (Regex.IsMatch(inputColor.ToString(), @"[A-Za-z]"))
				{
					Console.WriteLine("Your Color to Querry By is: " + inputColor);
					IEnumerable<string> colorQuerry = parts.Where(color => String.Equals(color.Color.ToLower(), inputColor.ToLower())).Select(city => city.City).Distinct();
					foreach (var contents in colorQuerry)
					{
						Console.WriteLine(contents);

					}
				}
				else
				{
					Console.WriteLine("Please enter a valid Color!");
				}
				Console.ReadLine();

			}


			void DisplayArrays()
			{
				Console.WriteLine("1.	Display the content of each of the arrays: suppliers, parts, and shipment.");
				Console.WriteLine("======================= suppliers ===============");
				Console.WriteLine();
				//IEnumerable<string> supplierName = suppliers.Select(sup => sup.SName);
				string tab = "	";
				Console.WriteLine("SN" + tab + "SName" + tab + "Status" + tab + "City");
				foreach (var contents in suppliers)
				{
					Console.WriteLine(contents.SN + tab + contents.SName + tab + contents.Status + tab + contents.City);
					
				}
				Console.WriteLine();
				Console.WriteLine("======================= parts ===============");
				Console.WriteLine();
				//IEnumerable<string> partName = parts.Select(sup => sup.PName);
				//string tab = "	";
				Console.WriteLine("PN" + tab + "PName" + tab + "Color" + tab + "Weight" + tab + "City");
				foreach (var contents in parts)
				{
					Console.WriteLine(contents.PN + tab + contents.PName + tab + contents.Color + tab + contents.Weight + tab + contents.City);

				}
				Console.WriteLine();
				Console.WriteLine("======================= shipment ===============");
				Console.WriteLine();
				//IEnumerable<string> partName = parts.Select(sup => sup.PName);
				//string tab = "	";
				Console.WriteLine("SN" + tab + "PN" + tab + "Qty" + tab );
				foreach (var contents in shipment)
				{
					Console.WriteLine(contents.SN + tab + contents.PN + tab + contents.Qty + tab );

				}
				
			}



		}

		
	}



}

