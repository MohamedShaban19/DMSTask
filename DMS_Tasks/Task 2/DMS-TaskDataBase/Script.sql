USE [DMSDataBase]
GO
/****** Object:  Table [dbo].[Appointments]    Script Date: 3/15/2025 11:09:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Appointments](
	[AppointmentId] [int] IDENTITY(1,1) NOT NULL,
	[DocId] [int] NULL,
	[PatientName] [nvarchar](20) NULL,
	[Phone] [nvarchar](20) NULL,
	[BirthDate] [date] NOT NULL,
	[AppointmentDate] [date] NOT NULL,
	[AppointmentTime] [nvarchar](20) NOT NULL,
	[Status] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Clinics]    Script Date: 3/15/2025 11:09:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Clinics](
	[ClinicId] [int] IDENTITY(1,1) NOT NULL,
	[ClinicName] [nvarchar](100) NOT NULL,
	[Is_Stopped] [int] NOT NULL,
 CONSTRAINT [PK_Clinics] PRIMARY KEY CLUSTERED 
(
	[ClinicId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Doctors]    Script Date: 3/15/2025 11:09:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Doctors](
	[DocId] [int] IDENTITY(1,1) NOT NULL,
	[ClinicId] [int] NULL,
	[DocName] [nvarchar](100) NOT NULL,
	[Day_Of_Week] [int] NULL,
	[StartTime] [time](7) NOT NULL,
	[EndTime] [time](7) NOT NULL,
	[Visit_Duration] [int] NULL,
	[Is_Stopped] [int] NOT NULL,
 CONSTRAINT [PK_Doctors] PRIMARY KEY CLUSTERED 
(
	[DocId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Appointments] ON 

INSERT [dbo].[Appointments] ([AppointmentId], [DocId], [PatientName], [Phone], [BirthDate], [AppointmentDate], [AppointmentTime], [Status]) VALUES (5, 1, N'Mohamed Samir', N'01066432015', CAST(N'1994-01-01' AS Date), CAST(N'2025-03-15' AS Date), N'04:30 PM', 0)
INSERT [dbo].[Appointments] ([AppointmentId], [DocId], [PatientName], [Phone], [BirthDate], [AppointmentDate], [AppointmentTime], [Status]) VALUES (6, 2, N'Sara Saad', N'01113452012', CAST(N'2002-01-23' AS Date), CAST(N'2025-03-14' AS Date), N'12:40 PM', 0)
INSERT [dbo].[Appointments] ([AppointmentId], [DocId], [PatientName], [Phone], [BirthDate], [AppointmentDate], [AppointmentTime], [Status]) VALUES (7, 5, N'Sayed Ahmed', N'012864', CAST(N'1997-06-23' AS Date), CAST(N'2025-03-16' AS Date), N'03:00 PM', 0)
SET IDENTITY_INSERT [dbo].[Appointments] OFF
GO
SET IDENTITY_INSERT [dbo].[Clinics] ON 

INSERT [dbo].[Clinics] ([ClinicId], [ClinicName], [Is_Stopped]) VALUES (1, N'Emergency ', 0)
INSERT [dbo].[Clinics] ([ClinicId], [ClinicName], [Is_Stopped]) VALUES (2, N'Surgical ', 0)
INSERT [dbo].[Clinics] ([ClinicId], [ClinicName], [Is_Stopped]) VALUES (3, N'Orthopedic ', 0)
INSERT [dbo].[Clinics] ([ClinicId], [ClinicName], [Is_Stopped]) VALUES (4, N'Cardiology ', 0)
INSERT [dbo].[Clinics] ([ClinicId], [ClinicName], [Is_Stopped]) VALUES (5, N'Nephrology ', 0)
SET IDENTITY_INSERT [dbo].[Clinics] OFF
GO
SET IDENTITY_INSERT [dbo].[Doctors] ON 

INSERT [dbo].[Doctors] ([DocId], [ClinicId], [DocName], [Day_Of_Week], [StartTime], [EndTime], [Visit_Duration], [Is_Stopped]) VALUES (1, 1, N'Dr. Mohamed Kahled', 5, CAST(N'16:00:00' AS Time), CAST(N'22:00:00' AS Time), 30, 0)
INSERT [dbo].[Doctors] ([DocId], [ClinicId], [DocName], [Day_Of_Week], [StartTime], [EndTime], [Visit_Duration], [Is_Stopped]) VALUES (2, 2, N'Dr. Ali Ibrahem', 6, CAST(N'12:00:00' AS Time), CAST(N'18:00:00' AS Time), 20, 0)
INSERT [dbo].[Doctors] ([DocId], [ClinicId], [DocName], [Day_Of_Week], [StartTime], [EndTime], [Visit_Duration], [Is_Stopped]) VALUES (3, 1, N'Dr. Asmaa Mohamed', 6, CAST(N'12:00:00' AS Time), CAST(N'18:00:00' AS Time), 25, 0)
INSERT [dbo].[Doctors] ([DocId], [ClinicId], [DocName], [Day_Of_Week], [StartTime], [EndTime], [Visit_Duration], [Is_Stopped]) VALUES (4, 3, N'Dr. Osama Ali', 5, CAST(N'08:00:00' AS Time), CAST(N'16:00:00' AS Time), 15, 0)
INSERT [dbo].[Doctors] ([DocId], [ClinicId], [DocName], [Day_Of_Week], [StartTime], [EndTime], [Visit_Duration], [Is_Stopped]) VALUES (5, 4, N'Dr. Nada Mostafa', 6, CAST(N'10:00:00' AS Time), CAST(N'18:00:00' AS Time), 30, 0)
SET IDENTITY_INSERT [dbo].[Doctors] OFF
GO
ALTER TABLE [dbo].[Appointments] ADD  DEFAULT ((0)) FOR [Status]
GO
ALTER TABLE [dbo].[Clinics] ADD  CONSTRAINT [DF_Clinics_IsStopped]  DEFAULT ((0)) FOR [Is_Stopped]
GO
ALTER TABLE [dbo].[Doctors] ADD  DEFAULT ((30)) FOR [Visit_Duration]
GO
ALTER TABLE [dbo].[Doctors] ADD  DEFAULT ((0)) FOR [Is_Stopped]
GO
ALTER TABLE [dbo].[Appointments]  WITH CHECK ADD FOREIGN KEY([DocId])
REFERENCES [dbo].[Doctors] ([DocId])
GO
ALTER TABLE [dbo].[Doctors]  WITH CHECK ADD FOREIGN KEY([ClinicId])
REFERENCES [dbo].[Clinics] ([ClinicId])
GO
