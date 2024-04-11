using AutoMapper;
using Library.DataAccess;

namespace Library.Services.BaseServices
{
    public class BaseReadService<T> where T : class, new()
    {
        private readonly IRepository<T> _repository;

        public BaseReadService(IRepository<T> repository)
        {
            _repository = repository;
        }

        public virtual TAVm GetEditVm<TAVm>(int id)
        {
            var entity = _repository.Find(id);

            var mapper = new Mapper(new MapperConfiguration(x =>
            {
                x.CreateMap<T, TAVm>().ReverseMap();
            }));

            return mapper.Map<TAVm>(entity);
        }
    }
}