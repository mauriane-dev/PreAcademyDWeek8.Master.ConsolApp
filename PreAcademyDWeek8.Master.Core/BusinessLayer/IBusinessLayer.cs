using PreAcademyDWeek8.Master.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreAcademyDWeek8.Master.Core.BusinessLayer
{
    public interface IBusinessLayer
    {
        //Visualizza tutti i corsi
        public List<Corso> GetAllCorsi();
        public Esito AggiungiCorso(Corso c);
        public Esito ModificaCorso(string codice, string nuovoNome, string nuovaDescrizione);
        public Esito EliminaCorso(string codice);
        //public bool EsisteCorso(string codice);

        //Funzionalità Studenti
        public Esito InserisciNuovoStudente(Studente nuovoStudente);
        public List<Studente> GetStudentiByCorsoCodice(string codiceCorso);
        public Esito EliminaStudente(int idStudenteDaEliminare);
        public Esito ModificaMailStudente(int idStudenteDaModificare, string nuovaEmail);
        public List<Studente> GetAllStudenti();
    }
}
