DECLARE @tao INT;
SELECT @tao = restaurantid FROM Restaurant WHERE restaurantname='Chez Tao';
Insert INTO DISH(restaurantid,dishName,price, description, allergies, Image)
Values (@tao,'Rouleaux de printemps Nem',6,
'2 pièces',
'', 
'nem.jpg'),
(@tao,'Poulet sauce satay',18.50,
'Avec Riz',
'', 
'pouletSatay.jpg');

DECLARE @beefore INT;
SELECT @beefore = restaurantid FROM Restaurant WHERE restaurantname='Beefore';
Insert INTO DISH(restaurantid,dishName,price, description, allergies, Image)
Values (@beefore,'Beefore',19.50,
'Bun artisanal de notre boulanger, steak BEEFORE 150g, fromage raclette (Bagnes, VS), bacon grillé, salade, lamelles de cornichon, sauce: mayo maison/bacon/oignons',
'gluten', 
'beefore.png'),
(@beefore,'Le Chanterote',21,
'Bun artisanal de notre boulanger, Steak Beefore 150g, Raclette de Bagnes (VS), Chanterelles et pleurotes à la crème, Bacon grillé, Salade, Sauce mayo maison à l''ail',
'gluten', 
'chanterote.jpg'),
(@beefore,'Chupa Chips',10,
'Notre spécialité de chips faites maison',
'', 
'chips.png');

DECLARE @crepe INT;
SELECT @crepe = restaurantid FROM Restaurant WHERE restaurantname='Crêperie Le Rustique';
Insert INTO DISH(restaurantid,dishName,price, description, allergies, Image)
Values (@crepe,'Crêpe Valaisanne',17,
'jambon, sauce tomate, fromage',
'oeuf', 
'crepeValaisanne.png'),
(@crepe,'Crêpe Paysanne',16,
'jambon, fromage au poivre, sauce tomate, oeuf',
'oeuf', 
'crepePaysanne.png');


DECLARE @pom INT;
SELECT @pom = restaurantid FROM Restaurant WHERE restaurantname='Mâe Khong Chez Pom';
Insert INTO DISH(restaurantid,dishName,price, description, allergies, Image)
Values (@pom,'Canard laqué',17,
'Avec nouilles',
'', 
'canardLaque.png'),
(@pom,'Pad Thaï au poulet',18,
'Avec salade de carottes',
'cacahuètes', 
'padThai.png');

DECLARE @foyer INT;
SELECT @foyer = restaurantid FROM Restaurant WHERE restaurantname='Brasserie Le Foyer';
Insert INTO DISH(restaurantid,dishName,price, description, allergies, Image)
Values (@foyer,'Burger Maison',25,
'Avec fromage à raclette, coleslaw et frites',
'gluten', 
'burger.png'),
(@foyer,'Entrecôte de chamois',32,
'Accompagné de pommes Amandine et courge',
'', 
'entrecoteDeChamois.png'),
(@foyer,'Bière 5 Quatre Mille surprise',5,
'Pale Ale surprise 33 dl',
'gluten', 
'biere.png');

DECLARE @salentina INT;
SELECT @salentina = restaurantid FROM Restaurant WHERE restaurantname='Pizzeria Salentina';
Insert INTO DISH(restaurantid,dishName,price, description, allergies, Image)
Values (@salentina,'Pizza Prosciutto funghi',18,
'Sauce tomate, mozzarella, jambon cuit, champignons de Paris',
'gluten', 
'pizzaProsciuttoFunghi.jpg'),
(@salentina,'Pizza Dello chef',19,
'Sauce tomate, mozzarella, salami piquant, oignons, poivrons, olives',
'gluten', 
'pizzaDelloChef.jpeg'),
(@salentina,'Pasta alla Bolognese',19,
'Sauce tomate à l''italienne, huile d''olive, oignons, boeuf/porc haché (CH)',
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
'makiSwissRoll.jpg'),
(@momiji,'Chicken Katsu Don',21,
'Poulet pané sur riz',
'', 
'chickenKatsuDon.jpg'),
(@momiji,'Ebi Tempura Soba',25,
'Soba, asperges, tempura de crevettes, bouillon de Tsuyu',
'', 
'ebiTempuraSoba.jpg'),
(@momiji,'Katsu Curry',19,
'Curry japonais, escalope de porc panée, riz',
'', 
'katsuCurry.jpg');

DECLARE @grottoFontaine INT;
SELECT @grottoFontaine = restaurantid FROM Restaurant WHERE restaurantname='Grotto de la Fontaine';
Insert INTO DISH(restaurantid,dishName,price, description, allergies, Image)
Values (@grottoFontaine,'Tartare de boeuf',38,
'Race d''Hérens» classique coupé au couteau, 200 g de viande)',
'', 
'tartareBoeuf.png'),
(@grottoFontaine,'Duo de foie gras de canard',28,
'Mi-cuit avec chutney du moment assaisoné au poivre du Val Maggia',
'', 
'foieGras.png');