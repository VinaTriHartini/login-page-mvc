create database Projects

create table users(
UserID int not null primary key identity,
Username nvarchar(50),
Email nvarchar(50),
UserPassword nvarchar(50)
)

select * from tblMeals where MealId = 3

sp_Register 'Jack','j@jack.com','jpword'

create procedure sp_Register
@username nvarchar(50),
@email nvarchar(50),
@password nvarchar(50)

as
begin
	insert into users(Username, Email, UserPassword) values (@username, @email, @password)
end

sp_Login Jack, jpword

create procedure sp_Login
@username nvarchar(50),
@password nvarchar(50)

as
begin
	select * from users where Username = @username and UserPassword = @password
end

select * from tblMeals

create table tblMeals(
MealId int identity primary key,
Name nvarchar(50),
Price int,
Description nvarchar(50),
image nvarchar(50)

)

insert into tblMeals (Name, Price, Description, image) values ('Burger', 40, 'Beef Burger', 'images/burger.png')
insert into tblMeals (Name, Price, Description, image) values ('Big Mac', 49, 'Beef Burger, coke & fries', 'images/big_mac.png')
insert into tblMeals (Name, Price, Description, image) values ('Zinger', 51, 'Beef Burger, 500ml coke # fries', 'images/zinger.jpg')
insert into tblMeals (Name, Price, Description, image) values ('SA Breakfast', 59, 'Breakfast Meal', 'images/SA_Breakfast.jpg')
insert into tblMeals (Name, Price, Description, image) values ('Cappaccino', 19, '1 cup of Cappaccino', 'images/cappaccino.jpg')
insert into tblMeals (Name, Price, Description, image) values ('Coke', 12, '500ml coke', 'images/coca_cola.png')

create table tblOrder(
	orderId int identity primary key,
	userId int foreign key references users(UserId)

)

create table tblOrderItem(
	ItemMealId int foreign key references tblMeals(MealId),
	ItemOrderId int foreign key references tblOrder(OrderId),

	quantity int

)

select * from users
select * from tblMeals
select * from tblOrder
select * from tblOrderItem

--*****************************************************************Reservation System********************************************

create table tblTable(
	TableId int primary key identity,
	NoOfPeople nvarchar(50)
)

select * from tblTable

insert into tblTable (NoOfPeople) values ('Table for 2')
insert into tblTable (NoOfPeople) values ('Table for 4')
insert into tblTable (NoOfPeople) values ('Table for 6')
insert into tblTable (NoOfPeople) values ('Table for 10')
insert into tblTable (NoOfPeople) values ('Table for 12')

create table tblReservation(
	TableId int foreign key references tblTable(TableId),
	UserId int foreign key references users(UserID),
	ReservationDate date,
	Time_in time(0),
	Time_out time(0)

)

sp_checkAvailabilityUsingTable  'Table for 2', '2019/04/19'

create procedure sp_checkAvailabilityUsingTable
@table nvarchar(50),
@sdate nvarchar(50)

as
begin
	select NoOfPeople, ReservationDate, Time_In, Time_out
	from tblReservation
	join tblTable
	on tblTable.TableId = tblReservation.TableId
	where NoOfPeople = @table and ReservationDate = @sdate
end

select * from users
select * from tblReservation
select * from tblTable

delete from tblReservation where TableId = 2

insert into tblReservation (TableId, UserId, ReservationDate, Time_in, Time_out) values (1, 1, '2019-04-20', '09:00', '10:00')

sp_checkAvailability '9:00', '10:00', 'Table for 2', '2019-04-20'

create procedure sp_checkAvailability
@tin nvarchar(50),
@tout nvarchar(50),
@table nvarchar(50),
@RDate date

as
begin
	
	select NoOfPeople, ReservationDate, Time_in, Time_out
	from tblReservation
	join tblTable
	on tblTable.TableId = tblReservation.TableId
	where NoOfPeople = @table and Time_in = @tin and Time_out = @tout and ReservationDate = @RDate

end

spMakeReservation '2019-04-20', '07:00', '08:00', 'Table for 4', 1


create procedure spMakeReservation
@Rdate date,
@Rtin time(0),
@Rtout time(0),
@Rtable time(0),
@RUid int

as
begin
	
	insert into tblReservation (TableId, UserId, ReservationDate, Time_in, Time_out) values(dbo.getTableId(@Rtable), @RUid, @Rdate, @Rtin, @Rtout) 
end

create function getTableId(@nop nvarchar(50))
returns int

as
begin
	declare @rv int
	select @rv = TableId from tblTable where NoOfPeople = @nop
	return @rv
end

select * from tblOrderItem
select * from users
select * from tblOrder
select * from tblReservation
 

delete from tblTable where TableId = 6
delete from tblTable where TableId = 7


--*****************************************************************House Keeping********************************************

sp_removeMealItem 6, 1

create procedure sp_removeMealItem
@mealId int,
@userId int

as
begin
	delete from tblOrderItem where ItemMealId = @mealId and ItemOrderId = dbo.OrderIdFromUserId(@userId)
end

sp_veiwReservations 1

create procedure sp_veiwReservations
@userId int

as
begin
	select ReservationDate, Time_in, Time_out, NoOfPeople
	from tblReservation
	join tblTable
	on tblReservation.TableId = tblTable.TableId
	where UserId = 1
	order by ReservationDate
end

select * from tblReservation

sp_deleteReservation 'Table for 2', 1, '23/04/2019', '08:00', '09:00'

create procedure sp_deleteReservation
@table nvarchar(50),
@userId int,
@reservationDate date,
@timeIn time(0),
@timeOut time(0)

as
begin
	delete from tblReservation where TableId = dbo.getTableId(@table) and UserId = @userId and ReservationDate = @reservationDate and Time_in= @timeIn and Time_out = @timeOut
end

select ReservationDate from tblReservation where TableId=1 and UserId=1 and Time_In = '07:00:00'
select Time_in from tblReservation where TableId=1 and UserId=1 and ReservationDate = '2019/05/16 00:00:00'
select * from tblReservation

select * from users
select * from tblOrder
select * from tblOrderItem
select * from tblReservation

--delete from users
--delete from tblOrder
--delete from tblOrderItem
--delete from tblReservation


delete from tblReservation where ReservationDate = '2019-04-23'
delete from tblReservation where ReservationDate = '" + lblTest3.Text + "'

sp_deleteOrderItem 6, 1

create procedure sp_deleteOrderItem
@mealId int,
@UserId int

as
begin
	delete from tblOrderItem where ItemMealId = @mealId and ItemOrderId = dbo.OrderIdFromUserId(@UserId)
end

dbcc checkident(users, reseed, 0)
dbcc checkident(tblOrder, reseed, 0)