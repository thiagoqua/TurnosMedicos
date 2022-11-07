USE [AplicacionWeb]
GO
INSERT [dbo].[Especialidad] ([EspecialidadId], [EspecialidadDescripcion]) VALUES (1, N'Otorrinolaringólogo                               ')
GO
INSERT [dbo].[Provincia] ([ProvinciaId], [ProvinciaDescripcion]) VALUES (1, N'Santa Fe')
GO
INSERT [dbo].[Localidad] ([LocalidadId], [LocalidadDescripcion], [IDProvincia]) VALUES (1, N'Rosario                                           ', 1)
INSERT [dbo].[Localidad] ([LocalidadId], [LocalidadDescripcion], [IDProvincia]) VALUES (2, N'Arroyo Seco                                       ', 1)
GO
INSERT [dbo].[Perfil] ([PerfilId], [PerfilDescripcion], [PerfilHabilitado]) VALUES (1, N'admin     ', 1)
GO
INSERT [dbo].[Usuario] ([UsuarioID], [UsuarioDNI], [UsuarioContraseña], [UsuarioNombre], [UsuarioApellido], [UsuarioEmail], [IDLocalidad], [IDPerfil], [IDMedico], [IDPaciente]) VALUES (1, N'123456    ', N'admin', N'Hernestino          ', N'El Admin Pa', N'admin@example.com', 1, 1, 1, NULL)
GO
INSERT [dbo].[Medico] ([MedicoID], [IDEspecialidad]) VALUES (1, 1)
GO
