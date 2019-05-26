CREATE TABLE Joukkue (
	JoukkueID SMALLINT PRIMARY KEY auto_increment,
	Nimi VARCHAR(20),
	Paikkakunta VARCHAR(20),
	Seura VARCHAR(20),
	LogoURL VARCHAR(200)
	)ENGINE = InnoDB;
    
INSERT INTO Joukkue (Nimi, Paikkakunta, Seura, LogoURL) VALUES
('Hpk-85','Hameenlinna', 'HPK', null),
('Huru-ukot', 'Siuntio','HK-76', null),
('SB Kynä','Laitila','SB Kynä', null),
('Kustavin loraus','Kustavi', 'KPK', null);

    
CREATE TABLE Henkilot (
	HenkiloID SMALLINT PRIMARY KEY auto_increment,
	Sukunimi VARCHAR(50),
	Etunimi VARCHAR(50),
	Pelinumero TINYINT,
	Pelipaikka CHAR(2) CHECK (Pelipaikka IN (MV,PL,HY)),
	Syntymavuosi SMALLINT,
	Rooli VARCHAR(10),
    JoukkueID smallint,
    CONSTRAINT uc_NumeroJoukkue UNIQUE (Pelinumero, JoukkueID),
	CONSTRAINT fk_Joukkue FOREIGN KEY (JoukkueID)
		REFERENCES Joukkue (JoukkueID)
		ON DELETE CASCADE
	)ENGINE = InnoDB;

INSERT INTO Henkilot (Sukunimi, Etunimi, Pelinumero, Pelipaikka, Syntymavuosi, Rooli, JoukkueID)VALUES
('Teppo','Antti', 22, 'PL', 2007, 'pelaaja', 1 ),
('Virtanen','Jasse', 3, 'MV', 2002, 'pelaaja', 1 ),
('Peltone','Ville', 16, 'PL', 1998, 'pelaaja', 2 ),
('Kuikka','Pasi', 9, 'HY', 2001, 'pelaaja', 2 ),
('Rantanen','Lasse', 22, 'PL', 2004, 'pelaaja', 3 ),
('Hepuli','Hanna', 12, 'HY', 1995, 'pelaaja', 3 ),
('Terävä','Pauliina', 8, 'HY', 1989, 'pelaaja', 4 ),
('Tohelo','Teppo', null, null, 1984, 'valmentaja', 1 ),
('Tavis','Tiina', 23, 'MV', 2001, 'pelaaja', 4 );

CREATE TABLE Ottelu (
	OtteluID SMALLINT PRIMARY KEY auto_increment,
	Aika DATETIME,
	Paikka VARCHAR(20),
    Kotijoukkue smallint,
    Vierasjoukkue smallint,
	Paatetty boolean,
	CONSTRAINT uc_AikaPaikka UNIQUE (Aika, Paikka),
	CONSTRAINT chk_KotiVieras CHECK (Kotijoukkue <> Vierasjoukkue),
	CONSTRAINT fk_Kotijoukkue FOREIGN KEY (Kotijoukkue)
		REFERENCES Joukkue (JoukkueID)
		ON DELETE RESTRICT,
	CONSTRAINT fk_Vierasjoukkue FOREIGN KEY (Vierasjoukkue)
		REFERENCES Joukkue (JoukkueID)
		ON DELETE RESTRICT
	)ENGINE = InnoDB;

INSERT INTO Ottelu (Aika, Paikka, Kotijoukkue, Vierasjoukkue, Paatetty) VALUES
('2019-05-14 14:00:00' , 'Kenttä 1', 1, 2, 1),
('2019-05-14 14:00:00' , 'Kenttä 2', 3, 4, 1),
('2019-05-14 16:00:00' , 'Kenttä 1', 1, 3, 1),
('2019-05-14 16:00:00' , 'Kenttä 2', 2, 4, 0),
('2019-05-14 18:00:00' , 'Kenttä 1', 2, 3, 0),
('2019-05-14 18:00:00' , 'Kenttä 2', 4, 1, 0);

CREATE TABLE Maali (
	MaaliID SMALLINT PRIMARY KEY auto_increment,
	Aika VARCHAR(5),
	Erikoistilanne CHAR(2) CHECK (Erikoistilanne IN (AV,YV,RL)),
    Maalintekija smallint NOT NULL,
    Syottaja smallint,
    Joukkue smallint NOT NULL,
    Ottelu smallint NOT NULL,
	CONSTRAINT fk_Ottelu FOREIGN KEY (Ottelu)
		REFERENCES Ottelu (OtteluID)
		ON DELETE RESTRICT,
    CONSTRAINT fk_Joukkue1 FOREIGN KEY (Joukkue)
		REFERENCES Joukkue (JoukkueID)
		ON DELETE RESTRICT,
	CONSTRAINT fk_Maalintekija FOREIGN KEY (Maalintekija)
		REFERENCES Henkilot(HenkiloID)
		ON DELETE RESTRICT,
	CONSTRAINT fk_Syottaja FOREIGN KEY (Syottaja)
		REFERENCES Henkilot(HenkiloID)
		ON DELETE RESTRICT
	)ENGINE = InnoDB;
    
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

CREATE TABLE Rangaistus (
	RangaistusID SMALLINT PRIMARY KEY auto_increment,
	Aika DATETIME NOT NULL,
	Kesto TINYINT NOT NULL,
	Syy VARCHAR(20) NOT NULL,
    Henkilo smallint NOT NULL,
    Joukkue smallint NOT NULL,
    Ottelu smallint NOT NULL,
	CONSTRAINT fk_Henkilo FOREIGN KEY (Henkilo)
		REFERENCES Henkilot(HenkiloID)
		ON DELETE RESTRICT,
    CONSTRAINT fk_Joukkue2 FOREIGN KEY (Joukkue)
		REFERENCES Joukkue (JoukkueID)
		ON DELETE RESTRICT,
	CONSTRAINT fk_Ottelu2 FOREIGN KEY (Ottelu)
		REFERENCES Ottelu (OtteluID)
		ON DELETE RESTRICT
	)ENGINE = InnoDB;
    
INSERT INTO Rangaistus (Aika, Kesto, Syy, Henkilo, Joukkue, Ottelu) VALUES
('2019-05-14 14:35:10', 2, 'Kampitus', 1, 1, 1),
('2019-05-14 16:30:10', 5, 'Väkivaltaisuus', 6, 3, 3);

CREATE VIEW `Ottelumaalit` AS
    SELECT 
        Ottelu, COUNT(0) AS Maalit, Joukkue
    FROM Maali
    GROUP BY Ottelu, Joukkue;

CREATE VIEW Maalit AS
    SELECT 
        Henkilot.HenkiloID AS HenkiloID,
        COUNT(Maali.Maalintekija) AS Maalit
    FROM
        (Henkilot
        JOIN Maali ON ((Henkilot.HenkiloID = Maali.Maalintekija)))
    GROUP BY Henkilot.HenkiloID;

CREATE VIEW Syotot AS
    SELECT 
        Henkilot.HenkiloID AS HenkiloID,
        COUNT(Maali.Syottaja) AS Syotot
    FROM
        (Henkilot
        JOIN Maali ON ((Henkilot.HenkiloID = Maali.Syottaja)))
    GROUP BY Henkilot.HenkiloID;
    
CREATE VIEW Rangaistukset AS
    SELECT 
        Henkilot.HenkiloID AS HenkiloID,
        SUM(Rangaistus.Kesto) AS Rangaistusminuutit
    FROM
        (Henkilot
        JOIN Rangaistus ON ((Henkilot.HenkiloID = Rangaistus.Henkilo)))
    GROUP BY Henkilot.HenkiloID;

CREATE VIEW Joukkueet AS SELECT JoukkueId, Nimi, Paikkakunta, Seura
    FROM Joukkue;

CREATE VIEW Pelaajat AS SELECT HenkiloID, Sukunimi, Etunimi, Pelinumero, Pelipaikka, Syntymavuosi, Rooli, Henkilot.JoukkueID, Joukkue.Nimi 
    FROM Henkilot
    INNER JOIN Joukkue ON Joukkue.JoukkueID = Henkilot.JoukkueID;
    
CREATE VIEW Ottelut AS
    SELECT OtteluId, Aika, Paikka, Kotijoukkue, kj.Nimi AS Koti, Vierasjoukkue, vj.Nimi AS Vieras, IFNULL(o1.Maalit,0) As Kotimaalit, IFNULL(o2.Maalit,0) As Vierasmaalit, Paatetty
    FROM Ottelu
    JOIN Joukkue kj ON kj.JoukkueId = Ottelu.Kotijoukkue
    JOIN Joukkue vj ON vj.JoukkueId = Ottelu.Vierasjoukkue
    LEFT JOIN Ottelumaalit o1 ON o1.Ottelu = Ottelu.OtteluID AND o1.Joukkue = Ottelu.Kotijoukkue
    LEFT JOIN Ottelumaalit o2 ON o2.Ottelu = Ottelu.OtteluID AND o2.Joukkue = Ottelu.Vierasjoukkue
    ORDER BY Aika, Paikka;