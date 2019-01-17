using System.Linq;
using System.Threading.Tasks;
using TrainingApp.Data.Contexts;
using TrainingApp.Data.Base.Models;

namespace TrainingApp.Data.Repositories
{
    public class ModelRepository<TModel> where TModel : BaseModel//CoreEntity
    {
        public readonly TrainingAppDbContext context;
        //public IUserIdentInfo UserInfo => context.UserInfo;

        public ModelRepository(TrainingAppDbContext context)
        {
            this.context = context;
        }

        public IQueryable<TModel> GetModel()
        {
            return context.Set<TModel>();
        }

        public async Task AddAsync(TModel model)
        {
            await context.AddAsync(model);
            //await Context.AddAsync_Auditable(entity);
        }

        public void Remove(TModel model)
        {
            context.Remove(model);
            //Context.Remove_Auditable(entity);
        }

        public async Task RemoveAsync(TModel model)
        {
            await Task.Run(() =>
            {
                Remove(model);
            });
        }

        public void Update(TModel model)
        {
            context.Update(model);
            //Context.Update_Auditable(entity);
        }

        public void Save()
        {
            context.SaveChanges();
            //Context.SaveChanges_Auditable();
        }

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
            //await Context.SaveChangesAsync_Auditable();
        }
    }
}
