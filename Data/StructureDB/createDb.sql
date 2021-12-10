/*==============================================================*/
/* Nom de SGBD :  Microsoft SQL Server 2017                     */
/* Date de création :  15.10.2021 15:23:13                      */
/*==============================================================*/


if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('COURRIER') and o.name = 'FK_COURRIER_REFERENCE_PERSON')
alter table COURRIER
   drop constraint FK_COURRIER_REFERENCE_PERSON
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('CUSTOMER') and o.name = 'FK_CUSTOMER_REFERENCE_LOCATION')
alter table CUSTOMER
   drop constraint FK_CUSTOMER_REFERENCE_LOCATION
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('CUSTOMER') and o.name = 'FK_CUSTOMER_REFERENCE_PERSON')
alter table CUSTOMER
   drop constraint FK_CUSTOMER_REFERENCE_PERSON
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('DELEVERYZONE') and o.name = 'FK_DELEVERY_REFERENCE_COURRIER')
alter table DELEVERYZONE
   drop constraint FK_DELEVERY_REFERENCE_COURRIER
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('DELEVERYZONE') and o.name = 'FK_DELEVERY_REFERENCE_LOCATION')
alter table DELEVERYZONE
   drop constraint FK_DELEVERY_REFERENCE_LOCATION
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('DISH') and o.name = 'FK_DISH_REFERENCE_RESTAURA')
alter table DISH
   drop constraint FK_DISH_REFERENCE_RESTAURA
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ORDERDETAILS') and o.name = 'FK_ORDERDET_REFERENCE_DISH')
alter table ORDERDETAILS
   drop constraint FK_ORDERDET_REFERENCE_DISH
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ORDERDETAILS') and o.name = 'FK_ORDERDET_REFERENCE_ORDERS')
alter table ORDERDETAILS
   drop constraint FK_ORDERDET_REFERENCE_ORDERS
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ORDERS') and o.name = 'FK_ORDERS_REFERENCE_CUSTOMER')
alter table ORDERS
   drop constraint FK_ORDERS_REFERENCE_CUSTOMER
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ORDERS') and o.name = 'FK_ORDERS_REFERENCE_COURRIER')
alter table ORDERS
   drop constraint FK_ORDERS_REFERENCE_COURRIER
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('RESTAURANT') and o.name = 'FK_RESTAURA_REFERENCE_LOCATION')
alter table RESTAURANT
   drop constraint FK_RESTAURA_REFERENCE_LOCATION
go

if exists (select 1
            from  sysobjects
           where  id = object_id('COURRIER')
            and   type = 'U')
   drop table COURRIER
go

if exists (select 1
            from  sysobjects
           where  id = object_id('CUSTOMER')
            and   type = 'U')
   drop table CUSTOMER
go

if exists (select 1
            from  sysobjects
           where  id = object_id('DELEVERYZONE')
            and   type = 'U')
   drop table DELEVERYZONE
go

if exists (select 1
            from  sysobjects
           where  id = object_id('DISH')
            and   type = 'U')
   drop table DISH
go

if exists (select 1
            from  sysobjects
           where  id = object_id('LOCATION')
            and   type = 'U')
   drop table LOCATION
go

if exists (select 1
            from  sysobjects
           where  id = object_id('ORDERDETAILS')
            and   type = 'U')
   drop table ORDERDETAILS
go

if exists (select 1
            from  sysobjects
           where  id = object_id('ORDERS')
            and   type = 'U')
   drop table ORDERS
go

if exists (select 1
            from  sysobjects
           where  id = object_id('PERSON')
            and   type = 'U')
   drop table PERSON
go

if exists (select 1
            from  sysobjects
           where  id = object_id('RESTAURANT')
            and   type = 'U')
   drop table RESTAURANT
go

/*==============================================================*/
/* Table : COURRIER                                             */
/*==============================================================*/
create table COURRIER (
   COURRIERID           int                  not null IDENTITY(1,1),
   PERSONID             int                  null,
   constraint PK_COURRIER primary key (COURRIERID)
)
go

/*==============================================================*/
/* Table : CUSTOMER                                             */
/*==============================================================*/
create table CUSTOMER (
   CUSTOMERID           int                  not null IDENTITY(1,1),
   LOCATIONID           int                  null,
   PERSONID             int                  null,
   ADDRESS              varchar(250)         null,
   constraint PK_CUSTOMER primary key (CUSTOMERID)
)
go

/*==============================================================*/
/* Table : DELEVERYZONE                                         */
/*==============================================================*/
create table DELEVERYZONE (
   IDZONE               int                  not null IDENTITY(1,1),
   COURRIERID           int                  null,
   LOCATIONID           int                  null,
   constraint PK_DELEVERYZONE primary key (IDZONE)
)
go

/*==============================================================*/
/* Table : DISH                                                 */
/*==============================================================*/
create table DISH (
   IDDISH               int                  not null IDENTITY(1,1),
   RESTAURANTID         int                  null,
   DISHNAME             varchar(50)          not null,
   PRICE                decimal              not null,
   DESCRIPTION          varchar(250)         null,
   ALLERGIES            varchar(250)          null,
   IMAGE                varchar(50)          null,
   constraint PK_DISH primary key (IDDISH)
)
go

/*==============================================================*/
/* Table : LOCATION                                             */
/*==============================================================*/
create table LOCATION (
   LOCATIONID           int                  not null IDENTITY(1,1),
   NPA                  varchar(10)          not null,
   LOCATION             varchar(25)          not null,
   constraint PK_LOCATION primary key (LOCATIONID)
)
go

/*==============================================================*/
/* Table : ORDERDETAILS                                         */
/*==============================================================*/
create table ORDERDETAILS (
   ORDERDETAILSID       int                  not null IDENTITY(1,1),
   IDDISH               int                  null,
   ORDERID              int                  null,
   QUANTITY             int                  null,
   ORDERNOTE            varchar(250)         null,
   constraint PK_ORDERDETAILS primary key (ORDERDETAILSID)
)
go

/*==============================================================*/
/* Table : ORDERS                                               */
/*==============================================================*/
create table ORDERS (
   ORDERID              int                  not null IDENTITY(1,1),
   CUSTOMERID           int                  null,
   COURRIERID           int                  null,
   STATUS               int                  not null,
   ORDERNOTE            varchar(250)         null,
   ORDERDATE            DATETIME             null,
   TOTALAMOUNT          decimal              null,
   constraint PK_ORDERS primary key (ORDERID)
)
go

/*==============================================================*/
/* Table : PERSON                                               */
/*==============================================================*/
create table PERSON (
   PERSONID             int                  not null IDENTITY(1,1),
   FIRSTNAME            varchar(25)          not null,
   NAME                 varchar(25)          not null,
   LOGIN                varchar(25)          not null,
   PASSWORD             varchar(25)          not null,
   constraint PK_PERSON primary key (PERSONID)
)
go

/*==============================================================*/
/* Table : RESTAURANT                                           */
/*==============================================================*/
create table RESTAURANT (
   RESTAURANTID         int                  not null IDENTITY(1,1),
   LOCATIONID           int                  null,
   RESTAURANTNAME       varchar(25)          not null,
   RESTAURANTDESCRIPTION varchar(250)         null,
   constraint PK_RESTAURANT primary key (RESTAURANTID)
)
go

alter table COURRIER
   add constraint FK_COURRIER_REFERENCE_PERSON foreign key (PERSONID)
      references PERSON (PERSONID)
go

alter table CUSTOMER
   add constraint FK_CUSTOMER_REFERENCE_LOCATION foreign key (LOCATIONID)
      references LOCATION (LOCATIONID)
go

alter table CUSTOMER
   add constraint FK_CUSTOMER_REFERENCE_PERSON foreign key (PERSONID)
      references PERSON (PERSONID)
go

alter table DELEVERYZONE
   add constraint FK_DELEVERY_REFERENCE_COURRIER foreign key (COURRIERID)
      references COURRIER (COURRIERID)
go

alter table DELEVERYZONE
   add constraint FK_DELEVERY_REFERENCE_LOCATION foreign key (LOCATIONID)
      references LOCATION (LOCATIONID)
go

alter table DISH
   add constraint FK_DISH_REFERENCE_RESTAURA foreign key (RESTAURANTID)
      references RESTAURANT (RESTAURANTID)
go

alter table ORDERDETAILS
   add constraint FK_ORDERDET_REFERENCE_DISH foreign key (IDDISH)
      references DISH (IDDISH)
go

alter table ORDERDETAILS
   add constraint FK_ORDERDET_REFERENCE_ORDERS foreign key (ORDERID)
      references ORDERS (ORDERID)
go

alter table ORDERS
   add constraint FK_ORDERS_REFERENCE_CUSTOMER foreign key (CUSTOMERID)
      references CUSTOMER (CUSTOMERID)
go

alter table ORDERS
   add constraint FK_ORDERS_REFERENCE_COURRIER foreign key (COURRIERID)
      references COURRIER (COURRIERID)
go

alter table RESTAURANT
   add constraint FK_RESTAURA_REFERENCE_LOCATION foreign key (LOCATIONID)
      references LOCATION (LOCATIONID)
go

