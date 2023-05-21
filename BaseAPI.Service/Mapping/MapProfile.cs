using System;
using AutoMapper;
using BaseAPI.Core.DTOs;
using BaseAPI.Core.Model;

namespace BaseAPI.Service.Mapping
{
	public class MapProfile:Profile
	{
		public MapProfile()
		{
			//reverse map iki classında birbirine dönüşebilmesini sağlıyor
			CreateMap<UserDto,User>()
                .ReverseMap()
                .ForMember(d => d.FullName, d => d.MapFrom(x => string.Format("{0} {1}", x.Firstname, x.Lastname)));
		}
	}
}

