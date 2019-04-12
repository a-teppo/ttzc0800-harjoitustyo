CREATE TABLE Joukkue (
	JoukkueID SMALLINT PRIMARY KEY auto_increment,
	Nimi VARCHAR(20),
	Paikkakunta VARCHAR(20),
	Seura VARCHAR(20),
	LogoURL VARCHAR(200)
	)ENGINE = InnoDB;
    
INSERT INTO Joukkue (Nimi, Paikkakunta, Seura, LogoURL) VALUES
('Hpk-85','Hameenlinna', 'HPK', '');

    
CREATE TABLE Henkilot (
	HenkiloID SMALLINT PRIMARY KEY auto_increment,
	Sukunimi VARCHAR(50),
	Etunimi VARCHAR(50),
	Pelinumero TINYINT,
	Pelipaikka CHAR(2),
	Syntymavuosi SMALLINT,
	Rooli VARCHAR(10),
    JoukkueID smallint,
	CONSTRAINT fk_Joukkue FOREIGN KEY (JoukkueID)
		REFERENCES Joukkue (JoukkueID)
	)ENGINE = InnoDB;

    INSERT INTO Henkilot (Sukunimi, Etunimi, Pelinumero, Pelipaikka, Syntymavuosi, Rooli, JoukkueID)VALUES
('Teppo','Antti', 22, 'VP', 2007, 'pelaaja', 1 );

CREATE TABLE Ottelu (
	OtteluID SMALLINT PRIMARY KEY auto_increment,
	Aika DATETIME,
	Paikka VARCHAR(20),
    Kotijoukkue smallint,
    Vierasjoukkue smallint,
	CONSTRAINT uc_AikaPaikka UNIQUE (Aika, Paikka),
	CONSTRAINT chk_KotiVieras CHECK (Kotijoukkue <> Vierasjoukkue),
	CONSTRAINT fk_Kotijoukkue FOREIGN KEY (Kotijoukkue)
		REFERENCES Joukkue (JoukkueID),
	CONSTRAINT fk_Vierasjoukkue FOREIGN KEY (Vierasjoukkue)
		REFERENCES Joukkue (JoukkueID)
	)ENGINE = InnoDB;

insert into Ottelu (Aika, Paikka) values
('2019-01-01 14:14:20' , 'Porvoo');

CREATE TABLE Maali (
	MaaliID SMALLINT PRIMARY KEY auto_increment,
	Aika DATETIME,
	Erikoistilanne CHAR(2) CHECK (Erikoistilanne IN ('AV','YV','RL')),
    Maalintekija smallint,
    Syottaja smallint,
    Ottelu smallint,
	CONSTRAINT fk_Ottelu FOREIGN KEY (Ottelu)
		REFERENCES Ottelu (OtteluID),
	CONSTRAINT fk_Maalintekija FOREIGN KEY (Maalintekija)
		REFERENCES Henkilot(HenkiloID),
	CONSTRAINT fk_Syottaja FOREIGN KEY (Syottaja)
		REFERENCES Henkilot(HenkiloID)
	)ENGINE = InnoDB;
    
insert into Maali (Aika, Erikoistilanne, Maalintekija, Syottaja, Ottelu) values
('2019-01-02 14:14:20', 'YV', 1, 1, 1);

CREATE TABLE Rangaistus (
	RangaistusID SMALLINT PRIMARY KEY auto_increment,
	Aika DATETIME,
	Kesto TINYINT,
	Syy VARCHAR(20),
    Henkilo smallint,
    Ottelu smallint,
	CONSTRAINT fk_Henkilo FOREIGN KEY (Henkilo)
		REFERENCES Henkilot(HenkiloID),
	CONSTRAINT fk_Ottelu2 FOREIGN KEY (Ottelu)
		REFERENCES Ottelu (OtteluID)
	)ENGINE = InnoDB;
    
insert into Rangaistus (Aika, Kesto, Syy, Henkilo, Ottelu) values
('2019-01-03 14:14:20', 5, 'Poikkari', 1, 1);