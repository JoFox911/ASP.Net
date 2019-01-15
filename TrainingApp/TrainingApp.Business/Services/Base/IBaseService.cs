using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingApp.Data.DTO.Base;
using TrainingApp.Data.Base.Models;

namespace TrainingApp.Business.Services.Base
{
    public interface IBaseService<TListDTO, TDetailDTO, TModel> : IBaseService<TDetailDTO, TModel>
        where TListDTO : BaseDTO //BaseDTO
        where TDetailDTO : BaseDTO //BaseDTO
        where TModel : BaseModel //BaseEntity
    {
        IDTOService<TDetailDTO> DetailService { get; }
        IDTOService<TListDTO> ListService { get; set; }

        IQueryable<TListDTO> GetListDTO(IDictionary<string, string> paramList, params object[] paramArray);
        IQueryable<TListDTO> GetListDTO(params object[] paramArray);

        IQueryable<TDetailDTO> GetDetailDTO(IDictionary<string, string> paramList, params object[] paramArray);
        IQueryable<TDetailDTO> GetDetailDTO(params object[] paramArray);
    }


    public interface IBaseService<TDTO, TModel> : IModelService<TModel>
        where TDTO : BaseDTO //BaseDTO
        where TModel : BaseModel // BaseEntity
    {
        IDTOService<TDTO> DTOService { get; set; }

        IQueryable<TDTO> GetDTO(IDictionary<string, string> paramList, params object[] paramArray);
        IQueryable<TDTO> GetDTO(params object[] paramArray);

        Task<Guid> SaveAsync(TDTO dto, bool transacted = false);
        Task<Guid> AttachAsync(TDTO dto);
    }
}
