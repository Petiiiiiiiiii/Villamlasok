using CA240212;

static void Beolvasas(List<Nap> lista) 
{
	try
	{
		StreamReader sr = new(@"..\..\..\src\villam.txt");
		while (!sr.EndOfStream) lista.Add(new Nap(sr.ReadLine()));
		sr.Close();
        Console.WriteLine("Sikeres fájl beolvasás!");
    }
	catch
	{
        Console.WriteLine("Hiba a fájl beolvasása során!");
    }
}
static (int nap, int ora) Feladat3(List<Nap> lista) 
{

    var ora = lista.SelectMany(x => x.OrakEsDbVillam).Where(x => x.Value == lista.SelectMany(x => x.OrakEsDbVillam.Values).Max()).First();
    var nap = lista.IndexOf(lista.Where(x => x.OrakEsDbVillam.Values.Contains(ora.Value)).First())+1;

    return (nap,ora.Key);
}
static int Feladat5(List<Nap> lista) 
{
	return lista.SelectMany(x => x.OrakEsDbVillam.Values).Where(x => x >= 1).ToList().Count();
}

static void Feladat4(List<Nap> lista) 
{
	var feladat4 = lista
		.SelectMany(x => x.OrakEsDbVillam, (peldany, villam) => new { Peldany = peldany, VillamDB = villam.Value, VillamOra = villam.Key })
		.Where(x => x.VillamDB >= 1)
		.DistinctBy(x => lista.IndexOf(x.Peldany))
		.Select(x => $"{lista.IndexOf(x.Peldany)+1}.nap {x.VillamOra}.ora")
		.ToList();

	for (int i = 0; i < feladat4.Count; i++)
	{
		if (!feladat4[i].StartsWith($"{i+1}."))
		{
			feladat4.Insert(i,$"{i+1}.nap null");
		}
	}

	try
	{
		StreamWriter sw = new(@"..\..\..\src\ujFile.txt");
		feladat4.ForEach(x => sw.WriteLine(x));
		sw.WriteLine("31.nap null");
        sw.Close();
        Console.WriteLine("\tSikeres fájlba való kiírás!");

    }
	catch
	{
        Console.WriteLine("\tHiba a fájlba való kiírás során!");
    }
}

static void Feladat6(List<Nap> lista) 
{
	int sorszama = lista.IndexOf(
			lista
			.SelectMany(x => x.OrakEsDbVillam, (p, i) => new { Peldany = p, VillamOra = i.Key, VillamDB = i.Value })
			.Select(x => x.Peldany)
			.Where(x => x.OrakEsDbVillam.Values.Sum() < 200)
			.First()
		)+1;


	Console.WriteLine($"\tAugusztus {sorszama}. napjan volt először 200-nál kevesebb villámlás!");
}

static int Feladat7(List<Nap> lista)
{
	try
	{
		int sorszama = lista.IndexOf(
				lista
                .SelectMany(x => x.OrakEsDbVillam, (p, i) => new { Peldany = p, VillamOra = i.Key, VillamDB = i.Value })
                .Select(x => x.Peldany)
                .Where(x => x.OrakEsDbVillam.Values.Sum() == 0)
                .First()
            )+1;
		return sorszama;
    }
	catch
	{
		return 32;
	}
}

static int Feladat8(List<Nap> lista)
{
	return lista
		.SelectMany(x => x.OrakEsDbVillam, (p, i) => new { Peldany = p, VillamOra = i.Key, VillamDB = i.Value })
		.Where(x => x.VillamOra == 1 || x.VillamOra == 2)
		.Select(x => x.VillamDB)
		.Where(x => x > 0)
		.Count();
}
static int Feladat9(List<Nap> lista) 
{
	return lista
		.SelectMany(x => x.OrakEsDbVillam, (p, i) => new { Peldany = p, VillamOra = i.Key, VillamDB = i.Value })
		.Where(x => lista.IndexOf(x.Peldany)+1 <= 20)
		.Select(x => x.VillamDB)
		.Sum();
}
static void Feladat10(List<Nap> lista) 
{
	var feladat10 =
		lista
		.SelectMany(x => x.OrakEsDbVillam, (p,i) => new {Peldany=p, VillamOra=i.Key, VillamDB=i.Value})
		.Where(x => x.VillamDB == lista.SelectMany(x => x.OrakEsDbVillam).Select(x => x.Value).Where(x => x >= 1).ToList().Min())
		.Select(x => $"{lista.IndexOf(x.Peldany)+1}.nap {x.VillamOra}.óra {x.VillamDB} db villám")
		.First();

    Console.WriteLine($"\t{feladat10}");
}
static void Feladat11(List<Nap> lista) 
{
	var feladat11 = lista
		.SelectMany(x => x.OrakEsDbVillam, (p, i) => new { Peldany = p, VillamOra = i.Key, VillamDB = i.Value })
		.Where(x => (lista.IndexOf(x.Peldany) + 1 == 7) && (x.VillamDB == x.Peldany.OrakEsDbVillam.Values.Max()))
		.Select(x => $"{lista.IndexOf(x.Peldany)+1}.nap {x.VillamOra}.órában villámlott a legtöbbet!")
		.First();

	Console.WriteLine($"\t{feladat11}");
}

//Main
List<Nap> napok = new();
Beolvasas(napok);

//2.feladat
Console.WriteLine("\n2.feladat:");
napok.ForEach(x => Console.WriteLine($"\t{x}"));

//3.feladat
Console.WriteLine("\n3.feladat:");
Console.WriteLine($"\t{Feladat3(napok).nap}.nap {Feladat3(napok).ora}.órájában villámlott a legtöbbet!");

//4.feladat
Console.WriteLine("\n4.feladat:");
Feladat4(napok);

//5.feladat
Console.WriteLine("\n5.feladat:");
Console.WriteLine($"\t{Feladat5(napok)} db olyan óra volt amikor villámlott!");

//6.feladat
Console.WriteLine("\n6.feladat:");
Feladat6(napok);

//7.feladat
Console.WriteLine("\n7.feladat:");
Console.WriteLine($"\tAugusztus {(Feladat7(napok) != 32 ? $"{Feladat7(napok)}.napján volt előszőr hogy eggyik órában sem villámlott!":"Augusztusban minden nap volt legalább 1 óra amikor legalább 1-szer villámlott!")}");

//8.feladat
Console.WriteLine("\n8.feladat:");
Console.WriteLine($"\t{Feladat8(napok)} db olyan óra volt, amikor villámlott éjfél után az első és második órában!");

//9.feladat
Console.WriteLine("\n9.feladat:");
Console.WriteLine($"\t{Feladat9(napok)} db villámlás volt Augusztusban!");

//10.feladat
Console.WriteLine("\n10.feladat:");
Feladat10(napok);

//11.feladat
Console.WriteLine("\n11.feladat:");
Feladat11(napok);

