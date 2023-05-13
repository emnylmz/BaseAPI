using System;
using System.Text.Json.Serialization;

namespace BaseAPI.Core.DTOs
{
	public class CustomResponseDto<T>
	{
		public T Data { get; set; }

		//jsona dönüşürken ignore et
		[JsonIgnore]
		public int StatusCode { get; set; }

		public List<string> Errors { get; set; }

        //static factory method design pattern
		public static CustomResponseDto<T> Success(int statusCode,T data) {
			return new CustomResponseDto<T> { Data = data, StatusCode = statusCode, Errors = null };
		}

        public static CustomResponseDto<T> Success(int statusCode)
        {
            return new CustomResponseDto<T> {StatusCode = statusCode};
        }

        public static CustomResponseDto<T> Fail(int statusCode, List<string> errors)
        {
            return new CustomResponseDto<T> { StatusCode = statusCode ,Errors=errors};
        }

        public static CustomResponseDto<T> Fail(int statusCode, string error)
        {
            return new CustomResponseDto<T> { StatusCode = statusCode, Errors = new List<string>{error }};
        }
    }
}

