namespace LiveScoreUpdateSystem.Common
{
    public class GlobalConstants
    {
        // regex validation patterns
        public const string AlphaNumericalPattern = "^[a-zA-Z0-9 _]+$";

        public const string AdminRole = "Admin";
        public const string LiveUpdaterRole = "LiveUpdater";
        public const string RegularUser = "Regular";

        // ==> Data models validation constants
  
        // FixtureEvent
        public const int MinFixtureEventMinuteValue = 0;

        // League
        public const int MinLeagueSeasonValue = 2017;
        public const int MaxLeagueSeasonValue = 2030;

        public const int MinLeagueNameLength = 4;
        public const int MaxLeagueNameLength = 30;

        // Player
        public const int MinPlayerNameLength = 2;
        public const int MaxPlayerNameLength = 20;

        public const int MinPlayerAge = 13;
        public const int MaxPlayerAge = 45;

        public const int MinPlayerShirtNumber = 1;
        public const int MaxPlayerShirtNumber = 100;

        // Team
        public const int MinTeamNameLength = 4;
        public const int MaxTeamNameLength = 30;

        // Fixture
        public const int MinFixtureMinute = 0;
        public const int MaxFixtureEventMinute = 140;

        // Country
        public const int MinCountryNameLength = 3;
        public const int MaxCountryNameLength = 40;

        // Grids
        public const int GridsPageSize = 12;

        // Notification messages
        public const string SuccessMessage = "Success";
        public const string ErrorMessage = "Error";
    }
}
