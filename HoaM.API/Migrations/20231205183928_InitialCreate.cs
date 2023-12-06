using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HoaM.SampleAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: true),
                    SecurityStamp = table.Column<string>(type: "TEXT", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Communities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Communities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoleId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "TEXT", nullable: false),
                    ProviderKey = table.Column<string>(type: "TEXT", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "TEXT", nullable: true),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    RoleId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    LoginProvider = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    IsPinned = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Body = table.Column<string>(type: "TEXT", nullable: false),
                    PublishedDate = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    CommunityId = table.Column<Guid>(type: "TEXT", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    LastModifiedBy = table.Column<Guid>(type: "TEXT", nullable: false),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Articles_Communities_CommunityId",
                        column: x => x.CommunityId,
                        principalTable: "Communities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AssociationFees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    CommunityId = table.Column<Guid>(type: "TEXT", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    LastModifiedBy = table.Column<Guid>(type: "TEXT", nullable: false),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "TEXT", nullable: true),
                    DeletionDate = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    Data = table.Column<string>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Occurrence_Start = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    Occurrence_Stop = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    Schedule_Interval = table.Column<TimeSpan>(type: "TEXT", nullable: true),
                    Schedule_EndsAt = table.Column<DateTimeOffset>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssociationFees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssociationFees_Communities_CommunityId",
                        column: x => x.CommunityId,
                        principalTable: "Communities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Committees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    MissionStatement = table.Column<string>(type: "TEXT", nullable: true),
                    EstablishedDate = table.Column<DateOnly>(type: "TEXT", nullable: true),
                    DissolvedDate = table.Column<DateOnly>(type: "TEXT", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "TEXT", nullable: true),
                    DeletionDate = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    CommunityId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Committees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Committees_Communities_CommunityId",
                        column: x => x.CommunityId,
                        principalTable: "Communities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Content = table.Column<byte[]>(type: "BLOB", nullable: false),
                    CommunityId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Documents_Communities_CommunityId",
                        column: x => x.CommunityId,
                        principalTable: "Communities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Event",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    CommunityId = table.Column<Guid>(type: "TEXT", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    LastModifiedBy = table.Column<Guid>(type: "TEXT", nullable: false),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "TEXT", nullable: true),
                    DeletionDate = table.Column<DateTimeOffset>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Event_Communities_CommunityId",
                        column: x => x.CommunityId,
                        principalTable: "Communities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "NotificationTemplates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Content = table.Column<string>(type: "TEXT", nullable: false),
                    CommunityId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NotificationTemplates_Communities_CommunityId",
                        column: x => x.CommunityId,
                        principalTable: "Communities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Parcels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    StreetNumber = table.Column<string>(type: "TEXT", nullable: true),
                    StreetName = table.Column<string>(type: "TEXT", nullable: true),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    DeletedBy = table.Column<Guid>(type: "TEXT", nullable: true),
                    DeletionDate = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    CommunityId = table.Column<Guid>(type: "TEXT", nullable: true),
                    ParcelType = table.Column<string>(type: "TEXT", maxLength: 13, nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: true),
                    Description_Id = table.Column<Guid>(type: "TEXT", nullable: true),
                    Description_Content = table.Column<string>(type: "TEXT", nullable: true),
                    Description_CreatedBy = table.Column<Guid>(type: "TEXT", nullable: true),
                    Description_CreatedDate = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    Description_LastModifiedBy = table.Column<Guid>(type: "TEXT", nullable: true),
                    Description_LastModifiedDate = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    OpeningHours_OpeningTime = table.Column<TimeOnly>(type: "TEXT", nullable: true),
                    OpeningHours_ClosingTime = table.Column<TimeOnly>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parcels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Parcels_Communities_CommunityId",
                        column: x => x.CommunityId,
                        principalTable: "Communities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Committees_AdditionalDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    CommitteeId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Content = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    LastModifiedBy = table.Column<Guid>(type: "TEXT", nullable: false),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Committees_AdditionalDetails", x => new { x.CommitteeId, x.Id });
                    table.ForeignKey(
                        name: "FK_Committees_AdditionalDetails_Committees_CommitteeId",
                        column: x => x.CommitteeId,
                        principalTable: "Committees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Meetings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    ScheduledDate = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    CommitteeId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CommunityId = table.Column<Guid>(type: "TEXT", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    LastModifiedBy = table.Column<Guid>(type: "TEXT", nullable: false),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "TEXT", nullable: true),
                    DeletionDate = table.Column<DateTimeOffset>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meetings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Meetings_Committees_CommitteeId",
                        column: x => x.CommitteeId,
                        principalTable: "Committees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Meetings_Communities_CommunityId",
                        column: x => x.CommunityId,
                        principalTable: "Communities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Data = table.Column<string>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Occurrence_Start = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    Occurrence_Stop = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    Schedule_Interval = table.Column<TimeSpan>(type: "TEXT", nullable: true),
                    Schedule_EndsAt = table.Column<DateTimeOffset>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Events_Event_Id",
                        column: x => x.Id,
                        principalTable: "Event",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AssociationMembers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    ParcelId = table.Column<Guid>(type: "TEXT", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "TEXT", nullable: true),
                    DeletionDate = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    CommunityId = table.Column<Guid>(type: "TEXT", nullable: true),
                    MemberType = table.Column<string>(type: "TEXT", maxLength: 13, nullable: false),
                    Position = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssociationMembers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssociationMembers_Communities_CommunityId",
                        column: x => x.CommunityId,
                        principalTable: "Communities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AssociationMembers_Parcels_ParcelId",
                        column: x => x.ParcelId,
                        principalTable: "Parcels",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Lots",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Number = table.Column<string>(type: "TEXT", nullable: false),
                    CommunityId = table.Column<Guid>(type: "TEXT", nullable: true),
                    ParcelId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lots_Communities_CommunityId",
                        column: x => x.CommunityId,
                        principalTable: "Communities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Lots_Parcels_ParcelId",
                        column: x => x.ParcelId,
                        principalTable: "Parcels",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Parcels_Amenities",
                columns: table => new
                {
                    ParcelId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parcels_Amenities", x => new { x.ParcelId, x.Id });
                    table.ForeignKey(
                        name: "FK_Parcels_Amenities_Parcels_ParcelId",
                        column: x => x.ParcelId,
                        principalTable: "Parcels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Parcels_Rules",
                columns: table => new
                {
                    ParcelId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parcels_Rules", x => new { x.ParcelId, x.Id });
                    table.ForeignKey(
                        name: "FK_Parcels_Rules_Parcels_ParcelId",
                        column: x => x.ParcelId,
                        principalTable: "Parcels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Meetings_Agenda",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    MeetingId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Content = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    LastModifiedBy = table.Column<Guid>(type: "TEXT", nullable: false),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meetings_Agenda", x => new { x.MeetingId, x.Id });
                    table.ForeignKey(
                        name: "FK_Meetings_Agenda_Meetings_MeetingId",
                        column: x => x.MeetingId,
                        principalTable: "Meetings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AuditLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    TableName = table.Column<string>(type: "TEXT", nullable: false),
                    RecordId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Action = table.Column<int>(type: "INTEGER", nullable: false),
                    ChangedColumns = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    OldValues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewValues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Who = table.Column<Guid>(type: "TEXT", nullable: false),
                    When = table.Column<DateTimeOffset>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuditLogs_AssociationMembers_Who",
                        column: x => x.Who,
                        principalTable: "AssociationMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CommitteeCommitteeMember",
                columns: table => new
                {
                    CommitteesId = table.Column<Guid>(type: "TEXT", nullable: false),
                    MembersId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommitteeCommitteeMember", x => new { x.CommitteesId, x.MembersId });
                    table.ForeignKey(
                        name: "FK_CommitteeCommitteeMember_AssociationMembers_MembersId",
                        column: x => x.MembersId,
                        principalTable: "AssociationMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommitteeCommitteeMember_Committees_CommitteesId",
                        column: x => x.CommitteesId,
                        principalTable: "Committees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Emails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    IsVerified = table.Column<bool>(type: "INTEGER", nullable: false),
                    Address = table.Column<string>(type: "TEXT", nullable: false),
                    AssociationMemberId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Emails_AssociationMembers_AssociationMemberId",
                        column: x => x.AssociationMemberId,
                        principalTable: "AssociationMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MeetingMinutes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    AssociationMemberId = table.Column<Guid>(type: "TEXT", nullable: true),
                    PublishedDate = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    MeetingId = table.Column<Guid>(type: "TEXT", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    LastModifiedBy = table.Column<Guid>(type: "TEXT", nullable: false),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "TEXT", nullable: true),
                    DeletionDate = table.Column<DateTimeOffset>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeetingMinutes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MeetingMinutes_AssociationMembers_AssociationMemberId",
                        column: x => x.AssociationMemberId,
                        principalTable: "AssociationMembers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MeetingMinutes_Meetings_MeetingId",
                        column: x => x.MeetingId,
                        principalTable: "Meetings",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    TemplateId = table.Column<Guid>(type: "TEXT", nullable: false),
                    AssociationMemberId = table.Column<Guid>(type: "TEXT", nullable: true),
                    ReceivedDate = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    ReadDate = table.Column<DateTimeOffset>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_AssociationMembers_AssociationMemberId",
                        column: x => x.AssociationMemberId,
                        principalTable: "AssociationMembers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Notifications_NotificationTemplates_TemplateId",
                        column: x => x.TemplateId,
                        principalTable: "NotificationTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PhoneNumbers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    CountryCode = table.Column<string>(type: "TEXT", nullable: false),
                    AreaCode = table.Column<string>(type: "TEXT", nullable: false),
                    Prefix = table.Column<string>(type: "TEXT", nullable: false),
                    Number = table.Column<string>(type: "TEXT", nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    AssociationMemberId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhoneNumbers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhoneNumbers_AssociationMembers_AssociationMemberId",
                        column: x => x.AssociationMemberId,
                        principalTable: "AssociationMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    AssociationMemberId = table.Column<Guid>(type: "TEXT", nullable: true),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Amount = table.Column<decimal>(type: "TEXT", nullable: false),
                    EffectiveDate = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    Memo_Id = table.Column<Guid>(type: "TEXT", nullable: true),
                    Memo_Content = table.Column<string>(type: "TEXT", nullable: true),
                    Memo_CreatedBy = table.Column<Guid>(type: "TEXT", nullable: true),
                    Memo_CreatedDate = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    Memo_LastModifiedBy = table.Column<Guid>(type: "TEXT", nullable: true),
                    Memo_LastModifiedDate = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    CommunityId = table.Column<Guid>(type: "TEXT", nullable: true),
                    Discriminator = table.Column<string>(type: "TEXT", maxLength: 13, nullable: false),
                    ParcelId = table.Column<Guid>(type: "TEXT", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    LastModifiedBy = table.Column<Guid>(type: "TEXT", nullable: false),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "TEXT", nullable: true),
                    DeletionDate = table.Column<DateTimeOffset>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_AssociationMembers_AssociationMemberId",
                        column: x => x.AssociationMemberId,
                        principalTable: "AssociationMembers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Transactions_Communities_CommunityId",
                        column: x => x.CommunityId,
                        principalTable: "Communities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Transactions_Parcels_ParcelId",
                        column: x => x.ParcelId,
                        principalTable: "Parcels",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ActionItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    MeetingMinutesId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Content = table.Column<string>(type: "TEXT", nullable: false),
                    AssignedTo = table.Column<Guid>(type: "TEXT", nullable: false),
                    IsCompleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    LastModifiedBy = table.Column<Guid>(type: "TEXT", nullable: false),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionItems", x => new { x.MeetingMinutesId, x.Id });
                    table.ForeignKey(
                        name: "FK_ActionItems_MeetingMinutes_MeetingMinutesId",
                        column: x => x.MeetingMinutesId,
                        principalTable: "MeetingMinutes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AssociationMemberMeetingMinutes",
                columns: table => new
                {
                    AttendeesId = table.Column<Guid>(type: "TEXT", nullable: false),
                    MeetingMinutesId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssociationMemberMeetingMinutes", x => new { x.AttendeesId, x.MeetingMinutesId });
                    table.ForeignKey(
                        name: "FK_AssociationMemberMeetingMinutes_AssociationMembers_AttendeesId",
                        column: x => x.AttendeesId,
                        principalTable: "AssociationMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssociationMemberMeetingMinutes_MeetingMinutes_MeetingMinutesId",
                        column: x => x.MeetingMinutesId,
                        principalTable: "MeetingMinutes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MeetingMinutes_AgendaNotes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    MeetingMinutesId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Content = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    LastModifiedBy = table.Column<Guid>(type: "TEXT", nullable: false),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeetingMinutes_AgendaNotes", x => new { x.MeetingMinutesId, x.Id });
                    table.ForeignKey(
                        name: "FK_MeetingMinutes_AgendaNotes_MeetingMinutes_MeetingMinutesId",
                        column: x => x.MeetingMinutesId,
                        principalTable: "MeetingMinutes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MeetingMinutes_Notes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    MeetingMinutesId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Content = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    LastModifiedBy = table.Column<Guid>(type: "TEXT", nullable: false),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeetingMinutes_Notes", x => new { x.MeetingMinutesId, x.Id });
                    table.ForeignKey(
                        name: "FK_MeetingMinutes_Notes_MeetingMinutes_MeetingMinutesId",
                        column: x => x.MeetingMinutesId,
                        principalTable: "MeetingMinutes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Articles_CommunityId",
                table: "Articles",
                column: "CommunityId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AssociationFees_CommunityId",
                table: "AssociationFees",
                column: "CommunityId");

            migrationBuilder.CreateIndex(
                name: "IX_AssociationMemberMeetingMinutes_MeetingMinutesId",
                table: "AssociationMemberMeetingMinutes",
                column: "MeetingMinutesId");

            migrationBuilder.CreateIndex(
                name: "IX_AssociationMembers_CommunityId",
                table: "AssociationMembers",
                column: "CommunityId");

            migrationBuilder.CreateIndex(
                name: "IX_AssociationMembers_ParcelId",
                table: "AssociationMembers",
                column: "ParcelId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_Who",
                table: "AuditLogs",
                column: "Who");

            migrationBuilder.CreateIndex(
                name: "IX_CommitteeCommitteeMember_MembersId",
                table: "CommitteeCommitteeMember",
                column: "MembersId");

            migrationBuilder.CreateIndex(
                name: "IX_Committees_CommunityId",
                table: "Committees",
                column: "CommunityId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_CommunityId",
                table: "Documents",
                column: "CommunityId");

            migrationBuilder.CreateIndex(
                name: "IX_Emails_AssociationMemberId",
                table: "Emails",
                column: "AssociationMemberId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Event_CommunityId",
                table: "Event",
                column: "CommunityId");

            migrationBuilder.CreateIndex(
                name: "IX_Lots_CommunityId",
                table: "Lots",
                column: "CommunityId");

            migrationBuilder.CreateIndex(
                name: "IX_Lots_ParcelId",
                table: "Lots",
                column: "ParcelId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingMinutes_AssociationMemberId",
                table: "MeetingMinutes",
                column: "AssociationMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingMinutes_MeetingId",
                table: "MeetingMinutes",
                column: "MeetingId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Meetings_CommitteeId",
                table: "Meetings",
                column: "CommitteeId");

            migrationBuilder.CreateIndex(
                name: "IX_Meetings_CommunityId",
                table: "Meetings",
                column: "CommunityId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationTemplates_CommunityId",
                table: "NotificationTemplates",
                column: "CommunityId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_AssociationMemberId",
                table: "Notifications",
                column: "AssociationMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_TemplateId",
                table: "Notifications",
                column: "TemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_Parcels_CommunityId",
                table: "Parcels",
                column: "CommunityId");

            migrationBuilder.CreateIndex(
                name: "IX_PhoneNumbers_AssociationMemberId",
                table: "PhoneNumbers",
                column: "AssociationMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_AssociationMemberId",
                table: "Transactions",
                column: "AssociationMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_CommunityId",
                table: "Transactions",
                column: "CommunityId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_ParcelId",
                table: "Transactions",
                column: "ParcelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActionItems");

            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "AssociationFees");

            migrationBuilder.DropTable(
                name: "AssociationMemberMeetingMinutes");

            migrationBuilder.DropTable(
                name: "AuditLogs");

            migrationBuilder.DropTable(
                name: "CommitteeCommitteeMember");

            migrationBuilder.DropTable(
                name: "Committees_AdditionalDetails");

            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.DropTable(
                name: "Emails");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Lots");

            migrationBuilder.DropTable(
                name: "MeetingMinutes_AgendaNotes");

            migrationBuilder.DropTable(
                name: "MeetingMinutes_Notes");

            migrationBuilder.DropTable(
                name: "Meetings_Agenda");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "Parcels_Amenities");

            migrationBuilder.DropTable(
                name: "Parcels_Rules");

            migrationBuilder.DropTable(
                name: "PhoneNumbers");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Event");

            migrationBuilder.DropTable(
                name: "MeetingMinutes");

            migrationBuilder.DropTable(
                name: "NotificationTemplates");

            migrationBuilder.DropTable(
                name: "AssociationMembers");

            migrationBuilder.DropTable(
                name: "Meetings");

            migrationBuilder.DropTable(
                name: "Parcels");

            migrationBuilder.DropTable(
                name: "Committees");

            migrationBuilder.DropTable(
                name: "Communities");
        }
    }
}
