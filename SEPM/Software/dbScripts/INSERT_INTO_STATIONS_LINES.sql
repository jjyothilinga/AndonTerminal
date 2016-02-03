truncate table lines

insert into lines(description,id,stages,idf_id) values('CELL 11A',1,3,8)
insert into lines(description,id,stages,idf_id) values('CELL 11B',2,4,8)
insert into lines(description,id,stages,idf_id) values('CELL 11C',3,2,8)
insert into lines(description,id,stages,idf_id) values('CELL 12A',4,4,8)
insert into lines(description,id,stages,idf_id) values('CELL 12B',5,4,8)
insert into lines(description,id,stages,idf_id) values('CELL 12C',6,5,8)

SELECT * FROM lines

truncate table stations



insert into stations(id,line_id,description) values(3,1,'STAGE-1')
insert into stations(id,line_id,description) values(2,1,'STAGE-2')
insert into stations(id,line_id,description) values(1,1,'STAGE-3')



insert into stations(id,line_id,description) values(4,2,'STAGE-1')
insert into stations(id,line_id,description) values(5,2,'STAGE-2')
insert into stations(id,line_id,description) values(6,2,'STAGE-3')
insert into stations(id,line_id,description) values(7,2,'STAGE-4')

insert into stations(id,line_id,description) values(21,3,'FINISHING')
insert into stations(id,line_id,description) values(22,3,'LOADING')

insert into stations(id,line_id,description) values(8,4,'STAGE-1')
insert into stations(id,line_id,description) values(9,4,'STAGE-2')
insert into stations(id,line_id,description) values(10,4,'STAGE-3')
insert into stations(id,line_id,description) values(11,4,'STAGE-4')


insert into stations(id,line_id,description) values(12,5,'STAGE-1')
insert into stations(id,line_id,description) values(13,5,'STAGE-2')
insert into stations(id,line_id,description) values(14,5,'STAGE-3')
insert into stations(id,line_id,description) values(15,5,'STAGE-4')


insert into stations(id,line_id,description) values(16,6,'STAGE-1')
insert into stations(id,line_id,description) values(17,6,'STAGE-2')
insert into stations(id,line_id,description) values(18,6,'AC ASSY')
insert into stations(id,line_id,description) values(19,6,'IGBT ASSY')
insert into stations(id,line_id,description) values(20,6,'TRANSFORMER')



SELECT * FROM stations


