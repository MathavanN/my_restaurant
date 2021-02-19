using AutoMapper;

namespace MyRestaurant.Business.Tests
{
    class AutoMapperSingleton
    {
        private static IMapper _mapper;
        public static IMapper Mapper
        {
            get
            {
                if (_mapper == null)
                {
                    var configuration = new MapperConfiguration(cfg =>
                    {
                        cfg.AddProfile(new AutoMapping.AutoMapping());
                    });
                    _mapper = configuration.CreateMapper();
                }
                return _mapper;
            }
        }
    }
}
