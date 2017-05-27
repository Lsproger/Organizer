create table Notes
(
	NoteDate nvarchar(30),
	NoteDescription nvarchar(300),
	StudentId int,
	constraint PK_StudId_NoteDate primary key(NoteDate, StudentId)
);