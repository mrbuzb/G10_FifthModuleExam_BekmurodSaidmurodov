create table ToDoListEntity(
Id bigint Primary Key identity(1,1),
Title Nvarchar(200) Not Null,
Discription Nvarchar(1000) Not Null,
IsCompleted bit default(0),
CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
DueDate datetime not null
);


create procedure AddToDoList
@Title nvarchar(200),
@Discription nvarchar(1000),
@IsCompleted bit,
@CreatedAt datetime,
@DueDate datetime
as
INSERT INTO ToDoListEntity (Title, Discription, IsCompleted, CreatedAt, DueDate) 
OUTPUT INSERTED.Id
VALUES (@Title, @Discription, @IsCompleted, @CreatedAt, @DueDate);



create procedure DeleteToDOList
@Id bigint
as
delete from ToDoListEntity
where Id = @Id


create procedure GetDoTOLists
as
select * from ToDoListEntity

create procedure GetToTOListByID
@Id bigint
as
select * from ToDoListEntity 
where Id = @Id



create procedure GetToDoListsPagenation
@Offset int,
@PageSize int
as
SELECT * FROM ToDoListEntity
ORDER BY Id
OFFSET @Offset ROWS
FETCH NEXT @PageSize ROWS ONLY;


create procedure UpdateToDoList
@Id bigint,
@Title nvarchar(200),
@Discription nvarchar(1000),
@IsCompleted bit,
@CreatedAt datetime,
@DueDate datetime
as
update ToDoListEntity
set Title = @Title,Discription = @Discription,IsCompleted = @IsCompleted,CreatedAt = @CreatedAt,DueDate = @DueDate
where Id = @Id


create procedure GetToDoListByDueDate
@DueDate datetime
as
select * from ToDoListEntity 
WHERE CAST(DueDate AS DATE) = CAST(@DueDate AS DATE)



create procedure SelectAllCompletedToDoListPagenation
@Offset INT, @PageSize INT
AS
SELECT * FROM ToDoListEntity
where IsCompleted = 1
ORDER BY Id
OFFSET @Offset ROWS
FETCH NEXT @PageSize ROWS ONLY;



create procedure SelectAllInCompletedToDoListPagenation
@Offset INT, @PageSize INT
AS
SELECT * FROM ToDoListEntity
where IsCompleted = 0
ORDER BY Id
OFFSET @Offset ROWS
FETCH NEXT @PageSize ROWS ONLY;



select * from ToDoListEntity


CREATE FUNCTION CountOfToDoLists()
RETURNS INT
AS
BEGIN
    DECLARE @counts INT;

    SELECT @counts = COUNT(*) FROM ToDoListEntity;

    RETURN @counts;
END;












