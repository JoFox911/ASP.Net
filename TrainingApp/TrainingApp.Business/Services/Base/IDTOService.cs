using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TrainingApp.Data.Repositories;

namespace TrainingApp.Business.Services.Base
{
    public interface IDTOService<TDTO> where TDTO : class
    {
        DTORepository<TDTO> DTORepository { get; set; }

        //string GetConditionsStringFromParameters(IDictionary<string, string> parameters);
        //IQueryable<TDTO> GetDTO(IDictionary<string, string> paramList, params object[] paramArray);
        IQueryable<TDTO> GetDTO(params object[] paramArray);
    }
}
