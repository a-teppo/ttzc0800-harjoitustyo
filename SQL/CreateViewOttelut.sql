CREATE VIEW Ottelut AS
    SELECT OtteluId, Aika, Paikka, Kotijoukkue, kj.Nimi AS Koti, Vierasjoukkue, vj.Nimi AS Vieras, IFNULL(o1.Maalit,0) As Kotimaalit, IFNULL(o2.Maalit,0) As Vierasmaalit, Paatetty
    FROM Ottelu
    JOIN Joukkue kj ON kj.JoukkueId = Ottelu.Kotijoukkue
    JOIN Joukkue vj ON vj.JoukkueId = Ottelu.Vierasjoukkue
    LEFT JOIN Ottelumaalit o1 ON o1.Ottelu = Ottelu.OtteluID AND o1.Joukkue = Ottelu.Kotijoukkue
    LEFT JOIN Ottelumaalit o2 ON o2.Ottelu = Ottelu.OtteluID AND o2.Joukkue = Ottelu.Vierasjoukkue
    ORDER BY Aika, Paikka;