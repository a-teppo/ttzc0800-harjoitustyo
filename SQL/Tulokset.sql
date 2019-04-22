SELECT OtteluID, j1.Nimi As Kotijoukkue, j2.Nimi As Vierasjoukkue, o1.Maalit As Kotimaalit, o2.Maalit As Vierasmaalit
FROM Ottelu
LEFT JOIN Joukkue j1 ON j1.JoukkueID = Ottelu.Kotijoukkue
LEFT JOIN Joukkue j2 ON j2.JoukkueID = Ottelu.Vierasjoukkue
LEFT JOIN Ottelumaalit o1 ON o1.Ottelu = Ottelu.OtteluID AND o1.Joukkue = Ottelu.Kotijoukkue
LEFT JOIN Ottelumaalit o2 ON o2.Ottelu = Ottelu.OtteluID AND o2.Joukkue = Ottelu.Vierasjoukkue


