﻿using System;

namespace CASNApp.Core.Entities
{
	public class Message
	{
		public int Id { get; set; }
		public int MessageType { get; set; }
		public string MessageText { get; set; }
        public bool? IsActive { get; set; }
		public DateTime Created { get; set; }
		public DateTime? Updated { get; set; }
	}
}