SELECT Henkilot.HenkiloID, CONCAT(Henkilot.Sukunimi,' ',Henkilot.Etunimi) As Nimi, Joukkue.Nimi As Joukkue, Maalit, Syotot, (Maalit + Syotot) AS Pisteet, Rangaistusminuutit
FROM Henkilot
JOIN Maalit ON Henkilot.HenkiloID = Maalit.HenkiloID
JOIN Syotot ON Henkilot.HenkiloID = Syotot.HenkiloID
JOIN Joukkue ON Henkilot.JoukkueID =Joukkue.JoukkueID
LEFT JOIN Rangaistukset ON Henkilot.HenkiloID = Rangaistukset.HenkiloID
GROUP BY HenkiloID
ORDER BY Pisteet DESC
