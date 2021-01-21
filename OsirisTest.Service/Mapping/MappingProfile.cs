using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using OsirisTest.Contracts.ResponseModels;
using OsirisTest.Utilities.DataAccess.Models;

namespace OsirisTest.Service.Consumer.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<Customer, Data.Customer>().ForMember(d => d.InsertedDateTime, o => o.Ignore())
                                                .ReverseMap();
            
            CreateMap<CustomerResponse, Customer>().ReverseMap();

            CreateMap<Wager, Data.Wager>().ForMember(d => d.InsertedDateTime, o => o.Ignore())
                                          .ReverseMap();
        }
    }
}
