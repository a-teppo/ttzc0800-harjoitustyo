INSERT INTO Joukkue (Nimi, Paikkakunta, Seura, LogoURL) VALUES
('Hpk-85','Hameenlinna', 'HPK', null),
('Huru-ukot', 'Siuntio','HK-76', null),
('SB Kynä','Laitila','SB Kynä', null),
('Kustavin loraus','Kustavi', 'KPK', null),
('Iitin Itikat','Kausala','Kausalan Urheilijat', null),
('Kuusamon Kumaus','Kuusamo', 'Kuusamon eräveikot', null),
('Porin porskuttajat','Pori', 'Porin Palloseura', null),
('Lahden lämäri','Lahti', 'Lahden ketterät', null);
    
INSERT INTO Henkilot (Sukunimi, Etunimi, Pelinumero, Pelipaikka, Syntymavuosi, Rooli, JoukkueID)VALUES
('Teppo','Antti', 22, 'PL', 2007, 'pelaaja', 1 ),
('Virtanen','Jasse', 3, 'MV', 2002, 'pelaaja', 1 ),
('Peltone','Ville', 16, 'PL', 1998, 'pelaaja', 2 ),
('Kuikka','Pasi', 9, 'HY', 2001, 'pelaaja', 2 ),
('Rantanen','Lasse', 22, 'PL', 2004, 'pelaaja', 3 ),
('Hepuli','Hanna', 12, 'HY', 1995, 'pelaaja', 3 ),
('Terävä','Pauliina', 8, 'HY', 1989, 'pelaaja', 4 ),
('Tohelo','Teppo', null, null, 1984, 'valmentaja', 1 ),
('Tavis','Tiina', 23, 'MV', 2001, 'pelaaja', 4 ),
('Ahonen','Matti', 31, 'HY', 1999, 'pelaaja', 4 ),
('Meikäläinen','Ville', null, null, 1975, 'valmentaja', 1 );

INSERT INTO Ottelu (Aika, Paikka, Kotijoukkue, Vierasjoukkue, Paatetty) VALUES
('2019-05-14 14:00:00' , 'Kenttä 1', 1, 2, 1),
('2019-05-14 14:00:00' , 'Kenttä 2', 3, 4, 1),
('2019-05-14 16:00:00' , 'Kenttä 1', 1, 3, 1),
('2019-05-14 16:00:00' , 'Kenttä 2', 2, 4, 0),
('2019-05-14 18:00:00' , 'Kenttä 1', 2, 3, 0),
('2019-05-14 18:00:00' , 'Kenttä 2', 4, 1, 0);
    
INSERT INTO Maali (Aika, Erikoistilanne, Maalintekija, Syottaja, Joukkue, Ottelu) VALUES
('7:21', 'YV', 2, 1, 1, 1),
('9:45', null, 4, 3, 2, 1),
('25:55', null, 1, 2, 1, 1),
('1:09', 'AV', 5, 6, 3, 2),
('59:58', null, 6, 5, 3, 2),
('59:59', null, 6, null, 3, 2),
('18:44', null, 1, null, 2, 3),
('26:25', null, 1, null, 2, 3),
('42:01', 'TM', 1, 2, 2, 3);

INSERT INTO Rangaistus (Aika, Kesto, Syy, Henkilo, Joukkue, Ottelu) VALUES
('14:35', 2, 'Kampitus', 1, 1, 1),
('30:10', 5, 'Väkivaltaisuus', 6, 3, 3);
