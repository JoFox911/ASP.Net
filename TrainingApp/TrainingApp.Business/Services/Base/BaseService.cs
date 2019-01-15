using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingApp.Business.Repositories;
using TrainingApp.Data.CustomAutoMapper;
using TrainingApp.Data.DTO.Base;
using TrainingApp.Data.Base.Models;

namespace TrainingApp.Business.Services.Base
{
    public class BaseService<TListDTO, TDetailDTO, TModel> : BaseService<TDetailDTO, TModel>, IBaseService<TListDTO, TDetailDTO, TModel>
        where TListDTO : BaseDTO
        where TDetailDTO : BaseDTO
        where TModel : BaseModel
    {
        public IDTOService<TDetailDTO> DetailService => DTOService;
        public IDTOService<TListDTO> ListService { get; set; }

        public BaseService(IDTOService<TListDTO> listService,
                           IDTOService<TDetailDTO> detailService,
                           ModelRepository<TModel> modelRepository)
            : base(detailService, modelRepository)
        {
            ListService = listService;
        }

        public IQueryable<TListDTO> GetListDTO(IDictionary<string, string> paramList, params object[] paramArray)
        {
            return ListService.GetDTO(paramList, paramArray);
        }
        public IQueryable<TListDTO> GetListDTO(params object[] paramArray)
        {
            return ListService.GetDTO(null, paramArray);
        }

        public IQueryable<TDetailDTO> GetDetailDTO(IDictionary<string, string> paramList, params object[] paramArray)
        {
            return DetailService.GetDTO(paramList, paramArray);
        }
        public IQueryable<TDetailDTO> GetDetailDTO(params object[] paramArray)
        {
            return DetailService.GetDTO(null, paramArray);
        }
    }

    public class BaseService<TDTO, TModel> : ModelService<TModel>, IBaseService<TDTO, TModel>
        where TDTO : BaseDTO
        where TModel : BaseModel
    {
        private static IMapper _mapper = new MapperConfiguration(cfg => cfg.CreateMap<TDTO, TModel>().MapOnlyIfChanged()).CreateMapper();

        //static BaseService()
        //{
        //    //var dtoType = typeof(TDTO);
        //    //if (typeof(IMapped).IsAssignableFrom(dtoType))
        //    //{
        //    //    _mapper = (IMapper)dtoType.GetProperty("GetMapper", BindingFlags.Public | BindingFlags.Static).GetValue(null, null);
        //    //}
        //    //else
        //    //{
        //    //    _mapper = new MapperConfiguration(cfg => cfg.CreateMap<TDTO, TEntity>().MapOnlyIfChanged(EntityName)).CreateMapper();
        //    //}
        //    _mapper = new MapperConfiguration(cfg => cfg.CreateMap<TDTO, TModel>().MapOnlyIfChanged()).CreateMapper();
        //}

        public IDTOService<TDTO> DTOService { get; set; }

        public BaseService(IDTOService<TDTO> dtoService,
                           ModelRepository<TModel> entityRepository)
            : base(entityRepository)
        {
            DTOService = dtoService;
        }

        public IQueryable<TDTO> GetDTO(IDictionary<string, string> paramList, params object[] paramArray)
        {
            return DTOService.GetDTO(paramList, paramArray);
        }
        public IQueryable<TDTO> GetDTO(params object[] paramArray)
        {
            return DTOService.GetDTO(null, paramArray);
        }

        /// <summary>
        /// Map DTO to Entity and Add/Update.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<Guid> SaveAsync(TDTO dto, bool transacted = false)
        {
            var id = await AttachAsync(dto);

            if (!transacted)
                await modelRepository.SaveAsync();

            return id;
        }

        public async Task<Guid> AttachAsync(TDTO dto)
        {
            TModel model = null;
            
            if (dto.Id == Guid.Empty)
            {
                model = _mapper.Map<TModel>(dto);
                await modelRepository.AddAsync(model);
                dto.Id = model.Id;
            }
            else
            {
                model = modelRepository.GetModel().FirstOrDefault(x => x.Id == dto.Id);
                if (model == null)
                {
                    model = _mapper.Map<TModel>(dto);
                    await modelRepository.AddAsync(model);
                }
                else
                {
                    _mapper.Map(dto, model);
                    modelRepository.Update(model);
                }
            }
            return model.Id;
        }
    }
}
