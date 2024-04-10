using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        GestisciPromemoria();
    }

    static void GestisciPromemoria()
    {
        List<(string titolo, string data, string note)> promemorie = new List<(string titolo, string data, string note)>();

        while (true)
        {
            Console.WriteLine("Benvenuto in Bosio Tasks\n\n\tCrea\tLista\n\nCosa vuoi fare?");
            string scelta = Console.ReadLine();

            if (scelta.ToLower() == "crea")
            {
                CreaPromemoria(promemorie);
            }
            else if (scelta.ToLower() == "lista")
            {
                VisualizzaPromemorie(promemorie);
            }
            else
            {
                Console.WriteLine("Scelta non valida.");
            }

            Console.WriteLine("Vuoi continuare (si/no)?");
            string continua = Console.ReadLine();
            if (continua.ToLower() != "si")
                break;
        }
    }

    static void CreaPromemoria(List<(string titolo, string data, string note)> promemorie)
    {
        Console.WriteLine("Inserire il titolo del promemoria");
        string titolo = Console.ReadLine();
        string data, note;
        bool valido;

        do
        {
            Console.WriteLine("Inserire la data del promemoria (gg/mm/yyyy)");
            data = Console.ReadLine();
            valido = InserisciData(data);
        } while (!valido);

        Console.WriteLine("Inserisci note del promemoria");
        note = Console.ReadLine();

        promemorie.Add((titolo, data, note));

        Console.WriteLine("Promemoria inserito con successo!");
    }

    static void VisualizzaPromemorie(List<(string titolo, string data, string note)> promemorie)
    {
        if (promemorie.Count == 0)
        {
            Console.WriteLine("Nessun promemoria presente.");
        }
        else
        {
            Console.WriteLine("Titolo\t\tData\t\tNote");

            foreach (var promemoria in promemorie)
            {
                // Calcola il numero di spazi vuoti per centrare "data" e "note" sopra il titolo
                int lunghezzaTitolo = promemoria.titolo.Length;
                int spaziVuotiData = (lunghezzaTitolo > 10) ? 1 : 10 - lunghezzaTitolo;
                int spaziVuotiNote = (lunghezzaTitolo > 10) ? 1 : 10 - lunghezzaTitolo;

                Console.WriteLine($"{promemoria.titolo}{new string('\t', spaziVuotiData)}{promemoria.data}{new string('\t', spaziVuotiNote)}{promemoria.note}");
            }
        }
    }

    static bool InserisciData(string data)
    {
        if (DateTime.TryParseExact(data, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime risultato))
        {
            if (risultato >= DateTime.Today)
            {
                Console.WriteLine("La data è valida");
                return true;
            }
            else
            {
                Console.WriteLine("Data non valida. Deve essere oggi o una data futura.");
                return false;
            }
        }
        else
        {
            Console.WriteLine("Formato data non valido. Deve essere gg/mm/yyyy.");
            return false;
        }
    }
}
