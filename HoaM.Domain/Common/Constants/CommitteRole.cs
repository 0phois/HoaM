namespace HoaM.Domain.Common
{
    public partial class CommitteRole
    {
        public static CommitteeRole Chairman => CommitteeRole.From("Chairman");
        public static CommitteeRole President => CommitteeRole.From("President");
        public static CommitteeRole VicePresident => CommitteeRole.From("Vice President");
        public static CommitteeRole Secretary => CommitteeRole.From("Secretary");
        public static CommitteeRole Treasurer => CommitteeRole.From("Treasurer");
        public static CommitteeRole Member => CommitteeRole.From("Member");
        public static CommitteeRole Liaison => CommitteeRole.From("Liaison");
    }
}
