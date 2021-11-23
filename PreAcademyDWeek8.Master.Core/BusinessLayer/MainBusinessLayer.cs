using PreAcademyDWeek8.Master.Core.Entities;
using PreAcademyDWeek8.Master.Core.InterfaceRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreAcademyDWeek8.Master.Core.BusinessLayer
{
    public class MainBusinessLayer : IBusinessLayer
    {
        private readonly IRepositoryCorsi corsiRepo;
        private readonly IRepositoryStudenti studentiRepo;


        public MainBusinessLayer(IRepositoryCorsi corsi, IRepositoryStudenti studenti)
        {
            corsiRepo = corsi;
            studentiRepo = studenti;
        }

        #region Funzionalità Corsi
        public List<Corso> GetAllCorsi()
        {
            return corsiRepo.GetAll();
        }
        public Esito AggiungiCorso(Corso c)
        {
            //non devi far inserire corsi con codici uguali
            Corso corsoEsistente= corsiRepo.GetByCode(c.CorsoCodice);
            if (corsoEsistente == null)
            {
                corsiRepo.Add(c);
                return new Esito { Messaggio = $"Corso aggiunto correttamente", IsOk = true } ;
            }
            return new Esito { Messaggio = $"Impossibile aggiungere il corso perché esiste già un corso con quel codice", IsOk = false };
        }

        public Esito EliminaCorso(string codice)
        {
            var corsoEsistente = corsiRepo.GetByCode(codice);
            if (corsoEsistente==null)
            {
                return new Esito { Messaggio = "Nessun corso corrispondente al codice inserito", IsOk = false };
            }
            corsiRepo.Delete(corsoEsistente);
            return new Esito { Messaggio = "Corso eliminato correttamente", IsOk = true };
        }
       
        public Esito ModificaCorso(string codice, string nuovoNome, string nuovaDescrizione)
        {
            var corsoEsistente = corsiRepo.GetByCode(codice);
            if (corsoEsistente == null)
            {
                return new Esito { Messaggio = "Nessun corso corrispondente al codice inserito", IsOk = false };
            }
            Corso corsoDaAggiornare = new Corso();
            corsoDaAggiornare.CorsoCodice = codice;
            corsoDaAggiornare.Nome = nuovoNome;
            corsoDaAggiornare.Descrizione = nuovaDescrizione;

            corsiRepo.Update(corsoDaAggiornare);
            return new Esito { Messaggio = "Corso aggiornato correttamente", IsOk = true };
        }
        #endregion Funzionalità Corsi

        #region Funzionalità Studenti
        public Esito EliminaStudente(int idStudenteDaEliminare)
        {
            var studenteEsistente = studentiRepo.GetById(idStudenteDaEliminare);
            if (studenteEsistente == null)
            {
                return new Esito { Messaggio = "Nessuno studente corrispondente all'id inserito", IsOk = false };
            }
            studentiRepo.Delete(studenteEsistente);
            return new Esito { Messaggio = "Studente eliminato correttamente", IsOk = true };
        }
        public List<Studente> GetAllStudenti()
        {
            return studentiRepo.GetAll();
        }

        public List<Studente> GetStudentiByCorsoCodice(string codiceCorso)
        {
            //controllo input
            //controllo se codice corso esiste. Se non esiste allora restituisco null
            //se il corso esiste, allora recupero dalla repo degli studenti la lista di quelli che hanno quel corsoCodice
            var corso = corsiRepo.GetByCode(codiceCorso);
            if (corso == null)
            {
                return null;
            }
            List<Studente> studentiFiltrati = new List<Studente>();
            foreach (var item in studentiRepo.GetAll())
            {
                if (item.CorsoCodice == codiceCorso)
                {
                    studentiFiltrati.Add(item);
                }
            }
            return studentiFiltrati;
            
        }

        public Esito InserisciNuovoStudente(Studente nuovoStudente)
        {
            //controllo input
            Corso corsoEsistente = corsiRepo.GetByCode(nuovoStudente.CorsoCodice);
            if (corsoEsistente == null)
            {
                return new Esito { Messaggio = "Codice corso errato", IsOk = false };
            }
            studentiRepo.Add(nuovoStudente);
            return new Esito { Messaggio = "studente inserito correttamente", IsOk = true };
        }
        public Esito ModificaMailStudente(int idStudenteDaModificare, string nuovaEmail)
        {
            //controllo input
            //controllo se id esiste
            var studente = studentiRepo.GetById(idStudenteDaModificare);
            if (studente == null)
            {
                return new Esito { Messaggio = "Id Studente errato o inesistente", IsOk = false };
            }
            studente.Email = nuovaEmail;
            studentiRepo.Update(studente);
            return new Esito { Messaggio = "Email Studente aggiornata correttamente", IsOk = true };
        }


        #endregion Funzionalità Studenti

    }
}
