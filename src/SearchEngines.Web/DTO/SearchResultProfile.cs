using AutoMapper;
using SearchEngines.Db.Entities;

namespace SearchEngines.Web.DTO
{
    public class SearchResultProfile : Profile
    {
        public SearchResultProfile()
        {
            CreateMap<SearchResult, SearchResultDto>()
                .ForMember(d => d.HeaderLinkText, opt => opt.MapFrom(src =>src.HeaderLinkText))
                .ForMember(d => d.Url, opt => opt.MapFrom(src =>src.Url))
                .ForMember(d => d.PreviewData, opt => opt.MapFrom(src =>src.PreviewData))
                .ForAllOtherMembers(opt => opt.Ignore())
                ;
        }
    }
}