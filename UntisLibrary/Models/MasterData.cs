using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace UntisLibrary.Models
{
	public class MasterData
	{
		[JsonProperty(PropertyName = "timeStamp")]
		public long TimeStamp { get; set; }
		[JsonProperty(PropertyName = "absenceReasons")]
		public AbsenceReason[] AbsenceReasons { get; set; }
		[JsonProperty(PropertyName = "departments")]
		public Department[] Departments { get; set; }
		[JsonProperty(PropertyName = "eventReasons")]
		public EventReason[] EventReasons { get; set; }
		// eventReasonGroups []
		[JsonProperty(PropertyName = "holidays")]
		public Holiday[] Holidays { get; set; }
		[JsonProperty(PropertyName = "klassen")]
		public Class[] Classes { get; set; }
		[JsonProperty(PropertyName = "rooms")]
		public Room[] Rooms { get; set; }
		[JsonProperty(PropertyName = "subjects")]
		public Subject[] Subjects { get; set; }
		[JsonProperty(PropertyName = "teachers")]
		public Teacher[] Teachers { get; set; }
		[JsonProperty(PropertyName = "timeGrid")]
		public TimeGrid TimeGrid { get; set; }

		public object GetElement(ElementType type, uint id)
		{
			switch (type)
			{
				case ElementType.CLASS:
					return Classes.Where(c => c.ID == id).FirstOrDefault();
				case ElementType.ROOM:
					return Rooms.Where(r => r.ID == id).FirstOrDefault();
				case ElementType.SUBJECT:
					return Subjects.Where(s => s.ID == id).FirstOrDefault();
				case ElementType.TEACHER:
					return Teachers.Where(t => t.ID == id).FirstOrDefault();
				default:
					return null;
			}
		}

		public T GetElement<T>(uint id)
		{
			ElementType type;
			switch (typeof(T))
			{
				case Type t when t == typeof(Class):
					type = ElementType.CLASS;
					break;
				case Type t when t == typeof(Room):
					type = ElementType.ROOM;
					break;
				case Type t when t == typeof(Subject):
					type = ElementType.SUBJECT;
					break;
				case Type t when t == typeof(Teacher):
					type = ElementType.TEACHER;
					break;
				default:
					return default(T);
			}
			return (T)GetElement(type, id);
		}
	}
}
