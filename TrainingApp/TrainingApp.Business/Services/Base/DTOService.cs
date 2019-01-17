using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TrainingApp.Data.Repositories;

namespace TrainingApp.Business.Services.Base
{
    public class DTOService<TDTO> : IDTOService<TDTO> where TDTO : class
    {
        public DTORepository<TDTO> DTORepository { get; set; }
        //private readonly IQueryConditionsHelper _conditionsHelper;

        public DTOService(DTORepository<TDTO> dtoRepository/*, IQueryConditionsHelper conditionsHelper*/)
        {
            DTORepository = dtoRepository;
            //_conditionsHelper = conditionsHelper;
        }

        /*public IQueryable<TDTO> GetDTO(IDictionary<string, string> parameters, params object[] paramArray)
        {
            return DTORepository.GetDTO(GetConditionsStringFromParameters(parameters), paramArray);
        }*/

        public IQueryable<TDTO> GetDTO(params object[] paramArray)
        {
            return DTORepository.GetDTO("", paramArray);
        }

        /*public string GetConditionsStringFromParameters(IDictionary<string, string> parameters)
        {
            return _conditionsHelper.GetQueryConditionsString(typeof(TDTO), parameters);
        }*/
    }
}
