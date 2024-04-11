using AutoMapper;
using Library.DataAccess;
using Library.Models;

namespace Library.Services.BaseServices
{
    public class BaseEditService<T> where T : class, new()
    {
        private readonly IRepository<T> _repository;

        public BaseEditService(IRepository<T> repository)
        {
            _repository = repository;
        }

        public virtual int Add<TAVm>(TAVm vm)
        {
            var mapper = new Mapper(new MapperConfiguration(x =>
            {
                x.CreateMap<T, TAVm>().ReverseMap();
            }));

            T val = mapper.Map<T>(vm);
            return _repository.Add(val);
        }

        public virtual void Change<TAVm>(TAVm vm) where TAVm : IEntity
        {
            var entity = _repository.Find(vm.Id);

            var mapper = new Mapper(new MapperConfiguration(x =>
            {
                x.CreateMap<T, TAVm>().ReverseMap();
            }));

            mapper.Map(vm, entity);

            _repository.Update(entity);
        }

    }
}