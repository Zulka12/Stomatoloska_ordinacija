using System.Globalization;
class StomatoloskaOrdinacija
{
    private Dictionary<string, double> usluge;
    private List<Termin> termini;
    public StomatoloskaOrdinacija()
    {
        usluge = new Dictionary<string, double>
        {
            { "preventivni pregled", 30.00 },
            { "punjenje zuba", 50.00 },
            { "vadjenje zuba", 70.00 },
            { "izbjeljivanje zuba", 100.00 }
        };
        termini = new List<Termin>();
    }
    // Dodavanje novih usluga
    public void DodajUslugu(string naziv, double cijena)
    {
        if (!usluge.ContainsKey(naziv))
        {
            usluge[naziv] = cijena;
            Console.WriteLine($"Usluga '{naziv}' je uspješno dodata sa cijenom {cijena.ToString("F2", CultureInfo.InvariantCulture)} KM.");
        }
        else
        {
            Console.WriteLine($"Usluga '{naziv}' već postoji u ponudi.");
        }
    }
    // Ispis svih usluga
    public void IspisiUsluge()
    {
        Console.WriteLine("\nPonuda usluga i cijena:");
        foreach (var usluga in usluge)
        {
            Console.WriteLine($"{usluga.Key} - {usluga.Value.ToString("F2", CultureInfo.InvariantCulture)} KM");
        }
    }
    // Zakazivanje termina
    public void ZakaziTermin(string imePacijenta, string usluga, DateTime datumVrijeme)
    {
        if (!usluge.ContainsKey(usluga))
        {
            Console.WriteLine($"Usluga '{usluga}' nije pronađena.");
            return;
        }
        termini.Add(new Termin(imePacijenta, usluga, datumVrijeme));
        Console.WriteLine($"Termin za uslugu '{usluga}' je zakazan za {imePacijenta} na datum {datumVrijeme}.");
    }
    // Pregled termina
    public void PregledajTermine()
    {
        if (termini.Count == 0)
        {
            Console.WriteLine("Nema zakazanih termina.");
        }
        else
        {
            Console.WriteLine("\nZakazani termini:");
            foreach (var termin in termini)
            {
                Console.WriteLine($"Pacijent: {termin.ImePacijenta}, Usluga: {termin.Usluga}, Datum i vrijeme: {termin.DatumVrijeme}");
            }
        }
    }
    // Brisanje termina
    public void ObrisiTermin(string imePacijenta, string usluga)
    {
        Termin terminZaBrisanje = termini.Find(t => t.ImePacijenta == imePacijenta && t.Usluga == usluga);
        if (terminZaBrisanje != null)
        {
            termini.Remove(terminZaBrisanje);
            Console.WriteLine($"Termin za uslugu '{usluga}' za pacijenta '{imePacijenta}' je obrisan.");
        }
        else
        {
            Console.WriteLine($"Termin za uslugu '{usluga}' za pacijenta '{imePacijenta}' nije pronađen.");
        }
    }
    // Otkazivanje termina (za pacijente)
    public void OtkaziTermin(string imePacijenta, string usluga)
    {
        ObrisiTermin(imePacijenta, usluga);
    }
}
class Termin
{
    public string ImePacijenta { get; }
    public string Usluga { get; }
    public DateTime DatumVrijeme { get; }
    public Termin(string imePacijenta, string usluga, DateTime datumVrijeme)
    {
        ImePacijenta = imePacijenta;
        Usluga = usluga;
        DatumVrijeme = datumVrijeme;
    }
}
class Program
{
    static void Main()
    {
        StomatoloskaOrdinacija ordinacija = new StomatoloskaOrdinacija();
        while (true)
        {
            Console.WriteLine("\nDobrodošli u stomatološku ordinaciju!");
            Console.WriteLine("Odaberite svoju ulogu:");
            Console.WriteLine("1. Pacijent");
            Console.WriteLine("2. Osoblje");
            Console.WriteLine("0. Izlaz");
            int izbor = UnosIzbora();
            if (izbor == 0) break;
            switch (izbor)
            {
                case 1:
                    IzborPacijenta(ordinacija);
                    break;
                case 2:
                    IzborOsoblja(ordinacija);
                    break;
                default:
                    Console.WriteLine("Nepoznat izbor. Molimo pokušajte ponovo.");
                    break;
            }
        }
        Console.WriteLine("Hvala na korištenju stomatološke ordinacije. Doviđenja!");
    }
    static int UnosIzbora()
    {
        while (true)
        {
            try
            {
                return int.Parse(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.WriteLine("Pogrešan unos. Molimo unesite broj.");
            }
        }
    }
    static void IzborPacijenta(StomatoloskaOrdinacija ordinacija)
    {
        Console.WriteLine("\nOpcije za pacijenta:");
        Console.WriteLine("1. Pregled usluga i cena");
        Console.WriteLine("2. Zakazivanje termina");
        Console.WriteLine("3. Pregled zakazanih termina");
        Console.WriteLine("4. Otkazivanje termina");
        Console.WriteLine("0. Povratak na glavni meni");
        int izbor = UnosIzbora();
        switch (izbor)
        {
            case 1:
                ordinacija.IspisiUsluge();
                break;
            case 2:
                ZakaziTerminPacijenta(ordinacija);
                break;
            case 3:
                ordinacija.PregledajTermine();
                break;
            case 4:
                OtkaziTerminPacijenta(ordinacija);
                break;
            case 0:
                break;
            default:
                Console.WriteLine("Nepoznat izbor.");
                break;
        }
    }
    static void IzborOsoblja(StomatoloskaOrdinacija ordinacija)
    {
        Console.WriteLine("\nOpcije za osoblje:");
        Console.WriteLine("1. Dodavanje novih usluga");
        Console.WriteLine("2. Pregled zakazanih termina");
        Console.WriteLine("3. Zakazivanje termina");
        Console.WriteLine("4. Brisanje termina");
        Console.WriteLine("0. Povratak na glavni meni");
        int izbor = UnosIzbora();
        switch (izbor)
        {
            case 1:
                DodajUslugu(ordinacija);
                break;
            case 2:
                ordinacija.PregledajTermine();
                break;
            case 3:
                ZakaziTerminOsoblja(ordinacija);
                break;
            case 4:
                ObrisiTerminOsoblja(ordinacija);
                break;
            case 0:
                break;
            default:
                Console.WriteLine("Nepoznat izbor.");
                break;
        }
    }
    static void ZakaziTerminPacijenta(StomatoloskaOrdinacija ordinacija)
    {
        Console.Write("Unesite svoje ime: ");
        string imePacijenta = Console.ReadLine();
        Console.Write("Unesite naziv usluge: ");
        string usluga = Console.ReadLine();
        Console.Write("Unesite datum termina (yyyy-MM-dd): ");
        DateTime datum = DateTime.Parse(Console.ReadLine());
        Console.Write("Unesite vrijeme termina (HH:mm): ");
        TimeSpan vrijeme = TimeSpan.Parse(Console.ReadLine());
        DateTime datumVrijeme = datum.Add(vrijeme);
        ordinacija.ZakaziTermin(imePacijenta, usluga, datumVrijeme);
    }
    static void OtkaziTerminPacijenta(StomatoloskaOrdinacija ordinacija)
    {
        Console.Write("Unesite svoje ime: ");
        string imePacijenta = Console.ReadLine();
        Console.Write("Unesite naziv usluge za otkazivanje: ");
        string usluga = Console.ReadLine();
        ordinacija.OtkaziTermin(imePacijenta, usluga);
    }
    static void DodajUslugu(StomatoloskaOrdinacija ordinacija)
    {
        Console.Write("Unesite naziv usluge: ");
        string naziv = Console.ReadLine();
        Console.Write("Unesite cijenu usluge: ");
        double cijena = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
        ordinacija.DodajUslugu(naziv, cijena);
    }
    static void ZakaziTerminOsoblja(StomatoloskaOrdinacija ordinacija)
    {
        ZakaziTerminPacijenta(ordinacija);
    }
    static void ObrisiTerminOsoblja(StomatoloskaOrdinacija ordinacija)
    {
        Console.Write("Unesite ime pacijenta: ");
        string imePacijenta = Console.ReadLine();
        Console.Write("Unesite naziv usluge: ");
        string usluga = Console.ReadLine();
        ordinacija.ObrisiTermin(imePacijenta, usluga);
    }
}
