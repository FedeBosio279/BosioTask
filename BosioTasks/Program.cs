using System;
using System.Collections.Generic;

class Program
{

    static void Main()
    {
        GestisciPromemoria();
    }

    static void GestisciPromemoria()
    {
        List<(string titolo, string data, string note)> promemorie = new List<(string titolo, string data, string note)>();

        while (true)
        {
            Console.WriteLine("Benvenuto in Bosio Tasks\n\n\tCrea\tModifica\tLista\n\nCosa vuoi fare?");
            string scelta = Console.ReadLine();
            string continua = "";

            if (scelta.ToLower() == "crea")
            {
                CreaPromemoria(promemorie);
            }
            else if (scelta.ToLower() == "lista")
            {
                VisualizzaPromemorie(promemorie);
            }
            else if (scelta.ToLower() == "modifica")
            {
                ModificaPromemoria(promemorie);
            }
            else
            {
                Console.WriteLine("Scelta non valida.");
            }

            do
            {
                Console.WriteLine("Vuoi continuare (si/no)?");
                continua = Console.ReadLine();
                if (continua.ToLower() == "no")
                {
                    break;
                }
            } while (continua != "si" && continua != "no");
        }
    }

    static void ModificaPromemoria(List<(string titolo, string data, string note)> promemorie)
    {
        if (promemorie.Count == 0)
        {
            Console.WriteLine("Nessun promemoria presente, vuoi crearne uno?");
            string scelta = Console.ReadLine();
            if (scelta == "si")
            {
                CreaPromemoria(promemorie);
            }
        }
        else
        {
            Console.WriteLine("Ecco l'elenco dei promemoria:");

            for (int i = 0; i < promemorie.Count; i++)
            {
                Console.WriteLine($"{i + 1}. Titolo: {promemorie[i].titolo}, Data: {promemorie[i].data}, Note: {promemorie[i].note}");
            }

            Console.WriteLine("Inserisci il numero del promemoria che vuoi modificare:");
            int indicePromemoria = Convert.ToInt32(Console.ReadLine()) - 1;

            if (indicePromemoria < 0 || indicePromemoria >= promemorie.Count)
            {
                Console.WriteLine("Indice non valido.");

            }
            else
            {
                Console.WriteLine("Vuoi modificare il titolo? (si/no)");
                string scelta = Console.ReadLine().ToLower();

                if (scelta == "si")
                {
                    Console.WriteLine("Inserisci il nuovo titolo:");
                    promemorie[indicePromemoria] = (Console.ReadLine(), promemorie[indicePromemoria].data, promemorie[indicePromemoria].note);
                }

                Console.WriteLine("Vuoi modificare la data? (si/no)");
                scelta = Console.ReadLine().ToLower();

                if (scelta == "si")
                {
                    Console.WriteLine("Inserisci la nuova data:");
                    promemorie[indicePromemoria] = (promemorie[indicePromemoria].titolo, Console.ReadLine(), promemorie[indicePromemoria].note);
                }

                Console.WriteLine("Vuoi modificare le note? (si/no)");
                scelta = Console.ReadLine().ToLower();

                if (scelta == "si")
                {
                    Console.WriteLine("Inserisci le nuove note:");
                    promemorie[indicePromemoria] = (promemorie[indicePromemoria].titolo, promemorie[indicePromemoria].data, Console.ReadLine());
                }

                Console.WriteLine("Modifica completata.");
            }
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

            // Trova la lunghezza massima per ogni colonna
            int maxLunghezzaTitolo = promemorie.Max(p => p.titolo.Length);
            int maxLunghezzaData = promemorie.Max(p => p.data.Length);
            int maxLunghezzaNote = promemorie.Max(p => p.note.Length);

            foreach (var promemoria in promemorie)
            {
                // Calcola gli spazi bianchi necessari per centrare ogni valore sotto il titolo della colonna
                int spaziPrimaTitolo = (maxLunghezzaTitolo - promemoria.titolo.Length) / 2;
                int spaziDopoTitolo = maxLunghezzaTitolo - promemoria.titolo.Length - spaziPrimaTitolo;

                int spaziPrimaData = (maxLunghezzaData - promemoria.data.Length) / 2;
                int spaziDopoData = maxLunghezzaData - promemoria.data.Length - spaziPrimaData;

                int spaziPrimaNote = (maxLunghezzaNote - promemoria.note.Length) / 2;
                int spaziDopoNote = maxLunghezzaNote - promemoria.note.Length - spaziPrimaNote;

                // Stampa ogni valore con spazi vuoti per centrare sotto il titolo della colonna
                Console.WriteLine($"{new string(' ', spaziPrimaTitolo)}{promemoria.titolo}{new string(' ', spaziDopoTitolo)}\t" +
                                  $"{new string(' ', spaziPrimaData)}{promemoria.data}{new string(' ', spaziDopoData)}\t" +
                                  $"{new string(' ', spaziPrimaNote)}{promemoria.note}{new string(' ', spaziDopoNote)}");
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
