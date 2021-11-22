using PreAcademyDWeek8.Master.Core.BusinessLayer;
using PreAcademyDWeek8.Master.Core.Entities;
using PreAcademyDWeek8.Master.RepositoryADO;
using PreAcademyDWeek8.Master.RepositoryMock;
using System;
using System.Collections.Generic;

namespace PreAcademyDWeek8.Master.ConsolApp
{
    class Program
    {

        //private static readonly IBusinessLayer bl = new MainBusinessLayer(new RepositoryCorsiMock());
        private static readonly IBusinessLayer bl = new MainBusinessLayer(new RepositoryCorsiADO());


        static void Main(string[] args)
        {
            Console.WriteLine("Benvenuto al Master!");

            bool continua = true;

            while (continua)
            {
                int scelta = SchermoMenu();
                continua = AnalizzaScelta(scelta);
            }
        }

        private static bool AnalizzaScelta(int scelta)
        {
            switch (scelta)
            {
                case 1:
                    VisualizzaCorsi();
                    break;
                case 2:
                    InserisciCorso();
                    break;
                case 3:
                    ModificaCorso();
                    break;
                case 4:
                    EliminaCorso();
                    break;
                case 0:
                    return false;
            }
            return true;
        }

        private static void EliminaCorso()
        {
            Console.WriteLine("Ecco l'elenco dei corsi disponibili");
            VisualizzaCorsi();
            Console.WriteLine("Quale corso vuoi eliminare? Inserisci il codice");
            string codice = Console.ReadLine();
            Esito e= bl.EliminaCorso(codice);
            Console.WriteLine(e.Messaggio);

        }

        private static void ModificaCorso()
        {
            Console.WriteLine("Ecco l'elenco dei corsi disponibili");
            VisualizzaCorsi();
            Console.WriteLine("Quale corso vuoi modificare? Inserisci il codice");
            string codice = Console.ReadLine();
            Console.WriteLine("Inserisci il nuovo nome del corso");
            string nuovoNome = Console.ReadLine();
            Console.WriteLine("Inserisci la nuova descrizione del corso");
            string nuovaDescrizione = Console.ReadLine();

            Esito esito = bl.ModificaCorso(codice, nuovoNome, nuovaDescrizione);
            Console.WriteLine(esito.Messaggio);


        }

        private static void InserisciCorso()
        {
            //chiedo all'utente i dati per "creare" il nuovoCorso
            Console.WriteLine("Inserisci il codice del nuovo corso");
            string codice = Console.ReadLine();
            Console.WriteLine("Inserisci il nome del nuovo corso");
            string nome = Console.ReadLine();
            Console.WriteLine("Inserisci la descrizione del nuovo corso");
            string descrizione = Console.ReadLine();

            Corso nuovoCorso = new Corso();
            nuovoCorso.CorsoCodice = codice;
            nuovoCorso.Nome = nome;
            nuovoCorso.Descrizione = descrizione;

            Esito esito=bl.AggiungiCorso(nuovoCorso);
            Console.WriteLine(esito.Messaggio);

        }

        private static void VisualizzaCorsi()
        {
            List<Corso> corsi=bl.GetAllCorsi();
            if (corsi.Count == 0)
            {
                Console.WriteLine("Lista vuota");
            }
            else
            {
                foreach (var item in corsi)
                {
                    Console.WriteLine(item);
                }
            }
        }

        private static int SchermoMenu()
        {
            Console.WriteLine("******************Menu*********************");
            //Funzionalità Corsi
            Console.WriteLine("\nFunzionalità Corsi");
            Console.WriteLine("1. Visualizza Corsi");
            Console.WriteLine("2. Inserire un nuovo Corso");
            Console.WriteLine("3. Modificare un Corso");
            Console.WriteLine("4. Elimina un Corso");


            Console.WriteLine("\n0. Exit");
            Console.WriteLine("********************************************");

            int scelta;
            Console.WriteLine("Inserisci la tua scelta: ");
            while (!(int.TryParse(Console.ReadLine(), out scelta) && scelta>=0 && scelta <= 4)){
                Console.WriteLine("Scelta errata. Inserisci scelta corretta:");
            }
            return scelta;
        }
    }
}
