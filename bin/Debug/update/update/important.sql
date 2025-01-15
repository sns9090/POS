--1 drop table ib_temp


--2 select [item_no],[barcode],[pack],[pk_qty],[price],[note],[pkorder],[price2],[price3],[min_price] into ib_temp from items_bc


--3 delete from items_bc


--4 ************** add br_no,sl_no to items_bc not null with default value '01' all ************


--5 insert into items_bc select *,'01' br_no,'01' sl_no from ib_temp


--6 Not Done