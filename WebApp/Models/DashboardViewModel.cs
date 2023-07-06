namespace WebApp.Models
{
	public class DashboardViewModel
	{
		public DashboardViewModel() { }

		public DashboardViewModel(User user, Subject subject)
		{
			IsValid = true;
			Username = user.Username;
			Characters = subject.Characters;
			ImageData = subject.ImageData;
			Meaning = subject.Meanings.FirstOrDefault(m => m.Primary)?.Meaning;
			Reading = subject.Readings.FirstOrDefault(m => m.Primary)?.Reading;
			Object = subject.Object;
			Level = user.Level;
			AvailableReviewsCount = user.AvailableReviewsCount;
			AvailableLessonsCount = user.AvailableLessonsCount;
		}

		public bool IsValid { get; }

		public string Username { get; }

		public string Characters { get; }

		public string? ImageData { get; }

		public string? Meaning { get; }

		public string? Reading { get; }

		public string Object { get; }

		public int Level { get; }

		public int AvailableReviewsCount { get; }

		public int AvailableLessonsCount { get; }

		public bool HasImage => !string.IsNullOrWhiteSpace(ImageData);

		public string FormattedObject
		{
			get
			{
				if (Object.Contains("_"))
				{
					var parts = Object.Split('_');

					if (parts.Length > 0 && parts[1].Length > 0)
						return parts[1][..1];
				}

				return Object[..1];
			}
		}
	}
}
