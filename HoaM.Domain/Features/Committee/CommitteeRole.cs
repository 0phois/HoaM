namespace HoaM.Domain
{
    public partial class CommitteeRole
    {
        public static CommitteeRole Chairman => From("Chairman");
        public static CommitteeRole President => From("President");
        public static CommitteeRole VicePresident => From("Vice President");
        public static CommitteeRole Secretary => From("Secretary");
        public static CommitteeRole Treasurer => From("Treasurer");
        public static CommitteeRole Liaison => From("Liaison");
        public static CommitteeRole Member => From("Member");
    }
}
