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

	public enum ElementType : uint
	{
		CLASS = 1,
		TEACHER = 2,
		SUBJECT = 3,
		ROOM = 4,
		STUDENT = 5
	}
}
