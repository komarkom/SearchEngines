﻿using AutoMapper;
using SearchEngines.Db.Entities;

namespace SearchEngines.Web.DTO
{
    public class SearchResponseProfile : Profile
    {
        public SearchResponseProfile()
        {
            CreateMap<SearchResponse, SearchResponseDto>()
                .ForMember(d => d.Data, opt => opt.MapFrom(src =>src.Data))
                .ForMember(d => d.HasError, opt => opt.MapFrom(src =>src.HasError))
                .ForMember(d => d.Error, opt => opt.MapFrom(src =>src.Error))
                .ForMember(d => d.SearchResults, opt => opt.MapFrom(src =>src.SearchResults))
                .ForAllOtherMembers(opt => opt.Ignore())
                ;
        }
    }
}