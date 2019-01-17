using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingApp.Data.Repositories;
using TrainingApp.Data.Base.Models;

namespace TrainingApp.Business.Services.Base
{
    public interface IModelService<TModel> where TModel : BaseModel//BaseEntity
    {
        ModelRepository<TModel> modelRepository { get; set; }

        IQueryable<TModel> GetModel();

        TModel Disable(Guid id, bool transacted = false);
        TModel Disable(TModel entity, bool transacted = false);

        TModel Remove(Guid id, bool transacted = false);
        TModel Remove(TModel entity, bool transacted = false);

        void RemoveList(IList<Guid> ids, bool transacted = false);

        Task<Guid> SaveAsync(TModel entity, bool transacted = false);
        Task<Guid> AttachAsync(TModel entity);

        Task SaveAsync();
    }
}
