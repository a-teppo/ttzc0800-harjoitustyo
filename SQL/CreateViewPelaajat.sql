CREATE VIEW Pelaajat AS SELECT HenkiloID, Sukunimi, Etunimi, Pelinumero, Pelipaikka, Syntymavuosi, Rooli, Henkilot.JoukkueID, Joukkue.Nimi 
    FROM Henkilot
    INNER JOIN Joukkue ON Joukkue.JoukkueID = Henkilot.JoukkueID