//See https://aka.ms/new-console-template for more information on the new simple console template.

using ConsoleTables;
using GuitarShop;
using Microsoft.EntityFrameworkCore;

Console.WriteLine("GUITAR STORE");
Console.WriteLine();


var dirksId = new Guid("9e105ebb-5e0e-450f-8a37-f664d40be1c1");
var stratId = new Guid("85b25108-1423-4f63-ad8e-7b823d5f8344");
using var db = new StoreContext();


//initialize the data
var dirk = db.Musicians.Include(x => x.Guitars).FirstOrDefault(x => x.Id == dirksId);
var strat = db.Guitars.FirstOrDefault(x => x.Id == stratId);

if (strat == null)
{
    strat = new Guitar(stratId, "Fender", "Stratocaster", "Black", 1000);
    db.Guitars.Add(strat);
}

if (dirk == null)
{
    dirk = new Musician(dirksId, "Dirk Watkins");
    db.Musicians.Add(dirk);
    dirk.Guitars.Add(strat);
}

db.SaveChanges();

Console.WriteLine($"GUITARS FOR: {dirk.Name}");
foreach (var guitar in dirk.Guitars)
{
    Console.WriteLine($" - {guitar.Make} {guitar.Model} {guitar.Color} {guitar.Price:c0}");
}


//Let's update the data so we can show some history in the temporal table logs
//separated by a second so we differentiate them in the db

//strat.Color = "Blue Crayon";
//db.SaveChanges();

//Task.Delay(1000);
//strat.Color = "Red Sharpie";
//db.SaveChanges();

//Task.Delay(1000);
//strat.Color = "Blue Splash";
//db.SaveChanges();

////the data will be updated to the latest 
//Console.WriteLine();
//Console.WriteLine("AFTER UPDATES:");

//foreach (var guitar in dirk.Guitars)
//{
//    Console.WriteLine($" - {guitar.Make} {guitar.Model} {guitar.Color} {guitar.Price:c0}");
//}


//now we can show some history!
//note: if you run this a bunch of times you'll see a bunch of logs!
Console.WriteLine();
Console.WriteLine("GUITAR UPDATE HISTORY:");

//project to an anonymous type so we can have the valid from/to shadow properties
var guitarSnapshots = await db.Guitars
    .TemporalAll()
    .OrderByDescending(x => EF.Property<DateTime>(x, "ValidFrom"))
    .Where(x => x.Id == stratId)
    .Select(x => new
    {
        ValidFrom = EF.Property<DateTime>(x, "ValidFrom"),
        ValidTo = EF.Property<DateTime>(x, "ValidTo"),
        x.Make,
        x.Model,
        x.Color,
        x.Price,
    })
    .ToListAsync();

//write out a table with this fancy nuget package
ConsoleTable.From(guitarSnapshots).Write(Format.Minimal);


//Pause the program
Console.WriteLine();
Console.WriteLine("DONE");
Console.ReadKey();