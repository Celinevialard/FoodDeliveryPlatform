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

