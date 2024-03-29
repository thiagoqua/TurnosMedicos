USE [AplicacionWeb]
GO
/****** Object:  Table [dbo].[Afiliado]    Script Date: 15/2/2023 08:59:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Afiliado](
	[AfiliadoID] [int] IDENTITY(1,1) NOT NULL,
	[nroDNI] [int] NOT NULL,
	[Nombre] [nchar](20) NOT NULL,
	[Apellido] [nchar](20) NOT NULL,
	[IDLocalidad] [int] NOT NULL,
	[IDPlan] [int] NOT NULL,
	[NroAfiliado] [nchar](20) NULL,
 CONSTRAINT [PK_Afiliado] PRIMARY KEY CLUSTERED 
(
	[AfiliadoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Año]    Script Date: 15/2/2023 08:59:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Año](
	[AñoID] [int] IDENTITY(1,1) NOT NULL,
	[NombreAño] [int] NOT NULL,
 CONSTRAINT [PK_Año] PRIMARY KEY CLUSTERED 
(
	[AñoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Dia]    Script Date: 15/2/2023 08:59:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Dia](
	[DiaID] [int] IDENTITY(1,1) NOT NULL,
	[NombreDia] [char](10) NOT NULL,
 CONSTRAINT [PK_Dia] PRIMARY KEY CLUSTERED 
(
	[DiaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[DisponibilidadMedico]    Script Date: 15/2/2023 08:59:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DisponibilidadMedico](
	[DisponibilidadID] [int] IDENTITY(1,1) NOT NULL,
	[IDMedico] [int] NOT NULL,
	[IDDia] [int] NOT NULL,
	[Consultorio] [int] NOT NULL,
	[HorarioInicio] [time](7) NOT NULL,
	[HorarioFin] [time](7) NOT NULL,
	[IDSucursal] [int] NOT NULL,
 CONSTRAINT [PK_DisponibilidadMedico] PRIMARY KEY CLUSTERED 
(
	[DisponibilidadID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Especialidad]    Script Date: 15/2/2023 08:59:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Especialidad](
	[EspecialidadId] [int] IDENTITY(1,1) NOT NULL,
	[EspecialidadDescripcion] [nchar](50) NULL,
 CONSTRAINT [PK_Especialidad] PRIMARY KEY CLUSTERED 
(
	[EspecialidadId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[FechaTurno]    Script Date: 15/2/2023 08:59:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FechaTurno](
	[FechaTurnoID] [int] IDENTITY(1,1) NOT NULL,
	[IDDia] [int] NOT NULL,
	[IDHorario] [int] NOT NULL,
	[Fecha] [date] NOT NULL,
 CONSTRAINT [PK_FechaTurno] PRIMARY KEY CLUSTERED 
(
	[FechaTurnoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Horario]    Script Date: 15/2/2023 08:59:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Horario](
	[HorarioID] [int] IDENTITY(1,1) NOT NULL,
	[Hora] [time](7) NOT NULL,
 CONSTRAINT [PK_Horario] PRIMARY KEY CLUSTERED 
(
	[HorarioID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Localidad]    Script Date: 15/2/2023 08:59:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Localidad](
	[LocalidadId] [int] IDENTITY(1,1) NOT NULL,
	[LocalidadDescripcion] [nchar](50) NOT NULL,
	[IDProvincia] [int] NOT NULL,
 CONSTRAINT [PK_Localidad] PRIMARY KEY CLUSTERED 
(
	[LocalidadId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Medico]    Script Date: 15/2/2023 08:59:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Medico](
	[MedicoID] [int] IDENTITY(1,1) NOT NULL,
	[IDEspecialidad] [int] NOT NULL,
	[IDUsuario] [int] NOT NULL,
 CONSTRAINT [PK_Medico] PRIMARY KEY CLUSTERED 
(
	[MedicoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MedicoSucursal]    Script Date: 15/2/2023 08:59:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MedicoSucursal](
	[IDMedicoSucursal] [int] IDENTITY(1,1) NOT NULL,
	[IDMedico] [int] NOT NULL,
	[IDSucursal] [int] NOT NULL,
 CONSTRAINT [PK_MedicoSucursal] PRIMARY KEY CLUSTERED 
(
	[IDMedicoSucursal] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Mes]    Script Date: 15/2/2023 08:59:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Mes](
	[MesID] [int] IDENTITY(1,1) NOT NULL,
	[NombreMes] [nchar](10) NOT NULL,
 CONSTRAINT [PK_Mes] PRIMARY KEY CLUSTERED 
(
	[MesID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[NuevaContraseña]    Script Date: 15/2/2023 08:59:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NuevaContraseña](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[IDUsuario] [int] NOT NULL,
	[Creacion] [datetime] NOT NULL,
 CONSTRAINT [PK_NuevaContraseña] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ObraSocial]    Script Date: 15/2/2023 08:59:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ObraSocial](
	[ObraSocialId] [int] IDENTITY(1,1) NOT NULL,
	[ObraSocialDescripcion] [nchar](50) NOT NULL,
 CONSTRAINT [PK_ObraSocial] PRIMARY KEY CLUSTERED 
(
	[ObraSocialId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Perfil]    Script Date: 15/2/2023 08:59:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Perfil](
	[PerfilId] [int] IDENTITY(1,1) NOT NULL,
	[PerfilDescripcion] [nchar](10) NULL,
	[PerfilHabilitado] [bit] NULL,
 CONSTRAINT [PK_Perfil] PRIMARY KEY CLUSTERED 
(
	[PerfilId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PlanObraSocial]    Script Date: 15/2/2023 08:59:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PlanObraSocial](
	[PlanId] [int] IDENTITY(1,1) NOT NULL,
	[PlanDescripcion] [nchar](50) NOT NULL,
	[IDObraSocial] [int] NOT NULL,
 CONSTRAINT [PK_Plan] PRIMARY KEY CLUSTERED 
(
	[PlanId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Provincia]    Script Date: 15/2/2023 08:59:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Provincia](
	[ProvinciaId] [int] IDENTITY(1,1) NOT NULL,
	[ProvinciaDescripcion] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Provincia] PRIMARY KEY CLUSTERED 
(
	[ProvinciaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ServidorMail]    Script Date: 15/2/2023 08:59:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ServidorMail](
	[ID] [int] NOT NULL,
	[Mail] [varchar](50) NOT NULL,
	[Pass] [varchar](200) NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Sucursal]    Script Date: 15/2/2023 08:59:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sucursal](
	[SucursalId] [int] IDENTITY(1,1) NOT NULL,
	[SucursalDescripcion] [nvarchar](50) NOT NULL,
	[IDLocalidad] [int] NOT NULL,
 CONSTRAINT [PK_Sucursal] PRIMARY KEY CLUSTERED 
(
	[SucursalId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Turno]    Script Date: 15/2/2023 08:59:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Turno](
	[TurnoId] [int] IDENTITY(1,1) NOT NULL,
	[IDFechaTurno] [int] NOT NULL,
	[IDMedico] [int] NOT NULL,
	[IDEspecialidad] [int] NOT NULL,
	[IDProvincia] [int] NOT NULL,
	[IDLocalidad] [int] NOT NULL,
	[IDSucursal] [int] NOT NULL,
	[IDUsuario] [int] NOT NULL,
 CONSTRAINT [PK_Turno] PRIMARY KEY CLUSTERED 
(
	[TurnoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 15/2/2023 08:59:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Usuario](
	[UsuarioID] [int] IDENTITY(1,1) NOT NULL,
	[IDAfiliado] [int] NOT NULL,
	[UsuarioContraseña] [varchar](200) NOT NULL,
	[UsuarioEmail] [varchar](50) NOT NULL,
	[IDPerfil] [int] NOT NULL,
	[isMedico] [bit] NOT NULL,
 CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED 
(
	[UsuarioID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Afiliado] ON 

INSERT [dbo].[Afiliado] ([AfiliadoID], [nroDNI], [Nombre], [Apellido], [IDLocalidad], [IDPlan], [NroAfiliado]) VALUES (1, 40310908, N'Florencia           ', N'Galvan              ', 1, 1, N'PAC-40310908        ')
INSERT [dbo].[Afiliado] ([AfiliadoID], [nroDNI], [Nombre], [Apellido], [IDLocalidad], [IDPlan], [NroAfiliado]) VALUES (2, 41085447, N'Esteban             ', N'Tosoni              ', 2, 1, N'PAC-41085447        ')
INSERT [dbo].[Afiliado] ([AfiliadoID], [nroDNI], [Nombre], [Apellido], [IDLocalidad], [IDPlan], [NroAfiliado]) VALUES (3, 43283893, N'Thiago              ', N'Quaglia             ', 1, 1, N'PAC-43283893        ')
INSERT [dbo].[Afiliado] ([AfiliadoID], [nroDNI], [Nombre], [Apellido], [IDLocalidad], [IDPlan], [NroAfiliado]) VALUES (4, 123456, N'Admin               ', N'Admin               ', 2, 1, N'ADM-123456          ')
INSERT [dbo].[Afiliado] ([AfiliadoID], [nroDNI], [Nombre], [Apellido], [IDLocalidad], [IDPlan], [NroAfiliado]) VALUES (5, 40123456, N'Jorge               ', N'Garcia              ', 2, 1, N'MED-40123456        ')
INSERT [dbo].[Afiliado] ([AfiliadoID], [nroDNI], [Nombre], [Apellido], [IDLocalidad], [IDPlan], [NroAfiliado]) VALUES (6, 26123456, N'Lucia               ', N'Lorenzini           ', 2, 1, N'MED-26123456        ')
INSERT [dbo].[Afiliado] ([AfiliadoID], [nroDNI], [Nombre], [Apellido], [IDLocalidad], [IDPlan], [NroAfiliado]) VALUES (7, 21126456, N'Julia               ', N'Benadi              ', 2, 1, N'MED-21126456        ')
INSERT [dbo].[Afiliado] ([AfiliadoID], [nroDNI], [Nombre], [Apellido], [IDLocalidad], [IDPlan], [NroAfiliado]) VALUES (8, 20123456, N'Rocio               ', N'Juarez              ', 2, 1, N'MED-20123456        ')
INSERT [dbo].[Afiliado] ([AfiliadoID], [nroDNI], [Nombre], [Apellido], [IDLocalidad], [IDPlan], [NroAfiliado]) VALUES (9, 14123456, N'Omar                ', N'Fiore               ', 2, 1, N'MED-14123456        ')
INSERT [dbo].[Afiliado] ([AfiliadoID], [nroDNI], [Nombre], [Apellido], [IDLocalidad], [IDPlan], [NroAfiliado]) VALUES (10, 12123456, N'Oscar               ', N'Perez               ', 2, 1, N'MED-12123456        ')
INSERT [dbo].[Afiliado] ([AfiliadoID], [nroDNI], [Nombre], [Apellido], [IDLocalidad], [IDPlan], [NroAfiliado]) VALUES (11, 14123456, N'Matias              ', N'Benedetti           ', 2, 1, N'MED-14123456        ')
INSERT [dbo].[Afiliado] ([AfiliadoID], [nroDNI], [Nombre], [Apellido], [IDLocalidad], [IDPlan], [NroAfiliado]) VALUES (12, 13283893, N'Pedro               ', N'Nuñez               ', 2, 1, N'MED-13283893        ')
INSERT [dbo].[Afiliado] ([AfiliadoID], [nroDNI], [Nombre], [Apellido], [IDLocalidad], [IDPlan], [NroAfiliado]) VALUES (13, 12456870, N'Jose                ', N'Nuñez               ', 2, 1, N'MED-12456870        ')
INSERT [dbo].[Afiliado] ([AfiliadoID], [nroDNI], [Nombre], [Apellido], [IDLocalidad], [IDPlan], [NroAfiliado]) VALUES (14, 12456870, N'Jose                ', N'Nuñez               ', 2, 1, N'MED-12456870        ')
INSERT [dbo].[Afiliado] ([AfiliadoID], [nroDNI], [Nombre], [Apellido], [IDLocalidad], [IDPlan], [NroAfiliado]) VALUES (15, 14515151, N'Jose                ', N'Lopez               ', 2, 1, N'MED-14515151        ')
INSERT [dbo].[Afiliado] ([AfiliadoID], [nroDNI], [Nombre], [Apellido], [IDLocalidad], [IDPlan], [NroAfiliado]) VALUES (16, 40123956, N'Lucia               ', N'Perez               ', 2, 1, N'MED-40123956        ')
INSERT [dbo].[Afiliado] ([AfiliadoID], [nroDNI], [Nombre], [Apellido], [IDLocalidad], [IDPlan], [NroAfiliado]) VALUES (17, 38456987, N'Sofia               ', N'Gonzalez            ', 2, 1, N'MED-38456987        ')
INSERT [dbo].[Afiliado] ([AfiliadoID], [nroDNI], [Nombre], [Apellido], [IDLocalidad], [IDPlan], [NroAfiliado]) VALUES (18, 36451245, N'Lorena              ', N'Ricci               ', 2, 1, N'MED-36451245        ')
INSERT [dbo].[Afiliado] ([AfiliadoID], [nroDNI], [Nombre], [Apellido], [IDLocalidad], [IDPlan], [NroAfiliado]) VALUES (19, 40111222, N'Florencia           ', N'Galvan              ', 2, 1, N'PAC-40111222        ')
INSERT [dbo].[Afiliado] ([AfiliadoID], [nroDNI], [Nombre], [Apellido], [IDLocalidad], [IDPlan], [NroAfiliado]) VALUES (20, 40789456, N'Florencia           ', N'Galvan              ', 2, 1, N'PAC-40789456        ')
INSERT [dbo].[Afiliado] ([AfiliadoID], [nroDNI], [Nombre], [Apellido], [IDLocalidad], [IDPlan], [NroAfiliado]) VALUES (21, 38123456, N'Florencia           ', N'asd                 ', 2, 1, N'PAC-38123456        ')
INSERT [dbo].[Afiliado] ([AfiliadoID], [nroDNI], [Nombre], [Apellido], [IDLocalidad], [IDPlan], [NroAfiliado]) VALUES (22, 112233, N'Florencia           ', N'asd                 ', 2, 1, N'PAC-112233          ')
INSERT [dbo].[Afiliado] ([AfiliadoID], [nroDNI], [Nombre], [Apellido], [IDLocalidad], [IDPlan], [NroAfiliado]) VALUES (23, 223344, N'asd                 ', N'asd                 ', 2, 1, N'MED-223344          ')
INSERT [dbo].[Afiliado] ([AfiliadoID], [nroDNI], [Nombre], [Apellido], [IDLocalidad], [IDPlan], [NroAfiliado]) VALUES (24, 9494, N'asd                 ', N'asd                 ', 2, 1, N'PAC-9494            ')
SET IDENTITY_INSERT [dbo].[Afiliado] OFF
SET IDENTITY_INSERT [dbo].[Dia] ON 

INSERT [dbo].[Dia] ([DiaID], [NombreDia]) VALUES (1, N'Lunes     ')
INSERT [dbo].[Dia] ([DiaID], [NombreDia]) VALUES (2, N'Martes    ')
INSERT [dbo].[Dia] ([DiaID], [NombreDia]) VALUES (3, N'Miercoles ')
INSERT [dbo].[Dia] ([DiaID], [NombreDia]) VALUES (4, N'Jueves    ')
INSERT [dbo].[Dia] ([DiaID], [NombreDia]) VALUES (5, N'Viernes   ')
INSERT [dbo].[Dia] ([DiaID], [NombreDia]) VALUES (6, N'Sabado    ')
INSERT [dbo].[Dia] ([DiaID], [NombreDia]) VALUES (7, N'Domingo   ')
SET IDENTITY_INSERT [dbo].[Dia] OFF
SET IDENTITY_INSERT [dbo].[DisponibilidadMedico] ON 

INSERT [dbo].[DisponibilidadMedico] ([DisponibilidadID], [IDMedico], [IDDia], [Consultorio], [HorarioInicio], [HorarioFin], [IDSucursal]) VALUES (1, 1, 1, 1, CAST(N'06:00:00' AS Time), CAST(N'18:00:00' AS Time), 2)
INSERT [dbo].[DisponibilidadMedico] ([DisponibilidadID], [IDMedico], [IDDia], [Consultorio], [HorarioInicio], [HorarioFin], [IDSucursal]) VALUES (2, 1, 2, 1, CAST(N'06:00:00' AS Time), CAST(N'18:00:00' AS Time), 2)
INSERT [dbo].[DisponibilidadMedico] ([DisponibilidadID], [IDMedico], [IDDia], [Consultorio], [HorarioInicio], [HorarioFin], [IDSucursal]) VALUES (3, 1, 3, 1, CAST(N'06:00:00' AS Time), CAST(N'18:00:00' AS Time), 2)
INSERT [dbo].[DisponibilidadMedico] ([DisponibilidadID], [IDMedico], [IDDia], [Consultorio], [HorarioInicio], [HorarioFin], [IDSucursal]) VALUES (4, 1, 4, 1, CAST(N'06:00:00' AS Time), CAST(N'18:00:00' AS Time), 2)
INSERT [dbo].[DisponibilidadMedico] ([DisponibilidadID], [IDMedico], [IDDia], [Consultorio], [HorarioInicio], [HorarioFin], [IDSucursal]) VALUES (5, 1, 5, 1, CAST(N'06:00:00' AS Time), CAST(N'18:00:00' AS Time), 2)
INSERT [dbo].[DisponibilidadMedico] ([DisponibilidadID], [IDMedico], [IDDia], [Consultorio], [HorarioInicio], [HorarioFin], [IDSucursal]) VALUES (6, 2, 1, 2, CAST(N'06:00:00' AS Time), CAST(N'18:00:00' AS Time), 2)
INSERT [dbo].[DisponibilidadMedico] ([DisponibilidadID], [IDMedico], [IDDia], [Consultorio], [HorarioInicio], [HorarioFin], [IDSucursal]) VALUES (7, 2, 2, 2, CAST(N'06:00:00' AS Time), CAST(N'18:00:00' AS Time), 2)
INSERT [dbo].[DisponibilidadMedico] ([DisponibilidadID], [IDMedico], [IDDia], [Consultorio], [HorarioInicio], [HorarioFin], [IDSucursal]) VALUES (8, 2, 3, 2, CAST(N'06:00:00' AS Time), CAST(N'18:00:00' AS Time), 2)
INSERT [dbo].[DisponibilidadMedico] ([DisponibilidadID], [IDMedico], [IDDia], [Consultorio], [HorarioInicio], [HorarioFin], [IDSucursal]) VALUES (9, 3, 1, 2, CAST(N'06:00:00' AS Time), CAST(N'18:00:00' AS Time), 2)
INSERT [dbo].[DisponibilidadMedico] ([DisponibilidadID], [IDMedico], [IDDia], [Consultorio], [HorarioInicio], [HorarioFin], [IDSucursal]) VALUES (10, 3, 2, 2, CAST(N'06:00:00' AS Time), CAST(N'18:00:00' AS Time), 2)
INSERT [dbo].[DisponibilidadMedico] ([DisponibilidadID], [IDMedico], [IDDia], [Consultorio], [HorarioInicio], [HorarioFin], [IDSucursal]) VALUES (11, 3, 1, 2, CAST(N'06:00:00' AS Time), CAST(N'18:00:00' AS Time), 2)
INSERT [dbo].[DisponibilidadMedico] ([DisponibilidadID], [IDMedico], [IDDia], [Consultorio], [HorarioInicio], [HorarioFin], [IDSucursal]) VALUES (12, 3, 5, 2, CAST(N'06:00:00' AS Time), CAST(N'18:00:00' AS Time), 2)
INSERT [dbo].[DisponibilidadMedico] ([DisponibilidadID], [IDMedico], [IDDia], [Consultorio], [HorarioInicio], [HorarioFin], [IDSucursal]) VALUES (13, 4, 1, 2, CAST(N'06:00:00' AS Time), CAST(N'18:00:00' AS Time), 2)
INSERT [dbo].[DisponibilidadMedico] ([DisponibilidadID], [IDMedico], [IDDia], [Consultorio], [HorarioInicio], [HorarioFin], [IDSucursal]) VALUES (14, 4, 2, 1, CAST(N'06:00:00' AS Time), CAST(N'18:00:00' AS Time), 2)
INSERT [dbo].[DisponibilidadMedico] ([DisponibilidadID], [IDMedico], [IDDia], [Consultorio], [HorarioInicio], [HorarioFin], [IDSucursal]) VALUES (15, 4, 4, 3, CAST(N'06:00:00' AS Time), CAST(N'18:00:00' AS Time), 2)
INSERT [dbo].[DisponibilidadMedico] ([DisponibilidadID], [IDMedico], [IDDia], [Consultorio], [HorarioInicio], [HorarioFin], [IDSucursal]) VALUES (16, 4, 5, 2, CAST(N'06:00:00' AS Time), CAST(N'18:00:00' AS Time), 2)
INSERT [dbo].[DisponibilidadMedico] ([DisponibilidadID], [IDMedico], [IDDia], [Consultorio], [HorarioInicio], [HorarioFin], [IDSucursal]) VALUES (17, 5, 1, 1, CAST(N'06:00:00' AS Time), CAST(N'18:00:00' AS Time), 2)
INSERT [dbo].[DisponibilidadMedico] ([DisponibilidadID], [IDMedico], [IDDia], [Consultorio], [HorarioInicio], [HorarioFin], [IDSucursal]) VALUES (18, 5, 2, 1, CAST(N'06:00:00' AS Time), CAST(N'18:00:00' AS Time), 2)
INSERT [dbo].[DisponibilidadMedico] ([DisponibilidadID], [IDMedico], [IDDia], [Consultorio], [HorarioInicio], [HorarioFin], [IDSucursal]) VALUES (19, 5, 3, 1, CAST(N'06:00:00' AS Time), CAST(N'18:00:00' AS Time), 2)
INSERT [dbo].[DisponibilidadMedico] ([DisponibilidadID], [IDMedico], [IDDia], [Consultorio], [HorarioInicio], [HorarioFin], [IDSucursal]) VALUES (20, 5, 4, 1, CAST(N'06:00:00' AS Time), CAST(N'18:00:00' AS Time), 2)
INSERT [dbo].[DisponibilidadMedico] ([DisponibilidadID], [IDMedico], [IDDia], [Consultorio], [HorarioInicio], [HorarioFin], [IDSucursal]) VALUES (21, 6, 1, 1, CAST(N'06:00:00' AS Time), CAST(N'18:00:00' AS Time), 2)
INSERT [dbo].[DisponibilidadMedico] ([DisponibilidadID], [IDMedico], [IDDia], [Consultorio], [HorarioInicio], [HorarioFin], [IDSucursal]) VALUES (22, 6, 2, 1, CAST(N'06:00:00' AS Time), CAST(N'18:00:00' AS Time), 2)
INSERT [dbo].[DisponibilidadMedico] ([DisponibilidadID], [IDMedico], [IDDia], [Consultorio], [HorarioInicio], [HorarioFin], [IDSucursal]) VALUES (23, 6, 3, 1, CAST(N'06:00:00' AS Time), CAST(N'18:00:00' AS Time), 2)
INSERT [dbo].[DisponibilidadMedico] ([DisponibilidadID], [IDMedico], [IDDia], [Consultorio], [HorarioInicio], [HorarioFin], [IDSucursal]) VALUES (24, 6, 4, 1, CAST(N'06:00:00' AS Time), CAST(N'18:00:00' AS Time), 2)
INSERT [dbo].[DisponibilidadMedico] ([DisponibilidadID], [IDMedico], [IDDia], [Consultorio], [HorarioInicio], [HorarioFin], [IDSucursal]) VALUES (25, 6, 5, 1, CAST(N'06:00:00' AS Time), CAST(N'18:00:00' AS Time), 2)
INSERT [dbo].[DisponibilidadMedico] ([DisponibilidadID], [IDMedico], [IDDia], [Consultorio], [HorarioInicio], [HorarioFin], [IDSucursal]) VALUES (26, 7, 1, 1, CAST(N'06:00:00' AS Time), CAST(N'18:00:00' AS Time), 2)
INSERT [dbo].[DisponibilidadMedico] ([DisponibilidadID], [IDMedico], [IDDia], [Consultorio], [HorarioInicio], [HorarioFin], [IDSucursal]) VALUES (27, 7, 2, 1, CAST(N'06:00:00' AS Time), CAST(N'18:00:00' AS Time), 2)
INSERT [dbo].[DisponibilidadMedico] ([DisponibilidadID], [IDMedico], [IDDia], [Consultorio], [HorarioInicio], [HorarioFin], [IDSucursal]) VALUES (28, 7, 3, 1, CAST(N'06:00:00' AS Time), CAST(N'18:00:00' AS Time), 2)
INSERT [dbo].[DisponibilidadMedico] ([DisponibilidadID], [IDMedico], [IDDia], [Consultorio], [HorarioInicio], [HorarioFin], [IDSucursal]) VALUES (29, 7, 4, 1, CAST(N'06:00:00' AS Time), CAST(N'18:00:00' AS Time), 2)
INSERT [dbo].[DisponibilidadMedico] ([DisponibilidadID], [IDMedico], [IDDia], [Consultorio], [HorarioInicio], [HorarioFin], [IDSucursal]) VALUES (30, 7, 5, 1, CAST(N'06:00:00' AS Time), CAST(N'18:00:00' AS Time), 2)
INSERT [dbo].[DisponibilidadMedico] ([DisponibilidadID], [IDMedico], [IDDia], [Consultorio], [HorarioInicio], [HorarioFin], [IDSucursal]) VALUES (31, 8, 1, 1, CAST(N'06:00:00' AS Time), CAST(N'18:00:00' AS Time), 2)
INSERT [dbo].[DisponibilidadMedico] ([DisponibilidadID], [IDMedico], [IDDia], [Consultorio], [HorarioInicio], [HorarioFin], [IDSucursal]) VALUES (32, 8, 2, 1, CAST(N'06:00:00' AS Time), CAST(N'18:00:00' AS Time), 2)
INSERT [dbo].[DisponibilidadMedico] ([DisponibilidadID], [IDMedico], [IDDia], [Consultorio], [HorarioInicio], [HorarioFin], [IDSucursal]) VALUES (33, 8, 3, 1, CAST(N'06:00:00' AS Time), CAST(N'18:00:00' AS Time), 2)
INSERT [dbo].[DisponibilidadMedico] ([DisponibilidadID], [IDMedico], [IDDia], [Consultorio], [HorarioInicio], [HorarioFin], [IDSucursal]) VALUES (34, 8, 4, 1, CAST(N'06:00:00' AS Time), CAST(N'18:00:00' AS Time), 2)
INSERT [dbo].[DisponibilidadMedico] ([DisponibilidadID], [IDMedico], [IDDia], [Consultorio], [HorarioInicio], [HorarioFin], [IDSucursal]) VALUES (35, 8, 5, 1, CAST(N'06:00:00' AS Time), CAST(N'18:00:00' AS Time), 2)
INSERT [dbo].[DisponibilidadMedico] ([DisponibilidadID], [IDMedico], [IDDia], [Consultorio], [HorarioInicio], [HorarioFin], [IDSucursal]) VALUES (36, 10, 1, 1, CAST(N'06:00:00' AS Time), CAST(N'18:00:00' AS Time), 2)
INSERT [dbo].[DisponibilidadMedico] ([DisponibilidadID], [IDMedico], [IDDia], [Consultorio], [HorarioInicio], [HorarioFin], [IDSucursal]) VALUES (37, 10, 2, 1, CAST(N'06:00:00' AS Time), CAST(N'18:00:00' AS Time), 2)
INSERT [dbo].[DisponibilidadMedico] ([DisponibilidadID], [IDMedico], [IDDia], [Consultorio], [HorarioInicio], [HorarioFin], [IDSucursal]) VALUES (38, 10, 3, 1, CAST(N'06:00:00' AS Time), CAST(N'18:00:00' AS Time), 2)
INSERT [dbo].[DisponibilidadMedico] ([DisponibilidadID], [IDMedico], [IDDia], [Consultorio], [HorarioInicio], [HorarioFin], [IDSucursal]) VALUES (39, 10, 4, 1, CAST(N'06:00:00' AS Time), CAST(N'18:00:00' AS Time), 2)
INSERT [dbo].[DisponibilidadMedico] ([DisponibilidadID], [IDMedico], [IDDia], [Consultorio], [HorarioInicio], [HorarioFin], [IDSucursal]) VALUES (40, 10, 5, 1, CAST(N'06:00:00' AS Time), CAST(N'18:00:00' AS Time), 2)
INSERT [dbo].[DisponibilidadMedico] ([DisponibilidadID], [IDMedico], [IDDia], [Consultorio], [HorarioInicio], [HorarioFin], [IDSucursal]) VALUES (41, 11, 1, 1, CAST(N'06:00:00' AS Time), CAST(N'18:00:00' AS Time), 2)
INSERT [dbo].[DisponibilidadMedico] ([DisponibilidadID], [IDMedico], [IDDia], [Consultorio], [HorarioInicio], [HorarioFin], [IDSucursal]) VALUES (42, 11, 2, 1, CAST(N'06:00:00' AS Time), CAST(N'18:00:00' AS Time), 2)
INSERT [dbo].[DisponibilidadMedico] ([DisponibilidadID], [IDMedico], [IDDia], [Consultorio], [HorarioInicio], [HorarioFin], [IDSucursal]) VALUES (43, 11, 3, 1, CAST(N'06:00:00' AS Time), CAST(N'18:00:00' AS Time), 2)
INSERT [dbo].[DisponibilidadMedico] ([DisponibilidadID], [IDMedico], [IDDia], [Consultorio], [HorarioInicio], [HorarioFin], [IDSucursal]) VALUES (44, 11, 4, 1, CAST(N'06:00:00' AS Time), CAST(N'18:00:00' AS Time), 2)
INSERT [dbo].[DisponibilidadMedico] ([DisponibilidadID], [IDMedico], [IDDia], [Consultorio], [HorarioInicio], [HorarioFin], [IDSucursal]) VALUES (45, 11, 5, 1, CAST(N'06:00:00' AS Time), CAST(N'18:00:00' AS Time), 2)
INSERT [dbo].[DisponibilidadMedico] ([DisponibilidadID], [IDMedico], [IDDia], [Consultorio], [HorarioInicio], [HorarioFin], [IDSucursal]) VALUES (46, 12, 1, 1, CAST(N'06:00:00' AS Time), CAST(N'18:00:00' AS Time), 2)
INSERT [dbo].[DisponibilidadMedico] ([DisponibilidadID], [IDMedico], [IDDia], [Consultorio], [HorarioInicio], [HorarioFin], [IDSucursal]) VALUES (47, 12, 2, 1, CAST(N'06:00:00' AS Time), CAST(N'18:00:00' AS Time), 2)
INSERT [dbo].[DisponibilidadMedico] ([DisponibilidadID], [IDMedico], [IDDia], [Consultorio], [HorarioInicio], [HorarioFin], [IDSucursal]) VALUES (48, 12, 3, 1, CAST(N'06:00:00' AS Time), CAST(N'18:00:00' AS Time), 2)
INSERT [dbo].[DisponibilidadMedico] ([DisponibilidadID], [IDMedico], [IDDia], [Consultorio], [HorarioInicio], [HorarioFin], [IDSucursal]) VALUES (49, 12, 4, 1, CAST(N'06:00:00' AS Time), CAST(N'18:00:00' AS Time), 2)
INSERT [dbo].[DisponibilidadMedico] ([DisponibilidadID], [IDMedico], [IDDia], [Consultorio], [HorarioInicio], [HorarioFin], [IDSucursal]) VALUES (50, 13, 1, 1, CAST(N'06:00:00' AS Time), CAST(N'18:00:00' AS Time), 2)
INSERT [dbo].[DisponibilidadMedico] ([DisponibilidadID], [IDMedico], [IDDia], [Consultorio], [HorarioInicio], [HorarioFin], [IDSucursal]) VALUES (51, 13, 2, 1, CAST(N'06:00:00' AS Time), CAST(N'18:00:00' AS Time), 2)
INSERT [dbo].[DisponibilidadMedico] ([DisponibilidadID], [IDMedico], [IDDia], [Consultorio], [HorarioInicio], [HorarioFin], [IDSucursal]) VALUES (52, 13, 3, 1, CAST(N'06:00:00' AS Time), CAST(N'18:00:00' AS Time), 2)
INSERT [dbo].[DisponibilidadMedico] ([DisponibilidadID], [IDMedico], [IDDia], [Consultorio], [HorarioInicio], [HorarioFin], [IDSucursal]) VALUES (53, 13, 4, 1, CAST(N'06:00:00' AS Time), CAST(N'18:00:00' AS Time), 2)
INSERT [dbo].[DisponibilidadMedico] ([DisponibilidadID], [IDMedico], [IDDia], [Consultorio], [HorarioInicio], [HorarioFin], [IDSucursal]) VALUES (54, 13, 5, 1, CAST(N'06:00:00' AS Time), CAST(N'18:00:00' AS Time), 2)
INSERT [dbo].[DisponibilidadMedico] ([DisponibilidadID], [IDMedico], [IDDia], [Consultorio], [HorarioInicio], [HorarioFin], [IDSucursal]) VALUES (55, 14, 1, 1, CAST(N'06:00:00' AS Time), CAST(N'18:00:00' AS Time), 2)
INSERT [dbo].[DisponibilidadMedico] ([DisponibilidadID], [IDMedico], [IDDia], [Consultorio], [HorarioInicio], [HorarioFin], [IDSucursal]) VALUES (56, 14, 2, 1, CAST(N'06:00:00' AS Time), CAST(N'18:00:00' AS Time), 2)
INSERT [dbo].[DisponibilidadMedico] ([DisponibilidadID], [IDMedico], [IDDia], [Consultorio], [HorarioInicio], [HorarioFin], [IDSucursal]) VALUES (57, 14, 3, 1, CAST(N'06:00:00' AS Time), CAST(N'18:00:00' AS Time), 2)
INSERT [dbo].[DisponibilidadMedico] ([DisponibilidadID], [IDMedico], [IDDia], [Consultorio], [HorarioInicio], [HorarioFin], [IDSucursal]) VALUES (58, 14, 4, 1, CAST(N'06:00:00' AS Time), CAST(N'18:00:00' AS Time), 2)
INSERT [dbo].[DisponibilidadMedico] ([DisponibilidadID], [IDMedico], [IDDia], [Consultorio], [HorarioInicio], [HorarioFin], [IDSucursal]) VALUES (59, 14, 5, 1, CAST(N'06:00:00' AS Time), CAST(N'18:00:00' AS Time), 2)
INSERT [dbo].[DisponibilidadMedico] ([DisponibilidadID], [IDMedico], [IDDia], [Consultorio], [HorarioInicio], [HorarioFin], [IDSucursal]) VALUES (61, 8, 1, 1, CAST(N'00:00:00' AS Time), CAST(N'23:59:00' AS Time), 2)
INSERT [dbo].[DisponibilidadMedico] ([DisponibilidadID], [IDMedico], [IDDia], [Consultorio], [HorarioInicio], [HorarioFin], [IDSucursal]) VALUES (62, 8, 2, 1, CAST(N'00:00:00' AS Time), CAST(N'23:59:00' AS Time), 2)
INSERT [dbo].[DisponibilidadMedico] ([DisponibilidadID], [IDMedico], [IDDia], [Consultorio], [HorarioInicio], [HorarioFin], [IDSucursal]) VALUES (63, 8, 3, 1, CAST(N'00:00:00' AS Time), CAST(N'23:59:00' AS Time), 2)
INSERT [dbo].[DisponibilidadMedico] ([DisponibilidadID], [IDMedico], [IDDia], [Consultorio], [HorarioInicio], [HorarioFin], [IDSucursal]) VALUES (65, 8, 3, 1, CAST(N'00:00:00' AS Time), CAST(N'23:59:00' AS Time), 2)
INSERT [dbo].[DisponibilidadMedico] ([DisponibilidadID], [IDMedico], [IDDia], [Consultorio], [HorarioInicio], [HorarioFin], [IDSucursal]) VALUES (66, 8, 4, 1, CAST(N'00:00:00' AS Time), CAST(N'23:59:00' AS Time), 2)
INSERT [dbo].[DisponibilidadMedico] ([DisponibilidadID], [IDMedico], [IDDia], [Consultorio], [HorarioInicio], [HorarioFin], [IDSucursal]) VALUES (67, 8, 5, 1, CAST(N'00:00:00' AS Time), CAST(N'23:59:00' AS Time), 2)
INSERT [dbo].[DisponibilidadMedico] ([DisponibilidadID], [IDMedico], [IDDia], [Consultorio], [HorarioInicio], [HorarioFin], [IDSucursal]) VALUES (69, 8, 6, 1, CAST(N'00:00:00' AS Time), CAST(N'23:59:00' AS Time), 2)
INSERT [dbo].[DisponibilidadMedico] ([DisponibilidadID], [IDMedico], [IDDia], [Consultorio], [HorarioInicio], [HorarioFin], [IDSucursal]) VALUES (70, 8, 7, 1, CAST(N'00:00:00' AS Time), CAST(N'23:59:00' AS Time), 2)
SET IDENTITY_INSERT [dbo].[DisponibilidadMedico] OFF
SET IDENTITY_INSERT [dbo].[Especialidad] ON 

INSERT [dbo].[Especialidad] ([EspecialidadId], [EspecialidadDescripcion]) VALUES (1, N'Cardiología                                       ')
INSERT [dbo].[Especialidad] ([EspecialidadId], [EspecialidadDescripcion]) VALUES (2, N'Cirugía                                           ')
INSERT [dbo].[Especialidad] ([EspecialidadId], [EspecialidadDescripcion]) VALUES (3, N'Dermatologia                                      ')
INSERT [dbo].[Especialidad] ([EspecialidadId], [EspecialidadDescripcion]) VALUES (8, N'Guardia                                           ')
SET IDENTITY_INSERT [dbo].[Especialidad] OFF
SET IDENTITY_INSERT [dbo].[FechaTurno] ON 

INSERT [dbo].[FechaTurno] ([FechaTurnoID], [IDDia], [IDHorario], [Fecha]) VALUES (1, 1, 1, CAST(N'2022-11-21' AS Date))
INSERT [dbo].[FechaTurno] ([FechaTurnoID], [IDDia], [IDHorario], [Fecha]) VALUES (2, 5, 3, CAST(N'2022-12-23' AS Date))
INSERT [dbo].[FechaTurno] ([FechaTurnoID], [IDDia], [IDHorario], [Fecha]) VALUES (3, 2, 5, CAST(N'2022-12-20' AS Date))
INSERT [dbo].[FechaTurno] ([FechaTurnoID], [IDDia], [IDHorario], [Fecha]) VALUES (4, 1, 10, CAST(N'2023-02-20' AS Date))
SET IDENTITY_INSERT [dbo].[FechaTurno] OFF
SET IDENTITY_INSERT [dbo].[Horario] ON 

INSERT [dbo].[Horario] ([HorarioID], [Hora]) VALUES (1, CAST(N'06:00:00' AS Time))
INSERT [dbo].[Horario] ([HorarioID], [Hora]) VALUES (2, CAST(N'06:30:00' AS Time))
INSERT [dbo].[Horario] ([HorarioID], [Hora]) VALUES (3, CAST(N'07:00:00' AS Time))
INSERT [dbo].[Horario] ([HorarioID], [Hora]) VALUES (4, CAST(N'07:30:00' AS Time))
INSERT [dbo].[Horario] ([HorarioID], [Hora]) VALUES (5, CAST(N'08:00:00' AS Time))
INSERT [dbo].[Horario] ([HorarioID], [Hora]) VALUES (6, CAST(N'08:30:00' AS Time))
INSERT [dbo].[Horario] ([HorarioID], [Hora]) VALUES (7, CAST(N'09:00:00' AS Time))
INSERT [dbo].[Horario] ([HorarioID], [Hora]) VALUES (8, CAST(N'09:30:00' AS Time))
INSERT [dbo].[Horario] ([HorarioID], [Hora]) VALUES (9, CAST(N'10:00:00' AS Time))
INSERT [dbo].[Horario] ([HorarioID], [Hora]) VALUES (10, CAST(N'10:30:00' AS Time))
INSERT [dbo].[Horario] ([HorarioID], [Hora]) VALUES (11, CAST(N'11:00:00' AS Time))
INSERT [dbo].[Horario] ([HorarioID], [Hora]) VALUES (12, CAST(N'11:30:00' AS Time))
INSERT [dbo].[Horario] ([HorarioID], [Hora]) VALUES (13, CAST(N'12:00:00' AS Time))
INSERT [dbo].[Horario] ([HorarioID], [Hora]) VALUES (14, CAST(N'12:30:00' AS Time))
INSERT [dbo].[Horario] ([HorarioID], [Hora]) VALUES (15, CAST(N'13:30:00' AS Time))
INSERT [dbo].[Horario] ([HorarioID], [Hora]) VALUES (16, CAST(N'14:00:00' AS Time))
INSERT [dbo].[Horario] ([HorarioID], [Hora]) VALUES (17, CAST(N'14:30:00' AS Time))
INSERT [dbo].[Horario] ([HorarioID], [Hora]) VALUES (18, CAST(N'15:00:00' AS Time))
INSERT [dbo].[Horario] ([HorarioID], [Hora]) VALUES (19, CAST(N'15:30:00' AS Time))
INSERT [dbo].[Horario] ([HorarioID], [Hora]) VALUES (20, CAST(N'16:00:00' AS Time))
INSERT [dbo].[Horario] ([HorarioID], [Hora]) VALUES (21, CAST(N'16:30:00' AS Time))
INSERT [dbo].[Horario] ([HorarioID], [Hora]) VALUES (22, CAST(N'17:00:00' AS Time))
INSERT [dbo].[Horario] ([HorarioID], [Hora]) VALUES (23, CAST(N'17:30:00' AS Time))
INSERT [dbo].[Horario] ([HorarioID], [Hora]) VALUES (24, CAST(N'18:00:00' AS Time))
INSERT [dbo].[Horario] ([HorarioID], [Hora]) VALUES (25, CAST(N'18:30:00' AS Time))
INSERT [dbo].[Horario] ([HorarioID], [Hora]) VALUES (26, CAST(N'19:00:00' AS Time))
INSERT [dbo].[Horario] ([HorarioID], [Hora]) VALUES (27, CAST(N'19:30:00' AS Time))
INSERT [dbo].[Horario] ([HorarioID], [Hora]) VALUES (28, CAST(N'20:00:00' AS Time))
INSERT [dbo].[Horario] ([HorarioID], [Hora]) VALUES (29, CAST(N'20:30:00' AS Time))
INSERT [dbo].[Horario] ([HorarioID], [Hora]) VALUES (30, CAST(N'21:00:00' AS Time))
INSERT [dbo].[Horario] ([HorarioID], [Hora]) VALUES (31, CAST(N'21:30:00' AS Time))
INSERT [dbo].[Horario] ([HorarioID], [Hora]) VALUES (32, CAST(N'22:00:00' AS Time))
INSERT [dbo].[Horario] ([HorarioID], [Hora]) VALUES (33, CAST(N'22:30:00' AS Time))
INSERT [dbo].[Horario] ([HorarioID], [Hora]) VALUES (34, CAST(N'23:00:00' AS Time))
INSERT [dbo].[Horario] ([HorarioID], [Hora]) VALUES (35, CAST(N'23:30:00' AS Time))
SET IDENTITY_INSERT [dbo].[Horario] OFF
SET IDENTITY_INSERT [dbo].[Localidad] ON 

INSERT [dbo].[Localidad] ([LocalidadId], [LocalidadDescripcion], [IDProvincia]) VALUES (1, N'Arroyo Seco                                       ', 1)
INSERT [dbo].[Localidad] ([LocalidadId], [LocalidadDescripcion], [IDProvincia]) VALUES (2, N'Rosario                                           ', 1)
INSERT [dbo].[Localidad] ([LocalidadId], [LocalidadDescripcion], [IDProvincia]) VALUES (3, N'Villa Constitucion                                ', 1)
SET IDENTITY_INSERT [dbo].[Localidad] OFF
SET IDENTITY_INSERT [dbo].[Medico] ON 

INSERT [dbo].[Medico] ([MedicoID], [IDEspecialidad], [IDUsuario]) VALUES (1, 1, 6)
INSERT [dbo].[Medico] ([MedicoID], [IDEspecialidad], [IDUsuario]) VALUES (2, 1, 7)
INSERT [dbo].[Medico] ([MedicoID], [IDEspecialidad], [IDUsuario]) VALUES (3, 1, 8)
INSERT [dbo].[Medico] ([MedicoID], [IDEspecialidad], [IDUsuario]) VALUES (4, 1, 9)
INSERT [dbo].[Medico] ([MedicoID], [IDEspecialidad], [IDUsuario]) VALUES (5, 1, 10)
INSERT [dbo].[Medico] ([MedicoID], [IDEspecialidad], [IDUsuario]) VALUES (6, 2, 11)
INSERT [dbo].[Medico] ([MedicoID], [IDEspecialidad], [IDUsuario]) VALUES (7, 2, 12)
INSERT [dbo].[Medico] ([MedicoID], [IDEspecialidad], [IDUsuario]) VALUES (8, 8, 13)
INSERT [dbo].[Medico] ([MedicoID], [IDEspecialidad], [IDUsuario]) VALUES (9, 2, 14)
INSERT [dbo].[Medico] ([MedicoID], [IDEspecialidad], [IDUsuario]) VALUES (10, 3, 15)
INSERT [dbo].[Medico] ([MedicoID], [IDEspecialidad], [IDUsuario]) VALUES (11, 3, 16)
INSERT [dbo].[Medico] ([MedicoID], [IDEspecialidad], [IDUsuario]) VALUES (12, 3, 17)
INSERT [dbo].[Medico] ([MedicoID], [IDEspecialidad], [IDUsuario]) VALUES (13, 3, 18)
INSERT [dbo].[Medico] ([MedicoID], [IDEspecialidad], [IDUsuario]) VALUES (14, 3, 19)
SET IDENTITY_INSERT [dbo].[Medico] OFF
SET IDENTITY_INSERT [dbo].[MedicoSucursal] ON 

INSERT [dbo].[MedicoSucursal] ([IDMedicoSucursal], [IDMedico], [IDSucursal]) VALUES (1, 1, 2)
INSERT [dbo].[MedicoSucursal] ([IDMedicoSucursal], [IDMedico], [IDSucursal]) VALUES (2, 2, 2)
INSERT [dbo].[MedicoSucursal] ([IDMedicoSucursal], [IDMedico], [IDSucursal]) VALUES (3, 3, 2)
INSERT [dbo].[MedicoSucursal] ([IDMedicoSucursal], [IDMedico], [IDSucursal]) VALUES (4, 4, 2)
INSERT [dbo].[MedicoSucursal] ([IDMedicoSucursal], [IDMedico], [IDSucursal]) VALUES (5, 5, 2)
INSERT [dbo].[MedicoSucursal] ([IDMedicoSucursal], [IDMedico], [IDSucursal]) VALUES (6, 6, 2)
INSERT [dbo].[MedicoSucursal] ([IDMedicoSucursal], [IDMedico], [IDSucursal]) VALUES (7, 7, 2)
INSERT [dbo].[MedicoSucursal] ([IDMedicoSucursal], [IDMedico], [IDSucursal]) VALUES (8, 8, 2)
INSERT [dbo].[MedicoSucursal] ([IDMedicoSucursal], [IDMedico], [IDSucursal]) VALUES (9, 9, 2)
INSERT [dbo].[MedicoSucursal] ([IDMedicoSucursal], [IDMedico], [IDSucursal]) VALUES (10, 10, 2)
INSERT [dbo].[MedicoSucursal] ([IDMedicoSucursal], [IDMedico], [IDSucursal]) VALUES (11, 11, 2)
INSERT [dbo].[MedicoSucursal] ([IDMedicoSucursal], [IDMedico], [IDSucursal]) VALUES (12, 12, 2)
INSERT [dbo].[MedicoSucursal] ([IDMedicoSucursal], [IDMedico], [IDSucursal]) VALUES (13, 13, 2)
INSERT [dbo].[MedicoSucursal] ([IDMedicoSucursal], [IDMedico], [IDSucursal]) VALUES (14, 14, 2)
SET IDENTITY_INSERT [dbo].[MedicoSucursal] OFF
SET IDENTITY_INSERT [dbo].[ObraSocial] ON 

INSERT [dbo].[ObraSocial] ([ObraSocialId], [ObraSocialDescripcion]) VALUES (1, N'OSDE                                              ')
INSERT [dbo].[ObraSocial] ([ObraSocialId], [ObraSocialDescripcion]) VALUES (2, N'Swiss Medical                                     ')
SET IDENTITY_INSERT [dbo].[ObraSocial] OFF
SET IDENTITY_INSERT [dbo].[Perfil] ON 

INSERT [dbo].[Perfil] ([PerfilId], [PerfilDescripcion], [PerfilHabilitado]) VALUES (1, N'Admin     ', 1)
INSERT [dbo].[Perfil] ([PerfilId], [PerfilDescripcion], [PerfilHabilitado]) VALUES (2, N'Medico    ', 1)
INSERT [dbo].[Perfil] ([PerfilId], [PerfilDescripcion], [PerfilHabilitado]) VALUES (3, N'Paciente  ', 1)
SET IDENTITY_INSERT [dbo].[Perfil] OFF
SET IDENTITY_INSERT [dbo].[PlanObraSocial] ON 

INSERT [dbo].[PlanObraSocial] ([PlanId], [PlanDescripcion], [IDObraSocial]) VALUES (1, N'Familiar                                          ', 1)
INSERT [dbo].[PlanObraSocial] ([PlanId], [PlanDescripcion], [IDObraSocial]) VALUES (2, N'Oro                                               ', 1)
INSERT [dbo].[PlanObraSocial] ([PlanId], [PlanDescripcion], [IDObraSocial]) VALUES (3, N'Plata                                             ', 1)
INSERT [dbo].[PlanObraSocial] ([PlanId], [PlanDescripcion], [IDObraSocial]) VALUES (4, N'Bronce                                            ', 1)
INSERT [dbo].[PlanObraSocial] ([PlanId], [PlanDescripcion], [IDObraSocial]) VALUES (5, N'Oro                                               ', 2)
INSERT [dbo].[PlanObraSocial] ([PlanId], [PlanDescripcion], [IDObraSocial]) VALUES (6, N'Plata                                             ', 2)
INSERT [dbo].[PlanObraSocial] ([PlanId], [PlanDescripcion], [IDObraSocial]) VALUES (7, N'Bronce                                            ', 2)
SET IDENTITY_INSERT [dbo].[PlanObraSocial] OFF
SET IDENTITY_INSERT [dbo].[Provincia] ON 

INSERT [dbo].[Provincia] ([ProvinciaId], [ProvinciaDescripcion]) VALUES (1, N'Santa Fe')
SET IDENTITY_INSERT [dbo].[Provincia] OFF
INSERT [dbo].[ServidorMail] ([ID], [Mail], [Pass]) VALUES (1, N'mailfortestingaus@gmail.com', N'hnmkjvpilnmktujj')
SET IDENTITY_INSERT [dbo].[Sucursal] ON 

INSERT [dbo].[Sucursal] ([SucursalId], [SucursalDescripcion], [IDLocalidad]) VALUES (1, N'Sede Arroyo Seco', 1)
INSERT [dbo].[Sucursal] ([SucursalId], [SucursalDescripcion], [IDLocalidad]) VALUES (2, N'Sede Rosario Centro', 2)
SET IDENTITY_INSERT [dbo].[Sucursal] OFF
SET IDENTITY_INSERT [dbo].[Turno] ON 

INSERT [dbo].[Turno] ([TurnoId], [IDFechaTurno], [IDMedico], [IDEspecialidad], [IDProvincia], [IDLocalidad], [IDSucursal], [IDUsuario]) VALUES (1, 1, 1, 1, 1, 2, 2, 1)
INSERT [dbo].[Turno] ([TurnoId], [IDFechaTurno], [IDMedico], [IDEspecialidad], [IDProvincia], [IDLocalidad], [IDSucursal], [IDUsuario]) VALUES (5, 2, 1, 1, 1, 2, 2, 24)
INSERT [dbo].[Turno] ([TurnoId], [IDFechaTurno], [IDMedico], [IDEspecialidad], [IDProvincia], [IDLocalidad], [IDSucursal], [IDUsuario]) VALUES (7, 4, 6, 2, 1, 2, 2, 28)
SET IDENTITY_INSERT [dbo].[Turno] OFF
SET IDENTITY_INSERT [dbo].[Usuario] ON 

INSERT [dbo].[Usuario] ([UsuarioID], [IDAfiliado], [UsuarioContraseña], [UsuarioEmail], [IDPerfil], [isMedico]) VALUES (1, 1, N'123456', N'asd@gmail.com', 3, 0)
INSERT [dbo].[Usuario] ([UsuarioID], [IDAfiliado], [UsuarioContraseña], [UsuarioEmail], [IDPerfil], [isMedico]) VALUES (3, 2, N'123456', N'tosoniesteban16@gmail.com', 3, 0)
INSERT [dbo].[Usuario] ([UsuarioID], [IDAfiliado], [UsuarioContraseña], [UsuarioEmail], [IDPerfil], [isMedico]) VALUES (4, 3, N'123456', N'thiagoqua16@gmail.com', 3, 0)
INSERT [dbo].[Usuario] ([UsuarioID], [IDAfiliado], [UsuarioContraseña], [UsuarioEmail], [IDPerfil], [isMedico]) VALUES (5, 4, N'123456', N'admin@gmail.com', 1, 0)
INSERT [dbo].[Usuario] ([UsuarioID], [IDAfiliado], [UsuarioContraseña], [UsuarioEmail], [IDPerfil], [isMedico]) VALUES (6, 5, N'123456', N'jorge.garcia@gmail.com', 3, 1)
INSERT [dbo].[Usuario] ([UsuarioID], [IDAfiliado], [UsuarioContraseña], [UsuarioEmail], [IDPerfil], [isMedico]) VALUES (7, 6, N'123456', N'lucia.lorenzini@gmail.com', 3, 1)
INSERT [dbo].[Usuario] ([UsuarioID], [IDAfiliado], [UsuarioContraseña], [UsuarioEmail], [IDPerfil], [isMedico]) VALUES (8, 7, N'123456', N'julia.benaldi@gmail.com', 3, 1)
INSERT [dbo].[Usuario] ([UsuarioID], [IDAfiliado], [UsuarioContraseña], [UsuarioEmail], [IDPerfil], [isMedico]) VALUES (9, 8, N'123456', N'rocio.juarez@gmail.com', 3, 1)
INSERT [dbo].[Usuario] ([UsuarioID], [IDAfiliado], [UsuarioContraseña], [UsuarioEmail], [IDPerfil], [isMedico]) VALUES (10, 9, N'123456', N'omar.fiore@gmail.com', 3, 1)
INSERT [dbo].[Usuario] ([UsuarioID], [IDAfiliado], [UsuarioContraseña], [UsuarioEmail], [IDPerfil], [isMedico]) VALUES (11, 10, N'123456', N'oscar.perez@gmail.com', 3, 1)
INSERT [dbo].[Usuario] ([UsuarioID], [IDAfiliado], [UsuarioContraseña], [UsuarioEmail], [IDPerfil], [isMedico]) VALUES (12, 11, N'123456', N'matias.benedetti@gmail.com', 3, 1)
INSERT [dbo].[Usuario] ([UsuarioID], [IDAfiliado], [UsuarioContraseña], [UsuarioEmail], [IDPerfil], [isMedico]) VALUES (13, 12, N'123456', N'pedro.nuñez@gmail.com', 2, 1)
INSERT [dbo].[Usuario] ([UsuarioID], [IDAfiliado], [UsuarioContraseña], [UsuarioEmail], [IDPerfil], [isMedico]) VALUES (14, 13, N'123456', N'jose1.nuñez@gmail.com', 3, 1)
INSERT [dbo].[Usuario] ([UsuarioID], [IDAfiliado], [UsuarioContraseña], [UsuarioEmail], [IDPerfil], [isMedico]) VALUES (15, 14, N'123456', N'jose2.nuñez@gmail.com', 3, 1)
INSERT [dbo].[Usuario] ([UsuarioID], [IDAfiliado], [UsuarioContraseña], [UsuarioEmail], [IDPerfil], [isMedico]) VALUES (16, 15, N'123456', N'jose.lopez@gmail.com', 3, 1)
INSERT [dbo].[Usuario] ([UsuarioID], [IDAfiliado], [UsuarioContraseña], [UsuarioEmail], [IDPerfil], [isMedico]) VALUES (17, 16, N'123456', N'lucia.perez@gmail.com', 3, 1)
INSERT [dbo].[Usuario] ([UsuarioID], [IDAfiliado], [UsuarioContraseña], [UsuarioEmail], [IDPerfil], [isMedico]) VALUES (18, 17, N'123456', N'sofia.gonzalez@gmail.com', 3, 1)
INSERT [dbo].[Usuario] ([UsuarioID], [IDAfiliado], [UsuarioContraseña], [UsuarioEmail], [IDPerfil], [isMedico]) VALUES (19, 18, N'123456', N'lorena.ricci@gmail.com', 3, 1)
INSERT [dbo].[Usuario] ([UsuarioID], [IDAfiliado], [UsuarioContraseña], [UsuarioEmail], [IDPerfil], [isMedico]) VALUES (20, 19, N'8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', N'', 1, 0)
INSERT [dbo].[Usuario] ([UsuarioID], [IDAfiliado], [UsuarioContraseña], [UsuarioEmail], [IDPerfil], [isMedico]) VALUES (23, 21, N'8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918', N'', 1, 0)
INSERT [dbo].[Usuario] ([UsuarioID], [IDAfiliado], [UsuarioContraseña], [UsuarioEmail], [IDPerfil], [isMedico]) VALUES (24, 22, N'8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918', N'', 1, 0)
INSERT [dbo].[Usuario] ([UsuarioID], [IDAfiliado], [UsuarioContraseña], [UsuarioEmail], [IDPerfil], [isMedico]) VALUES (25, 23, N'8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918', N'asd@gmail.com', 2, 1)
INSERT [dbo].[Usuario] ([UsuarioID], [IDAfiliado], [UsuarioContraseña], [UsuarioEmail], [IDPerfil], [isMedico]) VALUES (26, 24, N'8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', N'asd1@gmail.com', 1, 0)
INSERT [dbo].[Usuario] ([UsuarioID], [IDAfiliado], [UsuarioContraseña], [UsuarioEmail], [IDPerfil], [isMedico]) VALUES (27, 24, N'8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', N'', 1, 0)
INSERT [dbo].[Usuario] ([UsuarioID], [IDAfiliado], [UsuarioContraseña], [UsuarioEmail], [IDPerfil], [isMedico]) VALUES (28, 1, N'8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918', N'', 1, 0)
INSERT [dbo].[Usuario] ([UsuarioID], [IDAfiliado], [UsuarioContraseña], [UsuarioEmail], [IDPerfil], [isMedico]) VALUES (29, 12, N'8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918', N'florengalvan@gmail.com', 1, 0)
SET IDENTITY_INSERT [dbo].[Usuario] OFF
ALTER TABLE [dbo].[Afiliado]  WITH CHECK ADD  CONSTRAINT [FK_Afiliado_Localidad] FOREIGN KEY([IDLocalidad])
REFERENCES [dbo].[Localidad] ([LocalidadId])
GO
ALTER TABLE [dbo].[Afiliado] CHECK CONSTRAINT [FK_Afiliado_Localidad]
GO
ALTER TABLE [dbo].[Afiliado]  WITH CHECK ADD  CONSTRAINT [FK_Afiliado_Plan] FOREIGN KEY([IDPlan])
REFERENCES [dbo].[PlanObraSocial] ([PlanId])
GO
ALTER TABLE [dbo].[Afiliado] CHECK CONSTRAINT [FK_Afiliado_Plan]
GO
ALTER TABLE [dbo].[DisponibilidadMedico]  WITH CHECK ADD  CONSTRAINT [FK_DisponibilidadMedico_Dia] FOREIGN KEY([IDDia])
REFERENCES [dbo].[Dia] ([DiaID])
GO
ALTER TABLE [dbo].[DisponibilidadMedico] CHECK CONSTRAINT [FK_DisponibilidadMedico_Dia]
GO
ALTER TABLE [dbo].[DisponibilidadMedico]  WITH CHECK ADD  CONSTRAINT [FK_DisponibilidadMedico_Medico] FOREIGN KEY([IDMedico])
REFERENCES [dbo].[Medico] ([MedicoID])
GO
ALTER TABLE [dbo].[DisponibilidadMedico] CHECK CONSTRAINT [FK_DisponibilidadMedico_Medico]
GO
ALTER TABLE [dbo].[DisponibilidadMedico]  WITH CHECK ADD  CONSTRAINT [FK_DisponibilidadMedico_Sucursal] FOREIGN KEY([IDSucursal])
REFERENCES [dbo].[Sucursal] ([SucursalId])
GO
ALTER TABLE [dbo].[DisponibilidadMedico] CHECK CONSTRAINT [FK_DisponibilidadMedico_Sucursal]
GO
ALTER TABLE [dbo].[FechaTurno]  WITH CHECK ADD  CONSTRAINT [FK_FechaTurno_Dia] FOREIGN KEY([IDDia])
REFERENCES [dbo].[Dia] ([DiaID])
GO
ALTER TABLE [dbo].[FechaTurno] CHECK CONSTRAINT [FK_FechaTurno_Dia]
GO
ALTER TABLE [dbo].[FechaTurno]  WITH CHECK ADD  CONSTRAINT [FK_FechaTurno_Horario] FOREIGN KEY([IDHorario])
REFERENCES [dbo].[Horario] ([HorarioID])
GO
ALTER TABLE [dbo].[FechaTurno] CHECK CONSTRAINT [FK_FechaTurno_Horario]
GO
ALTER TABLE [dbo].[Localidad]  WITH CHECK ADD  CONSTRAINT [FK_Localidad_Provincia] FOREIGN KEY([IDProvincia])
REFERENCES [dbo].[Provincia] ([ProvinciaId])
GO
ALTER TABLE [dbo].[Localidad] CHECK CONSTRAINT [FK_Localidad_Provincia]
GO
ALTER TABLE [dbo].[Medico]  WITH CHECK ADD  CONSTRAINT [FK_Especialidad_Medico] FOREIGN KEY([IDEspecialidad])
REFERENCES [dbo].[Especialidad] ([EspecialidadId])
GO
ALTER TABLE [dbo].[Medico] CHECK CONSTRAINT [FK_Especialidad_Medico]
GO
ALTER TABLE [dbo].[Medico]  WITH CHECK ADD  CONSTRAINT [FK_Medico_Usuario] FOREIGN KEY([IDUsuario])
REFERENCES [dbo].[Usuario] ([UsuarioID])
GO
ALTER TABLE [dbo].[Medico] CHECK CONSTRAINT [FK_Medico_Usuario]
GO
ALTER TABLE [dbo].[MedicoSucursal]  WITH CHECK ADD  CONSTRAINT [FK_MedicoSucursal_Medico] FOREIGN KEY([IDMedico])
REFERENCES [dbo].[Medico] ([MedicoID])
GO
ALTER TABLE [dbo].[MedicoSucursal] CHECK CONSTRAINT [FK_MedicoSucursal_Medico]
GO
ALTER TABLE [dbo].[MedicoSucursal]  WITH CHECK ADD  CONSTRAINT [FK_MedicoSucursal_Sucursal] FOREIGN KEY([IDSucursal])
REFERENCES [dbo].[Sucursal] ([SucursalId])
GO
ALTER TABLE [dbo].[MedicoSucursal] CHECK CONSTRAINT [FK_MedicoSucursal_Sucursal]
GO
ALTER TABLE [dbo].[PlanObraSocial]  WITH CHECK ADD  CONSTRAINT [FK_Plan_ObraSocial] FOREIGN KEY([IDObraSocial])
REFERENCES [dbo].[ObraSocial] ([ObraSocialId])
GO
ALTER TABLE [dbo].[PlanObraSocial] CHECK CONSTRAINT [FK_Plan_ObraSocial]
GO
ALTER TABLE [dbo].[Sucursal]  WITH CHECK ADD  CONSTRAINT [FK_Sucursal_Localidad] FOREIGN KEY([IDLocalidad])
REFERENCES [dbo].[Localidad] ([LocalidadId])
GO
ALTER TABLE [dbo].[Sucursal] CHECK CONSTRAINT [FK_Sucursal_Localidad]
GO
ALTER TABLE [dbo].[Turno]  WITH CHECK ADD  CONSTRAINT [FK_Turno_FechaTurno] FOREIGN KEY([IDFechaTurno])
REFERENCES [dbo].[FechaTurno] ([FechaTurnoID])
GO
ALTER TABLE [dbo].[Turno] CHECK CONSTRAINT [FK_Turno_FechaTurno]
GO
ALTER TABLE [dbo].[Turno]  WITH CHECK ADD  CONSTRAINT [FK_Turno_Localidad] FOREIGN KEY([IDLocalidad])
REFERENCES [dbo].[Localidad] ([LocalidadId])
GO
ALTER TABLE [dbo].[Turno] CHECK CONSTRAINT [FK_Turno_Localidad]
GO
ALTER TABLE [dbo].[Turno]  WITH CHECK ADD  CONSTRAINT [FK_Turno_Medico] FOREIGN KEY([IDMedico])
REFERENCES [dbo].[Usuario] ([UsuarioID])
GO
ALTER TABLE [dbo].[Turno] CHECK CONSTRAINT [FK_Turno_Medico]
GO
ALTER TABLE [dbo].[Turno]  WITH CHECK ADD  CONSTRAINT [FK_Turno_Provincia] FOREIGN KEY([IDProvincia])
REFERENCES [dbo].[Provincia] ([ProvinciaId])
GO
ALTER TABLE [dbo].[Turno] CHECK CONSTRAINT [FK_Turno_Provincia]
GO
ALTER TABLE [dbo].[Turno]  WITH CHECK ADD  CONSTRAINT [FK_Turno_Sucursal] FOREIGN KEY([IDSucursal])
REFERENCES [dbo].[Sucursal] ([SucursalId])
GO
ALTER TABLE [dbo].[Turno] CHECK CONSTRAINT [FK_Turno_Sucursal]
GO
ALTER TABLE [dbo].[Turno]  WITH CHECK ADD  CONSTRAINT [FK_Turno_Usuario] FOREIGN KEY([IDUsuario])
REFERENCES [dbo].[Usuario] ([UsuarioID])
GO
ALTER TABLE [dbo].[Turno] CHECK CONSTRAINT [FK_Turno_Usuario]
GO
ALTER TABLE [dbo].[Usuario]  WITH CHECK ADD  CONSTRAINT [FK_Usuario_Afiliado] FOREIGN KEY([IDAfiliado])
REFERENCES [dbo].[Afiliado] ([AfiliadoID])
GO
ALTER TABLE [dbo].[Usuario] CHECK CONSTRAINT [FK_Usuario_Afiliado]
GO
ALTER TABLE [dbo].[Usuario]  WITH CHECK ADD  CONSTRAINT [FK_Usuario_Perfil] FOREIGN KEY([IDPerfil])
REFERENCES [dbo].[Perfil] ([PerfilId])
GO
ALTER TABLE [dbo].[Usuario] CHECK CONSTRAINT [FK_Usuario_Perfil]
GO
