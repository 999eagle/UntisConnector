using System;
using System.Collections.Generic;
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
	}
}
