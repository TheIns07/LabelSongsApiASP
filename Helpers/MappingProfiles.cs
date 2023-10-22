using AutoMapper;
using LabelSongsAPI.DTO;
using LabelSongsAPI.Models;
using System.Diagnostics.Metrics;

namespace LabelSongsAPI.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Song, SongDTO>();
            CreateMap<SongDTO, Song>();

            CreateMap<Category, CategoryDTO>();
            CreateMap<CategoryDTO, Category>();

            CreateMap<Label, LabelDTO>();
            CreateMap<LabelDTO, Label>();

            CreateMap<Composer, ComposerDTO>();
            CreateMap<ComposerDTO, Composer>();

            CreateMap<Review, ReviewDTO>();
            CreateMap<ReviewDTO, Review>();

            CreateMap<Reviewer, ReviewerDTO>();
            CreateMap<ReviewerDTO, Reviewer>();
 

        }
    }
}
