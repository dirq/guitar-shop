//See https://aka.ms/new-console-template for more information on the new simple console template.

using GuitarShop;
using Microsoft.EntityFrameworkCore;

Console.WriteLine("GUITAR STORE");
Console.WriteLine();


var dirksId = new Guid("9e105ebb-5e0e-450f-8a37-f664d40be1c1"); 
var stratId = new Guid("85b25108-1423-4f63-ad8e-7b823d5f8344");

//using (var db = new StoreContext())
//{
//    var strat = new Guitar(stratId, "Fender", "Stratocaster", "Black", 1000);
//    db.Guitars.Add(strat);

//    var dirk = new Musician(dirksId, "Dirk Watkins");
//    dirk.Guitars.Add(strat);
//    db.Musicians.Add(dirk);

//    db.SaveChanges();
//}


using (var db = new StoreContext())
{
    var dirk = db.Musicians.Include(x => x.Guitars).FirstOrDefault(x => x.Id == dirksId);
    Console.WriteLine($"Guitars for {dirk.Name}");
    foreach (var guitar in dirk.Guitars)
    {
        Console.WriteLine($" - {guitar.Make} {guitar.Model} {guitar.Price:c0}");
    }
}

//Pause the program
Console.ReadKey();