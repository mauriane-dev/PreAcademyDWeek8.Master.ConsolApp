using PreAcademyDWeek8.Master.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreAcademyDWeek8.Master.Core.InterfaceRepositories
{
    public interface IRepositoryCorsi: IRepository<Corso>
    {
        public Corso GetByCode(string codice);
    }
}
