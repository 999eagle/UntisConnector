using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

using UntisLibrary.Converters;

namespace UntisLibrary.Models
{
	public class Period
	{
		[JsonProperty(PropertyName = "id")]
		public uint Id { get; set; }
		[JsonProperty(PropertyName = "lessonId")]
		public uint LessonId { get; set; }
		[JsonProperty(PropertyName = "lessonNumber")]
		public uint LessonNumber { get; set; }
		[JsonProperty(PropertyName = "lessonCode")]
		[JsonConverter(typeof(StringEnumConverter))]
		public PeriodLessonCode LessonCode { get; set; }
		[JsonProperty(PropertyName = "lessonText")]
		public string LessonText { get; set; }
		[JsonProperty(PropertyName = "periodText")]
		public string PeriodText { get; set; }
		[JsonProperty(PropertyName = "hasPeriodText")]
		public bool HasPeriodText { get; set; }
		[JsonProperty(PropertyName = "date")]
		[JsonConverter(typeof(IntegerDateConverter))]
		public DateTime Date { get; set; }
		[JsonProperty(PropertyName = "startTime")]
		[JsonConverter(typeof(TimeSpanConverter), true)]
		public TimeSpan StartTime { get; set; }
		[JsonProperty(PropertyName = "endTime")]
		[JsonConverter(typeof(TimeSpanConverter), true)]
		public TimeSpan EndTime { get; set; }
		[JsonProperty(PropertyName = "elements")]
		public PeriodElement[] Elements { get; set; }
		[JsonProperty(PropertyName = "hasInfo")]
		public bool HasInfo { get; set; }
		[JsonProperty(PropertyName = "code")]
		public uint Code { get; set; }
		[JsonProperty(PropertyName = "cellState")]
		[JsonConverter(typeof(StringEnumConverter))]
		public PeriodCellState CellState { get; set; }
		[JsonProperty(PropertyName = "priority")]
		public uint Priority { get; set; }
		// is: {standard: true}
		// rights: ["READ_HOMEWORK"]
		// can: {requestStudents: false, requestLessonTopic: false, submitLessonTopic: false}

		private T[] GetTs<T>(MasterData masterData, ElementType tType)
		{
			return Elements.Where(e => e.ElementType == tType).Select(e => (T)masterData.GetElement(tType, e.ElementId)).ToArray();
		}

		public Class[] GetClasses(MasterData masterData) => GetTs<Class>(masterData, ElementType.CLASS);
		public Room[] GetRooms(MasterData masterData) => GetTs<Room>(masterData, ElementType.ROOM);
		public Subject[] GetSubjects(MasterData masterData) => GetTs<Subject>(masterData, ElementType.SUBJECT);
		public Teacher[] GetTeachers(MasterData masterData) => GetTs<Teacher>(masterData, ElementType.TEACHER);
	}
}
