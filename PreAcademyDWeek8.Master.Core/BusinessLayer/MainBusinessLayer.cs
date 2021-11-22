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


        public MainBusinessLayer(IRepositoryCorsi corsi)
        {
            corsiRepo = corsi;
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
            if (corsoEsistente == null)
            {
                return new Esito { Messaggio = "Nessun corso corrispondente al codice inserito", IsOk = false };
            }
            corsiRepo.Delete(corsoEsistente);
            return new Esito { Messaggio = "Corso eliminato correttamente", IsOk = true };
        }

        public List<Corso> GetAllCorsi()
        {
            return corsiRepo.GetAll();
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
    }
}
