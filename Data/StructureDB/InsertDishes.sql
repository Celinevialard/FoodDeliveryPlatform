DECLARE @tao INT;
SELECT @tao = restaurantid FROM Restaurant WHERE restaurantname='Chez Tao';
Insert INTO DISH(restaurantid,dishName,price, description, allergies, Image)
Values (@tao,'Rouleaux de printemps Nem (2pièces)',6,
'',
'si vous souffrez d’allergies ou d’intolérances alimentaires, demandez', 
'nem.jpg'),
(@tao,'Poulet sauce satay',18.50,
'avec Riz',
'si vous souffrez d’allergies ou d’intolérances alimentaires, demandez', 
'pouletSatay.jpg');

DECLARE @beefore INT;
SELECT @beefore = restaurantid FROM Restaurant WHERE restaurantname='Beefore';
Insert INTO DISH(restaurantid,dishName,price, description, allergies, Image)
Values (@beefore,'Beefore',19.50,
'Bun artisanal de notre boulanger, steak BEEFORE 150g, fromage raclette (Bagnes, VS), bacon grillé, salade, lamelles de cornichon, sauce: mayo maison/bacon/oignons.',
'si vous souffrez d’allergies ou d’intolérances alimentaires, demandez', 
'beefore.png'),
(@beefore,'Le Chanterote',21,
'Bun artisanal de notre boulanger, Steak Beefore 150g, Raclette de Bagnes (VS), Chanterelles et pleurotes à la crème, Bacon grillé, Salade, Sauce mayo maison à l''ail.',
'si vous souffrez d’allergies ou d’intolérances alimentaires, demandez', 
'Chanterote.jpg'),
(@beefore,'Chupa Chips',10,
'Notre spécialité de chips "faites maison". Choisissez votre assaisonnement.',
'si vous souffrez d’allergies ou d’intolérances alimentaires, demandez', 
'chips.jpg')
DECLARE @crepe INT;
SELECT @crepe = restaurantid FROM Restaurant WHERE restaurantname='Crêperie Le Rustique';

DECLARE @pom INT;
SELECT @pom = restaurantid FROM Restaurant WHERE restaurantname='Mâe Khong Chez Pom';

DECLARE @foyer INT;
SELECT @foyer = restaurantid FROM Restaurant WHERE restaurantname='Brasserie Le Foyer';
Insert INTO DISH(restaurantid,dishName,price, description, allergies, Image)
Values (@foyer,'Burger Maison avec fromage à raclette, coleslaw et frites',25,
'Pain brioché, steak haché, bacon, œuf, fromage à raclette, tomates, salade, sauce, caleslaw, frites',
'gluten', 
'Burger.png'),
(@foyer,'Entrecôte de chamois',32,
'Accompagné de pommes Amandine,courge',
'', 
'EntrecoteDeChamois.png'),
(@foyer,'Bière 5 Quatre Mille surprise 33 dl ',5,
'Pale Ale surprise 33 dl',
'gluten', 
'Biere.png');

DECLARE @salentina INT;
SELECT @salentina = restaurantid FROM Restaurant WHERE restaurantname='Pizzeria Salentina';
Insert INTO DISH(restaurantid,dishName,price, description, allergies, Image)
Values (@salentina,'Pizza Prosciutto funghi',18,
'sauce tomate, mozzarella, jambon cuit, champignons de Paris',
'gluten', 
'PizzaProsciuttoFunghi.jpg'),
(@salentina,'Pizza Dello chef',19,
'sauce tomate, mozzarella, salami piquant, oignons, poivrons, olives',
'gluten', 
'PizzaDelloChef.jpeg'),
(@salentina,'Pasta alla Bolognese',19,
'sauce tomate à l''italienne, huile d''olive, oignons, boeuf/porc haché (CH)',
'gluten', 
'spaghettiAllaBolognese.jpg');

DECLARE @momiji INT;
SELECT @momiji = restaurantid FROM Restaurant WHERE restaurantname='Momiji';
Insert INTO DISH(restaurantid,dishName,price, description, allergies, Image)
Values (@momiji,'Takoyaki',13,
'Boulettes de poulpe 6 pièces',
'gluten', 
'takoyaki.jpg'),
(@momiji,'Maki Swiss Roll',18,
'8 pièces, saumon, conconbre, creme cheese',
'', 
'MakiSwissRoll.jpg'),
(@momiji,'Chicken Katsu Don',21,
'Poulet pané sur riz',
'', 
'chickenKatsuDon.jpg'),
(@momiji,'Ebi Tempura Soba',25,
'SOBA(Nouilles de sarrasin au thé vert), ASPERGES, TEMPURA DE CREVETTES, BOUILLON DE TSUYU',
'', 
'ebiTempuraSoba.jpg'),
(@momiji,'Katsu Curry',19,
'Curry japonais, escalope de porc panée, riz',
'', 
'katsuCurry.jpg');