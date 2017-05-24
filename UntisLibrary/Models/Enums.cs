using System;
using System.Collections.Generic;
using System.Text;

namespace UntisLibrary.Models
{
	public enum PeriodConnectType
	{
		NONE
	}

	public enum PeriodElementState
	{
		REGULAR
	}

	public enum PeriodLessonCode
	{
		UNTIS_LESSON
	}

	public enum PeriodCellState
	{
		STANDARD,
		SHIFT,
		CANCEL
	}

	public enum DataObjectType : uint
	{
		Class = 1,
		Teacher = 2,
		Subject = 3,
		Room = 4,
		Student = 5
	}
}
