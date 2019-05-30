CREATE TABLE Joukkue (
	JoukkueID SMALLINT PRIMARY KEY auto_increment,
	Nimi VARCHAR(20),
	Paikkakunta VARCHAR(20),
	Seura VARCHAR(20),
	LogoURL VARCHAR(200)
	)ENGINE = InnoDB;
    
CREATE TABLE Henkilot (
	HenkiloID SMALLINT PRIMARY KEY auto_increment,
	Sukunimi VARCHAR(50),
	Etunimi VARCHAR(50),
	Pelinumero TINYINT,
	Pelipaikka VARCHAR(20),
	Syntymavuosi SMALLINT,
	Rooli VARCHAR(10),
    JoukkueID smallint,
    CONSTRAINT uc_NumeroJoukkue UNIQUE (Pelinumero, JoukkueID),
	CONSTRAINT fk_Joukkue FOREIGN KEY (JoukkueID)
		REFERENCES Joukkue (JoukkueID)
		ON DELETE CASCADE
	)ENGINE = InnoDB;

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

CREATE TABLE Maali (
	MaaliID SMALLINT PRIMARY KEY auto_increment,
	Aika VARCHAR(5),
	Erikoistilanne CHAR(2) CHECK (Erikoistilanne IN (AV,YV,RL,TM)),
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

CREATE TABLE Rangaistus (
	RangaistusID SMALLINT PRIMARY KEY auto_increment,
	Aika VARCHAR(5),
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