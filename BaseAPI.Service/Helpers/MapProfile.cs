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
			CreateMap<User, UserDto>().ReverseMap();
		}
	}
}

