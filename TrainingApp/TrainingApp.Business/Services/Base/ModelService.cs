using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingApp.Data.Repositories;
using AutoMapper;
using TrainingApp.Data.CustomAutoMapper;
using TrainingApp.Data.Base.Models;
using TrainingApp.Data.Enums;

namespace TrainingApp.Business.Services.Base
{
    public class ModelService<TModel> : IModelService<TModel> where TModel : BaseModel//BaseEntity
    {
        public ModelRepository<TModel> modelRepository { get; set; }

        private static readonly IMapper _mapper;

        static ModelService()
        {
            _mapper = new MapperConfiguration(cfg => cfg.CreateMap<TModel, TModel>().MapOnlyIfChanged()).CreateMapper();
        }

        public ModelService(ModelRepository<TModel> modelRepository)
        {
            this.modelRepository = modelRepository;
        }

        public IQueryable<TModel> GetModel()
        {
            return modelRepository.GetModel();
        }

        /// <summary>
        /// Add/Update Entity
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Guid> SaveAsync(TModel model, bool transacted = false)
        {
            await AttachAsync(model);
            if(!transacted)
                await modelRepository.SaveAsync();
            return model.Id;
        }

        /// <summary>
        /// Attach without saving
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Guid> AttachAsync(TModel model)
        {
            if (model.Id == Guid.Empty)
            {
                await modelRepository.AddAsync(model);
            }
            else
            {
                var modelUpdate = modelRepository.GetModel().FirstOrDefault(x => x.Id == model.Id);
                if (modelUpdate == null)
                {
                    await modelRepository.AddAsync(model);
                }
                else
                {
                    _mapper.Map(model, modelUpdate);
                    modelRepository.Update(modelUpdate);
                }
            }

            return model.Id;
        }

        /// <summary>
        /// Saves data for all repositories
        /// </summary>
        /// <returns></returns>
        public async Task SaveAsync()
        {
            await modelRepository.SaveAsync();
        }

        public TModel Disable(Guid id, bool transacted = false)
        {
            return Disable(modelRepository.GetModel().SingleOrDefault(x => x.Id == id && x.RecordState != RecordState.D), transacted);
        }

        public TModel Disable(TModel model, bool transacted = false)
        {
            model.RecordState = RecordState.D;
            modelRepository.Update(model);
            if (!transacted)
                modelRepository.Save();
            return model;
        }

        public TModel Remove(Guid id, bool transacted = false)
        {
            return Remove(modelRepository.GetModel().SingleOrDefault(x => x.Id == id && x.RecordState != RecordState.D), transacted);
        }

        public TModel Remove(TModel model, bool transacted = false)
        {
            modelRepository.Remove(model);
            if(!transacted)
                modelRepository.Save();
            return model;
        }

        public void RemoveList(IList<Guid> ids, bool transacted = false)
        {
            modelRepository.GetModel()
                .Where(x => x.RecordState != RecordState.D && ids.Contains(x.Id))
                .ToList()
                .ForEach(x => modelRepository.Remove(x));
            if (!transacted)
                modelRepository.Save();
        }
    }
}
