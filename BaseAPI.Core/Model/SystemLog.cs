using System;
namespace BaseAPI.Core.Model
{
	public class SystemLog
	{
		public int Id { get; set; }
		public int? UserId { get; set; }
		public string ExceptionMessage { get; set; }
		public string ExceptionType { get; set; }
		public string ExceptionSource { get; set; }
		public string ExceptionUrl { get; set; }
		public DateTime Date { get; set; }
	}
}

