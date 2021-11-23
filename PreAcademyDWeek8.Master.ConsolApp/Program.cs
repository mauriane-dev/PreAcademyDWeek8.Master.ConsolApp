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

        private static readonly IBusinessLayer bl = new MainBusinessLayer(new RepositoryCorsiMock(), new RepositoryStudentiMock());
        //private static readonly IBusinessLayer bl = new MainBusinessLayer(new RepositoryCorsiADO(), new RepositoryStudentiADO());


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
                case 5:
                    VisualizzaElencoCompletoStudenti();
                    break;
                case 6:
                    InserisciNuovoStudente();
                    break;
                case 7:
                    ModificaMailStudente(); //solo email
                    break;
                case 8:
                    EliminaStudente();
                    break;
                case 9:
                    VisualizzaStudentiIscrittiAdUnCorso();
                    break;                
                case 0:
                    return false;
                default:
                    Console.WriteLine("Scelta errata. Riprova");
                    break;
            }
            return true;
        }

        #region Corsi
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
        #endregion Corsi

        #region Studenti
        private static void VisualizzaStudentiIscrittiAdUnCorso()
        {
            //Visualizza studenti iscritti ad un corso
            //Faccio vedere l'elenco dei corsi
            //Chiedo all'utente di inserire il codice di un corso
            //"parlo" con il bl, gli passo il codice corso) e mi faccio restituire l'elenco di studenti iscritti a quel corso
            //Se l'elenco è null=> significa che ho sbagliato codice del corso
            //Se l'elenco è vuoto (count==0) => nessun iscritto a quel corso
            //Se l'elenco non è vuoto=> scorro la lista e stampo gli studenti
            Console.WriteLine("Ecco l'elenco dei corsi:");
            VisualizzaCorsi();
            Console.WriteLine("Inserisci codice corso ");
            string codiceCorso = Console.ReadLine();

            List<Studente> listaStudentiIscrittiAlCorso = bl.GetStudentiByCorsoCodice(codiceCorso);
            if (listaStudentiIscrittiAlCorso == null)
            {
                Console.WriteLine("Codice Corso errato!");
            }
            if (listaStudentiIscrittiAlCorso.Count == 0)
            {
                Console.WriteLine("Nessuno studente iscritto a questo corso!");
            }
            else
            {
                foreach (var item in listaStudentiIscrittiAlCorso)
                {
                    Console.WriteLine(item);
                }
            }

        }

        private static void EliminaStudente()
        {
            VisualizzaElencoCompletoStudenti();
            Console.WriteLine("Quale studente vuoi eliminare? Inserisci l'id dello studente");
            int idStudenteDaEliminare;
            while (!int.TryParse(Console.ReadLine(), out idStudenteDaEliminare))
            {
                Console.WriteLine("Riprova. Formato non corretto");
            }
            Esito esito = bl.EliminaStudente(idStudenteDaEliminare);
            Console.WriteLine(esito.Messaggio);
        }

        private static void ModificaMailStudente()
        {
            VisualizzaElencoCompletoStudenti();
            Console.WriteLine("Per quale studente vuoi modificare l'email? Inserisci l'id dello studente");
            int idStudenteDaModificare = int.Parse(Console.ReadLine());
            Console.WriteLine("Inserisci la nuova email:");
            string nuovaEmail = Console.ReadLine();
            Esito esito = bl.ModificaMailStudente(idStudenteDaModificare, nuovaEmail);
            Console.WriteLine(esito.Messaggio);
        }

        private static void InserisciNuovoStudente()
        {
            //Chiedo le info per creare il nuovo studente
            Console.WriteLine("Inserisci nome");
            string nome = Console.ReadLine();
            Console.WriteLine("Inserisci cognome");
            string cognome = Console.ReadLine();
            Console.WriteLine("Inserisci email");
            string email = Console.ReadLine();
            Console.WriteLine("Inserisci dat di nascita (formato gg-mm-aaaa)");
            DateTime dataNascita = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Inserisci titolo studio");
            string titoloStudio = Console.ReadLine();
            VisualizzaCorsi();
            Console.WriteLine("Inserisci codice corso a cui è iscritto");
            string codiceCorso = Console.ReadLine();

            //lo creo
            Studente nuovoStudente = new Studente();
            nuovoStudente.Nome = nome;
            nuovoStudente.Cognome = cognome;
            nuovoStudente.DataNascita = dataNascita;
            nuovoStudente.Email = email;
            nuovoStudente.TitoloStudio = titoloStudio;
            nuovoStudente.CorsoCodice = codiceCorso;

            //lo passo al bl
            Esito esito = bl.InserisciNuovoStudente(nuovoStudente);
            Console.WriteLine(esito.Messaggio);
        }

        private static void VisualizzaElencoCompletoStudenti()
        {
            List<Studente> studenti = bl.GetAllStudenti();
            if (studenti.Count == 0)
            {
                Console.WriteLine("Nessuno Studente presente");
            }
            else
            {
                foreach (var item in studenti)
                {
                    Console.WriteLine(item);
                }
            }
        }
        #endregion Studenti
        private static int SchermoMenu()
        {
            Console.WriteLine("******************Menu*********************");
            //Funzionalità Corsi
            Console.WriteLine("\nFunzionalità Corsi");
            Console.WriteLine("1. Visualizza Corsi");
            Console.WriteLine("2. Inserire un nuovo Corso");
            Console.WriteLine("3. Modificare un Corso");
            Console.WriteLine("4. Elimina un Corso");
            //Funzionalità su Studenti
            Console.WriteLine("\nFunzionalità Studenti");
            Console.WriteLine("5. Visualizza l'elenco completo degli studenti");
            Console.WriteLine("6. Inserimento nuovo Studente");
            Console.WriteLine("7. Modifica mail di uno Studente");//per semplicità solo email
            Console.WriteLine("8. Elimina Studente");
            Console.WriteLine("9. Visualizza l'elenco degli studenti iscritti ad un corso");

            Console.WriteLine("\n0. Exit");
            Console.WriteLine("********************************************");

            int scelta;
            Console.WriteLine("Inserisci la tua scelta: ");
            while (!(int.TryParse(Console.ReadLine(), out scelta) /*&& scelta>=0 && scelta <= 4)*/)){
                Console.WriteLine("Scelta errata. Inserisci scelta corretta:");
            }
            return scelta;
        }
    }
}
