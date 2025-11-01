using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace DataBaseManager.Extensions
{
    public static class QueryableProjectionExtensions
    {
        private static AutoMapper.IConfigurationProvider? _config;

        public static void Initialize(IMapper mapper)
        {
            _config = mapper.ConfigurationProvider;
        }
        public static IQueryable<TDestination> Project<TDestination>(this IQueryable source)
        {
            if (_config == null)
                throw new InvalidOperationException("AutoMapper is not initialized. Call QueryableProjectionExtensions.Initialize(mapper) once at startup.");

            return source.ProjectTo<TDestination>(_config);
        }
    }
}
