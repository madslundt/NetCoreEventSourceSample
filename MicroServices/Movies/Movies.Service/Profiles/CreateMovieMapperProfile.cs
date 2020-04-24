using AutoMapper;
using Domain.Business.Movies;
using Movies.Service.VIewModels;

namespace Movies.Service.Profiles
{
    public class CreateMovieMapperProfile : Profile
    {
        public CreateMovieMapperProfile()
        {
            CreateMap<CreateMovieViewModel, MovieEntity>()
                .ConstructUsing(x => new MovieEntity(MovieId.New));

        }
    }
}
