CREATE VIEW Pelaajat AS SELECT HenkiloId, Sukunimi, Etunimi, Syntymavuosi, Joukkue.Nimi 
FROM Henkilot
INNER JOIN Joukkue ON Joukkue.JoukkueID = Henkilot.JoukkueID